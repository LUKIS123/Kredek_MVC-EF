namespace JakubWiesniakLab3.Models
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public required string Phone { get; set; }
        public required string City { get; set; }
        public required string Address { get; set; }
        public DateTime Date { get; set; }

        public required string OrderStatus { get; set; }
        public List<OrderItemViewModel>? OrderItems { get; set; }
    }
}