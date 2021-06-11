using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Logistica.Cubo
{
    public class EBCuboInfoActualizado
    {
        public string num_requisicion { get; set; }
        public decimal? cant_req { get; set; }
        public string pedido_compra { get; set; }
        public string Fch_Reg_OC { get; set; }
        public string Fch_Ent_Prevista_OC { get; set; }
        public string Cod_Proveedor { get; set; }
        public string Des_Proveedor { get; set; }
        public string Cod_Local_Entrega { get; set; }
        public string Des_Local_Entrega { get; set; }
        public string Cod_Local_Cobranza { get; set; }
        public string Des_Local_Cobranza { get; set; }
        public string Cod_Condicion_pago { get; set; }
        public string Des_Condicion_pago { get; set; }
        public string Cod_Comprador { get; set; }
        public string Des_Comprador { get; set; }
        public string Contacto { get; set; }
        public string Cod_Moneda { get; set; }
        public string Des_Moneda { get; set; }
        public string Observacion_OC { get; set; }
        public string Secuencia_OC { get; set; }
        public string Cod_producto2 { get; set; }
        public string Des_producto { get; set; }
        public string Unidad_Consumo { get; set; }
        public string Unidad_Compra { get; set; }
        public decimal? Cant_Soli_UnidadConsumo { get; set; }
        public decimal? Cant_Soli_UnidadCompra { get; set; }
        public decimal? Valor_Unita_UnidadConsumo { get; set; } 
        public decimal? Valor_Total_UnidadConsumo { get; set; }
        public decimal? Porc_Descuento { get; set; }
        public decimal? Porc_Impuesto { get; set; }
        public string Cod_Almacen { get; set; }
        public string Des_Almacen { get; set; }
        public string Cod_CCO { get; set; }
        public string Des_CCO { get; set; }
        public string Orden_Planeamiento { get; set; }
        public string Grupo_Planeamiento { get; set; }
        public string Fch_Prevista_Entrega_OC { get; set; }
        public string Fch_Proveedor { get; set; }
        public string Estado_Item_OC { get; set; }
        public string Usuario_Reg_OC { get; set; }
        public string Fch_Aprobada_Firmante_OC1 { get; set; }
        public string Login_Firmante_OC1 { get; set; }
        public string Fch_Aprobada_Firmante_OC2 { get; set; }
        public string Login_Firmante_OC2 { get; set; }
        public string Fch_Aprobada_Firmante_OC3 { get; set; }
        public string Login_Firmante_OC3 { get; set; }
        public decimal? Cant_Ing_Almacen { get; set; }
        public string Fch_Ing_Almacen { get; set; }
        public string Fch_Ing_Ultima_Almacen { get; set; }
        public int? Veces_Ing_Almacen { get; set; }
        public decimal? Cant_Devuelta { get; set; }
        public decimal? Cant_Total { get; set; }
    }
}