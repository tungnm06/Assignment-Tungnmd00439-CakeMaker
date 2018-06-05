using NewCakeMakerASP.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
namespace NewCakeMakerASP.NET.Controllers
{

    public class ChuDeController : Controller
    {
        DBCakeMakerContext db = new DBCakeMakerContext();

        // GET: ChuDe
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChuDePartial()
        {
            var listType = db.NewTypeProducts.ToList();

            return  PartialView(listType);
                
        }
        //Sách theo chủ đề 
        public ActionResult BanhTheoType(int? page, int Id)
        {
            //Kiem tra chu de ton tai hay k
            NewTypeProduct t = db.NewTypeProducts.SingleOrDefault(n => n.TypeProductId == Id);
            if (t == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                int pageSize = 2;
                int pageNumber = (page ?? 1);
                var listProduct = db.Products.Where(n => n.TypeProductId == Id).OrderByDescending(n => n.Prince);

                if (listProduct == null)
                {
                    ViewBag.Cake = "Không Có Sách Nào Thuộc Chủ Đề Này";
                }
                return View(listProduct.ToPagedList(pageNumber, pageSize));

                
            }
        }
    }
}