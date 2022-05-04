namespace MagiCore.Results;

public class Result : IResult
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public IEnumerable<string>? Errors { get; set; }

    #region Builders

    public static ResultBuilder Builder() => new();
    public static ResultBuilder<T> Builder<T>(T payload) => new(payload);

    #endregion

    #region Builder Classes

    public class ResultBuilder : AbstractResultBuilder<Result>
    {
        internal ResultBuilder() { }
    }

    public class ResultBuilder<T> : AbstractResultBuilder<Result<T>, T>
    {
        internal ResultBuilder(T payload) : base(payload) { }
    }

    #endregion
}

public class Result<T> : Result, IResult<T>
{
    public T? Payload { get; set; }
}