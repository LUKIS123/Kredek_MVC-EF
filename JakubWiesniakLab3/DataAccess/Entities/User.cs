namespace JakubWiesniakLab3.DataAccess.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PasswordHash { get; set; }

        public virtual List<Order> Orders { get; set; } = [];
    }
}