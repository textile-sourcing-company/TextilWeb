using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPS_ComprobanteParametro
    {
        public int opcion { get; set; }
        public int idRendCompte { get; set; }
        public int idTipoComp { get; set; }
        public string serieNumero { get; set; }
        public string rucDNI { get; set; }
        public string rs { get; set; }
        public string codigoPC { get; set; }
        public string obs { get; set; }
        public DateTime fechaEmision { get; set; }
        public string periodo { get; set; }
        public int ceco { get; set; }
        public int idTipoMod { get; set; }

        public int idSolicitud { get; set; }
        public int secuencia { get; set; }

        public int idRendCompteDet { get; set; }
        public DateTime fecha { get; set; }
        public string detalle1 { get; set; }
        public string detalle2 { get; set; }
        public string codum { get; set; }
        public decimal valorunit { get; set; }
        public int cantidad { get; set; }
        public decimal total { get; set; }
    }
}