using Charisma.Domain.Core.Appointments;
using Charisma.Domain.Core.Doctors;
using Charisma.Domain.Core.Salary;
using Charisma.Domain.SubDomains;
using Charisma.Domain.SubDomains.Doctors.States;
using Microsoft.Extensions.DependencyInjection;

namespace Charisma.Domain
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