using Core.Utilities.Results.Abstracts;
using Core.Utilities.Results.Builders;

namespace Core.Utilities.Results;

public class ServiceResult : Result, IServiceResult
{
    public int StatusCode { get; set; }

    public new static ServiceResultBuilder Builder() => new();

    public new static ServiceResultBuilder<T> Builder<T>(T data) => new(data);
}

public class ServiceResult<T> : ServiceResult, IServiceResult<T>
{
    public T Data { get; set; }
}