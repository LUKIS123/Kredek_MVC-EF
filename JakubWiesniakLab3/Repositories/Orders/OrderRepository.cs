using JakubWiesniakLab3.Models;

namespace JakubWiesniakLab3.Repositories.Orders
{
    public class OrderRepository : IOrderRepository
    {
        public IEnumerable<OrderViewModel> GetOrdersByUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public OrderViewModel? GetByStatus(int statusId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Guid BeginOrder(BeginOrderViewModel dto, Guid userId)
        {
            throw new NotImplementedException();
        }

        public void AddOrderItem(Guid orderId, int productId, int quantity)
        {
            throw new NotImplementedException();
        }

        public void RemoveOrderItem(Guid orderId, int productId)
        {
            throw new NotImplementedException();
        }

        public void FinalizeOrder(Guid id)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrder(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}