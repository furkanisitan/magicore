using Core.Utilities.Results.Abstracts;
using Core.Utilities.Results.Builders;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public static ResultBuilder Builder() => new();
        public static ResultBuilder<T> Builder<T>(T data) => new(data);
    }

    public class Result<T> : Result, IResult<T>
    {
        public T Data { get; set; }
    }
}
