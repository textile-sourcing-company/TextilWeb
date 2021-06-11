using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Logistica.AlterarSituacion
{
    public class Lista11_AprobExcel
    {
        public int PEDIDO { get; set; }
        public string ESTADO_OC { get; set; }
        public string FECHA { get; set; }
        public string DESCRIPCIONPAGO { get; set; }
        public string PROVEEDOR { get; set; }
        public string COMPRADOR { get; set; }
        public decimal TOTAL_PEDIDO { get; set; }
        public string MONEDA { get; set; }
        public string USUARIO { get; set; }
        public string FECHA_APROB { get; set; }

    }
}