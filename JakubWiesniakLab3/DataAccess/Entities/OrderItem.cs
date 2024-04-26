namespace JakubWiesniakLab3.DataAccess.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }

        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public Guid OrderId { get; set; }
        public virtual Order? Order { get; set; }
    }
}