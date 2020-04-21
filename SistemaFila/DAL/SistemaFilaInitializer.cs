using SistemaFila.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaFila.DAL
{
    public class SistemaFilaInitializer :
        //System.Data.Entity.DropCreateDatabaseIfModelChanges<SistemaFilaContext>
        System.Data.Entity.DropCreateDatabaseAlways<SistemaFilaContext>

    {
        protected override void Seed(SistemaFilaContext context)
        {
            
            var comercios = new List<Comercio>
            {
                new Comercio
                {
                    NomeFantasia = "MercadinhoDaEsquina",
                    Endereco = "Rua da Esquina, 100",
                    Status = StatusComercio.FECHADO,
                    Cpnj = "12.345.678/0001-90",
                    Capacidade = 10
                },
                new Comercio
                {
                    NomeFantasia = "Farmacia Panaceia",
                    Endereco = "Rua da Esquina, 110",
                    Status = StatusComercio.FECHADO,
                    Cpnj = "21.345.678/0001-90",
                    Capacidade = 5
                }
            };

            comercios.ForEach(c => context.Comercios.Add(c));
            context.SaveChanges();

            /*
            comercios.ForEach(c => context.Filas.Add(
                new Fila
                {
                    Comercio = c,
                    Tipo = TipoFila.FILA_PORTA,
                    Tamanho = 5
                })
            );

            comercios.ForEach(c => context.Filas.Add(
                new Fila
                {
                    Comercio = c,
                    Tipo = TipoFila.FILA_VIRTUAL,
                    Tamanho = 5
                })
            );

            context.SaveChanges();
            */

        }
    }
}