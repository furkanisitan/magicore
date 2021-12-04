namespace Core.Utilities.Results.Builders;

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