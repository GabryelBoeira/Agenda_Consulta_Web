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

        [Display(Name = "Sala para a consulta")]
        public int LocalID { get; set; }
        public virtual Local _Local { get; set; }

        [Display(Name = "Nome do paciente")]
        public int PacienteID { get; set; }
        public virtual Paciente _Paciente { get; set; }

        [Display(Name = "Nome do Profissional")]
        public int ProfissionalID { get; set; }
        public virtual Profissional _Profissional { get; set; }

        [Required]
        [Display(Name = "Data consulta")]
        [DataType(DataType.Date)]
        public DateTime DataConsulta { get; set; }

        [Required]
        [Display(Name = "Hora consulta")]
        [DataType(DataType.Time)]
        public DateTime HoraConsulta { get; set; }

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
