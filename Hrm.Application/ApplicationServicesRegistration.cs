using Hrm.Application.Features.AttendanceDevice.Interfaces;
using Hrm.Application.Features.AttendanceDevice.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Hrm.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
           services.AddAutoMapper(Assembly.GetExecutingAssembly());
           services.AddMediatR(Assembly.GetExecutingAssembly());
           services.AddTransient<IAttendanceDevice, AttendanceDevice>();
       
            return services;
        }
    }
}
