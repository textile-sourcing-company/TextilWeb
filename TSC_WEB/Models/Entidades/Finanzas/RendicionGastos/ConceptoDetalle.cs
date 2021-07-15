using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class ConceptoDetalle
    {
        public int idConceptoDet { get; set; }
        public int codCeCo { get; set; }
        public int secuencia { get; set; }
        public int seleccionadoDet { get; set; }
        public int cantDias { get; set; }
        public decimal montoSolicitado { get; set; }
        public string observacionDet { get; set; }
    }
}