using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using NewCakeMakerASP.NET.Models;
namespace NewCakeMakerASP.NET.Controllers
{
    public class HomeController : Controller
    {
        DBCakeMakerContext db = new DBCakeMakerContext();

        // GET: Home
        public ActionResult Index(int? page)
        {

            var products = db.Products.OrderByDescending(n => n.ProductId);
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }
  
    }
}