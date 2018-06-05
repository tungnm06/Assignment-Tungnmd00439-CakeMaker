using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewCakeMakerASP.NET.Models;

namespace NewCakeMakerASP.NET.Areas.Admin.Controllers
{
    public class QLOrdersController : Controller
    {
        private DBCakeMakerContext db = new DBCakeMakerContext();

        // GET: Admin/QLOrders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Customer).Include(o => o.Employee);
            return View(orders.ToList());
        }

        // GET: Admin/QLOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Admin/QLOrders/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FullName");
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FullName");
            return View();
        }

        // POST: Admin/QLOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,TotalPrince,CreateAt,UpdateAt,FinishTime,Status,Pay,EmployeeId,CustomerId")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.CreateAt = DateTime.Now;
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FullName", order.CustomerId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FullName", order.EmployeeId);
            return View(order);
        }

        // GET: Admin/QLOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FullName", order.CustomerId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FullName", order.EmployeeId);
            return View(order);
        }

        // POST: Admin/QLOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,TotalPrince,CreateAt,UpdateAt,FinishTime,Status,Pay,EmployeeId,CustomerId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FullName", order.CustomerId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FullName", order.EmployeeId);
            return View(order);
        }

        // GET: Admin/QLOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/QLOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
