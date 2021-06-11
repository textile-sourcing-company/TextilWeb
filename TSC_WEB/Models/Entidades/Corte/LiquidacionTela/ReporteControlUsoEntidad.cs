using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionTela
{
    public class ReporteControlUsoEntidad
    {



        public string partida { get; set; }
        public string  ficha { get; set; }
        public string f_registro { get; set; }
        public string tizado { get; set; }
        public string tendido { get; set; }
        public string corte { get; set; }
        public string cantidadprogramada { get; set; }
        public string versiones { get; set; }
        public string combo { get; set; }
        public string maximo { get; set; }



        public bool etapas {
            get
            {
                return (tizado != string.Empty && tendido != string.Empty && corte != string.Empty) ? true : false;
            }
        }



    }
}