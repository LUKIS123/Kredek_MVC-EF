using JakubWiesniakLab3.Models;

namespace JakubWiesniakLab3.Repositories.Orders
{
    public interface IOrderRepository
    {
        IEnumerable<OrderViewModel> GetOrdersByUser(Guid userId);
        OrderViewModel? GetByStatus(int statusId, Guid userId);
        Guid BeginOrder(BeginOrderViewModel dto, Guid userId);
        void AddOrderItem(Guid orderId, int productId, int quantity);
        void RemoveOrderItem(Guid orderId, int productId);
        void FinalizeOrder(Guid id);
        void DeleteOrder(Guid id);
    }
}