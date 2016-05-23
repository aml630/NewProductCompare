using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ProductCompareDotNet.Models;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Authorization;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace ProductCompareDotNet.Controllers
{
    public class SubCategoriesController : Controller
    {
        private ProductCompareDbContext db = new ProductCompareDbContext();

        public IActionResult Index()
        {

            ViewBag.Products = db.Products.ToList();
            ViewBag.Categories = db.Categories.ToList();
            var SubList = db.SubCategories.ToList();

            return View(SubList);
        }

        public IActionResult SubList(int id)
        {
            ViewBag.SubId = id;
            var SubProducts = db.Products.Where(product => product.SubCategoryId == id);
            return View(SubProducts);
        }

        public IActionResult AddSub(string subCatName, string subCatImg)
        {
            SubCategory subCat = new SubCategory();
            subCat.SubCategoryName = subCatName;
            subCat.SubCategoryImage = subCatImg;

            db.SubCategories.Add(subCat);

            db.SaveChanges();

            ViewBag.Products = db.Products.ToList();
            var SubList = db.SubCategories.ToList();

            return RedirectToAction("Index", "SubCategories", SubList);
        }

    


        [HttpPost]

        public IActionResult AddProductToSublist (string Name,int subId )
        {

            var client = new RestClient("http://api.walmartlabs.com/v1");
            var request = new RestRequest("search", Method.GET);

            request.AddParameter("query", Name);
            request.AddParameter("format", "json");
            request.AddParameter("apikey", "k2waftsef676thk9khfnevds");

            var response = client.Execute(request);

            dynamic stuff = JObject.Parse(response.Content);
            string baseString = stuff.items[0].categoryPath;
            int stop = baseString.IndexOf("/");
            string catName = baseString.Substring(0, stop);

            int index1 = baseString.LastIndexOf('/');
            string subCatName = baseString.Substring(index1 + 1);

            Product product = new Product();
            product.ProductName = stuff.items[0].name;
            product.ProductImg = stuff.items[0].thumbnailImage;
            product.ProductBigImg = stuff.items[0].largeImage;
            product.ProductLink = stuff.items[0].productUrl;
            product.ProductDescription = stuff.items[0].shortDescription;

            product.ProductPrice = stuff.items[0].salePrice;
            product.DateTime = DateTime.Now;

            var test = db.Categories.FirstOrDefault(x => x.CategoryName == catName);
            if (test == null)
            {
                Category category = new Category();
                category.CategoryName = catName;
                db.Categories.Add(category);

                db.SaveChanges();
                product.CategoryId = category.CategoryId;

            }
            else
            {
                product.CategoryId = test.CategoryId;

            }

            var subCatTest = db.SubCategories.FirstOrDefault(x => x.SubCategoryName == subCatName);

         
            product.SubCategoryId = subId;

            

            db.Products.Add(product);

            db.SaveChanges();

            ViewBag.Products = db.Products.ToList();
            var SubList = db.SubCategories.ToList();

            return RedirectToAction("SubList", "SubCategories", new { id = subId });
        }




        public IActionResult CompareProducts(int id)
        {
            var SubProds = db.SubCategories.Where(x => x.SubCategoryId == id).Include(subcat => subcat.Products).ToList();

            return View(SubProds);
        }




    }
}