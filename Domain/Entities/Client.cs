namespace Domain.Entities
{
    public class Client
    { 
        public int Id { get; set;}
        public string? Name { get; set; }
        public List<Order> Orders { get; set; }
        
    }
}