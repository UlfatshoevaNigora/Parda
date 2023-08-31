using Domain.Dtos.Client;
using Domain.Entities;
using Infrastructure.Context;
using AutoMapper;
using Domain.Dtos.Order;
using Domain.Wrapper;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ClientService;

public class ClientService: IClientService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public  ClientService(DataContext context, IMapper mapper)
    {
        _context=context;
        _mapper = mapper;
    }
    
    public async Task<Response<AddClient>> AddClient (AddClient model)
    {
        try
        {
            var client = new Client()
            {
                Id = model.Id,
                Name = model.Name,
            };
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
            var response= model;
            return new Response<AddClient>(response);
        }
        catch (Exception ex){
            return new Response<AddClient>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async  Task<Response<AddClient>> UpdateClient(AddClient model)
    {
        try
        {
            var find =await _context.Clients.FindAsync(model.Id);
            if (find != null)
            {
                find.Name = model.Name;
                await _context.SaveChangesAsync();
                var response = model;
                return new Response<AddClient>(response);
            }
            else
            {
                return new Response<AddClient>(HttpStatusCode.BadRequest, "Tакого Клиента нет в списке Клиентов");
            }

        }
        catch (Exception ex){
            return new Response<AddClient>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }



    public async  Task<Response<List<GetClient>>> GetListOfClients(string? name)
    {
        try
        {
            var queryable =  _context.Clients.AsQueryable();

            if (name != null)
                queryable = queryable.Where(x => x.Name.ToLower().Contains(name.ToLower()));

          var response =   await queryable.Select(o => new GetClient()
            {
                Id = o.Id,
                Name = o.Name,
                OrdersOfClient = _mapper.Map<List<GetOrder>>(o.Orders), 

            }).ToListAsync();
            return new Response<List<GetClient>>(response); 

        }
        catch(Exception ex){
            return new Response<List<GetClient>>(HttpStatusCode.InternalServerError, ex.Message);
        }

    }


    public async  Task<Response<GetClient>> GetClientById(int id)
    {
        try
        {
            var response =await _context.Clients.Select(x => new GetClient()
            {
                Id = x.Id,
                Name = x.Name,
                OrdersOfClient = _mapper.Map<List<GetOrder>>(x.Orders),
            }).FirstOrDefaultAsync(p => p.Id == id);
            return new Response<GetClient>(response);
        }
        catch (Exception ex){
            return new Response<GetClient>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }





    public async Task<Response<bool>> DeleteClient (int id)
    {
        try
        {
            var client =await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                var result = await _context.SaveChangesAsync();
                var response = result == 1;
                return new Response<bool>(response);
            }
            else 
            {
                return new Response<bool>(HttpStatusCode.BadRequest, "такого Клиента нет в списке Клиентов");
            }
        }
        catch (Exception ex)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}