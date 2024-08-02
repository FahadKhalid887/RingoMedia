using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RingoMedia.Application.Common.Mappings;
using RingoMedia.Application.Interfaces.Repositories;
using RingoMedia.Infrastructure.Persistence;
using RingoMedia.Infrastructure.Repositories;
using RingoMedia.Infrastructure.Services;

namespace RingoMedia.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IReminderRepository, ReminderRepository>();

            services.AddDbContext<RingoMediaDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton<EmailSender>(sp => new EmailSender(
                configuration["Email:SmtpHost"],
                int.Parse(configuration["Email:SmtpPort"]),
                configuration["Email:SmtpUser"],
                configuration["Email:SmtpPass"]
            ));

            services.AddHostedService<EmailReminderService>();


            return services;
        }
    }
}
