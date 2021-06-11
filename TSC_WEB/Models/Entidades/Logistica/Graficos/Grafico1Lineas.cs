using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Logistica.Graficos
{
    public class Grafico1Lineas
    {
        public string PERIODO { get; set; }
        public decimal PRESUPUESTO { get; set; }
        public decimal CONSUMIDO { get; set; }
        public int CODP { get; set; }
    }
}