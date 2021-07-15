using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG8_SolicitudDetalle
    {
        public int idSolicitud { get; set; }
        public string codigo { get; set; }
        public string fecha { get; set; }
        public string estado { get; set; }
        public string tipoMoneda { get; set; }
        public string ceco { get; set; }
        public int idConceptoCab { get; set; }
        public string conceptoCab { get; set; }
        public int cantDias { get; set; }
        public string observacion { get; set; }
        public decimal valor { get; set; }
        public List<SPG25_ConceptoDetalle> conceptoDetalle { get; set; }
    }
}