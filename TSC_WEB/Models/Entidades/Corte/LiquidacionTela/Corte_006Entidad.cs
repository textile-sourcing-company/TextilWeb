using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte
{
    public class Corte_006Entidad
    {
        public string PARTIDA { get; set; }
        public decimal MER_PUNTAS { get; set; }
        public decimal MER_RETAZOSMAS { get; set; }
        public decimal MER_RETAZOSMEN { get; set; }
        public decimal EMPALMES { get; set; }
        public decimal DEVO_PRIMERA { get; set; }
        public decimal DEVO_SEGUNDA { get; set; }
        public decimal CONOS { get; set; }
        public decimal PLASTICO { get; set; }
        public decimal KGENTREGADOS { get; set; }

        public string U_REGISTRO { get; set; }

        // 
        public string KG_ADICIONALES { get; set; }
        public string MOT_KGADICIONAL { get; set; }

        public int DEVO_PRIMERA_MOT { get; set; }
        public int DEVO_SEGUNDA_MOT { get; set; }

        public string ESTADO { get; set; }

        public string fecha { get; set; }



    }
}