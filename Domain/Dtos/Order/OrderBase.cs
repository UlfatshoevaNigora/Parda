namespace Domain.Dtos.Order;

public class OrderBase
{
        public int Id { get; set; }
        public string SewingType { get; set; }
        public double CorniceWidth { get; set; }
        public double Height { get; set; }
        public double MaterialWidth { get; set; }
        public string Description { get; set; }
        public int PriceOfOneMetr { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
}
