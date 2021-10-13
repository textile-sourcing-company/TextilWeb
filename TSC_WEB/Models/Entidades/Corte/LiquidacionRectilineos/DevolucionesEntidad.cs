using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionRectilineos
{
    public class DevolucionesEntidad
    {
        public int? idrectilineodevolucion { get; set; }
        public int? idrectilineohead { get; set; }
        public string talla { get; set; }
        public int ordentalla { get; set; }

        public decimal cantidad { get; set; }
        public decimal kilos { get; set; }

    }
}