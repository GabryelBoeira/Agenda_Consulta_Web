namespace Agenda_Consulta_Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Endereco")]
    public partial class Endereco
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Endereco()
        {
            Local = new HashSet<Local>();
        }

        public int EnderecoID { get; set; }

        public string Cep { get; set; }

        public string Rua { get; set; }

        public int Numero { get; set; }

        public string Complemento { get; set; }

        public string Cidade { get; set; }

        public string Uf { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Local> Local { get; set; }
    }
}
