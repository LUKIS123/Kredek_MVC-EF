using JakubWiesniakLab3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using JakubWiesniakLab3.Repositories.Products;

namespace JakubWiesniakLab3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productrepository;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productrepository = productRepository;
        }

        public IActionResult Index()
        {
            var usernameCookieValue = HttpContext.Request.Cookies["username"];
            ViewData["Username"] = usernameCookieValue;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult AllProducts()
        {
            return View(_productrepository.GetAll());
        }

        public IActionResult ProductDetails(int id)
        {
            return View(_productrepository.Get(id));
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(
            [Bind("ProductName,ProductCategrory,Price,Description,ImageUrl")] ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                _productrepository.Add(product);
                return RedirectToAction("AllProducts", _productrepository.GetAll());
            }

            return View(product);
        }

        public IActionResult EditProduct(int id)
        {
            return View(_productrepository.Get(id));
        }

        [HttpPost]
        public IActionResult EditProduct(
            [Bind("Id,ProductName,ProductCategrory,Price,Description,ImageUrl")] ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                _productrepository.Update(product);
                return RedirectToAction("AllProducts", _productrepository.GetAll());
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            _productrepository.Delete(id);
            return RedirectToAction("AllProducts", _productrepository.GetAll());
        }
    }
}