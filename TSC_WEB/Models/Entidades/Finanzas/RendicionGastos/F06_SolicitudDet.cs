using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class F06_SolicitudDet
    {
        public int idSolicitud { get; set; }
        public int codCeCo { get; set; }
        public string codMotivo { get; set; }
        public decimal valor { get; set; }
        public string nota { get; set; }
    }
}