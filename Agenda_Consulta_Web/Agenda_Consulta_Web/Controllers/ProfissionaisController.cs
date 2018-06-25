﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Agenda_Consulta_Web.Models;
using Agenda_Consulta_Web.Models.DAL;

namespace Agenda_Consulta_Web.Controllers
{
    public class ProfissionaisController : Controller
    {
        // GET
        public ActionResult Index()
        {
            Contexto contexto = new Contexto();
            List<Profissional> profissionais =  contexto.Profissionais.ToList();
            return View(profissionais);
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

            Profissional profissional = contexto.Profissionais.Find(id);
            if (profissional == null)
            {
                return HttpNotFound();
            }
            return View(profissional);
        }

        // GET
        public ActionResult Create()
        {
            return View();

        }

        // POST
        [HttpPost]
        
        public ActionResult Create(Profissional profissional)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Contexto contexto = new Contexto();
                    contexto.Profissionais.Add(profissional);
                    contexto.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(profissional);

            }
            catch
            {
                return View();
            }
        }
        
        // GET
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contexto contexto = new Contexto();
            Profissional profissional = contexto.Profissionais.Find(id);

            if (profissional == null)
            {
                return HttpNotFound();
            }

            return View(profissional);
        }

        // POST
        [HttpPost]
        public ActionResult Edit(int? id, Profissional profissional)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Contexto contexto = new Contexto();

                    contexto.Entry(profissional).State = 
                        System.Data.Entity.EntityState.Modified;
                    contexto.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(profissional);

            }
            catch
            {
                return View(profissional);
            }
        }

        // GET
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contexto contexto = new Contexto();

            Profissional profissional = contexto.Profissionais.Find(id);

            if (profissional == null)
            {
                return HttpNotFound();
            }

            return View(profissional);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            try
            {
                Contexto contexto = new Contexto();
                Profissional profissional = contexto.Profissionais.Find(id);

                contexto.Profissionais.Remove(profissional);
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
