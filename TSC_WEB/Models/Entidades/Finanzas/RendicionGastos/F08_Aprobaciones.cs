using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class F08_Aprobaciones
    {
        public int idSolicitud { get; set; }
        public int idPermisoAprob { get; set; }
        public DateTime fechaFirma { get; set; }
    }
}