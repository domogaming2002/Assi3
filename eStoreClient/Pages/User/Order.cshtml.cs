using DTO.DTO.Response.Order;
using eBookStore.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace eStoreClient.Pages.User
{
    public class OrderModel : PageModel
    {
        private readonly HttpClient client = null;
        private string apiOrder = "";
        private string apiMember = "";

        public List<OrderResponseDTO>? listOrder { get; set; } = null;
        public OrderModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            apiOrder = Config.ApiUrl + "/Order";
            apiMember = Config.ApiUrl + "/Member";
        }

        public async Task GetData()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            var id = claimUser.FindFirstValue(ClaimTypes.Sid);
            HttpResponseMessage response = await client.GetAsync(apiOrder + "/OrderByMem/" + Int32.Parse(id));
            string strData = await response.Content.ReadAsStringAsync();
            listOrder = JsonConvert.DeserializeObject<List<OrderResponseDTO>>(strData);
        }

        public async Task OnGet()
        {
            await GetData();
        }
    }
}
