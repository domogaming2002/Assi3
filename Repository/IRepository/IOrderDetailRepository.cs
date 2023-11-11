using DTO.DTO.Request.OrderDetail;
using DTO.DTO.Response.OrderDetail;

namespace Repository.IRepository
{
    public interface IOrderDetailRepository
    {
        bool AddOrderDetail(OrderDetailAddDTO orderDetail);
        OrderDetailResponseDTO? GetOrderDetail(int orderId, int productId);
        List<OrderDetailResponseDTO>? GetOrderDetailByOrderId(int orderId);
        bool DeleteOrderDetail(OrderDetailUpdateDTO orderDetail);
        bool UpdateOrderDetail(OrderDetailUpdateDTO orderDetail);
        List<OrderDetailResponseDTO> GetOrderDetails();
    }
}
