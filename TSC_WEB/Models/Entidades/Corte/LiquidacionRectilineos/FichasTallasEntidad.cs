using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionRectilineos
{
    public class FichasTallasEntidad
    {
        public int ficha { get; set; }
        public int cantidad { get; set; }

        public int orden { get; set; }
        public string talla { get; set; }

    }
}