using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionTela
{
    public class ReporteTransaccionEntidad
    {
        public string ficha { get; set; }
        public string partida { get; set; }
        public string doc_movimento { get; set; }
        public string almacen { get; set; }
        public string cantidad { get; set; }
        public string rollo { get; set; }
        public string estado { get; set; }

    }
}