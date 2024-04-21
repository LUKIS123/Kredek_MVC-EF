using JakubWiesniakLab3.DataAccess;
using JakubWiesniakLab3.DataAccess.Entities;
using JakubWiesniakLab3.Models;
using Microsoft.EntityFrameworkCore;

namespace JakubWiesniakLab3.Repositories.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<OrderViewModel> GetOrdersByUser(Guid userId)
        {
            return _context.Orders
                .Include(x => x.OrderStatus)
                .Include(x => x.OrderItems)!
                .ThenInclude(x => x.Product)
                .Where(o => o.UserId == userId && o.OrderStatusId == 2)
                .Select(o => new OrderViewModel
                {
                    Id = o.Id,
                    Phone = o.Phone,
                    City = o.City,
                    Address = o.Address,
                    Date = o.Date,
                    OrderStatus = o.OrderStatus.Name,
                    OrderItems = o.OrderItems.Select(oi => new OrderItemViewModel
                    {
                        Id = oi.Id,
                        OrderId = oi.OrderId,
                        Quantity = oi.Quantity,
                        ProductId = oi.ProductId,
                        Name = oi.Product.Name,
                        Category = oi.Product.Category,
                        Price = oi.Product.Price * oi.Quantity,
                        Description = oi.Product.Description,
                        ImageUrl = oi.Product.ImageUrl
                    }).ToList()
                })
                .ToList();
        }

        public OrderViewModel? GetByStatus(int statusId, Guid userId)
        {
            var order = _context.Orders
                .Include(x => x.OrderStatus)
                .Include(x => x.OrderItems)!
                .ThenInclude(x => x.Product)
                .FirstOrDefault(o => (o.UserId == userId && o.OrderStatusId == statusId));

            if (order is null)
            {
                return null;
            }

            return new OrderViewModel
            {
                Id = order.Id,
                Phone = order.Phone,
                City = order.City,
                Address = order.Address,
                Date = order.Date,
                OrderStatus = order.OrderStatus.Name,
                OrderItems = order.OrderItems.Select(oi => new OrderItemViewModel
                {
                    Id = oi.Id,
                    OrderId = oi.OrderId,
                    Quantity = oi.Quantity,
                    ProductId = oi.ProductId,
                    Name = oi.Product.Name,
                    Category = oi.Product.Category,
                    Price = oi.Product.Price * oi.Quantity,
                    Description = oi.Product.Description,
                    ImageUrl = oi.Product.ImageUrl
                }).ToList()
            };
        }

        public Guid BeginOrder(BeginOrderViewModel dto, Guid userId)
        {
            var currentOrder = _context.Orders
                .FirstOrDefault(x => (x.OrderStatusId == 1 && x.UserId == userId));
            if (currentOrder is not null)
            {
                return currentOrder.Id;
            }

            var order = new Order
            {
                Phone = dto.Phone,
                City = dto.City,
                Address = dto.Address,
                UserId = userId,
                OrderStatusId = 1,
                Date = DateTime.Now,
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return order.Id;
        }

        public void AddOrderItem(Guid orderId, int productId, int quantity)
        {
            var itemByProductId = _context.OrderItems
                .FirstOrDefault(x => x.OrderId == orderId && x.ProductId == productId);
            if (itemByProductId is not null)
            {
                itemByProductId.Quantity = quantity;
            }
            else
            {
                var orderItem = new OrderItem
                {
                    OrderId = orderId,
                    ProductId = productId,
                    Quantity = quantity,
                };

                _context.OrderItems.Add(orderItem);
            }

            _context.SaveChanges();
        }

        public void RemoveOrderItem(Guid orderId, int productId)
        {
            var itemByProductId = _context.OrderItems
                .FirstOrDefault(x => x.OrderId == orderId && x.ProductId == productId);
            if (itemByProductId is not null)
            {
                itemByProductId.Quantity -= 1;
                if (itemByProductId.Quantity == 0)
                {
                    _context.OrderItems.Remove(itemByProductId);
                }

                _context.SaveChanges();
            }
        }

        public void FinalizeOrder(Guid id)
        {
            _context.Orders
                .Include(x => x.OrderItems)
                .Where(x => x.Id == id && x.OrderItems.Any())
                .ExecuteUpdate(x => x.SetProperty(o => o.OrderStatusId, 2));

            _context.SaveChanges();
        }

        public void DeleteOrder(Guid id)
        {
            _context.Orders
                .Where(x => x.Id == id)
                .ExecuteDelete();

            _context.SaveChanges();
        }
    }
}