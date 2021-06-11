using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Logistica.Graficos
{
    public class Grafico2TortaPresupuesto
    {
        public int CODFAMILIA { get; set; }
        public string FAMILIA { get; set; }
        public decimal PRESUPUESTO { get; set; }
        public string COLOR { get; set; }
    }
}