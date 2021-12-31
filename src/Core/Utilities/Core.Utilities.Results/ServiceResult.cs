namespace Core.Utilities.Results;

public class ServiceResult : Result, IServiceResult
{
    public int StatusCode { get; set; }

    #region Builders

    public new static ServiceResultBuilder Builder() => new();
    public new static ServiceResultBuilder<T> Builder<T>(T data) => new(data);

    #endregion

    #region Builder Classes

    public class ServiceResultBuilder : AbstractResultBuilder<ServiceResult, ServiceResultBuilder>
    {
        public ServiceResultBuilder StatusCode(int statusCode)
        {
            Result.StatusCode = statusCode;
            return Builder;
        }
    }

    public class ServiceResultBuilder<T> : AbstractResultBuilder<ServiceResult<T>, ServiceResultBuilder<T>, T>
    {
        public ServiceResultBuilder(T data) : base(data) { }

        // TODO DRY
        public ServiceResultBuilder<T> StatusCode(int statusCode)
        {
            Result.StatusCode = statusCode;
            return Builder;
        }
    }

    #endregion
}

public class ServiceResult<T> : ServiceResult, IServiceResult<T>
{
    public T? Data { get; set; }
}