var cod_estados = [];
var cod_gerencia = [];

$(document).ready(function () {
    ListarResponsables();
    ListarAreas();


    $("#cboGerencias").multipleSelect();
    LlenarComboGerencia();

    $("#cboEstados").multipleSelect();
    LlenarComboEstados();

});


//LISTAMOS LOS RESPONSABLES DE REQUERIMIENTOS 
function ListarResponsables() {
    $.ajax({
        url: '/seguridad/ListarResponsbles/',
        type: 'get',
        success: function (e) {
            console.log(e);
            let option = "<option value='0'>TODOS LOS RESPONSABLES</option>";
            e.forEach(function (obj) {
                               option += `
                            <option value='${obj.idresponsable}' >${obj.responsable} </option>
                        `;
            });

            $("#cboResponsables").html(option);
        }

    });
}



//LISTAMOS LAS AREAS DE LOS REQUERIMIENTOS 
function ListarAreas() {
    $.ajax({
        url: '/seguridad/ListarAreas/',
        type: 'get',
        success: function (e) {
            console.log(e);
            let option = "<option value='0'>TODOS LAS AREAS</option>";
            e.forEach(function (obj) {
                option += `
                            <option value='${obj.IDAREA}' >${obj.NOMBRE_AREA} </option>
                        `;
            });

            $("#cboAreas").html(option);
        }

    });
}


// COMBO PARA CARGAR LOS COMBOS CON LAS GERENCIAS
function LlenarComboGerencia() {
    $.ajax({
        type: "GET",
        url: '/Seguridad/ListarGerencias2',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log(data);
            $.each(data.lista, function () {
                $("#cboGerencias").append($("<option/>").val(this.COD_GER).text(this.DESC_GER));
            });
            $('#cboGerencias').multipleSelect("refresh");
        },
        failure: function () {
            console.error('error al cargar las Gerencias');
        }
    });
}

$("select.selectSeries").change(function () {
    cod_gerencia = $("#cboGerencias").val();
});


// COMBO PATA CARGAR LOS ESTADOS DEL REQ.
function LlenarComboEstados() {
    $.ajax({
        type: "GET",
        url: '/Seguridad/ListarEstados',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log(data);
            $.each(data.lista, function () {
                $("#cboEstados").append($("<option/>").val(this.ESTADO_REQ).text(this.ESTADO_REQ));
            });
            $('#cboEstados').multipleSelect("refresh");
        },
        failure: function () {
            console.error('error al cargar los Estados');
        }
    });
}

$("select.selectSeries").change(function () {
    cod_estados = $("#cboEstados").val();
});

// FUNCINALIDAD AL DAR CLIC EN EL BOTON BUSCAR 
$("#btnBuscar").click(function () {

    ListarRequerimientosProg();
});

