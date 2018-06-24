﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Agenda_Consulta_Web.Models
{
    public class Local
    {
        public int LocalID { get; set; }

        [Display(Name = "Nome da sala")]
        public string NomeLocal { get; set; }
        //Dias da semana
        public bool? Domingo { get; set; }
        public bool? Segunda { get; set; }
        public bool? Terca { get; set; }
        public bool? Quarta { get; set; }
        public bool? Quinta { get; set; }
        public bool? Sexta { get; set; }
        public bool? Sabado { get; set; }

        //horario de trabalho
        [Display(Name = "Horario inicial")]
        [DataType(DataType.Time)]
        public DateTime HrInicio { get; set; }

        [Display(Name = "Horario final")]
        [DataType(DataType.Time)]
        public DateTime HrFim { get; set; }



        public int EnderecoID { get; set; }
        public virtual Endereco _Endereco { get; set; }
    }
}
