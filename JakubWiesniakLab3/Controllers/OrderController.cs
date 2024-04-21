using JakubWiesniakLab3.Models;
using JakubWiesniakLab3.Repositories.Orders;
using JakubWiesniakLab3.Repositories.Products;
using JakubWiesniakLab3.Repositories.Users;
using Microsoft.AspNetCore.Mvc;

namespace JakubWiesniakLab3.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IProductRepository _productRepository;

        public OrderController(IOrderRepository orderRepository, IAccountRepository accountRepository,
            IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _accountRepository = accountRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = GetUserId();
            if (userId is null)
            {
                return RedirectToAction("Login", "Account");
            }

            var orders = _orderRepository.GetOrdersByUser(userId.Value);
            return View(orders);
        }

        [HttpGet]
        public IActionResult CreateOrder(int productId)
        {
            return View(new BeginOrderViewModel() { ProductId = productId });
        }

        [HttpPost]
        public IActionResult CreateOrder(
            [Bind("Phone, City, Address, ProductId")]
            BeginOrderViewModel beginOrderViewModel)
        {
            var userId = GetUserId();
            if (userId is null)
            {
                return RedirectToAction("Login", "Account");
            }

            var orderId = _orderRepository.BeginOrder(beginOrderViewModel, userId.Value);
            _orderRepository.AddOrderItem(orderId, beginOrderViewModel.ProductId, 1);

            return RedirectToAction("CurrentOrder");
        }

        [HttpGet]
        public IActionResult CurrentOrder()
        {
            var userId = GetUserId();
            if (userId is null)
            {
                return RedirectToAction("Login", "Account");
            }

            var cart = _orderRepository.GetByStatus(1, userId.Value);
            return View(cart ?? new OrderViewModel());
        }

        [HttpPost]
        public IActionResult AddItem(int itemId, int quantity)
        {
            var product = _productRepository.Get(itemId);
            if (product is null)
            {
                return BadRequest();
            }

            var userId = GetUserId();
            if (userId is null)
            {
                return RedirectToAction("Login", "Account");
            }

            var order = _orderRepository.GetByStatus(1, userId.Value);
            if (order is null)
            {
                return RedirectToAction("CreateOrder", new { productId = itemId });
            }

            _orderRepository.AddOrderItem(order.Id, product.Id, quantity);
            return RedirectToAction("CurrentOrder");
        }

        [HttpPost]
        public IActionResult DeleteItem(int itemId)
        {
            var product = _productRepository.Get(itemId);
            if (product is null)
            {
                return BadRequest();
            }

            var userId = GetUserId();
            if (userId is null)
            {
                return RedirectToAction("Login", "Account");
            }

            var order = _orderRepository.GetByStatus(1, userId.Value);
            if (order is null)
            {
                return RedirectToAction("CreateOrder", new { productId = itemId });
            }

            _orderRepository.RemoveOrderItem(order.Id, product.Id);
            return RedirectToAction("CurrentOrder");
        }

        [HttpPost]
        public IActionResult FinalizeOrder(Guid orderId)
        {
            var userId = GetUserId();
            if (userId is null)
            {
                return RedirectToAction("Login", "Account");
            }

            _orderRepository.FinalizeOrder(orderId);
            return RedirectToAction("Index");
        }

        private Guid? GetUserId()
        {
            var usernameCookieValue = HttpContext.Request.Cookies["username"];
            if (usernameCookieValue is null)
            {
                return null;
            }

            var user = _accountRepository.GetUser(usernameCookieValue);
            return user?.Id;
        }
    }
}