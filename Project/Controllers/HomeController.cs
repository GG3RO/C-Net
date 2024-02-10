using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Controllers;

public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        int? userId = context.HttpContext.Session.GetInt32("UserId");
        if(userId == null)
        {
            context.Result = new RedirectToActionResult("Auth", "Home", null);
        }
    }
}


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public HomeController(ILogger<HomeController> logger , MyContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    [SessionCheck]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("Auth")]
    public IActionResult Auth(){
        return View("Auth");

    }
    
    
    [HttpGet("Auth1")]
    public IActionResult Auth1(){
        return View("Auth1");

    }
    [HttpPost("Register")]
    public IActionResult Register(User useriNgaForma)
    {
        if (ModelState.IsValid)
        {
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            useriNgaForma.Password = Hasher.HashPassword(useriNgaForma, useriNgaForma.Password);
            _context.Add(useriNgaForma);
            _context.SaveChanges();
            return RedirectToAction("Auth1");
        }
        return View("Auth");

    }

    [HttpPost("Login")]
    public IActionResult Login(Login useriNgaForma)
    {

        if (ModelState.IsValid)
        {

            User useriNgaDB = _context.Users.FirstOrDefault(e => e.Email == useriNgaForma.Email);
            if (useriNgaDB == null)
            {
                ModelState.AddModelError("LoginEmail", "Invalid Email");
                return View("Auth1");
            }

            PasswordHasher<Login> hasher = new PasswordHasher<Login>();
            var result = hasher.VerifyHashedPassword(useriNgaForma, useriNgaDB.Password, useriNgaForma.Password);
            if (result == 0)
            {
                ModelState.AddModelError("LoginPassword", "Invalid Password");
                return View("Auth1");
            }
            HttpContext.Session.SetInt32("UserId", useriNgaDB.UserId);
            return RedirectToAction("Index");

        }
        return View("Auth1");

    }
        public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Auth1");
    }


[SessionCheck]
[HttpGet("AccountInfo")]
public IActionResult AccountInfo()
{
    int? userId = HttpContext.Session.GetInt32("UserId");

    if (userId != null)
    {
        
        var userWithOrders = _context.Users.Include(u => u.Orders.OrderByDescending(o => o.OrderDate)).FirstOrDefault(u => u.UserId == userId);

        if (userWithOrders != null)
        {
            int orderCount = _context.Orders.Count(o => o.UserId == userId);
            ViewBag.OrderCount = orderCount;
            return View(userWithOrders);
        }
    }
    
    return RedirectToAction("Index");
}

[SessionCheck]
[HttpPost]
    public IActionResult Update(User updateUser)
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        var user = _context.Users.FirstOrDefault(u => u.UserId == userId);

        if (string.IsNullOrWhiteSpace(updateUser.FirstName) || string.IsNullOrWhiteSpace(updateUser.LastName) || !IsValidEmail(updateUser.Email) || string.IsNullOrWhiteSpace(updateUser.Address) || string.IsNullOrWhiteSpace(updateUser.City))
        {
            ModelState.AddModelError("CustomError", "Invalid input for name, last name, or email.");
            return View("EditProfile", user);
        }

        user.FirstName = updateUser.FirstName;
        user.LastName = updateUser.LastName;
        user.Email = updateUser.Email;
        user.Address = updateUser.Address;
        user.City = updateUser.City;
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }


[SessionCheck]
[HttpGet("Order")]
public IActionResult Order(){
        return View();
    }


[SessionCheck]
[HttpPost("CreateOrder")]
public IActionResult CreateOrder(Order order)
{

    int toppingsPrice = (order.TOPPINGS?.Split(',').Length ?? 0) * 1; 
    int sizePrice = order.Size switch
    {
        "small" => 10,   
        "medium" => 15,   
        "large" => 20,   
        "extra_large" => 25, 
        _ => 0
    };

    int totalPrice = sizePrice + toppingsPrice;

    order.TotalPrice = totalPrice;


    if (ModelState.IsValid)
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId != null)
        {
            order.UserId = userId;
            order.OrderDate=DateTime.Now;

            _context.Orders.Add(order);
            _context.SaveChanges();

            return RedirectToAction("Purchase", new { orderId = order.OrderId });
        }
    }

    return View("Order", order);
}

