using Microsoft.AspNetCore.Http;
using RingoMedia.Application.DTOs;

namespace RingoMedia.Application.Interfaces.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<DepartmentDto>> FindAsync(CancellationToken cancellationToken = default);
        Task<DepartmentDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<int> AddAsync(string departmentName, IFormFile? departmentLogo, int? parentId, CancellationToken cancellationToken = default);
        Task UpdateAsync(int id, string departmentName, IFormFile? departmentLogo, int? parentId, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
