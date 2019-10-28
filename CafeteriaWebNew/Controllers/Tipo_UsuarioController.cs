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
    public class Tipo_UsuarioController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tipo_Usuario
        public ActionResult Index(string Criterio = null)
        {
            return View(db.IdentityRoles.Where(p => Criterio == null || p.Name.StartsWith(Criterio)).ToList());
        }

        // GET: Tipo_Usuario/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Usuario tipo_Usuario = db.IdentityRoles.Find(id);
            if (tipo_Usuario == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Usuario);
        }

        // GET: Tipo_Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tipo_Usuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Estado")] Tipo_Usuario tipo_Usuario)
        {
            if (ModelState.IsValid)
            {
                db.IdentityRoles.Add(tipo_Usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_Usuario);
        }

        // GET: Tipo_Usuario/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Usuario tipo_Usuario = db.IdentityRoles.Find(id);
            if (tipo_Usuario == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Usuario);
        }

        // POST: Tipo_Usuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Estado")] Tipo_Usuario tipo_Usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_Usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_Usuario);
        }

        // GET: Tipo_Usuario/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Usuario tipo_Usuario = db.IdentityRoles.Find(id);
            if (tipo_Usuario == null)
            {
                return HttpNotFound();
            }
            return View(tipo_Usuario);
        }

        // POST: Tipo_Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Tipo_Usuario tipo_Usuario = db.IdentityRoles.Find(id);
            db.IdentityRoles.Remove(tipo_Usuario);
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
