using NewCakeMakerASP.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewCakeMakerASP.NET.Controllers
{
    public class GioHangController : Controller
    {
        DBCakeMakerContext db = new DBCakeMakerContext();
        // GET: GioHang
        public List<GioHang> LayGioHang()
        {
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if(listGioHang == null)
            {
                listGioHang = new List<GioHang>();
                Session["GioHang"] = listGioHang;
            }
            return listGioHang;
        }
        public ActionResult ThemGioHang(int idProduct,string strURL)
        {
            Product product = db.Products.SingleOrDefault(n => n.ProductId == idProduct);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> listGioHang = LayGioHang();
            GioHang sanpham = listGioHang.Find(n => n.idProduct == idProduct);
            if (sanpham == null)
            {
                sanpham = new GioHang(idProduct);
                listGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strURL);

            }
        }
        public ActionResult CapNhatGioHang(int idProduct, FormCollection f)
        {
            Product product = db.Products.SingleOrDefault(n => n.ProductId == idProduct);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> listGioHang = LayGioHang();
            GioHang sanpham = listGioHang.SingleOrDefault(n => n.idProduct == idProduct);
            if (sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
          
        }

        public ActionResult XoaGioHang(int idProduct)
        {
            Product product = db.Products.SingleOrDefault(n => n.ProductId == idProduct);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> listGioHang = LayGioHang();
            GioHang sanpham = listGioHang.SingleOrDefault(n => n.idProduct == idProduct);
            if (sanpham != null)
            {
                listGioHang.RemoveAll(n => n.idProduct == idProduct);
            }
            if (listGioHang.Count == 0)
            {
                return RedirectToAction("Index","Home");

            }
            return RedirectToAction("GioHang");

        }
        //xay dung trang gio hang
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");

            }
            List<GioHang> listGioHang = LayGioHang();
            return View(listGioHang);

        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang != null)
            {
                iTongSoLuong = listGioHang.Sum(n => n.iSoLuong);

            }
            return iTongSoLuong;
        }
        private decimal TongTien()
        {
            decimal dTongTien = 0;
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang != null)
            {
                dTongTien = listGioHang.Sum(n => n.dThanhTien);

            }
            return dTongTien;
        }
        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
    
        }
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");

            }
            List<GioHang> listGioHang = LayGioHang();
            return View(listGioHang);
        }
        //Xay Dung chuc nang dat hang
        public ActionResult DatHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");

            }
            //Kiem tra dang nhap 
            if (Session["Account"]==null || Session["Account"].ToString()=="")
            {
               return RedirectToAction("Login", "Customer");
            }
           
         
            //them don dat hang
    
            Order order = new Order();
            List<GioHang> listGH = LayGioHang();
            Customer cus = Session["Account"] as Customer;

            order.CustomerId = cus.CustomerId;
            order.CreateAt = DateTime.Now;
            order.TotalPrince = TongTien();
            if (TongTien() == 0)
            {
                return RedirectToAction("Index", "Home");

            }
            db.Orders.Add(order);
            foreach(var item in listGH)
            {
                OrderDetail ordetail = new OrderDetail();

                ordetail.ProductId = item.idProduct;
                ordetail.Quantity = item.iSoLuong;
                ordetail.UnitPrince = item.dDonGia;
                db.OrderDetails.Add(ordetail);
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            LayGioHang();
            return RedirectToAction("Details", "Orders",new {id=order.OrderId });
        }
    }

}