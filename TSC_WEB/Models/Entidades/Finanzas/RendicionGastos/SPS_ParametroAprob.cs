using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPS_ParametroAprob
    {
        public int opcion { get; set; }
        public string usuario { get; set; }
        public int nivelInterfaz { get; set; }
        public IEnumerable<E_SolicitudID> solicitudesArray { get; set; }
    }
}