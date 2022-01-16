namespace MagiCore.Results;

public interface IResult : ISuccessResult, IErrorResult
{
    new bool Success { get; set; }
    new string? Message { get; set; }
}

public interface IResult<T> : IResult, ISuccessResult<T>, IErrorResult<T>
{
    new T? Payload { get; set; }
}