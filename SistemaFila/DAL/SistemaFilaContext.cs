using SistemaFila.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SistemaFila.DAL
{
    public class SistemaFilaContext : DbContext
    {
        public SistemaFilaContext() : base("SistemaFilaContext")
        {

        }

        public DbSet<Comercio> Comercios { get; set; }
        public DbSet<Fila> Filas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}