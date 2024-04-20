namespace JakubWiesniakLab3.Models
{
    public class OrderItemViewModel
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }

        public required string Name { get; set; }
        public required string Category { get; set; }
        public double Price { get; set; }
        public required string Description { get; set; }
        public required string ImageUrl { get; set; }
    }
}