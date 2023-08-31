using Domain.Dtos.Client;
using Domain.Wrapper;


namespace Infrastructure.Services.ClientService;

public interface IClientService
{
    public Task<Response<AddClient>> AddClient(AddClient client);
    public Task<Response<AddClient>> UpdateClient(AddClient client);
    public Task<Response<bool>> DeleteClient(int id);
    public Task<Response<GetClient>> GetClientById(int id);
    public Task<Response<List<GetClient>>> GetListOfClients(string? name);
}
