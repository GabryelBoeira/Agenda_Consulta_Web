using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace Agenda_Consulta_Web.Models.DAL
{
    public class Contexto: DbContext
    {
        public Contexto() : base("strConn") {

            //(Apaga toda a base cada vez que executa)
            //Database.SetInitializer<Contexto>(new DropCreateDatabaseAlways<Contexto>());

            //Migrantions (Utilizar para produção)
            Database.SetInitializer<Contexto>(new DropCreateDatabaseIfModelChanges<Contexto>());

        }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Local> Locais { get; set; }
        public DbSet<Profissional> Profissionais { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }

    }
}