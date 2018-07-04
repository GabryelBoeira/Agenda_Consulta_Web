using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Agenda_Consulta_Web.Models;
using Agenda_Consulta_Web.Models.DAL;

namespace Agenda_Consulta_Web.Controllers
{
    public class AgendamentosController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Agendamentos
        public ActionResult Index()
        {
            var agendamentoes = db.Agendamentos.Include(a => a._LocalViewModel).Include(a => a._Paciente).Include(a => a._Profissional);
            return View(agendamentoes.ToList());
        }

        // GET: Agendamentos/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agendamento agendamento = db.Agendamentos.Find(id);
            if (agendamento == null)
            {
                return HttpNotFound();
            }
            return View(agendamento);
        }

        // GET: Agendamentos/Create
        public ActionResult Create()
        {
            ViewBag.LocalViewModelID = new SelectList(db.LocalViewModels, "ID", "NomeLocal");
            ViewBag.PacienteID = new SelectList(db.Pacientes, "ID", "Nome");
            ViewBag.ProfissionalID = new SelectList(db.Profissionais, "ID", "Nome");
            return View();
        }

        // POST: Agendamentos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AgendamentoID,LocalViewModelID,PacienteID,ProfissionalID,DataConsulta,HoraConsulta")] Agendamento agendamento)
        {
            if (ModelState.IsValid)
            {
                db.Agendamentos.Add(agendamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocalViewModelID = new SelectList(db.LocalViewModels, "ID", "NomeLocal", agendamento.LocalViewModelID);
            ViewBag.PacienteID = new SelectList(db.Pacientes, "ID", "Nome", agendamento.PacienteID);
            ViewBag.ProfissionalID = new SelectList(db.Profissionais, "ID", "Nome", agendamento.ProfissionalID);
            return View(agendamento);
        }

        // GET: Agendamentos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agendamento agendamento = db.Agendamentos.Find(id);
            if (agendamento == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocalViewModelID = new SelectList(db.LocalViewModels, "ID", "NomeLocal", agendamento.LocalViewModelID);
            ViewBag.PacienteID = new SelectList(db.Pacientes, "ID", "Nome", agendamento.PacienteID);
            ViewBag.ProfissionalID = new SelectList(db.Profissionais, "ID", "Nome", agendamento.ProfissionalID);
            return View(agendamento);
        }

        // POST: Agendamentos/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AgendamentoID,LocalViewModelID,PacienteID,ProfissionalID,DataConsulta,HoraConsulta")] Agendamento agendamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agendamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocalViewModelID = new SelectList(db.LocalViewModels, "ID", "NomeLocal", agendamento.LocalViewModelID);
            ViewBag.PacienteID = new SelectList(db.Pacientes, "ID", "Nome", agendamento.PacienteID);
            ViewBag.ProfissionalID = new SelectList(db.Profissionais, "ID", "Nome", agendamento.ProfissionalID);
            return View(agendamento);
        }

        // GET: Agendamentos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agendamento agendamento = db.Agendamentos.Find(id);
            if (agendamento == null)
            {
                return HttpNotFound();
            }
            return View(agendamento);
        }

        // POST: Agendamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Agendamento agendamento = db.Agendamentos.Find(id);
            db.Agendamentos.Remove(agendamento);
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
