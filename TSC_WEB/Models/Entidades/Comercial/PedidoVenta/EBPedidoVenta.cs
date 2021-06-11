using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Comercial.PedidoVenta
{
    public class EBPedidoVenta
    {
        public int pedido { get; set; }
        public string cliente { get; set; }
        public string grupoplaneamiento { get; set; }
        public string desc_grupoplanea { get; set; }
        public string Coleccion { get; set; }
        public string pais { get; set; }
        public DateTime? fecha_programa { get; set; }
        public DateTime? fecha_venta { get; set; }
        public int? lt_registro_pedido { get; set; }
        public string usuario { get; set; }
        public DateTime? fecha_lib_pedido { get; set; }
    }
}