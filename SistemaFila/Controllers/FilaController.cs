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
    public class FilaController : Controller
    {
        private SistemaFilaContext db = new SistemaFilaContext();

        // GET: Fila
        public PartialViewResult Index(int ComercioId)
        {
            var filas = db.Filas.Where(f => f.ComercioId == ComercioId).ToList();

            ViewBag.ComercioId = ComercioId;

            List<TipoFila> TiposDisponiveis = ObterTiposDisponiveisParaComercio(ComercioId);

            Boolean PossuiBotaoNovaFila = (TiposDisponiveis.Count > 0);

            ViewBag.PossuiBotaoNovaFila = PossuiBotaoNovaFila;

            return PartialView("_FilaIndex", filas);
        }

        // GET: Fila/Details/5
        public ActionResult Details(int? ComercioId, TipoFila? tipo)
        {
            if (ComercioId == null || tipo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fila fila = db.Filas.Find(ComercioId, tipo);
            if (fila == null)
            {
                return HttpNotFound();
            }
            return View(fila);
        }

        // GET: Fila/Create
        public ActionResult Create(int ComercioId)
        {
            ViewBag.ComercioId = new SelectList(
                db.Comercios.Where(f => f.ComercioId == ComercioId),
                "ComercioId",
                "Cpnj");

            List<TipoFila> TiposDisponiveis = ObterTiposDisponiveisParaComercio(ComercioId);

            ViewBag.TiposDisponiveis = TiposDisponiveis.Select(t => new SelectListItem
            {
                Value = ((int)t) + "",
                Text = t + ""
            });

            return View();
        }

        private List<TipoFila> ObterTiposDisponiveisParaComercio(int ComercioId)
        {
            //Pega os tipos JaUsados pelo Comercio que possui ComercioId
            var TiposJaUsados =
                db.Filas
                .Where(f => f.ComercioId == ComercioId)
                .Select(f => new
                {
                    Tipo = f.Tipo
                }).Distinct()
                .ToList();

            //Pega todos os tipos do Enum TipoFila e guarda-os em uma Lista
            var TiposDisponiveis = Enum.GetValues(typeof(TipoFila)).Cast<TipoFila>().ToList();

            //Da Lista com todos os tipos, remove os TiposJaUsados e ficarao os TiposDisponiveis
            for (int i = 0; i < TiposJaUsados.Count; i++)
            {
                var TipoJaUsado = TiposJaUsados[i];
                for (int j = 0; j < TiposDisponiveis.Count; j++)
                {
                    var t = TiposDisponiveis[j];
                    if (TipoJaUsado.Tipo == t)
                    {
                        TiposDisponiveis.Remove(t);
                    }
                }
            }

            return TiposDisponiveis;
        }

        // POST: Fila/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ComercioId,Tipo,Tamanho")] Fila fila)
        {
            if (ModelState.IsValid)
            {
                Comercio c = db.Comercios.Find(fila.ComercioId);
                c.Filas.Add(fila);

                db.Filas.Add(fila);
                db.SaveChanges();
                return RedirectToAction("Edit", "Comercio", new { id = fila.ComercioId });
            }

            ViewBag.ComercioId = new SelectList(db.Comercios, "ComercioId", "Cpnj", fila.ComercioId);
            return View(fila);
        }

        // GET: Fila/Edit/5
        public ActionResult Edit(int? ComercioId, TipoFila? TipoFila)
        {
            //TipoFila TipoFila = (TipoFila) Enum.Parse(typeof(TipoFila), tipo, true);

            if (ComercioId == null || TipoFila == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //db.Filas
            //    .Join(
            //        db.Comercios,
            //        f => f.ComercioId,
            //        c => c.ComercioId).ToList();

            Fila fila = db.Filas
                .Where(f => f.ComercioId == ComercioId && f.Tipo == TipoFila)
                .Include(f => f.Comercio)
                .SingleOrDefault();
            if (fila == null)
            {
                return HttpNotFound();
            }
            ViewBag.ComercioId = new SelectList(db.Comercios, "ComercioId", "Cpnj", fila.ComercioId);
            return View(fila);
        }

        // POST: Fila/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ComercioId,Tipo,Tamanho")] Fila fila)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fila).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "Comercio", new { id = fila.ComercioId});
            }
            ViewBag.ComercioId = new SelectList(db.Comercios, "ComercioId", "Cpnj", fila.ComercioId);
            return View(fila);
        }

        // GET: Fila/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fila fila = db.Filas.Find(id);
            if (fila == null)
            {
                return HttpNotFound();
            }
            return View(fila);
        }

        // POST: Fila/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fila fila = db.Filas.Find(id);
            db.Filas.Remove(fila);
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
