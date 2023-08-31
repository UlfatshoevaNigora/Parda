using System.Net;
using AutoMapper;
using Domain.Dtos.Order;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.OrderService;

public class OrderService :IOrderService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;


    public OrderService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<AddOrder>> AddOrder(AddOrder model)
    {
        try
        {
            var order = new Order()
            {
                Id = model.Id,
                SewingType = model.SewingType,
                CorniceWidth = model.CorniceWidth,
                MaterialWidth=model.MaterialWidth,
                Height = model.Height,
                Description = model.Description,
                PriceOfOneMetr = model.PriceOfOneMetr,
                ClientId=model.ClientId,
                ClientName = model.ClientName,
            };
              await _context.Orders.AddAsync(order);
              await  _context.SaveChangesAsync();
            var n = new AddOrder()
            {
                Id = order.Id,
                SewingType=order.SewingType,
                CorniceWidth = order.CorniceWidth,
                Height = order.Height,
                Description = order.Description,
                PriceOfOneMetr = order.PriceOfOneMetr,
                ClientId=order.ClientId,
                MaterialWidth=order.MaterialWidth,
                ClientName = order.ClientName,
            };
           // var response =  _mapper.Map<AddOrder>(order);
            return new Response<AddOrder>(n);
        }
        catch (Exception ex)
        {
            return new Response<AddOrder>(HttpStatusCode.InternalServerError, ex.Message);
        }

    }

    public async Task<Response<AddOrder>> UpdateOrder(AddOrder model)
    {
        try
        {
            var find = await _context.Orders.FindAsync(model.Id);
            if (find != null)
            {
                find.SewingType = model.SewingType;
                find.CorniceWidth = model.CorniceWidth;
                find.Height = model.Height;
                find.Description = model.Description;
                find.PriceOfOneMetr = model.PriceOfOneMetr;
                find.ClientName = model.ClientName;
                find.ClientId = model.ClientId;
                find.MaterialWidth = model.MaterialWidth;
                await _context.SaveChangesAsync();
                var response = model;
                return new Response<AddOrder>(response);
            }
            else 
            {
                return new Response<AddOrder>(HttpStatusCode.BadRequest, "такого заказа нет в списке заказов");
            }
        }
        catch (Exception ex)
        {
            return new Response<AddOrder>(HttpStatusCode.InternalServerError, ex.Message);
        }

    }

    public async Task<Response<List<GetOrder>>> GetListOfOrders()
    {
        try
        {
            var response =await _context.Orders.Select(o => new GetOrder()
            {
                Id = o.Id,
                SewingType = o.SewingType,
                CorniceWidth = o.CorniceWidth,
                Height = o.Height,
                Description = o.Description,
                PriceOfOneMetr = o.PriceOfOneMetr,
                ClientName = o.ClientName,
                ClientId = o.ClientId,
                MaterialWidth = o.MaterialWidth,
                PriceOfOrder= (int)((double)o.PriceOfOneMetr*o.MaterialWidth),
            }).ToListAsync();

            return new Response<List<GetOrder>>(response);
        }
        catch (Exception ex)
        {
            return new Response<List<GetOrder>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<GetOrder>> GetOrderById(int id)
    {
        try
        {
            var response =await _context.Orders.Select(x => new GetOrder()
            {
                Id = x.Id,
                SewingType = x.SewingType,
                CorniceWidth = x.CorniceWidth,
                Height = x.Height,
                Description = x.Description,
                PriceOfOneMetr = x.PriceOfOneMetr,
                ClientName = x.ClientName,
                ClientId=x.ClientId,
                MaterialWidth = x.MaterialWidth,
                PriceOfOrder = (int)((double)x.PriceOfOneMetr * x.MaterialWidth),
            }).FirstOrDefaultAsync(p => p.Id == id);
            return new Response<GetOrder>(response);
        }
        catch (Exception ex)
        {
            return new Response<GetOrder>(HttpStatusCode.InternalServerError, ex.Message);
        }

    }



    public async Task<Response<bool>> DeleteOrder(int id)
    {
        try{
        var order =await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                var result = await _context.SaveChangesAsync();
                var response = result == 1;
                return new Response<bool>(response);
            }
            else 
            {
                return new Response<bool>(HttpStatusCode.BadRequest, "Tакого заказа нет в списке заказов");
            }
        }
        catch (Exception ex)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}

