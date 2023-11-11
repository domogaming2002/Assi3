using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class OrderDetailDAO
    {
        MyDBContext _context;

        public OrderDetailDAO(MyDBContext context)
        {
            _context = context;
        }

        public List<OrderDetail> GetOrderDetails()
        {
            return _context.OrderDetails.Include(o => o.Product).ToList();
        }
        public List<OrderDetail>? GetByIdOrder(int orderId)
        {
            return _context.OrderDetails.Include(o => o.Product).Where(m => m.OrderId == orderId).ToList();
        }

        public OrderDetail? GetByIdAll(int orderId, int productId)
        {
            return _context.OrderDetails.FirstOrDefault(m => m.OrderId == orderId && m.ProductId == productId);
        }

        public Boolean Create(OrderDetail orderDetail)
        {
            try
            {
                _context.OrderDetails.Add(orderDetail);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Boolean Delete(OrderDetail orderDetail)
        {
            try
            {
                _context.OrderDetails.Remove(orderDetail);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Boolean Update(OrderDetail orderDetail)
        {
            try
            {
                OrderDetail? orderDetailUpdate = GetByIdAll(orderDetail.OrderId, orderDetail.ProductId);
                if (orderDetailUpdate != null)
                {
                    orderDetailUpdate.ProductId = orderDetail.ProductId;
                    orderDetailUpdate.UnitPrice = orderDetail.UnitPrice;
                    orderDetailUpdate.Quantity = orderDetail.Quantity;
                    orderDetailUpdate.Discount = orderDetail.Discount;
                    _context.OrderDetails.Update(orderDetailUpdate);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
