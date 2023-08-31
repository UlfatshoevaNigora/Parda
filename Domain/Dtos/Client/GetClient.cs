using Domain.Dtos.Order;

namespace Domain.Dtos.Client;

public class GetClient:ClientBase
{
    public List<GetOrder>? OrdersOfClient { get; set; }

}
