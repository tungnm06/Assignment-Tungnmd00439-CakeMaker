using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewCakeMakerASP.NET.Models;
using PagedList;
using PagedList.Mvc;

namespace NewCakeMakerASP.NET.Areas.Admin.Controllers
{
    public class QLCustomersController : Controller
    {
        private DBCakeMakerContext db = new DBCakeMakerContext();

        // GET: Admin/QLCustomers
        public ActionResult Index(int? page)
        {
            var customer = db.Customers.OrderByDescending(n => n.CustomerId);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(customer.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/QLCustomers/Details/5
        public ActionResult Details(int? id)
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

        // GET: Admin/QLCustomers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/QLCustomers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,FullName,GenDer,Phone,Email,Address,BirthDay,Status,Password,Account")] Customer customer)
        {
            Customer customerTest = db.Customers.SingleOrDefault(n => n.Account == customer.Account);
            if (customerTest != null)
            {
                ViewBag.ThongBao = "Account đã được sử dụng";
                return View(customer);
            }
            if (ModelState.IsValid)
            {
                customer.Status = 1;
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Admin/QLCustomers/Edit/5
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
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Admin/QLCustomers/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Admin/QLCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
