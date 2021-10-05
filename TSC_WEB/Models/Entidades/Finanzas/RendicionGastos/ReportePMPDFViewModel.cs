using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class ReportePMPDFViewModel
    {
        public SPG47_CmpbteCabecera planillaMovilidadCab { get; set; }

        public List<SPG48_CmpbteDetalle> planillaMovilidadDet { get; set; }
    }
}