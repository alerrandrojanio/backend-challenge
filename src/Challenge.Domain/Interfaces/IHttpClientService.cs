using Challenge.Domain.DTOs.Client;
using Challenge.Domain.DTOs.Client.Response;

namespace Challenge.Domain.Interfaces;

public interface IHttpClientService
{
    Task<ClientResponseDTO> SendGetRequest(ClientGetRequestDTO clientGetRequestDTO);

    void ProcessClientError(ClientResponseDTO clientResponseDTO);
}
