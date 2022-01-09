using Core.Utilities.Results;

namespace Core.ExceptionHandlers;

public class ExceptionHandlerResult
{
    public int? StatusCode { get; set; }
    public IResult? Result { get; set; }
}