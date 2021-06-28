namespace Core.Utilities.Results.Builders
{
    public class ResultBuilder : AbstractResultBuilder<Result, ResultBuilder>
    {
    }

    public class ResultBuilder<T> : AbstractResultBuilder<Result<T>, ResultBuilder<T>, T>
    {
        public ResultBuilder(T data) : base(data) { }
    }

}
