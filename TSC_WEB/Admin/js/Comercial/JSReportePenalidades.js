
//AL MOMENTO QUE CARGA LLENE EL COMBO
$(document).ready(function () {
    ListarCentroCosto();
});


//NUEVA LOGICA PARA CARGAR LOS CENTRO DE COSTOS

//====== AL REALIZAR UNA ENTRADA EN LA XAJA DE TEXTO LISTO LLENA LAS DE LAS TXT =====//
$('#txtListaCC').on('change', function () {
    var value = $(this).val();
    idclienteText = $('#listaCentroCosto [value="' + value + '"]').data('id');
    //var ubigeo = $('#listaCliente [value="' + value + '"]').data('ubigeo');
    if (idclienteText != undefined) {
        LlenarTxtDatosCliente(idclienteText);
    }
});

///=========== BUSCAR CLIENTE EN TEXTBOX ============///
$("#txtListaCC").keypress(function (e) {

    if (e.keyCode == 13) {
        var objeto = {
            'v_centrocosto': $("#txtListaCC").val().trim()
        }
        $.ajax({
            url: '/Comercial/ListarCombosCentroCostoModal/',
            type: 'GET',
            data: objeto,
            success: function (e) {
                console.log(e);
                var option = "";
                e.forEach((obj) => {
                    //option += "<option value='" + obj.CODCENTROCOSTO2 + " - " + obj.DECRIPCENTROCOSTO2 + "' data-id='" + obj.CODCENTROCOSTO2 + "' > </option>";
                    option += "<option value='" + obj.DECRIPCENTROCOSTO2 + "' data-id='" + obj.CODCENTROCOSTO2 + "' > </option>";
                });
                $("#listaCentroCosto").html(option);
            }
        });
    }
});




//LISTAMOS LOS CENTRO DE COSTO PARA LA CABECERA - OLD
function ListarCentroCosto() {
    $.ajax({
        url: '/Almacenes/ListarCombosCentroCosto/',
        type: 'get',
        success: function (e) {
            console.log(e);
            let option = "<select id='OP'><option value='0'> <-- TODOS --> </option></select>";
            e.forEach(function (obj) {
                option += `
                            <option value='${obj.DECRIPCENTROCOSTO}' >${obj.DECRIPCENTROCOSTO} </option>
                        `;
            });

            $("#cboCentroCosto").html(option);
        }

    });
}
// FIN DE CODE OLD PARA LLENAR EL CENTRO DE COSTO



// HACE UN LISTAR DEL REPORTE PENALIDADES
$("#btnBuscar").click(function () {
    ListarReportePenalidades();    
});

//CLICK EN REGISTRAR BOTON DEL MODAL(btninsertar)
$("#btninsertar").click(function () {
    InsertarDatos();
});
//FUNCIONA PARA LIMPIAR LAS CAJAS DE TEXTOS  -> NO UTILIZADO
function LimpiarTexto(pconcepto, pcantfin) {
    document.getElementById(pconcepto).value = "";
    document.getElementById(pcantfin).value = "";
} 

