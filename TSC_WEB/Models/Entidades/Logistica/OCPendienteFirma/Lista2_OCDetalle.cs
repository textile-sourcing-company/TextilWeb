using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Logistica.OCPendienteFirma
{
    public class Lista2_OCDetalle
    {
        public int SECUENCIA { get; set; }
        public string CECO { get; set; }
        public string CODIGO { get; set; }
        public string DESCRIPCION { get; set; }
        public string UNI_CONV { get; set; }
        public int CANTIDAD { get; set; }
        public decimal TOTAL { get; set; }
        public string FIRMAS { get; set; }
    }
}