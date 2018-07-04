using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Agenda_Consulta_Web.Models
{
    /// <summary>
    /// ViewModel criada para adicionar os dados e local junto de endereço somente os campos necessario para serem 
    /// utilizados pela cotroller e view para gerarem 
    /// </summary>
    public class LocalViewModel
    {
        //adicionado pois para gerar automaticamete neessida de um id para ser gerada.
        public int ID { get; set; }

        [Display(Name = "Nome da sala")]
        public string NomeLocal { get; set; }
        //Dias da semana
        public virtual bool Domingo { get; set; }
        public virtual bool Segunda { get; set; }
        public virtual bool Terca { get; set; }
        public virtual bool Quarta { get; set; }
        public virtual bool Quinta { get; set; }
        public virtual bool Sexta { get; set; }
        public virtual bool Sabado { get; set; }

        //horario de trabalho
        [Display(Name = "Horario inicial")]
        [DataType(DataType.Time)]
        public DateTime HrInicio { get; set; }

        [Display(Name = "Horario final")]
        [DataType(DataType.Time)]
        public DateTime HrFim { get; set; }

        [Required(ErrorMessage = "Cep obrigatorio")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Rua obrigatorio")]
        public string Rua { get; set; }

        [Required(ErrorMessage = "Número obrigatorio")]
        public int Numero { get; set; }

        public string Complemento { get; set; }

        [Required(ErrorMessage = "Cidade obrigatorio")]
        public string Cidade { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Estado obrigatorio")]
        public string Uf { get; set; }

    }
}