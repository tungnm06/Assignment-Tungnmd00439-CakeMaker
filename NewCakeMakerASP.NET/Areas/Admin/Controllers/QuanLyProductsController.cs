using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using NewCakeMakerASP.NET.Models;

namespace NewCakeMakerASP.NET.Areas.Admin.Controllers
{
    public class QuanLyProductsController : Controller
    {
        private DBCakeMakerContext db = new DBCakeMakerContext();

        // GET: Admin/QuanLyProducts
        public ActionResult Index(int ?page)
        {
            var products = db.Products.OrderByDescending(n => n.ProductId);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/QuanLyProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/QuanLyProducts/Create
        public ActionResult Create()
        {
            ViewBag.TypeProductId = new SelectList(db.NewTypeProducts, "TypeProductId", "Name");
            return View();
        }

        // POST: Admin/QuanLyProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Name,Prince,Image,Description,CreateAt,UpdateAt,TypeProductId")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.CreateAt = DateTime.Now;

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TypeProductId = new SelectList(db.NewTypeProducts, "TypeProductId", "Name", product.TypeProductId);
            return View(product);
        }

        // GET: Admin/QuanLyProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeProductId = new SelectList(db.NewTypeProducts, "TypeProductId", "Name", product.TypeProductId);
            return View(product);
        }

        // POST: Admin/QuanLyProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,Name,Prince,Image,Description,CreateAt,UpdateAt,TypeProductId")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.UpdateAt = DateTime.Now;

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TypeProductId = new SelectList(db.NewTypeProducts, "TypeProductId", "Name", product.TypeProductId);
            return View(product);
        }

        // GET: Admin/QuanLyProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/QuanLyProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
