using Agenda_Consulta_Web.Models;
using Agenda_Consulta_Web.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Agenda_Consulta_Web.Controllers
{
    public class LocaisController : Controller
    {
        // GET
        public ActionResult Index()
        {
            Contexto contexto = new Contexto();
            List<Local> locais = contexto.Locais.ToList();

            return View(locais);
        }

        // GET
        public ActionResult Details(int? id)
        {
            //verifiaca se o id estiver nulo 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Contexto contexto = new Contexto();

            Local local = contexto.Locais.Find(id);
            if (local == null)
            {
                return HttpNotFound();
            }
            return View(local);
        }

        // GET
        public ActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        public ActionResult Create(Local local)
        {
            try
            {
                //salvar novo profissional cadastrado
                if (ModelState.IsValid)
                {
                    Contexto contexto = new Contexto();
                    contexto.Locais.Add(local);
                    contexto.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(local);
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
            
            Local local = contexto.Locais.Find(id);

            if (local == null)
            {
                return HttpNotFound();
            }

            return View(local);
        }

        // POST
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                //exclui os dados
                Contexto contexto = new Contexto();
                Local local = contexto.Locais.Find(id);

                contexto.Locais.Remove(local);
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
