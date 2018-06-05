using NewCakeMakerASP.NET.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NewCakeMakerASP.NET.Controllers
{
    public class CustomerController : Controller
    {
        DBCakeMakerContext db = new DBCakeMakerContext();
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                Customer customerTest = db.Customers.SingleOrDefault(n => n.Account == customer.Account);
                if (customerTest != null)
                {
                    ViewBag.ThongBao = "Account đã được sử dụng";
                    return View(customer);
                }

                customer.Status = 1;
                db.Customers.Add(customer);
                db.SaveChanges();
                ViewBag.DangKy="Đăng ký thành công";
                Session["Account"] = customer;
                return RedirectToAction("Index","Home");
                
            }
            return View(customer);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Admin/QLCustomers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,FullName,GenDer,Phone,Email,Address,BirthDay,Status,Password,Account")] Customer customer)
        {
            Customer customerTest = db.Customers.SingleOrDefault(n => n.Account == customer.Account);
            if (customerTest != null)
            {
                ViewBag.ThongBao = "Account đã được sử dụng";
                return View(customer);
            }
            if (ModelState.IsValid)
            {

                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(customer);
        }
        public ActionResult Details(Customer cus)
        {
            cus = Session["Account"] as Customer;
            if (cus == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          
            return View(cus);
        }
        public ActionResult Logout()
        {
            Session["Account"] = null;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Detail(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer cus = db.Customers.Find(id);
            if (cus == null)
            {
                return HttpNotFound();
            }
            return View(cus);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            Session["Account"] = null;
            string sAccount = f["txtAccount"].ToString();
            string sPass = f.Get("txtPassWord").ToString();
            Customer cus = db.Customers.SingleOrDefault(n => n.Account == sAccount && n.Password == sPass);
            if (cus!=null)
            {
                ViewBag.ThongBao = "Chúc Mừng Qúy Khách Đăng Nhập Thành Công";
                ViewBag.ThongBao2 = cus.CustomerId;
                Session["Account"] = cus;
                return View();
            }
            else
            {
                ViewBag.ThongBao = "Tài khoản hoặc mật khẩu không đúng";
                return View();
            }
        }
    }
}