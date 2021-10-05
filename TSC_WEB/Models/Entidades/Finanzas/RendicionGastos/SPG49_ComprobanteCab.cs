using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG49_ComprobanteCab
    {
        public int idRendCompte { get; set; }
        public int idTipoComp { get; set; }
        public string serieNumero { get; set; }
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
        public string simbolo { get; set; }
        public int idTipoMod { get; set; }
        public decimal totalCmpbnteBI { get; set; }
    }
}