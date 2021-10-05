using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG62_SolicitudCpte
    {
        public int idSolicitud { get; set; }
        public int secuencia { get; set; }
        public decimal valor { get; set; }
        public decimal totalrestante { get; set; }
    }
}