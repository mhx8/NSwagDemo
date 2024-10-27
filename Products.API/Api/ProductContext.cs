using Microsoft.EntityFrameworkCore;

namespace Api;

public class ProductContext(
    DbContextOptions<ProductContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
}