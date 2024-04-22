namespace JakubWiesniakLab3.DataAccess.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required string UserName { get; set; }
    }
}