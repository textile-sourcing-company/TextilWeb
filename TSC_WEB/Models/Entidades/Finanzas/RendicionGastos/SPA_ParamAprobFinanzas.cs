using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPA_ParamAprobFinanzas
    {
        public int opcion { get; set; }
        public int opcionTipoAprob { get; set; }
        public int idSolicitud { get; set; }
        public int[] secuencia { get; set; }
        public decimal valorEntrega { get; set; }
    }
}