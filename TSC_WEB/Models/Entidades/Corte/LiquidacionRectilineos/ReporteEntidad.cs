using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionRectilineos
{
    public class ReporteEntidad
    {
        public int idrectilineohead { get; set; }
        
        public string usuariocrea { get; set; }
        public string tipo { get; set; }
        public string cliente { get; set; }

        public string fechaliquidacion { get; set; }
        public string ficha { get; set; }
        public int ordentalla { get; set; }
        public string partida { get; set; }
        public string pedido { get; set; }
        public string combo { get; set; }
        public string estilotsc { get; set; }
        public string estilocliente { get; set; }
        public string talla { get; set; }

        public decimal realprimera { get; set; }
        public decimal programado { get; set; }

       

        public decimal pendienteunidades
        {
            get
            {
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


        public decimal porcentajeliquidacion
        {
            get
            {
                return programado > 0 ? realprimera * 100 / programado : 0;
            }
        }

        public decimal porcentajeliquidacion_excel
        {
            get
            {
                return programado > 0 ? realprimera  / programado : 0;
            }
        }

        public decimal porcentajeliquidacionkg
        {
            get
            {
                return pesoprogramado > 0 ? pesonetoreal * 100 / pesoprogramado : 0;
            }
        }

        public decimal porcentajeliquidacionkg_excel
        {
            get
            {
                return pesoprogramado > 0 ? pesonetoreal  / pesoprogramado : 0;
            }
        }

        public decimal mermaprogramadarecorte { get; set; }
        public decimal mermaprogramadahilo { get; set; }

        public decimal mermarealrecorte { get; set; }
        public decimal mermarealhilo { get; set; }

        //public decimal variacionmermaderecorte
        //{
        //    get
        //    {
        //        return mermarealrecorte - mermaprogramadarecorte;
        //    }
        //}

        //public decimal variacionmermadehilo
        //{
        //    get
        //    {
        //        return mermarealhilo - mermaprogramadahilo;
        //    }
        //}

        //public decimal variacionmermaderecortepor
        //{
        //    get
        //    {
        //        return mermaprogramadarecorte > 0 ? variacionmermaderecorte / mermaprogramadarecorte : 0;
        //    }
        //}

        //public decimal variacionmermadehilopor
        //{
        //    get
        //    {
        //        return mermaprogramadahilo > 0 ? variacionmermadehilo / mermaprogramadahilo : 0;
        //    }
        //}

    }
}