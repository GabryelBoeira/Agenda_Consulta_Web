﻿using System;
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
    [Authorize]
    public class LocaisController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Locais
        public ActionResult Index()
        {
            return View(db.LocalViewModels.ToList());
        }

        // GET: Locais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocalViewModel localViewModel = db.LocalViewModels.Find(id);
            if (localViewModel == null)
            {
                return HttpNotFound();
            }
            return View(localViewModel);
        }

        // GET: Locais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Locais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NomeLocal,Domingo,Segunda,Terca,Quarta,Quinta,Sexta,Sabado,HrInicio,HrFim,Cep,Rua,Numero,Complemento,Cidade,Uf")] LocalViewModel localViewModel)
        {
            if (ModelState.IsValid)
            {
                db.LocalViewModels.Add(localViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(localViewModel);
        }

        // GET: Locais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocalViewModel localViewModel = db.LocalViewModels.Find(id);
            if (localViewModel == null)
            {
                return HttpNotFound();
            }
            return View(localViewModel);
        }

        // POST: Locais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NomeLocal,Domingo,Segunda,Terca,Quarta,Quinta,Sexta,Sabado,HrInicio,HrFim,Cep,Rua,Numero,Complemento,Cidade,Uf")] LocalViewModel localViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(localViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(localViewModel);
        }

        // GET: Locais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocalViewModel localViewModel = db.LocalViewModels.Find(id);
            if (localViewModel == null)
            {
                return HttpNotFound();
            }
            return View(localViewModel);
        }

        // POST: Locais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LocalViewModel localViewModel = db.LocalViewModels.Find(id);
            db.LocalViewModels.Remove(localViewModel);
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
        public bool ValidaHoraAtendimento(Agendamento agendamento)
        {
            LocalViewModel localViewModel = db.LocalViewModels.Find(agendamento.LocalViewModelID);

            if (agendamento.HoraConsulta.Hour < localViewModel.HrInicio.Hour ||
                agendamento.HoraConsulta.AddMinutes(agendamento.TempoEmMinutosConsulta).Minute > localViewModel.HrFim.Minute) {
                return false;
            }                    
            return true;
        }

        public LocalViewModel localDiasemana(Agendamento agendamento)
        {
            LocalViewModel localViewModel = db.LocalViewModels.Find(agendamento.LocalViewModelID);
            return localViewModel;
        }
    }
}
