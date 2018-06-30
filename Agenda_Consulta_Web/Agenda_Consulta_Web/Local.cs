namespace Agenda_Consulta_Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Local")]
    public partial class Local
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Local()
        {
            Agendamentoes = new HashSet<Agendamentoes>();
        }

        public int LocalID { get; set; }

        public string NomeLocal { get; set; }

        public bool Domingo { get; set; }

        public bool Segunda { get; set; }

        public bool Terca { get; set; }

        public bool Quarta { get; set; }

        public bool Quinta { get; set; }

        public bool Sexta { get; set; }

        public bool Sabado { get; set; }

        public DateTime HrInicio { get; set; }

        public DateTime HrFim { get; set; }

        public int EnderecoID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Agendamentoes> Agendamentoes { get; set; }

        public virtual Endereco Endereco { get; set; }
    }
}
