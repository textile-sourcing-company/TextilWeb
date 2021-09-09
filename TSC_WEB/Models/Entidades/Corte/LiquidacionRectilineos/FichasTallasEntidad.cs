using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionRectilineos
{
    public class FichasTallasEntidad
    {
        public int? idrectilineoficha { get; set; }
        public int realprimera { get; set; }
        public decimal pesonetoreal { get; set; }
        public decimal consumoprogramado { get; set; }
        public decimal cantidadsegundadespachada { get; set; }



        public int ficha { get; set; }
        public decimal cantidadprimeraprogramada { get; set; }

        public int orden { get; set; }
        public string talla { get; set; }
        public int pedido { get; set; }
        public string estilotsc { get; set; }
        public string estilocliente { get; set; }
        public string color { get; set; }

        public decimal pesodespachado { get; set; }
        public decimal cantidadprimeradespachada { get; set; }
        public decimal cantliquidada { get; set; }
        public decimal consumodespachado
        {
            get
            {
                return cantidadprimeradespachada > 0 ? pesodespachado / cantidadprimeradespachada : 0;
            }                
        }

        public bool cantliquidadaestado
        {
            get;set;
            //get
            //{
            //    if (cantliquidada > 0)
            //    {
            //        cantidad = cantliquidada;
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //    //return cantliquidada > 0 ? true : false;
            //}
            //set
            //{

            //}
        }

        public decimal totalunidades
        {
            get
            {
                return cantidadprimeraprogramada + cantidadsegundadespachada;
            }
        }

        public decimal pendiente
        {
            get
            {
                return cantidadprimeraprogramada - realprimera;
            }
        }

        public decimal pesobrutotalla
        {
            get
            {
                return cantidadprimeraprogramada * consumoprogramado;
            }
        }

        public decimal pesobrutorealtalla
        {
            get
            {
                return realprimera * consumoprogramado;
            }
        }

    }
}