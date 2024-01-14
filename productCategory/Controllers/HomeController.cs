using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using productCategory.Models;

namespace productCategory.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private MyContext _context;

    public HomeController(ILogger<HomeController> logger ,MyContext context)
    {
        _logger = logger;
        _context= context;
    }
    public IActionResult Index(){
        ViewBag.Products= _context.Products.Include(p=> p.ProductAssociation).ToList();
        return View();
    }
    [HttpPost("CreateProduct")]
    public IActionResult CreateProduct(Product product){
        if (ModelState.IsValid)
        {
            _context.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        ViewBag.Products= _context.Products.Include(p=> p.ProductAssociation).ToList();
        return View("Index");
    }
    [HttpGet("Categories")]
    public IActionResult Categories(){
        ViewBag.Categorie= _context.Categories.Include(c=> c.CategoryAssociation).ToList();
        return View();
    }
    [HttpPost("CreateCategorie")]
    public IActionResult CreateCategorie(Categorie categorie){
            if (ModelState.IsValid) 
            {
                _context.Add(categorie);
                _context.SaveChanges();
                return RedirectToAction("Categories");
            }
            ViewBag.Categorie= _context.Categories.Include(c=> c.CategoryAssociation).ToList();
            return View("Categories");
    }

    [HttpGet("Products/{id}")]
    public IActionResult Products(int id){
        Product products= _context.Products.Include(p=> p.ProductAssociation).ThenInclude(c=> c.Categorie).FirstOrDefault(p=> p.ProductId == id);
        ViewBag.ExistingCategories = products?.ProductAssociation.Select(c=> c.Categorie).ToList()?? new List<Categorie>();
    
        List<Categorie> categories =_context.Categories.ToList();
        ViewBag.categoriesToInclude= categories;
        
        return View("Products",products);
    }

    [HttpPost("addCategory/{itemId}")]
    public IActionResult addCategory(int itemId, int CategorieId){
        if (ModelState.IsValid){
            if (!_context.Associations.Any(a => a.ProductId == itemId && a.CategorieId == CategorieId))
            {
                Association newAssociation= new Association{
                    ProductId=itemId,
                    CategorieId=CategorieId
                };
                _context.Add(newAssociation);
                _context.SaveChanges();
            }
            
        }
        return RedirectToAction( "Products" , new {id =itemId});
    }

    [HttpGet("Cat/{id}")]
    public IActionResult Cat(int id){
        Categorie categorie =_context.Categories.Include(c => c.CategoryAssociation).ThenInclude(p=> p.Products).FirstOrDefault(c=>c.CategorieId == id);
        ViewBag.ExcistingProduct =categorie?.CategoryAssociation.Select(c=>c.Products).ToList()??new List<Product>();

        List<Product> products= _context.Products.ToList();
        ViewBag.ProductsToInclude= products;
        
        return View("Cat",categorie);
    }

    [HttpPost("addProduct/{itemId}")]
    public IActionResult addProduct(int ProductId, int itemId){
        if (ModelState.IsValid)
        Console.WriteLine($"Attempting to add product {ProductId} to category {itemId}");
        {
            if (!_context.Associations.Any(a => a.CategorieId == itemId && a.ProductId == ProductId))
                {
                Association newAssociation = new Association
                {
                    ProductId = ProductId,
                    CategorieId = itemId
                };
                _context.Add(newAssociation);
            _context.SaveChanges();
            }
        Console.WriteLine("Product added to category successfully");
        return RedirectToAction("Cat" , new{id=itemId});
        }

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
