using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG30_ConceptoDetalle
    {
        public int idConceptoDet { get; set; }
        public string conceptoDet { get; set; }
        public string consideracion { get; set; }

        public int existe { get; set; }
        public int cantDias { get; set; }
        public decimal montoSolicitado { get; set; }
        public string observacionDet { get; set; }


        public decimal montoMaximoS { get; set; }
        public decimal montoMaximoD { get; set; }


    }
}