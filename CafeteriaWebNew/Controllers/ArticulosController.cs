using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CafeteriaWebNew.Models;

namespace CafeteriaWebNew.Controllers
{
    public class ArticulosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult exportaExcel()
        {
            string filename = "Articulos.csv";
            string filepath = @"c:\tmp\" + filename;
            StreamWriter sw = new StreamWriter(filepath);
            sw.WriteLine("ID,Descripcion,Marca,Costo,Proveedor,Existencia,Estado"); //Encabezado 
            foreach (var i in db.Articuloes.ToList())
            {
                if (i.Estado) {
                    sw.WriteLine(i.ID.ToString() + "," + i.Descripcion + "," + i.Marca.Descripcion + "," + i.Costo.ToString() + "," + i.Proveedor.Nombre + "," + i.Existencia.ToString() + "," + "Activo");
                }
                else
                {
                    sw.WriteLine(i.ID.ToString() + "," + i.Descripcion + "," + i.Marca.Descripcion + "," + i.Costo.ToString() + "," + i.Proveedor.Nombre + "," + i.Existencia.ToString() + "," + "Inactivo");
                }
            }
            sw.Close();

            byte[] filedata = System.IO.File.ReadAllBytes(filepath);
            string contentType = MimeMapping.GetMimeMapping(filepath);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = filename,
                Inline = false,
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(filedata, contentType);
        }

        // GET: Articulos
        [Authorize(Roles = "Administrador")]
        public ActionResult Index(string Criterio = null)
        {
            var articuloes = db.Articuloes.Include(a => a.Marca).Include(a => a.Proveedor);
            return View(articuloes.Where(p => Criterio == null || p.Descripcion.StartsWith(Criterio) ||
            p.Marca.Descripcion.StartsWith(Criterio) ||
            p.Proveedor.Nombre.StartsWith(Criterio)).ToList());
        }

        // GET: Articulos/Details/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulo articulo = db.Articuloes.Find(id);
            if (articulo == null)
            {
                return HttpNotFound();
            }
            return View(articulo);
        }

        // GET: Articulos/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            ViewBag.MarcaId = new SelectList(db.Marcas, "ID", "Descripcion");
            ViewBag.ProveedorId = new SelectList(db.Proveedors, "ID", "Nombre");
            return View();
        }

        // POST: Articulos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Descripcion,MarcaId,Costo,ProveedorId,Existencia,Estado")] Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                db.Articuloes.Add(articulo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MarcaId = new SelectList(db.Marcas, "ID", "Descripcion", articulo.MarcaId);
            ViewBag.ProveedorId = new SelectList(db.Proveedors, "ID", "Nombre", articulo.ProveedorId);
            return View(articulo);
        }

        // GET: Articulos/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulo articulo = db.Articuloes.Find(id);
            if (articulo == null)
            {
                return HttpNotFound();
            }
            ViewBag.MarcaId = new SelectList(db.Marcas, "ID", "Descripcion", articulo.MarcaId);
            ViewBag.ProveedorId = new SelectList(db.Proveedors, "ID", "Nombre", articulo.ProveedorId);
            return View(articulo);
        }

        // POST: Articulos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Descripcion,MarcaId,Costo,ProveedorId,Existencia,Estado")] Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articulo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MarcaId = new SelectList(db.Marcas, "ID", "Descripcion", articulo.MarcaId);
            ViewBag.ProveedorId = new SelectList(db.Proveedors, "ID", "Nombre", articulo.ProveedorId);
            return View(articulo);
        }

        // GET: Articulos/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulo articulo = db.Articuloes.Find(id);
            if (articulo == null)
            {
                return HttpNotFound();
            }
            return View(articulo);
        }

        // POST: Articulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Articulo articulo = db.Articuloes.Find(id);
            db.Articuloes.Remove(articulo);
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
