namespace Agenda_Consulta_Web
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Agendamentoes> Agendamentoes { get; set; }
        public virtual DbSet<Endereco> Endereco { get; set; }
        public virtual DbSet<Local> Local { get; set; }
        public virtual DbSet<Pacientes> Pacientes { get; set; }
        public virtual DbSet<Profissional> Profissional { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pacientes>()
                .HasMany(e => e.Agendamentoes)
                .WithRequired(e => e.Pacientes)
                .HasForeignKey(e => e.PacienteID);
        }
    }
}
