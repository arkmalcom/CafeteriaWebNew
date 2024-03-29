﻿using System;
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
    public class EmpleadosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult exportaExcel()
        {
            string filename = "Empleados.csv";
            string filepath = @"c:\tmp\" + filename;
            StreamWriter sw = new StreamWriter(filepath);
            sw.WriteLine("ID,Nombre,Cedula,Tanda,Porcentaje Comision, Fecha de ingreso, Estado"); //Encabezado 
            foreach (var i in db.Empleadoes.ToList())
            {
                if (i.Estado)
                {
                    sw.WriteLine(i.ID.ToString() + "," + i.Nombre + "," + i.Cedula + "," + i.Tanda + "," + i.PorcientoComision.ToString() + "%" + "," + i.FechaIngreso.ToString() + "," + "Activo");
                }
                else
                {
                    sw.WriteLine(i.ID.ToString() + "," + i.Nombre + "," + i.Cedula + "," + i.Tanda + "," + i.PorcientoComision.ToString() + "%" + "," + i.FechaIngreso.ToString() + "," + "Inactivo");
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
        // GET: Empleados
        [Authorize(Roles = "Administrador")]
        public ActionResult Index(string Criterio = null)
        {
            return View(db.Empleadoes.Where(p=> Criterio == null || p.Cedula.StartsWith(Criterio) ||
            p.Nombre.StartsWith(Criterio) ||
            p.Tanda.ToString().StartsWith(Criterio) || p.PorcientoComision.ToString().StartsWith(Criterio)).ToList());
        }

        // GET: Empleados/Details/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleadoes.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: Empleados/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,Cedula,Tanda,PorcientoComision,FechaIngreso,Estado")] Empleado empleado)
        {
            Usuario usuario = (from r in db.Usuarios.Where(a => a.Cedula == empleado.Cedula) select r).FirstOrDefault();
            if (!validaCedula(empleado.Cedula))
            {
                ModelState.AddModelError("Cedula", "Cedula invalida.");
            }
            if(usuario.Cedula != null && usuario.Nombre != empleado.Nombre)
            {
                ModelState.AddModelError("Cedula", "Esta cedula esta registrada bajo el nombre de " + usuario.Nombre);
            }
            if (ModelState.IsValid)
            {
                db.Empleadoes.Add(empleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(empleado);
        }

        // GET: Empleados/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleadoes.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombre,Cedula,Tanda,PorcientoComision,FechaIngreso,Estado")] Empleado empleado)
        {
            Usuario usuario = (from r in db.Usuarios.Where(a => a.Cedula == empleado.Cedula) select r).FirstOrDefault();
            if (!validaCedula(empleado.Cedula))
            {
                ModelState.AddModelError("Cedula", "Cedula invalida.");
            }
            if (usuario.Cedula != null && usuario.Nombre != empleado.Nombre)
            {
                ModelState.AddModelError("Cedula", "Esta cedula esta registrada bajo el nombre de " + usuario.Nombre);
            }
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empleado);
        }

        // GET: Empleados/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleadoes.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleado empleado = db.Empleadoes.Find(id);
            db.Empleadoes.Remove(empleado);
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
