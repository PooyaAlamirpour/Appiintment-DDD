using System;
using Appointment.Application.Common.Interfaces.Persistence;
using Appointment.Persistence.Caching;
using Appointment.Persistence.Common.Abstractions;
using Appointment.Persistence.Common.Constants;
using Appointment.Persistence.Common.Converters;
using Appointment.Persistence.Common.Converters.Abstracts;
using Appointment.Persistence.Common.Interceptors;
using Appointment.Persistence.Common.Logging;
using Appointment.Persistence.DbContexts.EfCore;
using Appointment.Persistence.Entities;
using Appointment.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Appointment.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInterceptors();
            
            // todo: Un-comment the below code for real database usage
            services.AddDbContext<EfCoreDbContext>((serviceProvider, options) =>
            {
                var updateAuditableEntitiesInterceptor =
                    serviceProvider.GetRequiredService<UpdateAuditableEntitiesInterceptor>();
                var updateSoftDeletableEntitiesInterceptor =
                    serviceProvider.GetRequiredService<UpdateSoftDeletableEntitiesInterceptor>();
                options.UseSqlServer(configuration.GetConnectionString(ConnectionStringKeys.Database))
                    .AddInterceptors(updateAuditableEntitiesInterceptor, updateSoftDeletableEntitiesInterceptor);
            });
            
            // todo: Comment the below code for real database usage
            // services.AddDbContext<EfCoreDbContext>(opt => opt.UseInMemoryDatabase("AppointmentDb"));
            
            services.AddTransient<IEntityConvertor, EntityConvertor>();
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddRepositories();

            services.AddCaching(configuration);

            return services;
        }

        private static IServiceCollection AddInterceptors(this IServiceCollection services)
        {
            services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
            services.AddSingleton<UpdateSoftDeletableEntitiesInterceptor>();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAppointmentRepository>(x =>
            {
                var convertor = x.GetService<IEntityConvertor>();
                var asyncRepository = x.GetService<IAsyncRepository<AppointmentEntity, Guid>>();
                IAppointmentRepository appointmentRepository = new AppointmentRepository(convertor, asyncRepository);

                appointmentRepository = LoggingProxy<IAppointmentRepository>
                    .SetProxy(appointmentRepository);
                
                return appointmentRepository;
            });
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IAsyncRepository<DoctorEntity, Guid>, AsyncRepository<DoctorEntity, Guid>>();
            services.AddScoped<IAsyncRepository<ScheduleEntity, long>, AsyncRepository<ScheduleEntity, long>>();
            services.AddScoped<IAsyncRepository<PatientEntity, Guid>, AsyncRepository<PatientEntity, Guid>>();
            services.AddScoped<IAsyncRepository<AppointmentEntity, Guid>, AsyncRepository<AppointmentEntity, Guid>>();
            
            return services;
        }

        private static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CacheSettings>(configuration.GetSection(CacheSettings.SectionName));

            services.AddStackExchangeRedisCache(options =>
                options.Configuration = configuration.GetConnectionString(ConnectionStringKeys.Redis));

            services.AddScoped<ICacheService, CacheService>();

            return services;
        }
    }
}