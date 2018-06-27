using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Agenda_Consulta_Web.Models;

namespace Agenda_Consulta_Web.Controllers
{
    public class LocalsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Locals
        public ActionResult Index()
        {
            var locals = db.Locals.Include(l => l._Endereco);
            return View(locals.ToList());
        }

        // GET: Locals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Local local = db.Locals.Find(id);
            if (local == null)
            {
                return HttpNotFound();
            }
            return View(local);
        }

        // GET: Locals/Create
        public ActionResult Create()
        {
            ViewBag.EnderecoID = new SelectList(db.Enderecoes, "EnderecoID", "Cep");
            return View();
        }

        // POST
        [ValidateAntiForgeryToken]
        public ActionResult Create(LocaisViewModel  model)
        {
            if (ModelState.IsValid)
            {
                Local local = new Local();
                Endereco endereco = new Endereco();


                //var Local = new ApplicationUser { UserName = model.Email, Email = model.Email };
                // var endereço = await UserManager.CreateAsync(user, model.Password);
                // if (result.Succeeded)
                // {
                //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                // return RedirectToAction("Index", "Home");
                //}
                //   AddErrors(result);


                // If we got this far, something failed, redisplay form
                //  return View(model);
                //}
                db.Locals.Add(local);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EnderecoID = new SelectList(db.Enderecoes, "EnderecoID", "Cep", local.EnderecoID);
            return View(local);
        }

        // GET: Locals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Local local = db.Locals.Find(id);
            if (local == null)
            {
                return HttpNotFound();
            }
            ViewBag.EnderecoID = new SelectList(db.Enderecoes, "EnderecoID", "Cep", local.EnderecoID);
            return View(local);
        }

        // POST: Locals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocalID,NomeLocal,Domingo,Segunda,Terca,Quarta,Quinta,Sexta,Sabado,HrInicio,HrFim,EnderecoID")] Local local)
        {
            if (ModelState.IsValid)
            {
                db.Entry(local).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EnderecoID = new SelectList(db.Enderecoes, "EnderecoID", "Cep", local.EnderecoID);
            return View(local);
        }

        // GET: Locals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Local local = db.Locals.Find(id);
            if (local == null)
            {
                return HttpNotFound();
            }
            return View(local);
        }

        // POST: Locals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Local local = db.Locals.Find(id);
            db.Locals.Remove(local);
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
