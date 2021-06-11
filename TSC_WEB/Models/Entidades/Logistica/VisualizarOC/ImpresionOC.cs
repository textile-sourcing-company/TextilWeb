using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Logistica.VisualizacionOC
{
    public class ImpresionOC
    {
       public VisualizacionOcCab Cab { get; set; }
       public List<VisualizacionOcDet>  Det { get; set; }
    }
}