using AutoMapper.Execution;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDAO
    {
        MyDBContext _context;

        public OrderDAO(MyDBContext context)
        {
            _context = context;
        }

        public Order? GetById(int orderId)
        {
            return _context.Orders.Include(o => o.Member).FirstOrDefault(m => m.OrderId == orderId);
        }

        public Order? GetLastIndex()
        {
            return _context.Orders.Include(o => o.Member).OrderBy(o => o.OrderId).LastOrDefault();
        }

        public List<Order> GetByMemberId(int memId)
        {
            return _context.Orders.Include(o => o.Member).Where(m => m.MemberId == memId).ToList();
        }

        public List<Order> GetOrders()
        {
            return _context.Orders.Include(o => o.Member).ToList();
        }

        public Boolean Create(Order order)
        {
            try
            {
                order.OrderDetails = null;
                _context.Orders.Add(order);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Boolean Delete(Order order)
        {
            try
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Boolean Update(Order order)
        {
            try
            {
                Order? orderUpdate = GetById(order.OrderId);
                if (orderUpdate != null)
                {
                    orderUpdate.MemberId = order.MemberId;
                    orderUpdate.OrderDate = order.OrderDate;
                    orderUpdate.RequireDate = order.RequireDate;
                    orderUpdate.ShippedDate = order.ShippedDate;
                    orderUpdate.Freight = order.Freight;
                    _context.Orders.Update(orderUpdate);
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
