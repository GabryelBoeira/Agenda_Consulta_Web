using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agenda_Consulta_Web.Models
{
    [Table("TBLocal")]
    public class Local
    {
        public int LocalID { get; set; }

        [Display(Name = "Nome da sala")]
        public string NomeLocal { get; set; }
        //Dias da semana
        public virtual bool Domingo { get; set; }
        public virtual bool Segunda { get; set; }
        public virtual  bool Terca { get; set; }
        public virtual bool Quarta { get; set; }
        public virtual bool Quinta { get; set; }
        public virtual bool Sexta { get; set; }
        public virtual  bool Sabado { get; set; }

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
