using MagiCore.Results;
using System.Net;

namespace MagiCore.ExceptionHandling;

public class ExceptionHandlerResult
{
    public HttpStatusCode? StatusCode { get; set; }
    public IResult? Result { get; set; }
}