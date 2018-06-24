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
    public class AgendamentosController : Controller
    {
        // GET
        public ActionResult Index()
        {
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
        public ActionResult Create(Agendamento agendamento)
        {
            try
            {
                if (ValidaHoraAtendimento(agendamento))
                {
                    if (ValidaDiaDaSemanaAtendimento(agendamento))
                    {
                        if (ValidaHorarioLivreProfissional(agendamento))
                        {
                            if (ValidaHorarioLivreLocal(agendamento))
                            {
                                return RedirectToAction("Index");
                            }
                        }
                    }
                }
                return View(agendamento);
            }
            catch
            {
                return View(agendamento);
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

            Agendamento agendamento = contexto.Agendamentos.Find(id);

            if (agendamento == null)
            {
                return HttpNotFound();
            }

            return View(agendamento);
        }

        // POST
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                //exclui os dados
                Contexto contexto = new Contexto();
                Agendamento agendamento = contexto.Agendamentos.Find(id);

                contexto.Agendamentos.Remove(agendamento);
                contexto.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Area para gerar as validações antes de cadastrar um novo agendamento
        /// </summary>
        /// <param name="agendamento"></param>
        /// <returns></returns>
        public bool ValidaHoraAtendimento(Agendamento agendamento)
        {
            //Validando horário em que o profissional atende e o horário que o local está aberto para atendimento (não valida dia da semana e horário com consulta já marcada)
            if (agendamento.DataHoraConsulta.TimeOfDay < agendamento._Profissional.HrInicio.TimeOfDay ||
                agendamento.DataHoraConsulta.AddMinutes(agendamento.TempoEmMinutosConsulta).TimeOfDay > agendamento._Profissional.HrFim.TimeOfDay)
                return false;
            else
            {
                if (agendamento.DataHoraConsulta.TimeOfDay < agendamento._Local.HrInicio.TimeOfDay ||
                agendamento.DataHoraConsulta.AddMinutes(agendamento.TempoEmMinutosConsulta).TimeOfDay > agendamento._Local.HrFim.TimeOfDay)
                    return false;
            }
            return true;
        }

        public bool ValidaDiaDaSemanaAtendimento(Agendamento agendamento)
        {
            //dia da semana em formato ddd para comparação (dom, seg, ter, qua, qui, sex, sab)
            //Validando dia da semana disponível para profissional e local
            String diaDaSemana = agendamento.DataHoraConsulta.ToString("ddd");
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

            DateTime inicioConsulta = agendamento.DataHoraConsulta.AddMinutes(-agendamento.TempoEmMinutosConsulta);
            DateTime terminoConsulta = agendamento.DataHoraConsulta.AddMinutes(agendamento.TempoEmMinutosConsulta);

            var a = (from x in contexto.Agendamentos
                     where x.ProfissionalID.Equals(agendamento.ProfissionalID) &&
                     (x.DataHoraConsulta > inicioConsulta && x.DataHoraConsulta < terminoConsulta)
                     select x).ToList();

            if (a.Any())
                return false;
            else
                return true;
        }

        public bool ValidaHorarioLivreLocal(Agendamento agendamento)
        {
            Contexto contexto = new Contexto();

            DateTime inicioConsulta = agendamento.DataHoraConsulta.AddMinutes(-agendamento.TempoEmMinutosConsulta);
            DateTime terminoConsulta = agendamento.DataHoraConsulta.AddMinutes(agendamento.TempoEmMinutosConsulta);

            var a = (from x in contexto.Agendamentos
                     where x.LocalID.Equals(agendamento.LocalID) &&
                     (x.DataHoraConsulta > inicioConsulta && x.DataHoraConsulta < terminoConsulta)
                     select x).ToList();

            if (a.Any())
                return false;
            else
                return true;
        }

    }


}
