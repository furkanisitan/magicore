namespace Core.Utilities.Results;

public interface IResult
{
    bool Success { get; set; }
    string? Message { get; set; }
}

public interface IResult<T> : IResult
{
    T? Data { get; set; }
}