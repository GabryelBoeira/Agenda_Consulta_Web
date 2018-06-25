﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.UI;

namespace Agenda_Consulta_Web.Models
{
    public class Profissional : Pessoa 
    {
        [Required]
        [Display(Name = "CRM")]
        public string ResgistroProfissional { get; set; }

        [Required]
        public string Especialidade { get; set; }

        //Dias da semana
        [BindableAttribute(true, BindingDirection.TwoWay)]
        [ThemeableAttribute(false)]
        public virtual bool Domingo { get; set; }

        [Display(Name = "Segunda-Feira")]
        [BindableAttribute(true, BindingDirection.TwoWay)]
        [ThemeableAttribute(false)]
        public virtual bool Segunda { get; set; }

        [Display(Name = "Terca-Feira")]
        [BindableAttribute(true, BindingDirection.TwoWay)]
        [ThemeableAttribute(false)]
        public virtual bool Terca { get; set; }

        [Display(Name = "Quarta-Feira")]
        [BindableAttribute(true, BindingDirection.TwoWay)]
        [ThemeableAttribute(false)]
        public virtual bool Quarta { get; set; }

        [Display(Name = "Quinta-Feira")]
        [BindableAttribute(true, BindingDirection.TwoWay)]
        [ThemeableAttribute(false)]
        public virtual bool Quinta { get; set; }

        [Display(Name = "Sexta-Feira")]
        [BindableAttribute(true, BindingDirection.TwoWay)]
        [ThemeableAttribute(false)]
        public virtual bool Sexta { get; set; }

        [BindableAttribute(true, BindingDirection.TwoWay)]
        [ThemeableAttribute(false)]
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
