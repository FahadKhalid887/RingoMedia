using RingoMedia.Application.DTOs;
using RingoMedia.Domain.Enums;

namespace RingoMedia.Application.Interfaces.Repositories
{
    public interface IReminderRepository
    {
        Task<IEnumerable<ReminderDto>> FindAsync(ReminderStatus? status, DateTime? dateTo, CancellationToken cancellationToken = default);
        Task<ReminderDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<int> AddAsync(string title, string? message, string email, DateTimeOffset dateTime, CancellationToken cancellationToken = default);
        Task UpdateAsync(int id, ReminderStatus status, string? errorMessage, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
