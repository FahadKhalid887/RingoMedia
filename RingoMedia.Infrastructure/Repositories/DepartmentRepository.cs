using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RingoMedia.Application.DTOs;
using RingoMedia.Application.Interfaces.Repositories;
using RingoMedia.Domain.Entities;
using RingoMedia.Domain.Exceptions;
using RingoMedia.Infrastructure.Persistence;

namespace RingoMedia.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly RingoMediaDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentRepository> _logger;
        private readonly string _uploadsFolderPath;

        public DepartmentRepository(RingoMediaDbContext context,
            ILogger<DepartmentRepository> logger,
            IHostEnvironment environment, 
            IMapper mapper
        )
        {
            _context = context;
            _uploadsFolderPath = Path.Combine(environment.ContentRootPath, "wwwroot", "images");
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentDto>> FindAsync(CancellationToken cancellationToken = default)
        {
            var departments = await _context.Departments
                .Where(x => !x.ParentId.HasValue)
                .ToListAsync(cancellationToken);

            foreach (var department in departments)
            {
                await LoadSubDepartments(department, cancellationToken);
            }

            return _mapper.Map<List<DepartmentDto>>(departments);
        }

        private async Task LoadSubDepartments(DepartmentEntity department, CancellationToken cancellationToken = default)
        {
            var subDepartments = await _context.Departments
                .Where(d => d.ParentId == department.Id)
                .ToListAsync(cancellationToken);

            department.SubDepartments = subDepartments;

            foreach (var subDepartment in subDepartments)
            {
                await LoadSubDepartments(subDepartment, cancellationToken);
            }
        }

        public async Task<DepartmentDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            DepartmentEntity? department = await _context.Departments.FindAsync(id, cancellationToken);

            if (department == null)
            {
                _logger.LogWarning($"Department with id {id} not found.");
                throw new NotFoundException($"Department with id {id} not found.");
            }

            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task<int> AddAsync(string departmentName, IFormFile? departmentLogo, int? parentId, CancellationToken cancellationToken = default)
        {
            string? logoPath = await UploadLogoAsync(departmentLogo, cancellationToken);

            await _context.AddAsync(new DepartmentEntity
            {
                DepartmentName = departmentName,
                DepartmentLogo = logoPath,
                ParentId = parentId,
            }, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(int id, string departmentName, IFormFile? departmentLogo, int? parentId, CancellationToken cancellationToken = default)
        {
            DepartmentEntity? department = await _context.Departments.FindAsync(id, cancellationToken);

            if (department == null)
            {
                _logger.LogWarning($"Department with id {id} not found.");
                throw new NotFoundException($"Department with id {id} not found.");
            }

            DeleteLogo(department.DepartmentLogo);
            string? logoPath = await UploadLogoAsync(departmentLogo, cancellationToken);

            department.DepartmentName = departmentName;
            department.DepartmentLogo = logoPath;

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            DepartmentEntity? department = await _context.Departments.FindAsync(id, cancellationToken);

            if (department == null)
            {
                _logger.LogWarning($"Department with id {id} not found.");
                throw new NotFoundException($"Department with id {id} not found.");
            }

            var hasChildren = _context.Departments.Any(c => c.ParentId == id);
            if (hasChildren)
            {
                throw new ChildExistsException("Cannot delete the entity because child entities exist.");
            }

            DeleteLogo(department.DepartmentLogo);

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task<string?> UploadLogoAsync(IFormFile? image, CancellationToken cancellationToken = default)
        {
            if (image == null || image.Length == 0)
            {
                return null; // Handle this case as per your application's requirements
            }

            // Generate a unique file name to avoid conflicts
            var fileExtension = Path.GetExtension(image.FileName);
            var uniqueFileName = $"{DateTime.UtcNow.Ticks}_{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(_uploadsFolderPath, uniqueFileName);

            // Save the file
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream, cancellationToken);
            }

            return uniqueFileName;
        }

        private void DeleteLogo(string? fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                var filePath = Path.Combine(_uploadsFolderPath, fileName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }
    }
}
