using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG47_CmpbteCabecera
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
        public decimal totalCmpbnteIGV { get; set; }

        public string simbolo { get; set; }
        public int idTipoMod { get; set; }

        public string cargo { get; set; }
        public decimal sumTotal { get; set; }

        public decimal totalEntregado { get; set; }


        public string ciudad { get; set; }
        public string pais { get; set; }
        public string fdesde { get; set; }
        public string fhasta { get; set; }

        public string firma_jefe { get; set; }
        public string firma_usuario { get; set; }

    }
}