namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }

        #region Builders

        public static ResultBuilder Builder() => new();
        public static ResultBuilder<T> Builder<T>(T data) => new(data);

        #endregion

        #region Builder Classes

        public class ResultBuilder : AbstractResultBuilder<Result, ResultBuilder> { }

        public class ResultBuilder<T> : AbstractResultBuilder<Result<T>, ResultBuilder<T>, T>
        {
            public ResultBuilder(T data) : base(data) { }
        }

        #endregion
    }

    public class Result<T> : Result, IResult<T>
    {
        public T? Data { get; set; }
    }
}
