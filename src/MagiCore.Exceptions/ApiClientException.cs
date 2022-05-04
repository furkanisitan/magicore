using System.Net;

namespace MagiCore.Exceptions;

/// <summary>
/// An exception representing an 3rd party API client error.
/// </summary>
[Serializable]
public sealed class ApiClientException : Exception
{
    private const string DefaultMessage = "An error status code was returned from the 3rd party API.";

    public string ApiName { get; }
    public IEnumerable<string> Errors { get; }
    public HttpStatusCode StatusCode { get; }

    public ApiClientException(string apiName, IEnumerable<string>? errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : this(DefaultMessage, apiName, errors, statusCode)
    {
    }

    public ApiClientException(string? message, string apiName, IEnumerable<string>? errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : this(message, null, apiName, errors, statusCode)
    {
    }

    public ApiClientException(string? message, Exception? innerException, string apiName, IEnumerable<string>? errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message, innerException)
    {
        ApiName = apiName;
        Errors = errors ?? Enumerable.Empty<string>();
        StatusCode = statusCode;
    }

}