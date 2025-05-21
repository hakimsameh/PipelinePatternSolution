// See https://aka.ms/new-console-template for more information
using CQRS.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using PipelinePattern.ClassLibrary;
using PipelinePattern.ClassLibrary.Command;
using PipelinePattern.Extensions;
using System.Reflection;
Console.WriteLine("Hello, World!");
var services = new ServiceCollection();
List<Assembly> assemblies = new List<Assembly>();
services.AddClassLibrary(assemblies);
services.GetPipelineServices(assemblies.ToArray());

Console.WriteLine("Finish Registration");
var provider = services.BuildServiceProvider();

var sender = provider.GetRequiredService<ISender>();
var command = new AddSupplierPaymentCommand(Guid.NewGuid(), 1000, "USD", DateTime.UtcNow, "Cash", "REF-001");
var result = await sender.Send(command);

Console.WriteLine(result.IsFailure ? $"Failed: {result.Error}" : "Success");