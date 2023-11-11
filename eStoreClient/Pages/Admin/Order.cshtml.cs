using DTO.DTO.Request.Order;
using DTO.DTO.Response.Member;
using DTO.DTO.Response.Order;
using eBookStore.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;

namespace eStoreClient.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class OrderModel : PageModel
    {
        private readonly HttpClient client = null;
        private string apiOrder = "";
        private string apiMember = "";

        public List<OrderResponseDTO>? listOrder { get; set; }
        public List<MemberResponseDTO>? listMember { get; set; }
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
            HttpResponseMessage response = await client.GetAsync(apiOrder);
            string strData = await response.Content.ReadAsStringAsync();
            listOrder = JsonConvert.DeserializeObject<List<OrderResponseDTO>>(strData); 
            response = await client.GetAsync(apiMember);
            strData = await response.Content.ReadAsStringAsync();
            listMember = JsonConvert.DeserializeObject<List<MemberResponseDTO>>(strData);
        }

        public async Task OnGet()
        {
            await GetData();
        }

        public async Task OnPostCreate(int memberId, DateTime orderDate, DateTime requireDate, DateTime shippedDate)
        {
            OrderAddDTO order = new OrderAddDTO()
            {
                MemberId = memberId,
                OrderDate = orderDate,
                RequireDate = requireDate,
                ShippedDate = shippedDate,
                Freight = 0

            };
            var response = client.PostAsJsonAsync(apiOrder, order).Result;
            var check = response.IsSuccessStatusCode;
            await GetData();
        }

        public async Task OnPostDelete(int deleteOrderId)
        {
            var response = client.DeleteAsync(apiOrder + "/" + deleteOrderId).Result;
            var check = response.IsSuccessStatusCode;
            await GetData();

        }

        public async Task OnPostUpdate(int updateMemberId, DateTime updateOrderDate, DateTime updateRequireDate, DateTime updateShippedDate,int updateFreight ,int updateOrderId)
        {
            OrderUpdateDTO order = new OrderUpdateDTO()
            {
                OrderId = updateOrderId,
                MemberId = updateMemberId,
                OrderDate = updateOrderDate,
                RequireDate = updateRequireDate,
                ShippedDate = updateShippedDate,
                Freight = updateFreight
            };
            var response = client.PutAsJsonAsync(apiOrder + "/" + updateOrderId, order).Result;
            var check = response.IsSuccessStatusCode;
            await GetData();

        }
    }
}
