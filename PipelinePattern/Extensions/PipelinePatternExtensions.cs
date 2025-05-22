using Microsoft.Extensions.DependencyInjection;
using PipelinePattern.Core;
using PipelinePattern.Interfaces;
using System.Reflection;

namespace PipelinePattern.Extensions;

public static class PipelinePatternExtensions
{
    public static IServiceCollection GetPipelineServices(this IServiceCollection services
        , params Assembly[] assemblies)
    {
        if (assemblies == null || assemblies.Length == 0)
            throw new ArgumentException("At least one assembly must be provided.", nameof(assemblies));
        services.AddPipelineExecutors(assemblies);
        services.AddPipelineSteps(
            assemblies,
            ServiceLifetime.Transient);
        return services;
    }
    private static IServiceCollection AddPipelineExecutors(
        this IServiceCollection services,
        IEnumerable<Assembly> assemblies,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        // Get the generic interface and executor type
        var pipelineExecutorInterface = typeof(IPipelineExecutor<>);
        var pipelineExecutorType = typeof(PipelineExecutor<>);

        foreach (var assembly in assemblies)
        {
            // Get all TContext types (including internal) that implement IPipeLineContext
            var contextTypes = assembly.GetTypes()
                .Where(t => t.IsClass && 
                !t.IsAbstract && 
                !t.IsGenericType && 
                typeof(IPipelineContextBase).IsAssignableFrom(t));

            foreach (var contextType in contextTypes)
            {
                // Create closed generics: IPipelineExecutor<TContext> and PipelineExecutor<TContext>
                var closedInterface = pipelineExecutorInterface.MakeGenericType(contextType);
                var closedExecutor = pipelineExecutorType.MakeGenericType(contextType);

                // Register IPipelineExecutor<TContext> -> PipelineExecutor<TContext>
                services.Add(new ServiceDescriptor(closedInterface, closedExecutor, lifetime));
            }
        }

        return services;
    }
    private static IServiceCollection AddPipelineSteps(
        this IServiceCollection services,
        IEnumerable<Assembly> assemblies,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        var pipelineStepInterface = typeof(IPipelineStep<>);

        foreach (var assembly in assemblies)
        {
            // Get all class types (including internal) that implement IPipelineStep<>
            var types = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Type = t,
                    Interfaces = t.GetInterfaces()
                        .Where(i => i.IsGenericType &&
                                     i.GetGenericTypeDefinition() == pipelineStepInterface)
                })
                .Where(x => x.Interfaces.Any());

            foreach (var type in types)
            {
                foreach (var interfaceType in type.Interfaces)
                {
                    // Register the type against its closed interface (e.g., IPipelineStep<MyContext>)
                    services.Add(new ServiceDescriptor(interfaceType, type.Type, lifetime));
                }
            }
        }
        return services;
    }
}