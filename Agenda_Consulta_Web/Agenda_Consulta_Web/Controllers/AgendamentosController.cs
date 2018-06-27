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
            var agendamentos = db.Agendamentos.Include(a => a._Local).Include(a => a._Paciente).Include(a => a._Profissional);
            return View(agendamentos.ToList());
        }

        // GET: Agendamentos/Details/5
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
            ViewBag.LocalID = new SelectList(db.Locais, "LocalID", "NomeLocal");
            ViewBag.PacienteID = new SelectList(db.Pacientes, "ID", "Nome");
            ViewBag.ProfissionalID = new SelectList(db.Profissionais, "ID", "ResgistroProfissional");
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AgendamentoID,LocalID,PacienteID,ProfissionalID,DataConsulta,HoraConsulta")] Agendamento agendamento)
        {
            if (ModelState.IsValid)
            {
                if (ValidaHoraAtendimento(agendamento))
                {
                    if (ValidaDiaDaSemanaAtendimento(agendamento))
                    {
                        if (ValidaHorarioLivreProfissional(agendamento))
                        {
                            if (ValidaHorarioLivreLocal(agendamento))
                            {
                                db.Agendamentos.Add(agendamento);
                                db.SaveChanges();
                                return RedirectToAction("Index");
                            }
                        }
                    }
                }
               
            }

            ViewBag.LocalID = new SelectList(db.Locais, "LocalID", "NomeLocal", agendamento.LocalID);
            ViewBag.PacienteID = new SelectList(db.Pacientes, "ID", "Nome", agendamento.PacienteID);
            ViewBag.ProfissionalID = new SelectList(db.Profissionais, "ID", "ResgistroProfissional", agendamento.ProfissionalID);
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
            ViewBag.LocalID = new SelectList(db.Locais, "LocalID", "NomeLocal", agendamento.LocalID);
            ViewBag.PacienteID = new SelectList(db.Pacientes, "ID", "Nome", agendamento.PacienteID);
            ViewBag.ProfissionalID = new SelectList(db.Profissionais, "ID", "ResgistroProfissional", agendamento.ProfissionalID);
            return View(agendamento);
        }

        // POST: Agendamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AgendamentoID,LocalID,PacienteID,ProfissionalID,DataConsulta,HoraConsulta")] Agendamento agendamento)
        {
            if (ModelState.IsValid)
            {
                if (ValidaHoraAtendimento(agendamento))
                {
                    if (ValidaDiaDaSemanaAtendimento(agendamento))
                    {
                        if (ValidaHorarioLivreProfissional(agendamento))
                        {
                            if (ValidaHorarioLivreLocal(agendamento))
                            {
                                db.Entry(agendamento).State = EntityState.Modified;
                                db.SaveChanges();
                                return RedirectToAction("Index");
                            }
                        }
                    }
                }
               
            }
            ViewBag.LocalID = new SelectList(db.Locais, "LocalID", "NomeLocal", agendamento.LocalID);
            ViewBag.PacienteID = new SelectList(db.Pacientes, "ID", "Nome", agendamento.PacienteID);
            ViewBag.ProfissionalID = new SelectList(db.Profissionais, "ID", "ResgistroProfissional", agendamento.ProfissionalID);
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
        /// <summary>
        /// Area para gerar as validações antes de cadastrar um novo agendamento
        /// </summary>
        /// <param name="agendamento"></param>
        /// <returns></returns>
        public bool ValidaHoraAtendimento(Agendamento agendamento)
        {
            //Validando horário em que o profissional atende e o horário que o local está aberto para atendimento (não valida dia da semana e horário com consulta já marcada)
            if (agendamento.HoraConsulta.TimeOfDay < agendamento._Profissional.HrInicio.TimeOfDay ||
                agendamento.HoraConsulta.AddMinutes(agendamento.TempoEmMinutosConsulta).TimeOfDay > agendamento._Profissional.HrFim.TimeOfDay)
                return false;
            else
            {
                if (agendamento.HoraConsulta.TimeOfDay < agendamento._Local.HrInicio.TimeOfDay ||
                agendamento.HoraConsulta.AddMinutes(agendamento.TempoEmMinutosConsulta).TimeOfDay > agendamento._Local.HrFim.TimeOfDay)
                    return false;
            }
            return true;
        }

        public bool ValidaDiaDaSemanaAtendimento(Agendamento agendamento)
        {
            //dia da semana em formato ddd para comparação (dom, seg, ter, qua, qui, sex, sab)
            //Validando dia da semana disponível para profissional e local
            String diaDaSemana = agendamento.DataConsulta.ToString("ddd");
            if (diaDaSemana.Equals("dom"))
            {
                if (agendamento._Profissional.Domingo.Equals(null) || agendamento._Local.Domingo.Equals(null))
                    return false;
            }
            else
            {
                if (diaDaSemana.Equals("seg"))
                {
                    if (agendamento._Profissional.Segunda.Equals(null) || agendamento._Local.Segunda.Equals(null))
                        return false;
                }
                else
                {
                    if (diaDaSemana.Equals("ter"))
                    {
                        if (agendamento._Profissional.Terca.Equals(null) || agendamento._Local.Terca.Equals(null))
                            return false;
                    }
                    else
                    {
                        if (diaDaSemana.Equals("qua"))
                        {
                            if (agendamento._Profissional.Quarta.Equals(null) || agendamento._Local.Quarta.Equals(null))
                                return false;
                        }
                        else
                        {
                            if (diaDaSemana.Equals("qui"))
                            {
                                if (agendamento._Profissional.Quinta.Equals(null) || agendamento._Local.Quinta.Equals(null))
                                    return false;
                            }
                            else
                            {
                                if (diaDaSemana.Equals("sex"))
                                {
                                    if (agendamento._Profissional.Sexta.Equals(null) || agendamento._Local.Sexta.Equals(null))
                                        return false;
                                }
                                else
                                {
                                    if (diaDaSemana.Equals("sab"))
                                    {
                                        if (agendamento._Profissional.Sabado.Equals(null) || agendamento._Local.Sabado.Equals(null))
                                            return false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }

        public bool ValidaHorarioLivreProfissional(Agendamento agendamento)
        {
            Contexto contexto = new Contexto();

            DateTime inicioConsulta = agendamento.HoraConsulta.AddMinutes(-agendamento.TempoEmMinutosConsulta);
            DateTime terminoConsulta = agendamento.HoraConsulta.AddMinutes(agendamento.TempoEmMinutosConsulta);

            var a = (from x in contexto.Agendamentos
                     where x.ProfissionalID.Equals(agendamento.ProfissionalID) &&
                     (x.HoraConsulta > inicioConsulta && x.HoraConsulta < terminoConsulta)
                     select x).ToList();

            if (a.Any())
                return false;
            else
                return true;
        }

        public bool ValidaHorarioLivreLocal(Agendamento agendamento)
        {
            Contexto contexto = new Contexto();

            DateTime inicioConsulta = agendamento.HoraConsulta.AddMinutes(-agendamento.TempoEmMinutosConsulta);
            DateTime terminoConsulta = agendamento.HoraConsulta.AddMinutes(agendamento.TempoEmMinutosConsulta);

            var a = (from x in contexto.Agendamentos
                     where x.LocalID.Equals(agendamento.LocalID) &&
                     (x.HoraConsulta > inicioConsulta && x.HoraConsulta < terminoConsulta)
                     select x).ToList();

            if (a.Any())
                return false;
            else
                return true;
        }

    }
}

