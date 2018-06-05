using NewCakeMakerASP.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewCakeMakerASP.NET.Controllers
{
    public class CakeController : Controller
    {
        DBCakeMakerContext db = new DBCakeMakerContext();

        // GET: Cake
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult BanhMoiPartial()
        {
            var listBanhMoi = db.Products.Take(3).ToList();
            return PartialView(listBanhMoi);
        }
        public ActionResult XemChiTiet(int Id=0)
        {
            Product product = db.Products.SingleOrDefault(n => n.ProductId == Id);
            if (product == null)
            {
                //Trả về trang báo lỗi
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                return View(product);   
            }
            
        }
    }
}