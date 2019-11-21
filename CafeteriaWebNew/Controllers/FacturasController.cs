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
    public class FacturasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult exportaExcel()
        {
            string filename = "Facturas.csv";
            string filepath = @"c:\tmp\" + filename;
            StreamWriter sw = new StreamWriter(filepath);
            sw.WriteLine("ID,Empleado,Articulo,Usuario,Fecha de venta,Monto,Cantidad,Comentario,Estado"); //Encabezado 
            foreach (var i in db.Facturas.ToList())
            {
                if (i.Estado)
                {
                    sw.WriteLine(i.ID.ToString() + "," + i.Empleado.Nombre + "," + i.Articulo.Descripcion + "," + i.Usuario.Nombre + "," + i.FechaVenta.ToString() + "," + "$" + i.Monto
                         + ","+ i.Cantidad + "," + i.Comentario + "," + "Activo");
                }
                else
                {
                    sw.WriteLine(i.ID.ToString() + "," + i.Empleado.Nombre + "," + i.Articulo.Descripcion + "," + i.Usuario.Nombre + "," + i.FechaVenta.ToString() + "," + "$" + i.Monto
                        + "," + i.Cantidad + "," + i.Comentario + "," + "Inactivo");
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
        // GET: Facturas
        [Authorize(Roles = "Administrador")]
        public ActionResult Index(string Criterio = null)
        {
            var facturas = db.Facturas.Include(f => f.Articulo).Include(f => f.Empleado).Include(f => f.Usuario);
            return View(facturas.Where(p => Criterio == null || p.Articulo.Descripcion.StartsWith(Criterio) ||
            p.Empleado.Nombre.StartsWith(Criterio) || p.Usuario.Nombre.StartsWith(Criterio) ||
            p.Comentario.StartsWith(Criterio)).ToList());
        }

        // GET: Facturas/Details/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Facturas.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // GET: Facturas/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            ViewBag.ArticuloId = new SelectList(db.Articuloes, "ID", "Descripcion");
            ViewBag.EmpleadoId = new SelectList(db.Empleadoes, "ID", "Nombre");
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "ID", "Nombre");

            return View();
        }

        // POST: Facturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,EmpleadoId,ArticuloId,UsuarioId,FechaVenta,Monto,Cantidad,Comentario,Estado")] Factura factura)
        {
            Articulo articulo = (from r in db.Articuloes.Where(a => a.ID == factura.ArticuloId) select r).FirstOrDefault();
            if (articulo.Estado == false)
            {
                ModelState.AddModelError("Cantidad", "Este articulo no esta abilitado para la venta");
            }
            else if (factura.Cantidad > articulo.Existencia)
            {
                ModelState.AddModelError("Cantidad", "Cantidad excede la existencia del articulo");
            }
            if (ModelState.IsValid)
            {
                db.Facturas.Add(factura);
                db.SaveChanges();
                articulo.Existencia = articulo.Existencia - factura.Cantidad;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArticuloId = new SelectList(db.Articuloes, "ID", "Descripcion", factura.ArticuloId);
            ViewBag.EmpleadoId = new SelectList(db.Empleadoes, "ID", "Nombre", factura.EmpleadoId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "ID", "Nombre", factura.UsuarioId);
            return View(factura);
        }

        // GET: Facturas/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Facturas.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArticuloId = new SelectList(db.Articuloes, "ID", "Descripcion", factura.ArticuloId);
            ViewBag.EmpleadoId = new SelectList(db.Empleadoes, "ID", "Nombre", factura.EmpleadoId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "ID", "Nombre", factura.UsuarioId);
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EmpleadoId,ArticuloId,UsuarioId,FechaVenta,Monto,Cantidad,Comentario,Estado")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(factura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArticuloId = new SelectList(db.Articuloes, "ID", "Descripcion", factura.ArticuloId);
            ViewBag.EmpleadoId = new SelectList(db.Empleadoes, "ID", "Nombre", factura.EmpleadoId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "ID", "Nombre", factura.UsuarioId);
            return View(factura);
        }

        // GET: Facturas/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Facturas.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Factura factura = db.Facturas.Find(id);
            db.Facturas.Remove(factura);
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
