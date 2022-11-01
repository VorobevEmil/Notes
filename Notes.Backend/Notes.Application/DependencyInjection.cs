using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Common.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection service)
        {
            service.AddMediatR(Assembly.GetExecutingAssembly());
            service.AddValidatorsFromAssemblies(new[] {Assembly.GetExecutingAssembly() });
            service.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return service;
        }
    }
}
