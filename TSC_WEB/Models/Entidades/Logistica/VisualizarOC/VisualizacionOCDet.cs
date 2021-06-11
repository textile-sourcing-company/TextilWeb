using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Logistica.VisualizacionOC
{
    // Se creara todas las columnas para poder Visualizar la OC
    public class VisualizacionOcDet
    {

       //DETALLE DE LA OC//
        public decimal?  I_Item       { get; set; }
        public string CH_MEDIDA      { get; set; }
        public string CH_CODIGO_TSC  { get; set; }
        public string CH_DESCRIPCION { get; set; }
        public string CH_CODIGO_PROVEEDOR { get; set; }
        public string I_OC_REFERENCIA { get; set; }
        public string I_OC_REFERENCIA_SEQ { get; set; }
        public decimal?  I_CANT      { get; set; }
        public decimal?  I_MAXIMO    { get; set; }
        public decimal?  I_P_UNIT    { get; set; }
        public decimal?  I_SUB_TOTAL { get; set; }
       
    }
}