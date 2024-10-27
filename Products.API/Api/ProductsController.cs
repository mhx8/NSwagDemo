using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSwag.Annotations;

namespace Api;

[ApiController]
public class ProductsController(
    ProductContext context) : ControllerBase
{
    [HttpDelete("products/{id}")]
    [OpenApiOperation("DeleteProductById")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        Product? product = await context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        context.Products.Remove(product);
        await context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("products/{id}")]
    [OpenApiOperation("UpdateProductById")]
    public async Task<IActionResult> UpdateProduct(int id, Product updatedProduct)
    {
        Product? product = await context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        product.Name = updatedProduct.Name;
        product.Price = updatedProduct.Price;
        product.Quantity = updatedProduct.Quantity;

        context.Entry(product).State = EntityState.Modified;
        await context.SaveChangesAsync();

        return Ok();
    }
}