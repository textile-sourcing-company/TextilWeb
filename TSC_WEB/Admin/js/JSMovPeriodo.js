$("#btnsearch").click(function () {
    ListarInformacion();
});

function DecirHola() {
    Swal.fire({
        icon: 'success',
        title: "Mostrando Datos",
        text: "Textile Sourcing Company",
        allowEscapeKey: false,
        showConfirmButton: false,
        timer: 1500,
    });
}

function ListarInformacion()
{
    var V_Fechaini = $("#DTFechaIni").val();
    var V_Fechafin = $("#DTFechaFin").val();
    var V_Lote = $("#TxtLote").val();
    var V_CodTran = $("#TxtCodTran").val();
    var V_CodAlm = $("#TxtCodAlm").val();
    //var V_Transaccion = $("#TxtTransaccion").val();
    var V_Nivel = $("#TxtNivel").val();
    var V_Grupo = $("#TxtGrupo").val();
    var V_SubGrupo = $("#TxtSubGrupo").val();
    var V_Item = $("#TxtItem").val();
    let datos = {
        'FECHAINI': V_Fechaini,
        'FECHAFIN': V_Fechafin,
        'LOTE': V_Lote,
        'COD': V_CodTran,
        'CODALM': V_CodAlm,
        'NIVEL': V_Nivel,
        'GRUPO': V_Grupo,
        'SUBGRUPO': V_SubGrupo,
        'ITEM': V_Item,
    }

    MostrarCarga("Cargando...");
    $.ajax({
        url: '/Logistica/ListarMovimientoPeriodo/',
        type: 'GET',
        contenType: 'json',
        data: datos,
        success: function (e) {

            let tabla = $("#TablaMovPeriodo").DataTable();  
            tabla.destroy();

            let tr = "";
            //debugger;
            e.forEach(function (obj) {
                tr += `
                    <tr>
                        <td>${obj.fechamovimiento}</td>                       
                        <td>${obj.nivel}</td> 
                        <td>${obj.grupo}</td> 
                        <td>${obj.subgrupo}</td> 
                        <td>${obj.talla}</td>
                        <td>${obj.item}</td> 
                        <td>${obj.color}</td> 
                        <td>${obj.codigo}</td> 
                        <td>${obj.codalmacen}</td> 
                        <td>${obj.alm_descripcion}</td> 
                        <td>${obj.numero_documento}</td> 
                        <td>${obj.doc_movimiento}</td> 
                        <td>${obj.transaccion}</td> 
                        <td>${obj.Desc_Transaccion}</td>
                        <td>${obj.entrada_salida}</td>
                        <td>${obj.lote}</td>
                        <td>${obj.partida}</td>
                        <td>${obj.cantidadinicial}</td>
                        <td>${obj.cantidad_entrada}</td>
                        <td>${obj.cantidad_salida}</td>
                        <td>${obj.cantidad_final}</td>
                        <td>${obj.Fecha_Ins_Entrada}</td>
                        <td>${obj.Hora_Ins_Entrada}</td>
                        <td>${obj.usuario_ins}</td>
                        <td>${obj.familia}</td>
                        <td>${obj.cantidad}</td>
                        <td>${obj.Pedido}</td>
                        <td>${obj.Corte_Destino}</td>
                        <td>${obj.App_Ch_Destino}</td>
                        <td>${obj.TipoServicio}</td>
                        <td>${obj.NombreTaller}</td>
                   </tr>`;

            });
            $("#TablaCuerpoMovPeriodo").html(tr);
            //$("#TablaMovPeriodo").DataTable();
         
            ArmarDataTable("TablaMovPeriodo", false, true ,true);
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

// ENTER 
$('#TxtCodAlm').keypress(function (event)
{
    // CODIGO DE ALMACEN
    var Almacen = $("#TxtCodAlm").val();
    // CODIGO DE TECLA 
    var keycode = (event.keyCode ? event.keyCode : event.which);
    console.log(keycode);
    // SI PRESIONAMOS ENTER
    if (keycode == '13')
    {
        ObtenerAlmacen(Almacen);
    }
});
function nextFocus(inputF, inputS)
{
    document.getElementById(inputF).addEventListener('keydown', function (event)
    {
        if (event.keyCode == 13)
        {
            document.getElementById(inputS).focus();
        }
    });
}
$('#TxtCodTran').keypress(function (event) {
    // CODIGO DE TRANSACCION
    var Transaccion = $("#TxtCodTran").val();
    // CODIGO DE TECLA 
    var keycode = (event.keyCode ? event.keyCode : event.which);
    console.log(keycode);
    // SI PRESIONAMOS ENTER
    if (keycode == '13') {
        ObtenerTransaccion(Transaccion);
    }
});
// TAB
$('#TxtCodAlm').focusout(function (event) {
    // CODIGO DE ALMACEN
    var Almacen = $("#TxtCodAlm").val();
    ObtenerAlmacen(Almacen);   
});
// TAB
$('#TxtCodTran').focusout(function (event) {
    // CODIGO DE ALMACEN
    var Transaccion = $("#TxtCodTran").val();
    ObtenerTransaccion(Transaccion);
});

function ObtenerAlmacen(Almacen)
{
    let Datos = { 'CODALM': Almacen };
    $.ajax(
        {
            url: '/Logistica/ListarAlmacen/',
            type: 'GET',
            //contenType: 'json',
            data: Datos,
            success: function (e) {
                //console.log(Almacen, e);
                // MOSTRAMOS VALOR EN LA CAJAD E TEXTO
                $("#TxtAlmacen").val(e.Almacen);
            }
        });
}
function ObtenerTransaccion(Transaccion) {
    let Datos = { 'CODTRAN': Transaccion };
    $.ajax(
        {
            url: '/Logistica/ListarTransaccion/',
            type: 'GET',
            //contenType: 'json',
            data: Datos,
            success: function (e) {
                //console.log(Almacen, e);
                // MOSTRAMOS VALOR EN LA CAJAD E TEXTO
                $("#TxtTransaccion").val(e.Transaccion);
            }
        });
}
