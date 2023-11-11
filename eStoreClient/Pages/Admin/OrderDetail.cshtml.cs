using DTO.DTO.Request.OrderDetail;
using DTO.DTO.Response.OrderDetail;
using DTO.DTO.Response.Product;
using eBookStore.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;

namespace eStoreClient.Pages.Admin
{
    [Authorize(Roles = "Admin")]
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

        public async Task OnPostCreate(int orderId, int productId, decimal unitPrice, int quantity, double discount)
        {
            var jwtToken = Request.Cookies["jwt"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            OrderDetailAddDTO orderDetail = new OrderDetailAddDTO()
            {
                OrderId = orderId,
                ProductId = productId,
                UnitPrice = unitPrice,
                Quantity = quantity,
                Discount = discount

            };
            HttpResponseMessage response = await client.GetAsync(apiProduct + "/" + productId);
            string strData = await response.Content.ReadAsStringAsync();
            ProductResponseDTO productResponseDTO = JsonConvert.DeserializeObject<ProductResponseDTO>(strData);
            if (productResponseDTO.UnitInStock >= quantity)
            {
                response = client.PostAsJsonAsync(apiOrderDetail + "/" + orderId, orderDetail).Result;
                var check = response.IsSuccessStatusCode;
                productResponseDTO.UnitInStock = productResponseDTO.UnitInStock - quantity;
                response = client.PutAsJsonAsync(apiProduct + "/" + productId, productResponseDTO).Result;
            }
            else
            {
                Notice = "Quantity > Unit in stock product";
            }
            await GetData(orderId);
        }

        public async Task OnPostDelete(int deleteOrderId, int deleteProductId, int deleteQuantity)
        {
            var jwtToken = Request.Cookies["jwt"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            HttpResponseMessage response = client.DeleteAsync(apiOrderDetail + "/deleteOrderDetail?orderId=" + deleteOrderId + "&productId=" + deleteProductId).Result;
            var check = response.IsSuccessStatusCode;

            response = await client.GetAsync(apiProduct + "/" + deleteProductId);
            string strData = await response.Content.ReadAsStringAsync();
            ProductResponseDTO productResponseDTO = JsonConvert.DeserializeObject<ProductResponseDTO>(strData);
            productResponseDTO.UnitInStock = productResponseDTO.UnitInStock + deleteQuantity;
            response = client.PutAsJsonAsync(apiProduct + "/" + deleteProductId, productResponseDTO).Result;
            await GetData(deleteOrderId);

        }

        //public async Task OnPostUpdate(decimal updateUnitPrice, int updateQuantity, double updateDiscount, int updateOrderId, int updateProductId)
        //{
        //    OrderDetailUpdateDTO order = new OrderDetailUpdateDTO()
        //    {
        //        OrderId = updateOrderId,
        //        ProductId = updateProductId,
        //        OrderDate = updateOrderDate,
        //        RequireDate = updateRequireDate,
        //        ShippedDate = updateShippedDate,
        //        Freight = updateFreight
        //    };
        //    var response = client.PutAsJsonAsync(apiOrder + "/" + updateOrderId, order).Result;
        //    var check = response.IsSuccessStatusCode;
        //    await GetData();

        //}
    }
}
