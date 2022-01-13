namespace MagiCore.Results;

/// <summary>
/// Base class for classes that implement the builder pattern to classes that implement the <see cref="IResult"/>.
/// </summary>
/// <typeparam name="TResult">The type of the class that implements the <see cref="IResult"/>.</typeparam>
/// <typeparam name="TBuilder">The type of the class that will inherit this class.</typeparam>
public abstract class AbstractResultBuilder<TResult, TBuilder>
    where TResult : class, IResult, new()
    where TBuilder : AbstractResultBuilder<TResult, TBuilder>
{
    protected readonly TBuilder Builder;
    protected readonly TResult Result;

    protected AbstractResultBuilder()
    {
        Builder = (TBuilder)this;
        Result = new TResult();
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
        if (error is null) return Builder;

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

/// <summary>
/// Base class for classes that implement the builder pattern to classes that implement the <see cref="IResult{T}"/>.
/// </summary>
/// <typeparam name="TResult">The type of the class that implements the <see cref="IResult{T}"/>.</typeparam>
/// <typeparam name="TBuilder">The type of the class that will inherit this class.</typeparam>
/// <typeparam name="TPayload">The type of payload.</typeparam>
public abstract class AbstractResultBuilder<TResult, TBuilder, TPayload> : AbstractResultBuilder<TResult, TBuilder>
    where TResult : class, IResult<TPayload>, new()
    where TBuilder : AbstractResultBuilder<TResult, TBuilder, TPayload>
{
    protected AbstractResultBuilder(TPayload payload)
    {
        Result.Payload = payload;
    }
}