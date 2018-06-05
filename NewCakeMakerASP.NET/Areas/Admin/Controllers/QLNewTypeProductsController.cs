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
    public class QLNewTypeProductsController : Controller
    {
        private DBCakeMakerContext db = new DBCakeMakerContext();

        // GET: Admin/QLNewTypeProducts
        public ActionResult Index()
        {
            return View(db.NewTypeProducts.ToList());
        }

        // GET: Admin/QLNewTypeProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewTypeProduct newTypeProduct = db.NewTypeProducts.Find(id);
            if (newTypeProduct == null)
            {
                return HttpNotFound();
            }
            return View(newTypeProduct);
        }

        // GET: Admin/QLNewTypeProducts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/QLNewTypeProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TypeProductId,Name,Description,CreateAt,UpdateAt,Status")] NewTypeProduct newTypeProduct)
        {
            if (ModelState.IsValid)
            {
                db.NewTypeProducts.Add(newTypeProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newTypeProduct);
        }

        // GET: Admin/QLNewTypeProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewTypeProduct newTypeProduct = db.NewTypeProducts.Find(id);
            if (newTypeProduct == null)
            {
                return HttpNotFound();
            }
            return View(newTypeProduct);
        }

        // POST: Admin/QLNewTypeProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TypeProductId,Name,Description,CreateAt,UpdateAt,Status")] NewTypeProduct newTypeProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newTypeProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newTypeProduct);
        }

        // GET: Admin/QLNewTypeProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewTypeProduct newTypeProduct = db.NewTypeProducts.Find(id);
            if (newTypeProduct == null)
            {
                return HttpNotFound();
            }
            return View(newTypeProduct);
        }

        // POST: Admin/QLNewTypeProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewTypeProduct newTypeProduct = db.NewTypeProducts.Find(id);
            db.NewTypeProducts.Remove(newTypeProduct);
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
