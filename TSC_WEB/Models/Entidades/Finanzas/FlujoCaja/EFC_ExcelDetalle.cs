using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Finanzas.FlujoCaja
{
    public class EFC_ExcelDetalle
    {
        public int efIdTipoAct { get; set; }
        public string efActividad { get; set; }
        public string efCodEFx { get; set; }
        public string efDescCodEf { get; set; }
        public int efIdRSx { get; set; }
        public string efConcepto { get; set; }
        public string efperiodo { get; set; }
        public int efperorden { get; set; }

        public string cvouch { get; set; }
        public string dvouch { get; set; }
        public int ncpbte { get; set; }
        public int cctaco { get; set; }
        public string dctaco { get; set; }


        public decimal sdebe { get; set; }
        public decimal shaber { get; set; }

        public decimal ddebe { get; set; }
        public decimal dhaber { get; set; }

        public string moneda { get; set; }
        public decimal itpcam { get; set; }
        public string fecha { get; set; }
        public string dglosa { get; set; }
        public string ruc { get; set; }
        public string razonSocial { get; set; }
        public string td { get; set; }
        public string documento { get; set; }
        public string numero { get; set; }
        public string fechaEmision { get; set; }
        public string fechaVencimiento { get; set; }
        public string cc { get; set; }
    }
}