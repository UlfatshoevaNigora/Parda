using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string SewingType  { get; set; }
        public double CorniceWidth { get; set; }
        public double Height { get; set; }
        public string Description { get; set; }
        public Client Client { get; set; }
        public int PriceOfOneMetr { get; set; }
        [ForeignKey("Client")] 
        public int ClientId { get; set; }
        public double MaterialWidth { get; set; }
        public string ClientName { get; set; }
    }
}