using System.Net;

namespace Challenge.Domain.Exceptions;

public class ServiceException : Exception
{
    public int ErrorCode { get; }

    public ServiceException(string message, HttpStatusCode errorCode)
        : base(message)
    {
        ErrorCode = (int)errorCode;
    }

    public ServiceException(string message, Exception innerException, HttpStatusCode errorCode)
        : base(message, innerException)
    {
        ErrorCode = (int)errorCode;
    }
}
