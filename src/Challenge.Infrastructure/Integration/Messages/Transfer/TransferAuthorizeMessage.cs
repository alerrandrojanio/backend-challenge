using System.Text.Json.Serialization;

namespace Challenge.Infrastructure.Integration.Messages.Transfer;

public class TransferAuthorizeMessage
{
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("data")]
    public TransferAuthorizeDataMessage? Data { get; set; }
}
