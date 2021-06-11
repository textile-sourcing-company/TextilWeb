using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionTela.tablasgenerales
{
    public class cort006Entidad
    {
        public string partida { get; set; }
        public int ficha { get; set; }
        public int combo { get; set; }
        public int turno { get; set; }
        public int versiones { get; set; }
        public int reproceso { get; set; }
        public string tipo_tela { get; set; }
        public double mer_puntas { get; set; }
        public double mer_retazosmas { get; set; }
        public double mer_retazosmen { get; set; }
        public double empalmes { get; set; }
        public double devo_primera { get; set; }
        public int devo_primera_mot { get; set; }
        public double devo_segunda { get; set; }
        public int devo_segunda_mot { get; set; }
        public double conos { get; set; }
        public double plastico { get; set; }
        public string u_registro { get; set; }
        public string f_registro { get; set; }
        public double kg_adicionales { get; set; }
        public int mot_kgadicional { get; set; }
        public double kgentregados { get; set; }
        public int almacen { get; set; }
        public string estadotendido { get; set; }
        public string estadocorte { get; set; }


    }
}