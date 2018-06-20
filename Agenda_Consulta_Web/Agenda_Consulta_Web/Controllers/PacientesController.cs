using Agenda_Consulta_Web.Models;
using Agenda_Consulta_Web.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
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

            return View();
        }

        // GET
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET
        public ActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    

    }
}
