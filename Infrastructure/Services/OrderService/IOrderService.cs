using Domain.Dtos.Order;
using Domain.Wrapper;

namespace Infrastructure.Services.OrderService;

public interface IOrderService
{ 
    public Task<Response<AddOrder>> AddOrder(AddOrder order);
    public Task<Response<AddOrder>> UpdateOrder(AddOrder order);
    public Task<Response<bool>> DeleteOrder(int id);
    public Task<Response<GetOrder>> GetOrderById(int id);
    public Task<Response<List<GetOrder>>> GetListOfOrders();
}
