using Challenge.Domain.DTOs.Client;
using Challenge.Domain.DTOs.Client.Response;
using Challenge.Domain.DTOs.Transfer.Response;
using Challenge.Domain.Interfaces;
using Challenge.Infrastructure.Configurations;
using Challenge.Infrastructure.Integration.Messages.Transfer;
using Mapster;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json;

namespace Challenge.Infrastructure.Integration.Services;

public class TransferAuthorizeIntegrationService : ITransferAuthorizeIntegrationService
{
    private readonly IHttpClientService _httpClientService;
    private readonly TransferSettings _transferSettings;

    public TransferAuthorizeIntegrationService(IHttpClientService httpClientService, IOptions<TransferSettings> transferSettings)
    {
        _httpClientService = httpClientService;
        _transferSettings = transferSettings.Value;
    }

    public async Task<TransferAuthorizationResponseDTO> AuthorizeTransfer()
    {
        ClientGetRequestDTO clientGetRequestDTO = new()
        {
            Uri = new Uri(_transferSettings.UrlAuthorize)
        };

        ClientResponseDTO clientResponseDTO = await _httpClientService.SendGetRequest(clientGetRequestDTO);

        if (clientResponseDTO.StatusCode != HttpStatusCode.OK)
            _httpClientService.ProcessClientError(clientResponseDTO);

        TransferAuthorizeMessage transferAuthorizeMessage = JsonSerializer.Deserialize<TransferAuthorizeMessage>(clientResponseDTO.Content!);

        TransferAuthorizationResponseDTO transferAuthorizationResponseDTO = transferAuthorizeMessage.Adapt<TransferAuthorizationResponseDTO>();

        return transferAuthorizationResponseDTO;
    }
}
