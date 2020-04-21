using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaFila.Models
{
    public enum StatusComercio
    {
        ABERTO,
        FECHADO
    }

    public class Comercio
    {
        public int ComercioId { get; set; }
        public string Cpnj { get; set; }
        public string NomeFantasia { get; set; }
        public string Endereco { get; set; }
        //Aberto ou Fechado
        public StatusComercio Status { get; set; } 
        //Numero de pessoas dentro do mercado
        public int Capacidade { get; set; } 

        public virtual List<Fila> Filas { get; set; }
    }
}