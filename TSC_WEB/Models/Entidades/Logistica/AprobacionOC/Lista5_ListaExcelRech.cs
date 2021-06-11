using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Logistica.AprobacionOC
{
    public class Lista5_ListaExcelRech
    {

        public int PEDIDO { get; set; }
        public string FECHA { get; set; }
        public string COD_CC { get; set; }
        public string CENTRO_COSTO { get; set; }
        public string PROVEEDOR { get; set; }
        public string COMPRADOR { get; set; }
        public string DESCRIPCIONPAGO { get; set; }
        public string PRODUCTO { get; set; }
        public string CODIGO_CUENTA { get; set; }
        public string CUENTA { get; set; }
        public string ESTADO_ITEM { get; set; }
        public string MONEDA { get; set; }
        public int CANTIDAD { get; set; }
        public string UNIDADE_MEDIDA { get; set; }
        public decimal TOTAL_PEDIDO { get; set; }
        public string CH_USUARIO_APROB { get; set; }
    }
}