using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RingoMedia.Application.Commands.Department.CreateDepartment;
using RingoMedia.Application.Commands.Department.DeleteDepartment;
using RingoMedia.Application.Commands.Department.UpdateDepartment;
using RingoMedia.Application.Queries.Department.GetDepartment;
using RingoMedia.Application.Queries.Department.GetFilteredDepartments;
using RingoMedia.Domain.Exceptions;
using RingoMedia.Web.Models.ViewModels;

namespace RingoMedia.Web.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DepartmentsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var departments = await _mediator.Send(new GetFilteredDepartmentsQuery(), cancellationToken);
            return View(departments);
        }

        public async Task<IActionResult> DetailsAsync(int id, CancellationToken cancellationToken)
        {
            var department = await _mediator.Send(new GetDepartmentQuery
            {
                Id = id
            }, cancellationToken);
            return View(department);
        }

        public IActionResult Create(int? parentId)
        {
            return View(new CreateEditDepartmentVM
            {
                ParentId = parentId,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(CreateEditDepartmentVM model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new CreateDepartmentCommand
                {
                    DepartmentName = model.DepartmentName,
                    DepartmentLogo = model.DepartmentLogo,
                    ParentId = model.ParentId,
                }, cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> EditAsync(int id, CancellationToken cancellationToken)
        {
            var department = await _mediator.Send(new GetDepartmentQuery
            {
                Id = id
            }, cancellationToken);

            return View(_mapper.Map<CreateEditDepartmentVM>(department));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(CreateEditDepartmentVM model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new UpdateDepartmentCommand
                {
                    Id = model.Id,
                    DepartmentName = model.DepartmentName,
                    DepartmentLogo = model.DepartmentLogo,
                    ParentId = model.ParentId,
                }, cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var department = await _mediator.Send(new GetDepartmentQuery
            {
                Id = id
            }, cancellationToken);
            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.Send(new DeleteDepartmentCommand
                {
                    Id = id
                }, cancellationToken);
            }
            catch (ChildExistsException)
            {
                TempData["ErrorMessage"] = "Cannot delete the department because child entities exist.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
