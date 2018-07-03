using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Agenda_Consulta_Web;
using Agenda_Consulta_Web.Models;
using Agenda_Consulta_Web.Models.DAL;

namespace Agenda_Consulta_Web.Controllers
{
    public class ProfissionaisController : Controller
    {     
        // GET: Profissionais
        public ActionResult Index()
        {
            Contexto db = new Contexto();
            return View(db.Profissionais.ToList());
        }

        // GET: Profissionais/Details/5
        public ActionResult Details(int? id)
        {
            Contexto db = new Contexto();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profissional profissional = db.Profissionais.Find(id);
            if (profissional == null)
            {
                return HttpNotFound();
            }
            return View(profissional);
        }

        // GET: Profissionais/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profissionais/Create
       
        [HttpPost]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,ResgistroProfissional,Especialidade,Domingo,Segunda,Terca,Quarta,Quinta,Sexta,Sabado,HrInicio,HrFim,Nome,Celular,Email,CPF,DtNascimento")] Profissional profissional)
        {
            Contexto db = new Contexto();

            if (ModelState.IsValid)
            {
                db.Profissionais.Add(profissional);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(profissional);
        }

        // GET: Profissionais/Edit/
        [Authorize]
        public ActionResult Edit(int? id)
        {
            Contexto db = new Contexto();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profissional profissional = db.Profissionais.Find(id);
            if (profissional == null)
            {
                return HttpNotFound();
            }
            return View(profissional);
        }

        // POST: Profissionais/Edit/
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ResgistroProfissional,Especialidade,Domingo,Segunda,Terca,Quarta,Quinta,Sexta,Sabado,HrInicio,HrFim,Nome,Celular,Email,CPF,DtNascimento")] Profissional profissional)
        {
            Contexto db = new Contexto();

            if (ModelState.IsValid)
            {
                db.Entry(profissional).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profissional);
        }

        // GET: Profissionais/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            Contexto db = new Contexto();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profissional profissional = db.Profissionais.Find(id);
            if (profissional == null)
            {
                return HttpNotFound();
            }
            return View(profissional);
        }

        // POST: Profissionais/Delete       
        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Contexto db = new Contexto();

            Profissional profissional = db.Profissionais.Find(id);
            db.Profissionais.Remove(profissional);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
