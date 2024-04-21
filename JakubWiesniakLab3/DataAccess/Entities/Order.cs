namespace JakubWiesniakLab3.DataAccess.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public required string Phone { get; set; }
        public required string City { get; set; }
        public required string Address { get; set; }
        public DateTime Date { get; set; }

        public Guid UserId { get; set; }
        public virtual User? User { get; set; }

        public int OrderStatusId { get; set; }
        public virtual OrderStatus? OrderStatus { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; } = [];
    }
}