using System;
using Charisma.Application.Common.Interfaces.Persistence;
using Charisma.Persistence.Caching;
using Charisma.Persistence.Common.Abstractions;
using Charisma.Persistence.Common.Constants;
using Charisma.Persistence.Common.Converters;
using Charisma.Persistence.Common.Converters.Abstracts;
using Charisma.Persistence.Common.Interceptors;
using Charisma.Persistence.DbContexts.EfCore;
using Charisma.Persistence.DbContexts.EfCore.InMemory;
using Charisma.Persistence.Entities;
using Charisma.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Charisma.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInterceptors();
            
            // todo: Un-comment the below code for real database usage
            /*services.AddDbContext<EfCoreDbContext>((serviceProvider, options) =>
            {
                var updateAuditableEntitiesInterceptor =
                    serviceProvider.GetRequiredService<UpdateAuditableEntitiesInterceptor>();
                var updateSoftDeletableEntitiesInterceptor =
                    serviceProvider.GetRequiredService<UpdateSoftDeletableEntitiesInterceptor>();
                options.UseSqlServer(configuration.GetConnectionString(ConnectionStringKeys.Database))
                    .AddInterceptors(updateAuditableEntitiesInterceptor, updateSoftDeletableEntitiesInterceptor);
            });*/
            
            // todo: Comment the below code for real database usage
            services.AddDbContext<EfCoreDbContext>(opt => opt.UseInMemoryDatabase("CharismaDb"));
            
            services.AddTransient<IEntityConvertor, EntityConvertor>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IAsyncRepository<DoctorEntity, Guid>, AsyncRepository<DoctorEntity, Guid>>();
            services.AddScoped<IAsyncRepository<ScheduleEntity, long>, AsyncRepository<ScheduleEntity, long>>();
            services.AddScoped<IAsyncRepository<PatientEntity, Guid>, AsyncRepository<PatientEntity, Guid>>();
            services.AddScoped<IAsyncRepository<AppointmentEntity, Guid>, AsyncRepository<AppointmentEntity, Guid>>();
            
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
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();

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