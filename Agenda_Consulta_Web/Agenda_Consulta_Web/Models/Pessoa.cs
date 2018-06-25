using System;
using System.ComponentModel.DataAnnotations;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agenda_Consulta_Web.Models
{
    //cria a classe que corresponde as caracteristicas que seram usados como base 
    public abstract class Pessoa 
    {
        public int ID { get; set; }
        [Required]
        public string Nome { get; set; }
       
        [Display(Name = "Contato")]
        public string Celular { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(11, ErrorMessage = "Deve conter 11 Digitos")]
        [Required(ErrorMessage = "CPF obrigatório")]
        public string CPF { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime DtNascimento { get; set; } 
 
    }
}
