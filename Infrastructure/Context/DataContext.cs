using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {}
    public DbSet<Order> Orders { get; set; }
    public DbSet<Client> Clients { get; set; }
}
