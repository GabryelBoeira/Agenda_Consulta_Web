namespace Agenda_Consulta_Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Pacientes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pacientes()
        {
            Agendamentoes = new HashSet<Agendamentoes>();
        }

        public int ID { get; set; }

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
