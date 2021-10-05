using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class ReportePDFViewModel
    {
        public SPG53_RpteCabIndividual cabecera { get; set; }
        public List<SPG54_RpteDetIndividual> detalle { get; set; }
    }
}