namespace JakubWiesniakLab3.Models
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }

        public string OrderStatus { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; } = [];
    }
}