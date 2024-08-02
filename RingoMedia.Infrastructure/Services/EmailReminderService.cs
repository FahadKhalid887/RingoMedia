using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RingoMedia.Application.Commands.Reminder.UpdateReminder;
using RingoMedia.Application.DTOs;
using RingoMedia.Application.Queries.Reminder.GetReminders;
using RingoMedia.Domain.Enums;
using RingoMedia.Infrastructure.Repositories;

namespace RingoMedia.Infrastructure.Services
{
    public class EmailReminderService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly EmailSender _emailSender;
        private readonly ILogger<EmailReminderService> _logger;

        public EmailReminderService(IServiceProvider serviceProvider,
            ILogger<EmailReminderService> logger, 
            EmailSender emailSender
        )
        {
            _serviceProvider = serviceProvider;
            _emailSender = emailSender;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                while (!stoppingToken.IsCancellationRequested)
                {
                    await SendDueReminders(mediator, stoppingToken);
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Run every minute
                }
            }
        }

        private async Task SendDueReminders(IMediator mediator, CancellationToken cancellationToken = default)
        {
            var dueReminders = await mediator.Send(new GetRemindersQuery
            {
                Status = ReminderStatus.Pending,
                DateTo = DateTime.UtcNow,
            });

            foreach (var reminder in dueReminders)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
                ReminderStatus status = ReminderStatus.Success;
                string? errorMessage = null;
                try
                {
                    await SendEmailAsync(reminder);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Sending email reminder for: {reminder.Title} to {reminder.Email}");
                    status = ReminderStatus.Failed;
                    errorMessage = ex.InnerException?.Message ?? ex.Message;
                }

                await mediator.Send(new UpdateReminderCommand
                {
                    Id = reminder.Id,
                    Status = status,
                    ErrorMessage = errorMessage,
                }, cancellationToken);
            }
        }

        private async Task SendEmailAsync(ReminderDto reminder)
        {
            await _emailSender.SendEmailAsync(reminder.Email, reminder.Title, reminder.Message);

            _logger.LogInformation($"Sending email reminder for: {reminder.Title} to {reminder.Email}");
        }
    }
}
