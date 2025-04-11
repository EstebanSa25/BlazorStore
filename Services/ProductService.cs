using System.Net.Http.Json;
using System.Text.Json;
using blazorappdemo;

namespace blazorappdemo
{
    public class ProductService : IProductService{
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};

        }
        public async Task<List<Product>?> Get (){
            var response=await _httpClient.GetAsync("v1/products");
            return await JsonSerializer.DeserializeAsync<List<Product>>(await response.Content.ReadAsStreamAsync());
        }
        public async Task Add(Product product){
            var response=await _httpClient.PostAsync("v1/products", JsonContent.Create(product));
            var content=await response.Content.ReadAsStringAsync();
            if(!response.IsSuccessStatusCode) throw new ApplicationException(content);
        }
        public async Task Delete (int id){
            var response=await _httpClient.DeleteAsync($"v1/products/{id}");
            var content=await response.Content.ReadAsStringAsync();
            if(!response.IsSuccessStatusCode) throw new ApplicationException(content);
        }
    }
}
public interface IProductService{
    Task<List<Product>?> Get ();
    Task Add(Product product);
    Task Delete (int id);

}