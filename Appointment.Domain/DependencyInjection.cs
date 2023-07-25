using Appointment.Domain.Core.Appointments;
using Appointment.Domain.Core.Salary;
using Microsoft.Extensions.DependencyInjection;

namespace Appointment.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddTransient<IAppointmentAggregateRoot, AppointmentAggregateRoot>();
            services.AddTransient<ISalaryAggregateRoot, SalaryAggregateRoot>();

            return services;
        }
    }
}