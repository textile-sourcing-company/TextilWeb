using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionTela.reporteliquidacion
{
    public class IndicadorPcpLiquidacionEntidad
    {

        public datosgeneralesEntidad DataGeneral { get; set; }
        public List<datosfichaEntidad> DataFichas { get; set; }
        public List<datostendidoEntidad> DataTendido { get; set; }


    }
}