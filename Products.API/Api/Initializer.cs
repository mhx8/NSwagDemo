using Microsoft.EntityFrameworkCore;

namespace Api;

public class Initializer(
    IServiceProvider serviceProvider) : BackgroundService
{
    protected override Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        ProductContext context = scope.ServiceProvider.GetRequiredService<ProductContext>();
        context.Database.EnsureCreated();
        
        context.Database.ExecuteSqlRaw("DELETE FROM [Products]");
        context.Database.ExecuteSqlRaw("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Products';");
        
        context.Products.AddRange(
            new Product { Name = "Product A", Price = 10.00m, Quantity = 100 },
            new Product { Name = "Product B", Price = 20.00m, Quantity = 50 },
            new Product { Name = "Product C", Price = 15.00m, Quantity = 75 }
        );
        context.SaveChanges();

        return Task.CompletedTask;
    }
}