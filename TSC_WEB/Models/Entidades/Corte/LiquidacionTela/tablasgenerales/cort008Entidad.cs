using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionTela.tablasgenerales
{
    public class cort008Entidad
    {
        public int ficha { get; set; }
        public string partida { get; set; }
        public int combo { get; set; }
        public int version { get; set; }
        public string tipotela { get; set; }
        public int turno { get; set; }
        public double orillos { get; set; }
        public double extremos { get; set; }
        public double entrecorte { get; set; }
        public string u_registro { get; set; }
        public string etapa { get; set; }
        public string estadotendido { get; set; }
        public string estadocorte { get; set; }


    }
}