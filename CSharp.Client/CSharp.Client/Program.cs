using MyNamespace;
using Newtonsoft.Json;

ProductsApiClient apiClient = new("http://localhost:5227/");
IEnumerable<Product>? products = await apiClient.GetAllProductsAsync();

Console.WriteLine(JsonConvert.SerializeObject(products));