// See https://aka.ms/new-console-template for more information
using CQRS.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using PipelinePattern.ClassLibrary;
using PipelinePattern.ClassLibrary.Command;
using PipelinePattern.Extensions;
using System.Reflection;
Console.WriteLine("Hello, World!");
var services = new ServiceCollection();
List<Assembly> assemblies = [];
services.AddClassLibrary(assemblies);
services.GetPipelineServices([.. assemblies]);

Console.WriteLine("Finish Registration");
var provider = services.BuildServiceProvider();

var sender = provider.GetRequiredService<ISender>();
var command = new AddSupplierPaymentCommand(Guid.NewGuid(), 1000, "USD", DateTime.UtcNow, "Cash", "REF-001");
var result = await sender.Send(command);
Console.WriteLine(result.IsFailure ? $"Failed: {result.Error}" : "Success");
// UnSuccess Command

var command2 = new AddSupplierPaymentCommand(Guid.Empty, 1000, "USD", DateTime.UtcNow, "Cash", "REF-001");
var result2 = await sender.Send(command2);
Console.WriteLine(result2.IsFailure ? $"Failed: {result2.Error}" : "Success");
