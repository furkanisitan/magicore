namespace Core.Utilities.Results.Builders
{
    public class ResultBuilder<T> : AbstractResultBuilder<Result<T>, ResultBuilder<T>, T>
    {
        public ResultBuilder() { }

        public ResultBuilder(T data) : base(data) { }
    }

}
