using Contose.DAL.Core.IReposotories;
using Contose.DAL.Core.Reposotories;
using Contoso.Services.IServices;
using Contoso.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.API.Infrastructure.Extensions
{
    public static class RegisterCustomServicesExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            //services
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IStudentService, StudentService>();

            //respositories
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();

            return services;
        }
    }
}
