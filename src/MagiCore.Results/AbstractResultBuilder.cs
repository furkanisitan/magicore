namespace MagiCore.Results;

/// <summary>
/// Base class for classes that implement the builder pattern to classes that implement the <see cref="IResult"/>.
/// </summary>
/// <typeparam name="TResult">The type of the class that implements the <see cref="IResult"/>.</typeparam>
public abstract class AbstractResultBuilder<TResult>
    where TResult : class, IResult, new()
{
    protected readonly AbstractResultBuilder<TResult> Builder;
    protected readonly TResult Result;

    protected AbstractResultBuilder()
    {
        Builder = this;
        Result = new TResult();
    }

    public AbstractResultBuilder<TResult> Success(bool success = true)
    {
        Result.Success = success;
        return Builder;
    }

    public AbstractResultBuilder<TResult> Message(string message)
    {
        Result.Message = message;
        return Builder;
    }

    public AbstractResultBuilder<TResult> Errors(params string[] errors)
    {
        Result.Errors = errors;
        return Builder;
    }

    public AbstractResultBuilder<TResult> Errors(IEnumerable<string> errors)
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
/// <typeparam name="TPayload">The type of payload.</typeparam>
public abstract class AbstractResultBuilder<TResult, TPayload> : AbstractResultBuilder<TResult>
    where TResult : class, IResult<TPayload>, new()
{
    protected AbstractResultBuilder(TPayload payload)
    {
        Result.Payload = payload;
    }
}