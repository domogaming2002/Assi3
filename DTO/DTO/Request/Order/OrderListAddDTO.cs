using DTO.DTO.Request.OrderDetail;

namespace DTO.DTO.Request.Order
{
    public class OrderListAddDTO
    {
        public int? MemberId { get; set; } = 0;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime? RequireDate { get; set; } = DateTime.Now;
        public DateTime? ShippedDate { get; set; } = DateTime.Now;
        public decimal Freight { get; set; } = 0;
        public virtual ICollection<OrderDetailAddDTO> OrderDetails { get; set; }

    }
}