[SessionCheck]
[HttpPost("ReOrderFavorite")]
public IActionResult ReOrderFavorite()
{
    int? userId = HttpContext.Session.GetInt32("UserId");
    var favoriteOrder = _context.Orders.FirstOrDefault(o => o.UserId == userId && o.IsFavorite);

    

    if (favoriteOrder != null)
    {
        Order newOrder = new Order
        {
            OrderDate = DateTime.Now,
            Method = favoriteOrder.Method,
            Size = favoriteOrder.Size,
            Crust = favoriteOrder.Crust,
            QTY = favoriteOrder.QTY,
            TOPPINGS = favoriteOrder.TOPPINGS,
            TotalPrice = favoriteOrder.TotalPrice,
            UserId = userId,
            FavouritePizzaId = null,
            IsFavorite = false
        };

        _context.Orders.Add(newOrder);
        _context.SaveChanges();

        return RedirectToAction("AccountInfo");
    }

    return NotFound();
}


[SessionCheck]
[HttpPost("SurprisePizza")]
public IActionResult SurprisePizza()
{   
    Dictionary<string, int> sizePrices = new Dictionary<string, int>
    {
        { "small", 10 },   // $10
        { "medium", 15 },  // $15
        { "large", 20 }    // $20
    };

    string[] availableMethods = { "carry_out", "delivery" };
    string[] availableSizes = { "small", "medium", "large" };
    string[] availableCrusts = { "hand_tossed", "thin_crust", "deep_dish" };

    Random random = new Random();
    string selectedMethod = availableMethods[random.Next(availableMethods.Length)];
    string selectedSize = availableSizes[random.Next(availableSizes.Length)];
    string selectedCrust = availableCrusts[random.Next(availableCrusts.Length)];

    string[] availableToppings = { "pepperoni", "sausage", "mushrooms", "onions", "olives", "tomatoes", "anchovies", "bacon", "ham", "spinach", "pineapple", "extra_cheese" };

    int numberOfToppings = random.Next(1, 6); 
    string[] selectedToppings = availableToppings.OrderBy(t => random.Next()).Take(numberOfToppings).ToArray();

    int toppingsPrice = selectedToppings.Length * 1; // Assuming each topping costs $1
    int sizePrice = sizePrices[selectedSize]; // Get the price of the selected size

    int totalPrice = sizePrice + toppingsPrice; // This line calculates totalPrice

    var userId = HttpContext.Session.GetInt32("UserId");

    Order randomPizzaOrder = new Order
    {
        OrderDate = DateTime.Now,
        Method = selectedMethod, 
        Size = selectedSize, 
        Crust = selectedCrust, 
        QTY = 1,
        TOPPINGS = string.Join(",", selectedToppings), 
        TotalPrice = totalPrice, 
        UserId = userId, 
        FavouritePizzaId = null,
        IsFavorite = false
    };

    _context.Orders.Add(randomPizzaOrder);
    _context.SaveChanges();

    return RedirectToAction("AccountInfo");
}
[SessionCheck]
[HttpPost]
    public IActionResult UpdateFavorite(int orderId, bool isFavorite)
    {
        var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order != null)
        {
            order.IsFavorite = isFavorite;
            _context.SaveChanges();
            return Ok(); 
        }
        return NotFound(); 
    }

    [SessionCheck]
    [HttpGet("Purchase")]
public IActionResult Purchase(int orderId)
{
    var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);


    if (order == null)
    {
        return NotFound();
    }


    return View(order); 
}


[HttpPost]
[SessionCheck]
public IActionResult DeleteOrder(int orderId)
{
    var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
    if (order != null)
    {
        _context.Orders.Remove(order);
        _context.SaveChanges();
    }
    
    return RedirectToAction("AccountInfo");
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
}
