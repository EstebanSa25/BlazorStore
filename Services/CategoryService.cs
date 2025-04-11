using System.Net.Http.Json;
using System.Text.Json;
using blazorappdemo;

namespace blazorappdemo
{
    public class CategoryService : ICategoryService{
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;
        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
        }

         public async Task<List<Category>?> Get (){
            var response=await _httpClient.GetAsync("v1/categories");
            return await JsonSerializer.DeserializeAsync<List<Category>>(await response.Content.ReadAsStreamAsync());
        }
        public async Task Add(Category category){
            var response=await _httpClient.PostAsync("v1/categories", JsonContent.Create(category));
            var content=await response.Content.ReadAsStringAsync();
            if(!response.IsSuccessStatusCode) throw new ApplicationException(content);
        }
        public async Task Delete (string id){
            var response=await _httpClient.DeleteAsync($"v1/categories/{id}");
            var content=await response.Content.ReadAsStringAsync();
            if(!response.IsSuccessStatusCode) throw new ApplicationException(content);
        }
    }
}
public interface ICategoryService{
    Task<List<Category>?> Get ();
}