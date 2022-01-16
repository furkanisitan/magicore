using System.ComponentModel;

namespace MagiCore.Results;

public interface ISuccessResult
{
    [DefaultValue(true)]
    public bool Success { get; set; }
    public string? Message { get; set; }
}

public interface ISuccessResult<T> : ISuccessResult
{
    T? Payload { get; set; }
}