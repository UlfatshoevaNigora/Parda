using Microsoft.AspNetCore.Mvc;
using Infrastructure.Services;
using Domain.Dtos.Order;
using Domain.Wrapper;
using Infrastructure.Services.OrderService;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class OrderController
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService OrderService)
    {
        _orderService = OrderService;
    }


    [HttpPost("AddOrder")]
     public async Task<Response<AddOrder>> AddOrder (AddOrder Order)
    {
        return await _orderService.AddOrder(Order);
    }


    [HttpPut("UpdateOrder")]
    public async Task<Response<AddOrder>> UpdateOrder (AddOrder Order)
    {
        return await _orderService.UpdateOrder(Order);
    }

    [HttpGet("GetOrders")]
    public async Task<Response<List<GetOrder>>> GetListOfOrders()
    {
        return await _orderService.GetListOfOrders();
    }


    [HttpGet("GetById")] 
    public async Task<Response<GetOrder>> GetOrderById (int id)
    {
        return await _orderService.GetOrderById(id);
    }


    [HttpDelete("DeleteOrder")]
    public async Task<Response<bool>> DeleteOrder (int id)
    {
        return await _orderService.DeleteOrder(id);
    }

}    
