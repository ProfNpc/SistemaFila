using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaFila.Models
{
    public enum TipoFila
    {
        FILA_PORTA,
        FILA_VIRTUAL
    }

    public class Fila
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Comercio")]
        public int ComercioId { get; set; }
        [Key, Column(Order = 1)]
        public TipoFila Tipo { get; set; } = TipoFila.FILA_VIRTUAL;

        public Comercio Comercio { get; set; }

        public int? Tamanho { get; set; } = null;

    }
}