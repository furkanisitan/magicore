namespace Core.Utilities.Results.Builders
{
    public class ServiceResultBuilder<T> : AbstractResultBuilder<ServiceResult<T>, ServiceResultBuilder<T>, T>
    {
        public ServiceResultBuilder() { }

        public ServiceResultBuilder(T data) : base(data) { }

        public ServiceResultBuilder<T> StatusCode(int statusCode)
        {
            Result.StatusCode = statusCode;
            return Builder;
        }

    }
}