using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionRectilineos
{
    public class LiquidacionRectilineosPCPIndividualEntidad
    {

        public int idrectilineohead { get; set; }
        public string partida { get; set; }
        public decimal mermahilos { get; set; }
        public decimal mermarecorte { get; set; }
        public string tipo { get; set; }
        public string estado { get; set; }
        public int ficha { get; set; }
        public string talla { get; set; }
        public decimal realprimera { get; set; }
        public decimal pesonetoreal { get; set; }
        public int ordentalla { get; set; }
        public decimal programado { get; set; }
        public decimal pesoprogramado { get; set; }
        public decimal diferenciacantidad { 
            get {
                return programado - realprimera;
            } 
        }



    }
}