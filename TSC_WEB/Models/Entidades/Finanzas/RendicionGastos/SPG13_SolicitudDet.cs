using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG13_SolicitudDet
    {
        public string codigo { get; set; }
        public int secuencia { get; set; }
        public string fecha { get; set; }
        public string ceco { get; set; }
        public string conceptoDet { get; set; }
        public string moneda { get; set; }
        public decimal valor { get; set; }
    }
}