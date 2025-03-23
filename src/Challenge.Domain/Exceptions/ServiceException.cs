using System.Net;

namespace Challenge.Domain.Exceptions;

public class ServiceException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public ServiceException(string message, HttpStatusCode statusCode)
        : base(message)
    {
        StatusCode = statusCode;
    }

    public ServiceException(string message, Exception innerException, HttpStatusCode statusCode)
        : base(message, innerException)
    {
        StatusCode = statusCode;
    }
}
