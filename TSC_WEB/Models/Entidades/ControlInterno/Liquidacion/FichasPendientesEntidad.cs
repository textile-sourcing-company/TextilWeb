using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.ControlInterno.Liquidacion
{
    public class FichasPendientesEntidad
    {
        public int pedido { get; set; }
        public int ficha { get; set; }
        public string kilo { get; set; }

        public string fechadespacho { get; set; }
        public string confirmado { get; set; }
        public string cliente { get; set; }


    }
}