using DTO.DTO.Response.Product;

namespace DTO.DTO.Response.OrderDetail
{
    public class OrderDetailResponseDTO
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double? Discount { get; set; }

        public virtual ProductResponseDTO Product { get; set; } = null!;
    }
}
