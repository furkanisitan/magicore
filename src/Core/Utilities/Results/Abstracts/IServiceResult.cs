namespace Core.Utilities.Results.Abstracts;

public interface IServiceResult : IResult
{
    public int StatusCode { get; set; }
}

public interface IServiceResult<T> : IServiceResult, IResult<T>
{
}