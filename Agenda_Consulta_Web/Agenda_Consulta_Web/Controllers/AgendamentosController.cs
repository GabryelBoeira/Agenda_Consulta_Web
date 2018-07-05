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
        LocaisController local = new LocaisController();
        ProfissionaisController prof = new ProfissionaisController();
        PacientesController paciente = new PacientesController(); 

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
        public ActionResult Create(Agendamento agendamento)
        {
            agendamento._Profissional = prof.profissionalDiasemana(agendamento);
            agendamento._LocalViewModel = local.localDiasemana(agendamento);
            agendamento._Paciente = paciente.pacienteDiasemana(agendamento);
                
            if (ModelState.IsValid)
            {
                if (agendamento.DataConsulta >= DateTime.Now) {

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

        /// <summary>
        /// Area para gerar as validações antes de cadastrar um novo agendamento
        /// </summary>
        /// <param name="agendamento"></param>
        /// <returns></returns>
        public bool ValidaHoraAtendimento(Agendamento agendamento)
        {
           
            //Validando horário em que o profissional atende e o horário que o local está aberto para atendimento (não valida dia da semana e horário com consulta já marcada)
            if (prof.ValidaHoraAtendimento(agendamento))
            {
                return false;
            }
            else
            {
                if (local.ValidaHoraAtendimento(agendamento))
                {
                    return false;
                }
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
                if (agendamento._Profissional.Domingo.Equals(null) || agendamento._LocalViewModel.Domingo.Equals(null))
                    return false;
            }
            else
            {
                if (diaDaSemana.Equals("seg"))
                {
                    if (agendamento._Profissional.Segunda.Equals(null) || agendamento._LocalViewModel.Segunda.Equals(null))
                        return false;
                }
                else
                {
                    if (diaDaSemana.Equals("ter"))
                    {
                        if (agendamento._Profissional.Terca.Equals(null) || agendamento._LocalViewModel.Terca.Equals(null))
                            return false;
                    }
                    else
                    {
                        if (diaDaSemana.Equals("qua"))
                        {
                            if (agendamento._Profissional.Quarta.Equals(null) || agendamento._LocalViewModel.Quarta.Equals(null))
                                return false;
                        }
                        else
                        {
                            if (diaDaSemana.Equals("qui"))
                            {
                                if (agendamento._Profissional.Quinta.Equals(null) || agendamento._LocalViewModel.Quinta.Equals(null))
                                    return false;
                            }
                            else
                            {
                                if (diaDaSemana.Equals("sex"))
                                {
                                    if (agendamento._Profissional.Sexta.Equals(null) || agendamento._LocalViewModel.Sexta.Equals(null))
                                        return false;
                                }
                                else
                                {
                                    if (diaDaSemana.Equals("sab"))
                                    {
                                        if (agendamento._Profissional.Sabado.Equals(null) || agendamento._LocalViewModel.Sabado.Equals(null))
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
                     where x.LocalViewModelID.Equals(agendamento.LocalViewModelID) &&
                     (x.HoraConsulta > inicioConsulta && x.HoraConsulta < terminoConsulta)
                     select x).ToList();

            if (a.Any())
                return false;
            else
                return true;
        }

    }
}