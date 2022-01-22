namespace MagiCore.Results;

public interface IResult
{
    bool Success { get; set; }
    string? Message { get; set; }
    ICollection<string>? Errors { get; set; }
}

public interface IResult<T> : IResult
{
    T? Payload { get; set; }
}