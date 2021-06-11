using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionTela.reporteliquidacion
{
    public class datosfichaEntidad
    {

        public string partida { get; set; } 
        public int combo { get; set; }
        public int version { get; set; }
        public string tipotela { get; set; }
        public int? ficha{ get; set; }
        public string estilo{ get; set; }
        public double prendasprogramado { get; set; }
        public double consumoprogramado{ get; set; }
        public double kilosprogramado { get; set; }
        public double porcentajekilosprogramado { get; set; }
        public double prendastizado { get; set; }
        public double consumotizado { get; set; }
        public double kilostizado { get; set; }
        public double prendasneto { get; set; }
        public double consumoneto { get; set; }
        public double kilosneto { get; set; }
        public double consumolinealbruto { get; set; }
        public double metrosbruto{ get; set; }
        public double consumopesobruto { get; set; }
        public double kilosbruto { get; set; }
    }
}