using FluentValidation;
using HR_Platform.Application.Common.Behaviors;
using HR_Platform.Application.Services;
using HR_Platform.Application.ServicesInterfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HR_Platform.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
        });

        services.AddScoped
        (        
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>)
        );

        services.AddTransient<ICalculateTimeDifference, CalculateTimeDifference>();
        services.AddTransient<ICalculateTimeCollaboratorWorked, CalculateTimeCollaboratorWorked>();
        services.AddTransient<IEqualsIgnoreCaseAndDiacriticsService, EqualsIgnoreCaseAndDiacriticsService>();
        services.AddTransient<ITimeElapsedFormatter, TimeElapsedFormatter>();
        services.AddTransient<IReferenceGenerator, ReferenceGenerator>();

        services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();



        return services;
    }
}