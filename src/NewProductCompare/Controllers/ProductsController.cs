using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using ProductCompareDotNet.Models;
using Microsoft.AspNet.Authorization;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.Globalization;

namespace ProductCompareDotNet.Controllers
{
    public class ProductsController : Controller
    {
        private ProductCompareDbContext db = new ProductCompareDbContext();

        private readonly ProductCompareDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ProductsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ProductCompareDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public IActionResult Index()
        {
            return View(db.Products.ToList());
        }

      

        public IActionResult ProductList(int id)
        {
            Product findProd = db.Products.FirstOrDefault(x => x.ProductId == id);
           

            var prodList = db.Products.Where(x => x.ProductId == id).ToList();

            ViewBag.ProdId = id;



            return View(prodList);
        }

        public IActionResult CreateRoute()
        {
            return View();
        }

        //[Authorize]
        [HttpPost, ActionName("CreateRoute")]
        public IActionResult CreateProduct(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
   

        public IActionResult AddPost(string title, string intro, string par1, string par2, string par3, string conclusion, int id)
        {

            Product findProd = db.Products.FirstOrDefault(x => x.ProductId == id);

            findProd.title = title;
            findProd.intro = intro;
            findProd.par1 = par1;
            findProd.par2 = par2;
            findProd.par3 = par3;
            findProd.conclusion = conclusion;
            db.SaveChanges();



            return RedirectToAction("ProductList", "Products", new { id = id });
        }


    }
}
