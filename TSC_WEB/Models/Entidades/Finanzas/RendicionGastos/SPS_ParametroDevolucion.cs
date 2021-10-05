using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPS_ParametroDevolucion
    {
        public int opcion { get; set; }
        public int idSolicitud { get; set; }
        public int secuencia { get; set; }
        public decimal monto { get; set; }
    }
}