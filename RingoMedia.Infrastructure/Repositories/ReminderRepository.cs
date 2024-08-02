using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RingoMedia.Application.DTOs;
using RingoMedia.Application.Interfaces.Repositories;
using RingoMedia.Domain.Entities;
using RingoMedia.Domain.Enums;
using RingoMedia.Domain.Exceptions;
using RingoMedia.Infrastructure.Persistence;

namespace RingoMedia.Infrastructure.Repositories
{
    public class ReminderRepository : IReminderRepository
    {
        private readonly RingoMediaDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ReminderRepository> _logger;
        private const int ERROR_MESSAGE_MAX_LENGTH = 1000;

        public ReminderRepository(RingoMediaDbContext context, IMapper mapper, ILogger<ReminderRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ReminderDto>> FindAsync(ReminderStatus? status, DateTime? dateTo, CancellationToken cancellationToken = default)
        {
            var query = _context.Reminders.AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(x => x.Status == status.Value);
            }
            if (dateTo.HasValue)
            {
                query = query.Where(x => x.DateTime <= dateTo.Value);
            }

            IEnumerable<ReminderEntity> reminders = await query.ToListAsync(cancellationToken);
            return _mapper.Map<IEnumerable<ReminderDto>>(reminders);
        }

        public async Task<ReminderDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            ReminderEntity? reminder = await _context.Reminders.FindAsync(id, cancellationToken);

            if (reminder == null)
            {
                _logger.LogWarning($"Reminder with id {id} not found.");
                throw new NotFoundException($"Reminder with id {id} not found.");
            }

            return _mapper.Map<ReminderDto>(reminder);
        }

        public async Task<int> AddAsync(string title, string? message, string email, DateTimeOffset dateTime, CancellationToken cancellationToken = default)
        {
            await _context.Reminders.AddAsync(new ReminderEntity
            {
                Title = title,
                Message = message,
                Email = email,
                DateTime = dateTime.UtcDateTime,
                Status = ReminderStatus.Pending
            }, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(int id, ReminderStatus status, string? errorMessage, CancellationToken cancellationToken = default)
        {
            ReminderEntity? reminder = await _context.Reminders.FindAsync(id, cancellationToken);

            if (reminder == null)
            {
                _logger.LogWarning($"Reminder with id {id} not found.");
                throw new NotFoundException($"Reminder with id {id} not found.");
            }

            if (!String.IsNullOrEmpty(errorMessage) && errorMessage.Length > ERROR_MESSAGE_MAX_LENGTH)
            {
                errorMessage = errorMessage.Substring(0, ERROR_MESSAGE_MAX_LENGTH);
            }

            reminder.Status = status;
            reminder.ErrorMessage = errorMessage;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            ReminderEntity? reminder = await _context.Reminders.FindAsync(id);

            if (reminder == null)
            {
                _logger.LogWarning($"Reminder with id {id} not found.");
                throw new NotFoundException($"Reminder with id {id} not found.");
            }

            _context.Reminders.Remove(reminder);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
