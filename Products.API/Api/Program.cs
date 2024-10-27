using Api;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProductContext>(options =>
    options.UseSqlite("Data Source=products.db"));

builder.Services.AddHostedService<Initializer>();
builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        corsPolicyBuilder =>
        {
            corsPolicyBuilder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

WebApplication app = builder.Build();
app.UseCors("AllowSpecificOrigins");

app.UseHttpsRedirection();

app.MapGet("/products", async (
            ProductContext db) =>
        await db.Products.ToListAsync())
    .WithName("GetAllProducts")
    .WithOpenApi();

app.MapGet("/products/{id}", async (
        ProductContext db,
        int id) =>
    {
        Product? product = await db.Products.FindAsync(id);
        return product != null ? Results.Ok(product) : Results.NotFound("Not found");
    })
    .Produces<Product>(200)
    .Produces<string>(404)
    .WithName("GetProduct")
    .WithOpenApi();

app.MapControllers();

app.Run();