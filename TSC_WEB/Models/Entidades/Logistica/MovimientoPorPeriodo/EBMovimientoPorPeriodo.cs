using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.MovimientoPorPeriodo
{
    public class EBMovimientoPorPeriodo
    {
        public string codigo { get; set; }
        public string narrativa { get; set; }
        public string fechamovimiento { get; set; }
        public int? lote { get; set; }
        public int? codalmacen { get; set; }
        public string numero_documento { get; set; }
        public string doc_movimiento { get; set; }
        public string transaccion { get; set; }
        public string entrada_salida { get; set; }
        public int? codigo_transacao { get; set; }
        public decimal? cantidad { get; set; }
        public decimal? cantidadinicial { get; set; }
        public decimal? cantidad_entrada { get; set; }
        public decimal? cantidad_salida { get; set; }
        public decimal? cantidad_final { get; set; }
        public decimal? stock { get; set; }
        public string alm_descripcion { get; set; }
        public string nivel { get; set; }
        public string grupo { get; set; }
        public string subgrupo { get; set; }
        public string item { get; set; }
        public string color { get; set; }
        public string talla { get; set; }
        public string partida { get; set; }
        public string Desc_Transaccion { get; set; }
        public string Fecha_Ins_Entrada { get; set; }
        public string Hora_Ins_Entrada { get; set; }
        public string Fecha_Ins_Salida { get; set; }
        public string Hora_Ins_salida { get; set; }
        public string usuario_ins { get; set; }

        //agregado el dia 24-05-2017
        public string familia { get; set; }
        public int? Pedido { get; set; }
        /*Agregado 25/04/2018*/
        public string Corte_Destino { get; set; }
        public string App_Ch_Destino { get; set; }
        public string TipoServicio { get; set; }
        public string NombreTaller { get; set; }
        /* REPORTE DE STOCK TIENDA el 19/05/2018 */
        public string CH_LINEA { get; set;}
        public string CH_SUB_LINEA { get; set; }
        public decimal? I_STOCK { get; set; }
        public decimal? I_PRECIO { get; set; }
        public string CH_GENERO { get; set; }
        public string CH_TALLA { get; set; }
        public string CH_COLOR { get; set; }
        public string CH_DESCRPICION { get; set; }
        public string CH_CH_COD_PRODUCTO { get; set; }
    }
}