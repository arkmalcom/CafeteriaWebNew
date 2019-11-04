using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CafeteriaWebNew.Models;

namespace CafeteriaWebNew.Controllers
{
    public class CafeteriasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cafeterias
        [Authorize(Roles = "Administrador")]
        public ActionResult Index(string Criterio = null)
        {
            var cafeterias = db.Cafeterias.Include(c => c.Campus);
            return View(cafeterias.Where(p => Criterio == null || p.Descripcion.StartsWith(Criterio) ||
            p.Campus.Descripcion.StartsWith(Criterio) || p.Encargado.StartsWith(Criterio)).ToList());
        }

        // GET: Cafeterias/Details/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cafeteria cafeteria = db.Cafeterias.Find(id);
            if (cafeteria == null)
            {
                return HttpNotFound();
            }
            return View(cafeteria);
        }

        // GET: Cafeterias/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            ViewBag.CampusId = new SelectList(db.Campus, "ID", "Descripcion");
            return View();
        }

        // POST: Cafeterias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Descripcion,CampusId,Encargado,Estado")] Cafeteria cafeteria)
        {
            if (ModelState.IsValid)
            {
                db.Cafeterias.Add(cafeteria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CampusId = new SelectList(db.Campus, "ID", "Descripcion", cafeteria.CampusId);
            return View(cafeteria);
        }

        // GET: Cafeterias/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cafeteria cafeteria = db.Cafeterias.Find(id);
            if (cafeteria == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampusId = new SelectList(db.Campus, "ID", "Descripcion", cafeteria.CampusId);
            return View(cafeteria);
        }

        // POST: Cafeterias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Descripcion,CampusId,Encargado,Estado")] Cafeteria cafeteria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cafeteria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CampusId = new SelectList(db.Campus, "ID", "Descripcion", cafeteria.CampusId);
            return View(cafeteria);
        }

        // GET: Cafeterias/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cafeteria cafeteria = db.Cafeterias.Find(id);
            if (cafeteria == null)
            {
                return HttpNotFound();
            }
            return View(cafeteria);
        }

        // POST: Cafeterias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cafeteria cafeteria = db.Cafeterias.Find(id);
            db.Cafeterias.Remove(cafeteria);
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
