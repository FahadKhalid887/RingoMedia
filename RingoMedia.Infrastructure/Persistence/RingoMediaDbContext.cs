using Microsoft.EntityFrameworkCore;
using RingoMedia.Domain.Entities;

namespace RingoMedia.Infrastructure.Persistence
{
    public class RingoMediaDbContext : DbContext
    {
        public RingoMediaDbContext(DbContextOptions<RingoMediaDbContext> options)
            : base(options) { }

        public DbSet<DepartmentEntity> Departments { get; set; }
        public DbSet<ReminderEntity> Reminders { get; set; }
    }
}
