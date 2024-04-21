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
            var usernameCookieValue = HttpContext.Request.Cookies["username"];
            if (usernameCookieValue is null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _accountRepository.GetUser(usernameCookieValue);
            if (user is null)
            {
                return RedirectToAction("Login", "Account");
            }

            var orders = _orderRepository.GetOrdersByUser(user.Id);
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
            var usernameCookieValue = HttpContext.Request.Cookies["username"];
            if (usernameCookieValue is null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _accountRepository.GetUser(usernameCookieValue);
            if (user is null)
            {
                return RedirectToAction("Login", "Account");
            }

            var orderId = _orderRepository.BeginOrder(beginOrderViewModel, user.Id);
            _orderRepository.AddOrderItem(orderId, beginOrderViewModel.ProductId, 1);

            return RedirectToAction("CurrentOrder");
        }

        [HttpGet]
        public IActionResult CurrentOrder()
        {
            var usernameCookieValue = HttpContext.Request.Cookies["username"];
            if (usernameCookieValue is null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _accountRepository.GetUser(usernameCookieValue);
            if (user is null)
            {
                return RedirectToAction("Login", "Account");
            }

            var cart = _orderRepository.GetByStatus(1, user.Id);
            return View(cart);
        }

        [HttpPost]
        public IActionResult AddItem(int itemId)
        {
            var product = _productRepository.Get(itemId);
            if (product is null)
            {
                return BadRequest();
            }

            var usernameCookieValue = HttpContext.Request.Cookies["username"];
            if (usernameCookieValue is null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _accountRepository.GetUser(usernameCookieValue);
            if (user is null)
            {
                return RedirectToAction("Login", "Account");
            }

            var order = _orderRepository.GetByStatus(1, user.Id);
            if (order is null)
            {
                return RedirectToAction("CreateOrder", new { productId = itemId });
            }

            _orderRepository.AddOrderItem(order.Id, product.Id, 1);
            return RedirectToAction("CurrentOrder");
        }

        [HttpPost]
        public IActionResult DeleteItem(Guid orderId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult FinalizeOrder(Guid orderId)
        {
            throw new NotImplementedException();
        }

        private Guid GetUserId()
        {
            var usernameCookieValue = HttpContext.Request.Cookies["username"];
            if (usernameCookieValue is null)
            {
                return Guid.Empty;
            }

            var user = _accountRepository.GetUser(usernameCookieValue);
            if (user is null)
            {
                return Guid.Empty;
            }

            return user.Id;
        }
    }
}