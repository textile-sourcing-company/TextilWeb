using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionRectilineos
{
    public class ReporteEntidad
    {
        public string usuariocrea { get; set; }
        public string fechamod { get; set; }
        public int ficha { get; set; }
        public int ordentalla { get; set; }
        public string partida { get; set; }
        public int pedido { get; set; }
        public string combo { get; set; }
        public string estilotsc { get; set; }
        public string estilocliente { get; set; }
        public string talla { get; set; }
        public decimal realprimera { get; set; }
        public decimal programado { get; set; }
        public decimal pendiente { 
            get {
                return programado - realprimera;
                     
            } 
        }

        public decimal porcentajeliquidaciontalla
        {
            get
            {
                return programado > 0 ? realprimera / programado : 0;  
            }
        }


    }
}