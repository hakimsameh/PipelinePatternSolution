# Async Pipeline Pattern in C#

A lightweight, extensible, and asynchronous **Pipeline Pattern** implementation in C# for orchestrating sequential logic, validation, transformation, and processing tasks.

This solution is designed to be reusable across applications and integrates smoothly with dependency injection systems (like ASP.NET Core DI).

---

## ✨ Features

- ✅ Simple and clean asynchronous pipeline implementation
- ✅ Steps executed in defined order
- ✅ Fully DI-compatible using `IServiceCollection`
- ✅ Easily extensible for business logic scenarios
- ✅ Generic and context-agnostic

---

## 📦 Installation

Add the pipeline project to your solution or package it as a NuGet package.

Then, reference it from your application:

```bash
dotnet add reference ../PipelinePatternAsync/PipelinePattern.csproj
```

> Or publish and install it as a NuGet package if shared across projects.

---

## 🧩 Interfaces

### `IPipelineContext`

Marker interface for all pipeline contexts.

```csharp
public interface IPipelineContext { }
```

---

### `IPipelineStep<TContext>`

Represents a single step in the pipeline.

```csharp
public interface IPipelineStep<TContext> where TContext : IPipelineContext
{
    int Order { get; }
    Task<Result> ProcessAsync(TContext context, Func<Task<Result>> next, CancellationToken cancellationToken);
}
```

- `Order`: execution order of the step.
- `next`: a delegate to invoke the next step.
- `context`: your custom pipeline context.

---

### `IPipelineExecutor<TContext>`

Responsible for executing the entire pipeline for a specific context.

```csharp
public interface IPipelineExecutor<TContext> where TContext : IPipelineContext
{
    Task<Result> ExecuteAsync(TContext context, CancellationToken cancellationToken = default);
}
```

---

## 🏗️ Setup

### 1. Define Your Context

```csharp
public class MyPipelineContext : IPipelineContext
{
    public string Input { get; set; }
}
```

---

### 2. Create Your Steps

```csharp
public class ValidateInputStep : IPipelineStep<MyPipelineContext>
{
    public int Order => 1;

    public Task<Result> ProcessAsync(MyPipelineContext context, Func<Task<Result>> next, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(context.Input))
            return Task.FromResult(Result.Failure("Input cannot be empty."));

        return next();
    }
}
```

---

### 3. Register Services

In `Program.cs` or `Startup.cs`:

```csharp
builder.Services.GetPipelineServices(typeof(MyPipelineContext).Assembly);
```

This scans and registers:

- All `IPipelineStep<TContext>` implementations
- The corresponding `IPipelineExecutor<TContext>`

---

### 4. Execute the Pipeline

```csharp
public class MyHandler
{
    private readonly IPipelineExecutor<MyPipelineContext> _executor;

    public MyHandler(IPipelineExecutor<MyPipelineContext> executor)
    {
        _executor = executor;
    }

    public async Task<Result> HandleAsync(string input)
    {
        var context = new MyPipelineContext { Input = input };
        return await _executor.ExecuteAsync(context);
    }
}
```

---

## 🔧 Result Handling

Includes a simple `Result` and `Result<T>` class:

```csharp
var result = await _executor.ExecuteAsync(context);

if (result.IsFailure)
{
    Console.WriteLine($"Error: {result.Error}");
}
```

---

## 🧪 Example Use Cases

- Request validation pipelines
- Business rule enforcement
- ETL (Extract, Transform, Load) pipelines
- File or message processing flows
- Authorization steps

---

## 📁 Folder Structure

```
/PipelinePattern
│
├── IPipelineContext.cs
├── IPipelineStep.cs
├── IPipelineExecutor.cs
├── PipelineExecutor.cs
├── Extensions.cs
├── Result.cs
├── ResultT.cs
```

---

## 📜 License

Licensed under the [MIT License](LICENSE).

---

## 🤝 Contributions

Contributions are welcome! Open issues, suggest features, or submit PRs.

---

## 🙋 Support

Feel free to open an issue or reach out for implementation questions or ideas for improvements.