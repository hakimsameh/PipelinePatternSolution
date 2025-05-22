using CQRS.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace PipelinePattern.ClassLibrary;

public static class ClassLibExtension
{
    public static IServiceCollection AddClassLibrary(this IServiceCollection services, List<Assembly> assemblies) 
    {
        var currentAssembly = typeof(ClassLibExtension).Assembly;
        assemblies.Add(currentAssembly); ;
        services.AddMediatorService(
            cfg =>
            {
                cfg.RegisterServicesFromAssemblies([currentAssembly]);
            });
        services.AddLogging(builder => builder.AddConsole());
        return services;
    } 
}