function ListarRequerimientosProg() {
    //Variables

    var v_tipo_fecha  = null;
    var v_fecha_ini   = null;
    var v_fecha_fin   = null;
    var v_solicitante = null;
    var v_num_reque   = null;
    var v_resposable  = null;
    var v_areas       = null;



    var v_tipo_fecha = $('#cboTipoFecha').val();
    var v_fecha_ini = $('#FechaIni').val();
    var v_fecha_fin = $('#FechaFin').val();
    var v_solicitante = $("#txtSolicitante").val();
    var v_num_reque = $("#txtRequerimiento").val();
    var v_resposable = $("#cboResponsables").val();
    var v_areas = $("#cboAreas").val();

    //Si se selecciona mas de una Gerencia
    var v_gerencia = '';
    for (var i = 0; i < cod_gerencia.length; i++) {
        v_gerencia = v_gerencia + "'" + cod_gerencia[i] + "',"
    }
    v_gerencia = v_gerencia.slice(0, v_gerencia.length - 1);

    //Si se selecciona mas de un estado
    var v_estados = '';
    for (var i = 0; i < cod_estados.length; i++) {
        v_estados = v_estados + "'" + cod_estados[i] + "',"
    }
    v_estados = v_estados.slice(0, v_estados.length - 1);

    var datos =
    {
        'tipo_fecha': v_tipo_fecha,
        'fechaInicio': v_fecha_ini,
        'fechaFin': v_fecha_fin,
        'gerencia': v_gerencia,
        'solicitante': v_solicitante,
        'num_req': v_num_reque,
        'responsable': v_resposable,
        'area': v_areas,
        'estado': v_estados 
    }

    console.log(datos);
    MostrarCarga("Cargando...");
    $.ajax({
        url: '/Seguridad/ListarREquerimientosWeb',
        type: 'GET',
        contenType: 'json',
        data: datos,
        success: function (e) {

            let tabla = $("#tablaPrincipal").DataTable();
            tabla.destroy();

            //Llamar a tabla como tipo objeto ////
            //    var tr = "";
            //    e.lista.forEach((obj) => {

            //        tr += "<tr> "
            //            + " <td> " + obj.IDREQUERIMIENTO  + "</td>"
            //            + " <td> " + obj.REQUERIMIENTO  + "</td>"
            //            + " <td> " + obj.FECHA_REQ  + "</td>"
            //            + " <td> " + obj.SOLICITANTE  + "</td>"
            //            + " <td> " + obj.NOMBRE_AREA  + "</td>";
            //            + " <td> " + obj.DESC_GER  + "</td>"
            //            + " <td> " + obj.RESPONSABLE   + "</td>"
            //            + " <td> " + obj.FECHA_INI_REQ   + "</td>"
            //            + " <td> " + obj.FECHA_FIN_REQ   + "</td>"
            //            + " <td> " + obj.FECHA_INICIO_REAL    + "</td>"
            //            + " <td> " + obj.OBSERVACIONES    + "</td>"
            //            + " <td> " + obj.ESTADO_REQ   + "</td></tr>";

            //    });

            //    $("#tablaCabeceraContenido").html(tr);

            //    $("#tablaPrincipal").DataTable(
            //        {
            //            'language': { 'url': '/libs/datatables/spanish.json' },
            //            "scrollX": true,
            //            lengthMenu: [[5, 10, 20, -1], [5, 10, 20, 'Todos']]
            //        }
            //    );


            //    Swal.fire({
            //        icon: 'success',
            //        title: "Mostrando Datos",
            //        text: "Textile Sourcing Company",
            //        allowEscapeKey: false,
            //        showConfirmButton: false,
            //        timer: 1500,
            //    });

            //},
            //failure: function () {
            //    console.error('error al cargar los Requerimientos');
            //}

            //// Llamar a tablas Como lista normal /////
            var tr = "";
            e.forEach(function (obj) {

                tr += `
                    <tr>
                        <td style="width:15px">${obj.IDREQUERIMIENTO}</td>                       
                        <td style="width:100px; padding-right:5px">${obj.REQUERIMIENTO}</td> 
                        <td>${obj.FECHA_REQ}</td> 
                        <td>${obj.SOLICITANTE}</td> 
                        <td>${obj.NOMBRE_AREA}</td> 
                        <td>${obj.DESC_GER}</td> 
                        <td>${obj.RESPONSABLE}</td> 
                        <td>${obj.FECHA_INI_REQ}</td> 
                        <td>${obj.FECHA_FIN_REQ}</td> 
                        <td>${obj.FECHA_INICIO_REAL}</td> 
                        <td tyle="width:100px; padding-right:5px">${obj.OBSERVACIONES}</td> 
                        <td>${obj.ESTADO_REQ}</td> 
                   </tr>`;
            });


            $("#tablaCabeceraContenido").html(tr);

            $("#tablaPrincipal").DataTable(
                {
                    'language': { 'url': '/libs/datatables/spanish.json' },
                    "scrollX": true,
                    dom : 'Bfrtip',
                    buttons : ['excel','print'],
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
