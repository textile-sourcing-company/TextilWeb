using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.RendicionGastos
{
    public class ReporteDJPDFViewModel
    {
        public SPG51_DecJurCab declaracionJuradaCab { get; set; }


        public List<SPG52_DecJurDet> declaracionJuradaDet_1 { get; set; }
        public List<SPG52_DecJurDet> declaracionJuradaDet_2 { get; set; }
    }
}