namespace Core.Utilities.Results;

public abstract class AbstractResultBuilder<TResult, TBuilder>
    where TResult : class, IResult, new()
    where TBuilder : AbstractResultBuilder<TResult, TBuilder>
{
    protected readonly TBuilder Builder;
    protected readonly TResult Result;

    protected AbstractResultBuilder()
    {
        Builder = (TBuilder)this;
        Result = Activator.CreateInstance<TResult>();
    }

    public TBuilder Success(bool success = true)
    {
        Result.Success = success;
        return Builder;
    }

    public TBuilder Message(string message)
    {
        Result.Message = message;
        return Builder;
    }

    public TBuilder AddError(string? error)
    {
        if (error == null) return Builder;

        Result.Errors ??= new List<string>();
        Result.Errors.Add(error);
        return Builder;
    }

    public TBuilder Errors(ICollection<string> errors)
    {
        Result.Errors = errors;
        return Builder;
    }

    public TResult Build() => Result;
}

public abstract class AbstractResultBuilder<TResult, TBuilder, TData> : AbstractResultBuilder<TResult, TBuilder>
    where TResult : class, IResult<TData>, new()
    where TBuilder : AbstractResultBuilder<TResult, TBuilder, TData>
{
    protected AbstractResultBuilder(TData payload)
    {
        Result.Payload = payload;
    }
}