namespace Agenda_Consulta_Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Agendamentoes
    {
        [Key]
        public int AgendamentoID { get; set; }

        public int LocalID { get; set; }

        public int PacienteID { get; set; }

        public int ProfissionalID { get; set; }

        public DateTime DataConsulta { get; set; }

        public DateTime HoraConsulta { get; set; }

        public virtual Local Local { get; set; }

        public virtual Pacientes Pacientes { get; set; }

        public virtual Profissional Profissional { get; set; }
    }
}
