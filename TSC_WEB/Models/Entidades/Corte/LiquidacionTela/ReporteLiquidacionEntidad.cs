using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace TSC_WEB.Models.Entidades.Corte.LiquidacionTela
{
    public class ReporteLiquidacionEntidad
    {
        public string fechaliquidacion { get; set; }
        public int fichas { get; set; }
        public int pedido { get; set; }
        public string partida { get; set; }
        public string programa { get; set; }
        public string estilo { get; set; }
        public string color { get; set; }
        public string codigotela { get; set; }
        public string descripciontela { get; set; }
        public double prendasprogramadas { get; set; }
        public double prendasreales { get; set; }
        public double diferencia1 { get; set; }
        public double kilosprogramados { get; set; }
        public double kilostizados { get; set; }
        public double kilosreales { get; set; }
        public double largotizado { get; set; }
        public double largopanoreal { get; set; }
        public double tallas { get; set; }
        public double panos { get; set; }
        public double pesopanoprogramado { get; set; }
        public double pesopanoreal { get; set; }
        public double consnetoprogramado { get; set; }
        public double consnetoreal { get; set; }
        public double mermatendprog { get; set; }
        public double mermatendreal { get; set; }
        public double consbrutprog { get; set; }
        public double consbrutoreal { get; set; }
        public double diferencia2 { get; set; }
        public double eficprog { get; set; }
        public double eficreal { get; set; }
        public double diferencia3 { get; set; }
        public double puntas { get; set; }
        public double retazos { get; set; }
        public double empalmes { get; set; }
        public double mermaprog { get; set; }
        public double mermareal { get; set; }
        public double entrecorte { get; set; }
        public double orillos { get; set; }
        public double extremos { get; set; }

        public double realesliquidados { get; set; }

        //public double kilostendido { get; set; }

        public double conos { get; set; }
        public double bolsas { get; set; }
        public double dev1ra { get; set; }
        public double dev2da { get; set; }
        public string dev1ramotivo { get; set; }
        public string dev2damotivo { get; set; }
        public double deverp { get; set; }
        public string coderp { get; set; }
        public double anchprog { get; set; }
        public double anchreal { get; set; }
        public double direfencia4 { get; set; }
        public double anchotizadoutil { get; set; }
        public double diferenciametros { get; set; }
        public double diferenciametrosp { get; set; }
        public double densprog { get; set; }
        public double diferencia5 { get; set; }
        public double cuadretela { get; set; }
        public double diferencia6 { get; set; }
        public double adicional { get; set; }

        public double consumoponderado { get; set; }
        public double kilosdespachados { get; set; }

        public double collareta { get; set; }
        public string estadotendido { get; set; }
        public string estadocorte { get; set; }

        public string motdevolucion { get; set; }
        public string motadicional { get; set; }

        public string version { get; set; }
        public string combo { get; set; }

        public string usuario { get; set; }
        public string celula { get; set; }
        public string turno { get; set; }


        public double totalmermacorte
        {
            get
            {
                return entrecorte + orillos + extremos;
            }
        }

        public double totalmerma
        {
            get
            {
                return collareta + puntas + retazos + empalmes + conos + bolsas;
            }
        }




    }
}