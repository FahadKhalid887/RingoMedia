using MediatR;
using Microsoft.AspNetCore.Mvc;
using RingoMedia.Application.Commands.Reminder.CreateReminder;
using RingoMedia.Application.Commands.Reminder.DeleteReminder;
using RingoMedia.Application.Queries.Reminder.GetReminder;
using RingoMedia.Application.Queries.Reminder.GetReminders;
using RingoMedia.Web.Models.ViewModels;

namespace RingoMedia.Web.Controllers
{
    public class RemindersController : Controller
    {
        private readonly IMediator _mediator;

        public RemindersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> IndexAsync(CancellationToken cancellationToken)
        {
            var reminders = await _mediator.Send(new GetRemindersQuery(), cancellationToken);
            return View(reminders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(CreateReminderVM reminder, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new CreateReminderCommand
                {
                    Title = reminder.Title,
                    Message = reminder.Message,
                    Email = reminder.Email,
                    DateTime = reminder.DateTime,
                }, cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            return View(reminder);
        }

        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var reminder = await _mediator.Send(new GetReminderQuery
            {
                Id = id
            }, cancellationToken);
            return View(reminder);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedAsync(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteReminderCommand
            {
                Id = id
            }, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
    }
}
