using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPS_CpteReembolsoParametro
    {

        public int opcion { get; set; }
        public int idRendCompte { get; set; }
        public int idTipoComp { get; set; }

        public string nserie { get; set; }
        public string ndocof { get; set; }

        public string rucDNI { get; set; }
        public string rs { get; set; }
        public string codigoPC { get; set; }
        public string obs { get; set; }
        public string fechaEmision { get; set; }
        public int codceco { get; set; }
        public int idTipoMod { get; set; }
        public decimal porcentajeIGV { get; set; }
        public string obs1 { get; set; }
        public string obs2 { get; set; }
        public string fecha1 { get; set; }
        public string fecha2 { get; set; }
        public string periodo { get; set; }
        public string observacionDet { get; set; }
        public string sede { get; set; }

        public IEnumerable<SPS_CpteReembolsoParametroDet> detalleComprobanteArray { get; set; }
    }
}