using BusinessObject.Models;
using DTO.DTO.Request.Order;
using DTO.DTO.Response.Order;

namespace Repository.IRepository
{
    public interface IOrderRepository
    {
        bool AddOrder(OrderAddDTO o);
        OrderResponseDTO? GetOrderById(int id);
        OrderResponseDTO? GetLastIndex();
        bool DeleteOrder(OrderUpdateDTO o);
        bool UpdateOrder(OrderUpdateDTO o);
        bool AddOrder(OrderListAddDTO o);
        List<OrderResponseDTO> GetOrders();
        List<OrderResponseDTO> GetOrderByMemId(int memId);

    }
}
