using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTO.Request.OrderDetail
{
    public class OrderDetailAddDTO
    {
        public int? OrderId { get; set; } = 0;   
        public int? ProductId { get; set; } = 0;
        public decimal? UnitPrice { get; set; } = 0;
        public int? Quantity { get; set; } = 0; 
        public double? Discount { get; set; } = 0;
    }
}
