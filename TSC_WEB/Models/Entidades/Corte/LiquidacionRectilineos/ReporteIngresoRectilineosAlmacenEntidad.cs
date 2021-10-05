using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionRectilineos
{
    public class ReporteIngresoRectilineosAlmacenEntidad
    {
        public string partidatela { get; set; }
        public string partidarectilineo { get; set; }
        public string tiporectilineo { get; set; }
        public string observacion { get; set; }
        public string talla { get; set; }
        public int orden { get; set; }
        public decimal cantidadprimera { get; set; }
        public decimal cantidadsegunda { get; set; }
        public decimal kilostotales { get; set; }

        public decimal totalcantidadfila { get;set;}

        public decimal kilosportalla
        {

            get
            {

                return totalcantidad * consumotalla;
            }
        }

        public decimal totalcantidad {

            get {
                //decimal cantpri = cantidadprimera == null ? 0 : cantidadprimera;
                //decimal cantseg = cantidadsegunda == null ? 0 : cantidadsegunda;

                return cantidadprimera + cantidadsegunda;
            }
        }

        public decimal consumotalla { 
            get {

                decimal resultado;

                //if (kilostotales != null) {
                    resultado = totalcantidadfila > 0 ? kilostotales / totalcantidadfila : 0;
                //}
                //else
                //{
                    //resultado = 0;
                //}

                return resultado;

            } 
        }


    }
}