using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionTela
{
    public class CaidasEntidad
    {
        public  int id { get; set; }
        public string partida { get; set; }
        public int combo { get; set; }
        public int version { get; set; }
        public string tipo_tela { get; set; }
        public string usuario_tendido { get; set; }
        public string fecha_tendido { get; set; }
        public int ficha { get; set; }
        public double cantidadprogramada { get; set; }
        public double cantidadreal { get; set; }
        public string estado { get; set; }
        public string fichacaida { get; set; }
        public string cantidadcaida { get; set; }


    }
}