using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionRectilineos
{
    public class ReporteEntidad
    {
        public string usuariocrea { get; set; }
        public string tipo { get; set; }
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

        public decimal pendienteliquidacionkg
        {
            get
            {
                return pesoprogramado - pesonetoreal;

            }
        }

        public decimal pesonetoreal { get; set; }
        public decimal pesoprogramado { get; set; }
        public decimal mermahilos { get; set; }
        public decimal mermarecorte { get; set; }


        public decimal porcentajeliquidaciontalla
        {
            get
            {
                return programado > 0 ? realprimera / programado : 0;  
            }
        }

        public decimal porcentajeliquidaciontallakg
        {
            get
            {
                return pesoprogramado > 0 ? pesonetoreal / pesoprogramado : 0;
            }
        }

        public decimal mermaprogramadarecorte { get; set; }
        public decimal mermaprogramadahilo { get; set; }

        public decimal mermarealrecorte { get; set; }
        public decimal mermarealhilo { get; set; }

        public decimal variacionmermaderecorte
        {
            get
            {
                return mermarealrecorte - mermaprogramadarecorte;
            }
        }

        public decimal variacionmermadehilo
        {
            get
            {
                return mermarealhilo - mermaprogramadahilo;
            }
        }

        public decimal variacionmermaderecortepor
        {
            get
            {
                return mermaprogramadarecorte > 0 ? variacionmermaderecorte / mermaprogramadarecorte : 0;
            }
        }

        public decimal variacionmermadehilopor
        {
            get
            {
                return mermaprogramadahilo > 0 ? variacionmermadehilo / mermaprogramadahilo : 0;
            }
        }

    }
}