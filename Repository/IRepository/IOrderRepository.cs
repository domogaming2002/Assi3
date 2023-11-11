using DTO.DTO.Request.Order;
using DTO.DTO.Response.Order;

namespace Repository.IRepository
{
    public interface IOrderRepository
    {
        bool AddOrder(OrderAddDTO o);
        OrderResponseDTO? GetOrderById(int id);
        bool DeleteOrder(OrderUpdateDTO o);
        bool UpdateOrder(OrderUpdateDTO o);
        List<OrderResponseDTO> GetOrders();
        List<OrderResponseDTO> GetOrderByMemId(int memId);

    }
}
