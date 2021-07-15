using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPS_ComprobanteParametroTodo
    {
        public int opcion { get; set; }
        public int idRendCompte { get; set; }
        public int idTipoComp { get; set; }
        public string serieNumero { get; set; }
        public string rucDNI { get; set; }
        public string rs { get; set; }
        public string codigoPC { get; set; }
        public decimal monto { get; set; }
        public string obs { get; set; }
        public DateTime fechaEmision { get; set; }
        public string periodo { get; set; }
        public int codceco { get; set; }
        public int idTipoMod { get; set; }

        public IEnumerable<E_SolicitudSeq> solitudSeqArray { get; set; }
        public IEnumerable<F18_ComprobanteDetalle> detalleComprobanteArray { get; set; }

    }
}