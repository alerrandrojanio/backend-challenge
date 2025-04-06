using Challenge.Domain.DTOs.Transfer.Response;

namespace Challenge.Domain.Interfaces;

public interface ITransferAuthorizeIntegrationService
{
    Task<TransferAuthorizationResponseDTO> AuthorizeTransfer();
}
