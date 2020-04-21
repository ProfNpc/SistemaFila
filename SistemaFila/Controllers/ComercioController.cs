using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaFila.DAL;
using SistemaFila.Models;

namespace SistemaFila.Controllers
{
    public class ComercioController : Controller
    {
        private SistemaFilaContext db = new SistemaFilaContext();

        // GET: Comercios
        public ActionResult Index()
        {
            return View(db.Comercios.ToList());
        }

        public ActionResult DetailsPorCnpj(string cnpj)
        {
            if (cnpj == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comercio comercio = db.Comercios.Where(c => c.Cpnj == cnpj).FirstOrDefault();
            if (comercio == null)
            {
                return HttpNotFound();
            }
            return View("Edit", comercio);
        }

        // GET: Comercios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comercio comercio = db.Comercios.Find(id);
            if (comercio == null)
            {
                return HttpNotFound();
            }
            return View(comercio);
        }

        // GET: Comercios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comercios/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ComercioId,Cpnj,NomeFantasia,Endereco,Status,Capacidade")] Comercio comercio)
        {
            if (ModelState.IsValid)
            {
                db.Comercios.Add(comercio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(comercio);
        }

        // GET: Comercios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comercio comercio = db.Comercios.Find(id);
            if (comercio == null)
            {
                return HttpNotFound();
            }
            return View(comercio);
        }

        // POST: Comercios/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ComercioId,Cpnj,NomeFantasia,Endereco,Status,Capacidade")] Comercio comercio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comercio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comercio);
        }

        // GET: Comercios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comercio comercio = db.Comercios.Find(id);
            if (comercio == null)
            {
                return HttpNotFound();
            }
            return View(comercio);
        }

        // POST: Comercios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comercio comercio = db.Comercios.Find(id);
            db.Comercios.Remove(comercio);
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
