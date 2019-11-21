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
    public class UsuariosController : Controller
    {
        public ActionResult exportaExcel()
        {
            string filename = "Usuarios.csv";
            string filepath = @"c:\tmp\" + filename;
            StreamWriter sw = new StreamWriter(filepath);
            sw.WriteLine("ID,Nombre,Cedula,Tipo de Usuario,Limite de credito,Fecha de Registro,Estado"); //Encabezado 
            foreach (var i in db.Usuarios.ToList())
            {
                if (i.Estado)
                {
                    sw.WriteLine(i.ID.ToString() + "," + i.Nombre + "," + i.Cedula + "," + i.Tipo_Usuario.Name + "," + i.LimiteCredito.ToString()
                        + "," + i.FechaRegistro.ToString()+"," + "Activo");
                }
                else
                {
                    sw.WriteLine(i.ID.ToString() + "," + i.Nombre + "," + i.Cedula + "," + i.Tipo_Usuario.Name + "," + i.LimiteCredito.ToString()
                        + "," + i.FechaRegistro.ToString() + "," + "Inactivo");
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
        private ApplicationDbContext db = new ApplicationDbContext();

        public static bool validaCedula(string pCedula)
        {
            int vnTotal = 0;
            string vcCedula = pCedula.Replace("-", "");
            int pLongCed = vcCedula.Trim().Length;
            int[] digitoMult = new int[11] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1 };

            if (pLongCed < 11 || pLongCed > 11)
                return false;

            for (int vDig = 1; vDig <= pLongCed; vDig++)
            {
                int vCalculo = Int32.Parse(vcCedula.Substring(vDig - 1, 1)) * digitoMult[vDig - 1];
                if (vCalculo < 10)
                    vnTotal += vCalculo;
                else
                    vnTotal += Int32.Parse(vCalculo.ToString().Substring(0, 1)) + Int32.Parse(vCalculo.ToString().Substring(1, 1));
            }

            if (vnTotal % 10 == 0)
                return true;
            else
                return false;
        }
        // GET: Usuarios
        [Authorize(Roles = "Administrador")]
        public ActionResult Index(string Criterio = null)
        {
            var usuarios = db.Usuarios.Include(u => u.Tipo_Usuario);
            return View(usuarios.Where(p => Criterio == null || p.Nombre.StartsWith(Criterio) || 
            p.LimiteCredito.ToString().StartsWith(Criterio) || p.Cedula.StartsWith(Criterio) ||
            p.Tipo_Usuario.Name.StartsWith(Criterio)).ToList());
        }

        // GET: Usuarios/Details/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            ViewBag.Tipo_UsuarioId = new SelectList(db.Roles, "Id", "Name");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,Cedula,Tipo_UsuarioId,LimiteCredito,FechaRegistro,Estado")] Usuario usuario)
        {
            Empleado empleado = (from r in db.Empleadoes.Where(a => a.Cedula == usuario.Cedula) select r).FirstOrDefault();
            if (!validaCedula(usuario.Cedula))
            {
                ModelState.AddModelError("Cedula", "Cedula invalida.");
            }
            if (empleado.Cedula != null && usuario.Nombre != empleado.Nombre)
            {
                ModelState.AddModelError("Cedula", "Esta cedula esta registrada bajo el nombre de " + empleado.Nombre);
            }
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Tipo_UsuarioId = new SelectList(db.Roles, "Id", "Name", usuario.Tipo_UsuarioId);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tipo_UsuarioId = new SelectList(db.Roles, "Id", "Name", usuario.Tipo_UsuarioId);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombre,Cedula,Tipo_UsuarioId,LimiteCredito,FechaRegistro,Estado")] Usuario usuario)
        {
            Empleado empleado = (from r in db.Empleadoes.Where(a => a.Cedula == usuario.Cedula) select r).FirstOrDefault();
            if (!validaCedula(usuario.Cedula))
            {
                ModelState.AddModelError("Cedula", "Cedula invalida.");
            }
            if (empleado.Cedula != null && empleado.Nombre != usuario.Nombre)
            {
                ModelState.AddModelError("Cedula", "Esta cedula esta registrada bajo el nombre de " + empleado.Nombre);
            }
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Tipo_UsuarioId = new SelectList(db.Roles, "Id", "Name", usuario.Tipo_UsuarioId);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
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
