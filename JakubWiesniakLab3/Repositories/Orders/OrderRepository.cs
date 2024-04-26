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
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
                .Where(o => o.UserId == userId)
                .Select(order => new OrderViewModel
                {
                    Id = order.Id,
                    Phone = order.Phone,
                    City = order.City,
                    Address = order.Address,
                    Date = order.Date,
                    OrderStatus = order.OrderStatus.Name,
                    OrderItems = order.OrderItems
                        .Select(orderItem => new OrderItemViewModel
                        {
                            ProductId = orderItem.ProductId,
                            OrderId = orderItem.OrderId,
                            Quantity = orderItem.Quantity,
                            Price = orderItem.Product.Price,
                            Name = orderItem.Product.Name,
                            Category = orderItem.Product.Category,
                            Description = orderItem.Product.Description,
                            ImageUrl = orderItem.Product.ImageUrl
                        }).ToList()
                }).ToList();
        }

        public OrderViewModel? GetByStatus(int statusId, Guid userId)
        {
            var order = _context.Orders
                .Include(o => o.OrderStatus)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.OrderStatusId == statusId && o.UserId == userId);

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
                OrderItems = order.OrderItems
                    .Select(orderItem => new OrderItemViewModel
                    {
                        ProductId = orderItem.ProductId,
                        OrderId = orderItem.OrderId,
                        Quantity = orderItem.Quantity,
                        Price = orderItem.Product.Price,
                        Name = orderItem.Product.Name,
                        Category = orderItem.Product.Category,
                        Description = orderItem.Product.Description,
                        ImageUrl = orderItem.Product.ImageUrl
                    }).ToList()
            };
        }

        public Guid BeginOrder(BeginOrderViewModel dto, Guid userId)
        {
            var currentOrder = _context.Orders
                .FirstOrDefault(o => o.UserId == userId && o.OrderStatusId == 1);

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
                Date = DateTime.Now
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return order.Id;
        }

        public void AddOrderItem(Guid orderId, int productId, int quantity)
        {
            var itemByProductId = _context.OrderItems
                .FirstOrDefault(oi => oi.OrderId == orderId && oi.ProductId == productId);

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
                    Quantity = quantity
                };
                _context.OrderItems.Add(orderItem);
            }

            _context.SaveChanges();
        }

        public void RemoveOrderItem(Guid orderId, int productId)
        {
            var itemByProductId = _context.OrderItems
                .FirstOrDefault(oi => oi.OrderId == orderId && oi.ProductId == productId);

            if (itemByProductId is not null)
            {
                itemByProductId.Quantity -= 1;
                if (itemByProductId.Quantity == 0)
                {
                    _context.OrderItems.Remove(itemByProductId);
                }
            }

            _context.SaveChanges();
        }

        public void FinalizeOrder(Guid id)
        {
            _context.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.Id == id && o.OrderItems.Any())
                .ExecuteUpdate(x => x.SetProperty(o => o.OrderStatusId, 2));

            _context.SaveChanges();
        }

        public void DeleteOrder(Guid id)
        {
            _context.Orders
                .Where(o => o.Id == id)
                .ExecuteDelete();

            _context.SaveChanges();
        }
    }
}