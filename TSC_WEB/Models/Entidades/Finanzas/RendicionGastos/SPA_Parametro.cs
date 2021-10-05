using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPA_Parametro
    {
        public int opcion { get; set; }
        public int opcionTipoAprob { get; set; }
        public int idSolicitud { get; set; }
        public int secuencia { get; set; }
        public decimal valorEntrega { get; set; }
        public string usuario { get; set; }
        public int nivelInterfaz { get; set; }
        public int idAnulado { get; set; }
        public int idAnuladoDet { get; set; }
    }
}