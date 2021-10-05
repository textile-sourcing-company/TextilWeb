using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG69_DetalleCabecera
    {
        public int idSolicitud { get; set; }
        public string fecha { get; set; }
        public string codigo { get; set; }
        public string observacion { get; set; }
        public string estado { get; set; }
        public string nivelAprobacion { get; set; }
        public string moneda { get; set; }
        public decimal total { get; set; }
        public string usuSolicitante { get; set; }
        public string ceco { get; set; }
        public string colaborador { get; set; }
        public string fechaAplicacion { get; set; }

        public List<SPG70_SolicitudDetalle> listaDetalle { get; set; }
    }
}