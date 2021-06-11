using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG8_IdSolicitudDet
    {
        public int idSolicitud { get; set; }
        public int secuencia { get; set; }
        public string codigo { get; set; }
        public string fecha { get; set; }
        public string estados { get; set; }
        public string tipoMoneda { get; set; }
        public string ceco { get; set; }
        public string conceptoDet { get; set; }
        public decimal valor { get; set; }
        public string observacion { get; set; }
        public string nota { get; set; }

    }
}