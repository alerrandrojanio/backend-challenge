namespace Challenge.Domain.DTOs.Email;

public class EmailDTO
{
    public string ReceiverName { get; set; } = string.Empty;

    public string EmailTo { get; set; } = string.Empty;
    
    public string Subject { get; set; } = string.Empty;
    
    public string Body { get; set; } = string.Empty;
}
