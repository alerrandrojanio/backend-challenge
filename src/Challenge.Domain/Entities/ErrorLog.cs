using Challenge.Domain.Entities.Base;

namespace Challenge.Domain.Entities;

public class ErrorLog : MongoBaseEntity
{
    public string Message { get; set; } = string.Empty;

    public string StackTrace { get; set; } = string.Empty;

    public string Source { get; set; } = string.Empty;

    public string InnerException { get; set; } = string.Empty;
}
