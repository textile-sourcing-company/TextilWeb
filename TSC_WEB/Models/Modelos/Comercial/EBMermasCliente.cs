using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Comercial
{
    public class EBMermasCliente
    {
        public string CLIENTE { get; set; }
        public string CGC9 { get; set; }
        public string CGC4 { get; set; }
        public string CGC2 { get; set; }
        public int SEQ_LIMITE { get; set; }
        public int LIMITE_INFERIOR { get; set; }
        public int LIMITE_SUPERIOR { get; set; }
        public int QTDE_PERDA { get; set; }
        public decimal PERC_PERDA { get; set; }

    }
}