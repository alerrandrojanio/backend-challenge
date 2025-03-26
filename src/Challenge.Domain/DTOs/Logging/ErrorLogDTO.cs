namespace Challenge.Domain.DTOs.Logging;

public class ErrorLogDTO
{
    public string Message { get; set; } = string.Empty;
    
    public string StackTrace { get; set; } = string.Empty;

    public string Source { get; set; } = string.Empty;

    public string InnerException { get; set; } = string.Empty;
}
