using System.ComponentModel;

namespace MagiCore.Results;

public interface IErrorResult
{
    [DefaultValue(false)]
    public bool Success { get; set; }
    public string? Message { get; set; }
    ICollection<string>? Errors { get; set; }
}

public interface IErrorResult<T> : IErrorResult
{
    T? Payload { get; set; }
}