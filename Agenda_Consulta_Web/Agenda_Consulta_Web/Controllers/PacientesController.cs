﻿using Agenda_Consulta_Web.Models;
using Agenda_Consulta_Web.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Agenda_Consulta_Web.Controllers
{
    public class PacientesController : Controller
    {
        // GET
        public ActionResult Index()
        {
            Contexto contexto = new Contexto();
            List<Paciente> pacientes = contexto.Pacientes.ToList();

            return View(pacientes);
        }

        // GET
        public ActionResult Details(int? id)        {

            //verifiaca se o id estiver nulo 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Contexto contexto = new Contexto();

            Paciente paciente = contexto.Pacientes.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // GET
        public ActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        public ActionResult Create(Paciente paciente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Contexto contexto = new Contexto();
                    contexto.Pacientes.Add(paciente);
                    contexto.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(paciente);

            }
            catch
            {
                return View();
            }
        }

        // GET
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET
        public ActionResult Delete(int? id)
        {
            //busca para confirmar antes de excluir
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contexto contexto = new Contexto();

            Paciente paciente = contexto.Pacientes.Find(id);

            if (paciente == null)
            {
                return HttpNotFound();
            }

            return View(paciente);
        }

        // POST
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                //exclui os dados
                Contexto contexto = new Contexto();
                Paciente paciente = contexto.Pacientes.Find(id);

                contexto.Pacientes.Remove(paciente);
                contexto.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    

    }
}
