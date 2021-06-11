
$(document).ready(function () {
    ListarCentroCosto();
});


//LISTAMOS LOS CENTRO DE COSTO PARA LA CABECERA 
function ListarCentroCosto() {
    $.ajax({
        url: '/Almacenes/ListarCombosCentroCosto/',
        type: 'get',
        success: function (e) {
            console.log(e);
            let option = "<option value='0'> <-- TODOS --> </option>";
            e.forEach(function (obj) {
                option += `
                            <option value='${obj.CODCENTROCOSTO}' >${obj.DECRIPCENTROCOSTO} </option>
                        `;
            });

            $("#cboCentroCosto").html(option);
        }

    });
}



// FUNCINALIDAD AL DAR CLIC EN EL BOTON BUSCAR 
$("#btnBuscar").click(function () {

    ListarReporteStockFecha();
});

//FUNCION PARA LISTAR EL REPORTE
function ListarReporteStockFecha() {
    //Declaramos Variables en blanco
    var v_partida_ = null;
    var v_ubicacion_ = null;
    var v_tipotela_ = null;
    var v_centrocosto_ = null;
    var v_almacenes_ = null;

    //Pasamos los valores a las variables
    var v_partida_ = $("#txtpartida").val();
    var v_ubicacion_ = $("#txtubicacion").val();
    var v_tipotela_ = $("#txttipotela").val();
    var v_centrocosto_ = $("#cboCentroCosto").val();
    var v_almacenes_ = $("#txtalmacenes").val();


    //pasamos las variables al obketo
    var datos =
    {
        'v_partida': v_partida_,
        'v_ubicacion': v_ubicacion_,
        'v_tipotela': v_tipotela_,
        'v_centrocosto': v_centrocosto_,
        'v_almacenes': v_almacenes_,
    }

    console.log(datos);
    MostrarCarga("Cargando...");
    $.ajax({
        url: '/Almacenes/ListarReporteStockFecha',
        type: 'GET',
        contenType: 'json',
        data: datos,
        success: function (e) {

            let tabla = $("#tablaPrincipal").DataTable();
            tabla.destroy();

            //// Llamar a tablas Como lista normal /////
            var tr = "";
            e.forEach(function (obj) {

                tr += `
                    <tr>
                        <td>${obj.COD_ITEMS_}</td> 
                        <td>${obj.CENTRO_COSTO_}</td> 
                        <td>${obj.DESC_CENTRO_CUSTO_}</td> 
                        <td>${obj.COD_ALMACEN_}</td> 
                        <td>${obj.ALMACEN_}</td> 
                        <td>${obj.PARTIDA_}</td> 
                        <td>${obj.LOTE_ACOMP_}</td> 
                        <td>${obj.TIPO_TELA_}</td> 
                        <td>${obj.DESC_PRODUCTO_}</td> 
                        <td>${obj.ARTICULO_PRODUCTO_}</td> 
                        <td>${obj.DESC_TALLA_}</td> 
                        <td>${obj.COLOR_}</td> 
                        <td>${obj.STOCK_ANT_}</td> 
                        <td>${obj.CANTIDAD_ENTRADA_}</td> 
                        <td>${obj.CANTIDAD_SALIDA_}</td> 
                        <td>${obj.STOCK2_}</td> 
                        <td>${obj.UM_}</td> 
                        <td>${obj.COD_PROVEEDOR_}</td> 
                        <td>${obj.PROVEEDOR_}</td> 
                        <td>${obj.PROGRAMA_}</td> 
                        <td>${obj.UBICACION_}</td> 
                        <td>${obj.ULT_FECHA_INGRESO_}</td> 
                        <td>${obj.ULT_FECHA_SALIDA_}</td> 
                        <td>${obj.OC_}</td> 
                        <td>${obj.FECHA_INGRESO_GR_}</td> 
                        <td>${obj.CLIENTE_}</td> 
                        <td>${obj.ESTILO_CLIENTE_}</td> 
                        <td>${obj.OBS_OC_}</td> 
                   </tr>`;
            });


            $("#tablaCabeceraContenido").html(tr);

            $("#tablaPrincipal").DataTable(
                {
                    'language': { 'url': '/libs/datatables/spanish.json' },
                    "scrollX": true,
                    dom: 'Bfrtip',
                    buttons: ['excel', 'print'],
                    lengthMenu: [[5, 10, 20, -1], [5, 10, 20, 'Todos']]
                }
            );

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
}


//Funcion para exportar
$("#btnExportarExcel").click(function () {

    var vPeriodos = "";
    var vSecciones = "";
    var vFamilias = "";
    var vCuentas = "";


    var datos = {
    }

    $.ajax({
        url: '/Almacenes/ExportarExcelReporteStockFecha',// '@Url.Action("ExportarExcelReporteStockFecha", "Almacenes")',
        contentType: 'application/json; charset=utf-8',
        datatype: 'json',
        type: "GET",
        data: datos,
        success: function () {

            Swal.fire({
                icon: 'success',
                title: 'Reporte Generado',
                text: "Textile Sourcing Company",
                allowEscapeKey: false,
                allowOutsideClick: false,
                onClose: () => {
                    window.location = "/Almacenes/ExportarExcelReporteStockFecha";
                }
            });
        }
    });
});
