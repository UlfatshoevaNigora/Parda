namespace Domain.Dtos.Order;

public class GetOrder:OrderBase
{
      public GetOrder()
      {
        PriceOfOrder = (int)((double)PriceOfOneMetr * MaterialWidth);
      }
      public int PriceOfOrder { get; set; }
      

}
