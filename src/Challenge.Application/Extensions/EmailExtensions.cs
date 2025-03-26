using Challenge.Domain.DTOs.Account.Response;

namespace Challenge.Application.Extensions;

public static class EmailExtensions
{
    public static string GenerateEmailBody(string emailTemplate, CreateTransferResponseDTO createTransferResponseDTO)
    {
        return emailTemplate.Replace("{{NomeDestinatário}}", createTransferResponseDTO.PayeeName)
                            .Replace("{{Data}}", createTransferResponseDTO.CreatedAt.ToString("dd/MM/yyyy"))
                            .Replace("{{Valor}}", "R$ " + createTransferResponseDTO.Value)
                            .Replace("{{Conta}}", createTransferResponseDTO.AccountNumber)
                            .Replace("{{Identificacao}}", createTransferResponseDTO.TransferId.ToString());
    }
}
