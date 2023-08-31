using Microsoft.AspNetCore.Mvc;
using Infrastructure.Services;
using Domain.Dtos.Client;
using Domain.Wrapper;
using Infrastructure.Services.ClientService;


namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class ClientController
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }


    [HttpPost("AddClient")]
     public async Task<Response<AddClient>> AddClient (AddClient client)
    {
        return await _clientService.AddClient(client);
    }



    [HttpPut("UpdateClient")]
    public async Task<Response<AddClient>> UpdateClient (AddClient client)
    {
        return await _clientService.UpdateClient(client);
    }



    [HttpGet("GetById")] 
    public async Task<Response<GetClient>> GetClientById (int id)
    {
        return await _clientService.GetClientById(id);
    }
    

    


    [HttpGet("GetClients")]
    public async Task<Response<List<GetClient>>> GetListOfClients(string name)
    {
        return await _clientService.GetListOfClients(name);
    }



    [HttpDelete("DeleteClient")]
    public async Task<Response<bool>> DeleteClient (int id)
    {
        return await _clientService.DeleteClient(id);
    }

}    
