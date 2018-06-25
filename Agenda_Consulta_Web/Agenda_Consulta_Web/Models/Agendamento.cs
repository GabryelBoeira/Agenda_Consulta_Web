using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agenda_Consulta_Web.Models
{
    public class Agendamento
    {
        //virtual serve para facilitar a visualização do objeto que retorna do banco de dados
        public int AgendamentoID { get; set; }

        
        public int LocalID { get; set; }
        public virtual Local _Local { get; set; }

        public int PacienteID { get; set; }
        public virtual Profissional _Paciente { get; set; }

        public int ProfissionalID { get; set; }
        public virtual Profissional _Profissional { get; set; }


        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Data e hora da consulta")]
        public DateTime DataHoraConsulta { get; set; }

        //tempo de cada consulta realizada
        //public DateTime TempoConsulta = DateTime.Now.AddMinutes(30);
        public int TempoEmMinutosConsulta        {
            get
            {
                return 30;
            }
        }
       
    }
}
