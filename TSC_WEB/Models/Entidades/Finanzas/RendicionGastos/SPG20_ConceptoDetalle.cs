using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class SPG20_ConceptoDetalle
    {
        public int idConceptoDet { get; set; }
        public string conceptoDet { get; set; }
        public string consideracion { get; set; }

        public decimal montoMaximoS { get; set; }
        public decimal montoMaximoD { get; set; }
    }
}