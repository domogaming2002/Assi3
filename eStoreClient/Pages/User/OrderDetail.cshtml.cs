using DTO.DTO.Response.OrderDetail;
using DTO.DTO.Response.Product;
using eBookStore.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace eStoreClient.Pages.User
{
    public class OrderDetailModel : PageModel
    {
        private readonly HttpClient client = null;
        private string apiOrderDetail = "";
        private string apiProduct = "";

        public List<OrderDetailResponseDTO>? listOrderDetail { get; set; }
        public List<ProductResponseDTO>? listProduct { get; set; }
        public int OrderId { get; set; }
        public string Notice { get; set; }
        public OrderDetailModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            apiOrderDetail = Config.ApiUrl + "/OrderDetail";
            apiProduct = Config.ApiUrl + "/Product";
        }

        public async Task GetData(int orderId)
        {
            HttpResponseMessage response = await client.GetAsync(apiOrderDetail + "/orderDetailByOrderId?orderId=" + orderId);
            string strData = await response.Content.ReadAsStringAsync();
            listOrderDetail = JsonConvert.DeserializeObject<List<OrderDetailResponseDTO>>(strData);
            response = await client.GetAsync(apiProduct);
            strData = await response.Content.ReadAsStringAsync();
            listProduct = JsonConvert.DeserializeObject<List<ProductResponseDTO>>(strData);
            OrderId = orderId;
        }

        public async Task OnGet(int orderId)
        {
            await GetData(orderId);
        }
    }
}
