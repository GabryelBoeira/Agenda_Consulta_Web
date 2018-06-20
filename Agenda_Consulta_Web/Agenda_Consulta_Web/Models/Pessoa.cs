﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Agenda_Consulta_Web.Models
{
    //cria a classe que corresponde as caracteristicas que seram usados como base 
    public abstract class Pessoa
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Celular { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string Cpf { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DtNascimento { get; set; } 

 
    }
}
