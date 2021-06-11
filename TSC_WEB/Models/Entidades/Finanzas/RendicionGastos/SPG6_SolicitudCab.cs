using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG6_SolicitudCab
    {
        public int idSolicitud { get; set; }
        public string codigo { get; set; }
        public string fechaEmision { get; set; }
        public int idEmpresa { get; set; }
        public int idEstado { get; set; }
        public string estado { get; set; }
        public int idTipoMod { get; set; }
    }
}