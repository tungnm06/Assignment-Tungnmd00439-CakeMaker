using NewCakeMakerASP.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewCakeMakerASP.NET.Areas.Admin.Controllers
{
    public class IndexController : Controller
    {
        DBCakeMakerContext db = new DBCakeMakerContext();
        // GET: Admin/Index
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }
    }
}