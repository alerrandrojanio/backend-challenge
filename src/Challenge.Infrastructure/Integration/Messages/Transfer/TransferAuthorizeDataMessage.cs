using System.Text.Json.Serialization;

namespace Challenge.Infrastructure.Integration.Messages.Transfer;

public class TransferAuthorizeDataMessage
{
    [JsonPropertyName("authorization")]
    public bool? Authorization { get; set; }
}
