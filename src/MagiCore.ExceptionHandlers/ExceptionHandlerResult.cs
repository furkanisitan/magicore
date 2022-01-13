using MagiCore.Results;

namespace MagiCore.ExceptionHandlers;

public class ExceptionHandlerResult
{
    public int? StatusCode { get; set; }
    public IResult? Result { get; set; }
}