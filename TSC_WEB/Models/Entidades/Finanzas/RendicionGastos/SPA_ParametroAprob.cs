using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPA_ParametroAprob
    {
        public int idsolicitud { get; set; }
        public IEnumerable<E_SolicitudAprobF> solicitudesArray { get; set; }
    }
}