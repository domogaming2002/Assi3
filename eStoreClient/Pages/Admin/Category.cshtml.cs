using DTO.DTO.Request.Category;
using DTO.DTO.Response.Category;
using eBookStore.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;

namespace eStoreClient.Pages
{
    [Authorize(Roles = "Admin")]
    public class CategoryModel : PageModel
    {
        private readonly HttpClient client = null;
        private string apiCategory = "";

        public List<CategoryResponseDTO> listCategory { get; set; }
        public CategoryModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            apiCategory = Config.ApiUrl + "/Category";
        }

        public async Task GetData()
        {
            HttpResponseMessage response = await client.GetAsync(apiCategory);
            string strData = await response.Content.ReadAsStringAsync();
            listCategory = JsonConvert.DeserializeObject<List<CategoryResponseDTO>>(strData);
        }

        public async Task OnGet()
        {
            await GetData();
        }

        public async Task OnPostCreate(string categoryName)
        {
            CategoryAddDTO category = new CategoryAddDTO()
            {
               CategoryName = categoryName
            };
            var response = client.PostAsJsonAsync(apiCategory, category).Result;
            var check = response.IsSuccessStatusCode;
            await GetData();
        }

        public async Task OnPostDelete(int deleteCategoryId)
        {
            var response = client.DeleteAsync(apiCategory + "/" + deleteCategoryId).Result;
            var check = response.IsSuccessStatusCode;
            await GetData();

        }

        public async Task OnPostUpdate(string updateCategoryName,int updateCategoryId)
        {
            CategoryUpdateDTO category = new CategoryUpdateDTO()
            {
                CategoryId = updateCategoryId,
                CategoryName = updateCategoryName
            };
            var response = client.PutAsJsonAsync(apiCategory + "/" + updateCategoryId, category).Result;
            var check = response.IsSuccessStatusCode;
            await GetData();

        }
    }
}
