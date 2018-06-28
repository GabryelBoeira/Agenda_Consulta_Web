using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.UI;

namespace Agenda_Consulta_Web.Models
{
    [Table("Profissional")]
    public class Profissional : Pessoa 
    {
        [Required]
        [Display(Name = "CRM")]
        public string ResgistroProfissional { get; set; }

        [Required]
        public string Especialidade { get; set; }

        //Dias da semana
        public virtual bool Domingo { get; set; }

        [Display(Name = "Segunda-Feira")]
        public virtual bool Segunda { get; set; }

        [Display(Name = "Terca-Feira")]
        public virtual bool Terca { get; set; }

        [Display(Name = "Quarta-Feira")]
        public virtual bool Quarta { get; set; }

        [Display(Name = "Quinta-Feira")]
        public virtual bool Quinta { get; set; }

        [Display(Name = "Sexta-Feira")]
        public virtual bool Sexta { get; set; }

        public virtual bool Sabado { get; set; }

        //horario de trabalho
        [Display(Name = "Horario inicial")]
        [DataType(DataType.Time)]
        public DateTime HrInicio { get; set; }

        [Display(Name = "Horario final")]
        [DataType(DataType.Time)]
        public DateTime HrFim { get; set; }
        
    }
}
