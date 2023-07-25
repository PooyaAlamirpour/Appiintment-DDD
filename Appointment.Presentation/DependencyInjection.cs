using Appointment.Presentation.Common.Errors;
using Appointment.Presentation.Common.Mappings;
using Appointment.Presentation.Common.Mappings.Abstracts;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Appointment.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
    
            services.AddEndpointsApiExplorer();
    
            services.AddSingleton<ProblemDetailsFactory, AppointmentProblemDetailsFactory>();
    
            services.AddMappings();
    
            services.AddHttpContextAccessor();
    
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            services.AddTransient<IDtoConvertor, DtoConvertor>();
    
            return services;
        }
    
        private static IServiceCollection AddMappings(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(PresentationAssembly.Assembly);
            services.AddSingleton(config);
    
            services.AddScoped<IMapper, ServiceMapper>();
    
            return services;
        }
    }
}

