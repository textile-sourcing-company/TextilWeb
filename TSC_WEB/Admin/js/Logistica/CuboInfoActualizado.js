$("#btnsearch").click(function () {
    ListarInformacion();
});

$("#ChkOC").change(function () {

    let check = $(this).prop("checked");

    if (check) {
        $("#DTFechaIni").prop('disabled', false);
        $("#DTFechaFin").prop('disabled', false);
    }
    else {
        $("#DTFechaIni").prop('disabled', true);
        $("#DTFechaFin").prop('disabled', true); 
        $("#DTFechaIni").val = "";
        $("#DTFechaFin").val = "";
    }

});

function ListarInformacion() {
    /*
    let check1 = $("#ChkOC").prop("checked");
    if (check1) {
        var fchini = $("#DTFechaIni").val
        var fchfin = $("#DTFechaFin").val
    }
    else {
        var fchini = "";
        var fchfin = "";
    }
    */
    var V_Proveedor = $("#TxtProveedor").val();
    var V_Comprador = $("#TxtComprador").val();
    var V_Almacen = $("#TxtAlmacen").val();
    var V_OC = $("#TxtOC").val();
    var V_FECHAINI = $("#DTFechaIni").val();
    var V_FECHAFIN = $("#DTFechaFin").val();
    let datos = {
        'FECHAINI': V_FECHAINI,
        'FECHAFIN': V_FECHAFIN,
        'PROVEEDOR': V_Proveedor,
        'COMPRADOR': V_Comprador,
        'ALMACEN': V_Almacen,
        'OC': V_OC,
    }

    MostrarCarga("Cargando...");
    $.ajax({
        url: '/Logistica/ListarDataCuboInformacionActualizado/',
        type: 'GET',
        contenType: 'json',
        data: datos,
        success: function (e) {

            let tabla = $("#TablaCuboInfoActualizado").DataTable();
            tabla.destroy();

            let tr = "";
            //debugger;
            e.forEach(function (obj) {
                tr += `
                    <tr>
                        <td>${obj.num_requisicion}</td>                       
                        <td>${obj.cant_req}</td> 
                        <td>${obj.pedido_compra}</td> 
                        <td>${obj.Fch_Reg_OC}</td> 
                        <td>${obj.Fch_Ent_Prevista_OC}</td>
                        <td>${obj.Cod_Proveedor}</td> 
                        <td>${obj.Des_Proveedor}</td> 
                        <td>${obj.Cod_Local_Entrega}</td> 
                        <td>${obj.Des_Local_Entrega}</td> 
                        <td>${obj.Cod_Local_Cobranza}</td> 
                        <td>${obj.Des_Local_Cobranza}</td> 
                        <td>${obj.Cod_Condicion_pago}</td> 
                        <td>${obj.Des_Condicion_pago}</td> 
                        <td>${obj.Cod_Comprador}</td>
                        <td>${obj.Des_Comprador}</td>
                        <td>${obj.Contacto}</td>
                        <td>${obj.Cod_Moneda}</td>
                        <td>${obj.Des_Moneda}</td>
                        <td>${obj.Observacion_OC}</td>
                        <td>${obj.Secuencia_OC}</td>
                        <td>${obj.Cod_producto2}</td>
                        <td>${obj.Des_producto}</td>
                        <td>${obj.Unidad_Consumo}</td>
                        <td>${obj.Unidad_Compra}</td>
                        <td>${obj.Cant_Soli_UnidadConsumo}</td>
                        <td>${obj.Cant_Soli_UnidadCompra}</td>
                        <td>${obj.Valor_Unita_UnidadConsumo}</td>
                        <td>${obj.Valor_Total_UnidadConsumo}</td>
                        <td>${obj.Porc_Descuento}</td>
                        <td>${obj.Porc_Impuesto}</td>
                        <td>${obj.Cod_Almacen}</td>
                        <td>${obj.Des_Almacen}</td>
                        <td>${obj.Cod_CCO}</td>
                        <td>${obj.Des_CCO}</td>
                        <td>${obj.Orden_Planeamiento}</td>
                        <td>${obj.Grupo_Planeamiento}</td>
                        <td>${obj.Fch_Prevista_Entrega_OC}</td>
                        <td>${obj.Fch_Proveedor}</td>
                        <td>${obj.Estado_Item_OC}</td>
                        <td>${obj.Usuario_Reg_OC}</td>
                        <td>${obj.Fch_Aprobada_Firmante_OC1}</td>
                        <td>${obj.Login_Firmante_OC1}</td>
                        <td>${obj.Fch_Aprobada_Firmante_OC2}</td>
                        <td>${obj.Login_Firmante_OC2}</td>
                        <td>${obj.Fch_Aprobada_Firmante_OC3}</td>
                        <td>${obj.Login_Firmante_OC3}</td>
                        <td>${obj.Cant_Ing_Almacen}</td>
                        <td>${obj.Fch_Ing_Almacen}</td>
                        <td>${obj.Fch_Ing_Ultima_Almacen}</td>
                        <td>${obj.Veces_Ing_Almacen}</td>
                        <td>${obj.Cant_Devuelta}</td>
                        <td>${obj.Cant_Total}</td>
                   </tr>`;

            });
            $("#TablaCuerpoCuboInfoActualizado").html(tr);
            //$("#TablaMovPeriodo").DataTable();

            ArmarDataTable("TablaCuboInfoActualizado", false, true, true);
            Swal.fire({
                icon: 'success',
                title: "Mostrando Datos",
                text: "Textile Sourcing Company",
                allowEscapeKey: false,
                showConfirmButton: false,
                timer: 1500,
            });
        }
    });
};