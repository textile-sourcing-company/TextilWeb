using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionRectilineos
{
    public class IngresoRectilineosEntidad
    {
        public string partida { get; set; }
        public string tiporectilineo { get; set; }
        public string talla { get; set; }
        public decimal? cantidad { get; set; }
        public decimal? cantidadsegunda { get; set; }

        public int tipocantidad { get; set; }
        public string usuario { get; set; }

    }
}