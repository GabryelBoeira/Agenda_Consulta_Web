namespace Agenda_Consulta_Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Profissional")]
    public partial class Profissional
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Profissional()
        {
            Agendamentoes = new HashSet<Agendamentoes>();
        }

        public int ID { get; set; }

        [Required]
        public string ResgistroProfissional { get; set; }

        [Required]
        public string Especialidade { get; set; }

        public bool Domingo { get; set; }

        public bool Segunda { get; set; }

        public bool Terca { get; set; }

        public bool Quarta { get; set; }

        public bool Quinta { get; set; }

        public bool Sexta { get; set; }

        public bool Sabado { get; set; }

        public DateTime HrInicio { get; set; }

        public DateTime HrFim { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Celular { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(11)]
        public string CPF { get; set; }

        public DateTime DtNascimento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Agendamentoes> Agendamentoes { get; set; }
    }
}
