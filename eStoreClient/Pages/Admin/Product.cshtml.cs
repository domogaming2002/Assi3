using DTO.DTO.Request.Product;
using DTO.DTO.Response.Category;
using DTO.DTO.Response.Product;
using eBookStore.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace eStoreClient.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class ProductModel : PageModel
    {
        private readonly HttpClient client = null;
        private string apiProduct = "";
        private string apiCategory = "";

        public List<ProductResponseDTO>? listProduct { get; set; }
        public List<CategoryResponseDTO>? listCategory { get; set; }
        public ProductModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            apiProduct = Config.ApiUrl + "/Product";
            apiCategory = Config.ApiUrl + "/Category";
        }

        public async Task GetData()
        {
            var jwtToken = Request.Cookies["jwt"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            HttpResponseMessage response = await client.GetAsync(apiProduct);
            string strData = await response.Content.ReadAsStringAsync();
            listProduct = JsonConvert.DeserializeObject<List<ProductResponseDTO>>(strData);

            response = await client.GetAsync(apiCategory);
            strData = await response.Content.ReadAsStringAsync();
            listCategory = JsonConvert.DeserializeObject<List<CategoryResponseDTO>>(strData);
        }

        public async Task OnGet()
        {
            await GetData();
        }

        public async Task OnPostCreate(int categoryId, string productName, double weight, decimal unitPrice, int unitInStock)
        {
            var jwtToken = Request.Cookies["jwt"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            ProductAddDTO product = new ProductAddDTO()
            {
                CategoryId = categoryId,
                ProductName = productName,
                Weight = weight,
                UnitPrice = unitPrice,
                UnitInStock = unitInStock
            };
            var response = client.PostAsJsonAsync(apiProduct, product).Result;
            var check = response.IsSuccessStatusCode;
            await GetData();
        }

        public async Task OnPostDelete(int deleteProductId)
        {
            var jwtToken = Request.Cookies["jwt"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            var response = client.DeleteAsync(apiProduct + "/" + deleteProductId).Result;
            var check = response.IsSuccessStatusCode;
            await GetData();

        }

        public async Task OnPostUpdate(int updateCategoryId, string updateProductName, double updateWeight, decimal updateUnitPrice, int updateUnitInStock, int updateProductId)
        {
            var jwtToken = Request.Cookies["jwt"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            ProductUpdateDTO product = new ProductUpdateDTO()
            {
                ProductId = updateProductId,
                CategoryId = updateCategoryId,
                ProductName = updateProductName,
                Weight = updateWeight,
                UnitPrice = updateUnitPrice,
                UnitInStock = updateUnitInStock
            };
            var response = client.PutAsJsonAsync(apiProduct + "/" + updateProductId, product).Result;
            var check = response.IsSuccessStatusCode;
            await GetData();

        }
    }
}
