using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionTela.aperturaficha
{
    public class AperturaFichaEntidad
    {
        public string partida { get; set; }
        public int combo { get; set; }
        public int version { get; set; }
        public string tipotela { get; set; }
        public string fichaspartida { get; set; }
        public string fichasdespachada { get; set; }
        public string estadotendido { get; set; }
        public string estadocorte { get; set; }



    }
}