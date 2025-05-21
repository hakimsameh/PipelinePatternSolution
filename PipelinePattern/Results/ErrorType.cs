namespace PipelinePattern.Results;

// https://youtu.be/YBK93gkGRj8?t=334

public enum ErrorType
{
    Failure = 0,
    Validation = 1,
    NotFound = 2,
    Conflict = 3,
    Problem = 4,
    None = 5
}