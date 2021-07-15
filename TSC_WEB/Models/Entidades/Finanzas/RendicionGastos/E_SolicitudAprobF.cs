using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class E_SolicitudAprobF
    {
        public int secuencia { get; set; }
        public int opcion { get; set; }
        public int opcionAprob { get; set; }
        public decimal montoIngresado { get; set; }
    }
}