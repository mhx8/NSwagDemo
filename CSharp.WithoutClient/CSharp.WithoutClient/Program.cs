using System.Net.Http.Json;
using System.Text.Json;
using Api;

HttpClient client = new();
client.BaseAddress = new Uri("http://localhost:5227/");

IEnumerable<Product>? products =
    await client.GetFromJsonAsync<IEnumerable<Product>>("/products");

Console.WriteLine(JsonSerializer.Serialize(products));