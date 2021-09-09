using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionRectilineos
{
    public class ReporteIngresoRectilineosAlmacenEntidad
    {
        public string partidatela { get; set; }
        public string partidarectilineo { get; set; }
        public string tiporectilineo { get; set; }
        public string observacion { get; set; }
        public string talla { get; set; }
        public int orden { get; set; }
        public decimal? cantidadprimera { get; set; }
        public decimal? cantidadsegunda { get; set; }

    }
}