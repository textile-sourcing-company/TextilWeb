using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionRectilineos
{
    public class LiquidacionRectilineosPCPEntidad
    {
        public int idrectilineohead { get; set; }
        public string partida { get; set; }
        public string tipo { get; set; }
        public string fichas { get; set; }
        public string estado { get; set; }
        public string fechacrea { get; set; }
        public string usuariocrea { get; set; }
        public string fechaliquidacion { get; set; }
        public string usuarioliquidacion { get; set; }

        public string bgestado
        {
            get
            {
                string bg = string.Empty;

                if (estado == "PENDIENTE")
                {
                    bg = "bg-warning";
                }

                if (estado == "APROBADO")
                {
                    bg = "bg-success";
                }


                if (estado == "APERTURADO")
                {
                    bg = "bg-info";
                }

                if (estado == "PROCESO")
                {
                    bg = "bg-primary";
                }

                return bg;

            }
        }



    }
}