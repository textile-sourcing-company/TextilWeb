using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG80_CpteCabecera
    {
        public string nombreEmpresa { get; set; }
        public string rucEmpresa { get; set; }


        public int idRendCompte { get; set; }
        public int idTipoComp { get; set; }
        public string serieNumero { get; set; }

        public string nserie { get; set; }
        public string ndocof { get; set; }

        public string rucDNI { get; set; }
        public string rs { get; set; }
        public string codigoPC { get; set; }
        public string obs { get; set; }
        public string fechaEmision { get; set; }
        public string periodo { get; set; }
        public int codceco { get; set; }
        public string ceco { get; set; }

        public decimal totalCmpbnte { get; set; }
        public decimal totalCmpbnteSolicitud { get; set; }
        public decimal totalCmpbnteBI { get; set; }

        public string simbolo { get; set; }
        public int idTipoMod { get; set; }

        public string cargo { get; set; }
        public decimal sumTotal { get; set; }

        public decimal totalEntregado { get; set; }

        public int idConceptoDet { get; set; }
        public int idConceptoCab { get; set; }
        public string observacionDet { get; set; }

        public string obs1 { get; set; }
        public string obs2 { get; set; }

        public string fecha1 { get; set; }
        public string fecha2 { get; set; }

        public int idSolicitud { get; set; }
        public decimal totalCmpbnteIGV { get; set; }
    }
}