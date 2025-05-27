using SamSoft.Common.Results;

namespace PipelinePattern.Interfaces;

public interface IPipelineContextBase
{
    Result ContextResult { get; }
}
