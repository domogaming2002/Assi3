using DTO.DTO.Response.Category;

namespace DTO.DTO.Response.Product
{
    public class ProductResponseDTO
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string? ProductName { get; set; }
        public double Weight { get; set; }
        public decimal UnitPrice { get; set; }
        public int? UnitInStock { get; set; }

        public virtual CategoryResponseDTO Category { get; set; } = null!;
    }
}