//INSERTA LOS CAMPOS QUE APARECEN EN EL MODAL
function InsertarDatos() {
    var _documento = $("#txtnumerodocumento").val(); // A LA VARIABLE _DOCUMENTO LE PASAMOS EL VALOR QUE CONTIENE EL TXTNUMERODOCUMENTO
    var _concepto = $("#txtconcepto").val();
    var _descripcion = $("#txtdescripcion").val();
    //var _centrocosto = $("#cboCentroCosto").val();
    var _centrocosto = $("#txtListaCC").val();
    var _usuarioregistro = $("#txtusuarioregistro").val();

//AHORA EL CONTENIDO DE LA VARIABLE _documento SE LO PASAMOS A NUESTRO PARAMETRO 'documento'
    var datos = {
        'documento': _documento,
        'concepto': _concepto,
        'descripcion': _descripcion,
        'centrocosto': _centrocosto,
        'usuarioregistro': _usuarioregistro,
    }
    console.log(datos);
    $.ajax({
        url: '/Comercial/InsertarDatosPenalidadesControler',
        type: 'GET',
        data: datos,
        datatype: 'json',
        success: function (e) {
            if (e.success) {
                window.location.href = e.redurl;               
            }
            else {
                if (e.rsperror != "F") {
                    Swal.fire({
                        icon: 'success',
                        title: e.mensaje,
                        text: 'Textil Sourcing Company',
                        allowEscapeKey: false,
                        allowOutsideClick: false,                        
                    });
                }
                else {
                    Swal.fire({
                        icon: 'error',
                        title: e.mensaje,
                        text: 'Textil Sourcing Company',
                        allowEscapeKey: false,
                        allowOutsideClick: false
                        
                    });
                }
            }  
            $("#ModalEdicion").modal("hide");
            ListarReportePenalidades();
        }
    });
}
//ACCIONAR EL BOTON EDITAR DEL DATATABLE PRINCIPAL
$("#tablaCabeceraContenido1").on('click', '.editar', function () {
    var _numerodocumento = $(this).data("numerodocumento"); //Cargamos el campo numero de documento en la variable _numerodocumento    
    var _concepto = $(this).data("concepto");
    var _descripcion = $(this).data("descripcion");
    var _centrocosto = $(this).data("centrocosto");
    //let option = "<option value='" + _centrocosto + "'>"+_centrocosto;
    
    $("#txtnumerodocumento").val(_numerodocumento); //Le Pasamos al Texto del modal el contenido de la variable _numerodocumento   
    $("#txtconcepto").val(_concepto);
    $("#txtdescripcion").val(_descripcion);
    //$("#OP").append("<option value='" + _centrocosto + "'>" + _centrocosto + "</option>");
    $("#txtListaCC").val(_centrocosto);
    $("#ModalEdicion").modal("show");
})

//FUNCION PARA LISTAR EL REPORTE
function ListarReportePenalidades() {
    //Declaramos Variables en blanco
    var _Cliente_ = null;
    var _Documento_ = null;
    var _Fechainicio_ = null;
    var _Fechafin_ = null;    

    //Pasamos los valores a las variables
    var _Cliente_ = $("#txtcliente").val();
    var _Documento_ = $("#txtdocumento").val();
    var _Fechainicio_ = $('#FechaInicio').val();
    var _Fechafin_ = $('#FechaFin').val();    


    //pasamos las variables al objeto
    var datos =
    {
        'Cliente': _Cliente_,
        'Documento': _Documento_,
        'Fechainicio': _Fechainicio_,
        'Fechafin': _Fechafin_,
    }

    console.log(datos);
    MostrarCarga("Cargando...");
    $.ajax({
        url: '/Comercial/ReportePenalidadesControler',
        type: 'GET',
        contenType: 'json',
        data: datos,
        success: function (e) {

            let tabla = $("#tablaPrincipal").DataTable();
            tabla.destroy();
            var tr = "";
            e.lista.forEach(function (objModel) {

                tr += `
                    <tr>
                                                                  
                        <td>${objModel.mes}</td> 
                        <td>${objModel.fecha}</td> 
                        <td>${objModel.cliente}</td>                         
                        <td>${objModel.numerodocumento}</td>
                        <td>${objModel.moneda}</td>    
                        <td>${objModel.concepto}</td>
                        <td>${objModel.descripcion}</td>
                        <td>${objModel.centrocosto}</td>
                        <td class='text-center'>
                            <a class='editar iconedi mr-1'
                                
                                data-mes    ='${objModel.mes}'
                                data-fecha  ='${objModel.fecha}'
                                data-cliente ='${objModel.cliente}'
                                data-numerodocumento ='${objModel.numerodocumento}'
                                data-moneda       ='${objModel.moneda}'
                                data-concepto   ='${objModel.concepto}'
                                data-descripcion   ='${objModel.descripcion}'
                                data-centrocosto   ='${objModel.centrocosto}'

                                href='#'>  
                                <i class='fas fa-edit'></i> 
                            </a>
                           
                        </td>
                   </tr>`;
            });


            $("#tablaCabeceraContenido1").html(tr);

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