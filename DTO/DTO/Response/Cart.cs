namespace DTO.DTO.Response
{
    public class Cart
    {
        public int? ProductId { get; set; } = 0;
        public string? ProductName { get; set; }
        public decimal? UnitPrice { get; set; } = 0;
        public int? Quantity { get; set; } = 0;
    }
}
