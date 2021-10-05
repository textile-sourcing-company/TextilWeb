// Sin Solicitud : REEMBOLSO

const rNIVEL_INTERFAZ = 10;
var rswitchInsertarEditar = 0; // 1: Insertar, 2: Editar

var rvIdSolicitud = 0;
var rvIdRendCompte = 0;
var rvCodTipoComprobante = 0;
var rvIdTipoMod = 0;
var rvSimboloMoneda = "";
var rvCodCeCo = 0;
var rvCeCo = "";
var rvIdTipo = 0;

var rvTotalSolicitado1 = 0;
var rvTotalSolicitado2 = 0;
var rvTotalSolicitado3 = 0;

var rvIdConceptoCab1 = 0;
var rvIdConceptoCab2 = 0;
var rrvIdConceptoCab3 = 0;

var rvIdConceptoDet1 = 0;
var rvIdConceptoDet2 = 0;
var rvIdConceptoDet3 = 0;

var rvConceptoDet1 = "";
var rvConceptoDet2 = "";
var rvConceptoDet3 = "";

var rvChecked = false;

var rdiasArray = [];
var rentidadEditar = {};
var rdiasArrayEditar = [];
var rtipoMonedaArray = [];

var rsumTotalDetalle = 0;

var rvCodColSofya = "";
var rvColaboradorSofya = "";

var rvctpava_bnfcol = "";
var rvcanvar_bnfcol = "";
var rvcolaborador = "";

var rvCodSede = "";

var rsolicitudCabCreada = {};
var riDetalle1 = 0;
var riDetalle2 = 0;
var riDetalle3 = 0;

var riSecuencia1 = 0;
var riSecuencia2 = 0;
var riSecuencia3 = 0;

var rproveedor = {};
var rdetalleArray = [];
var rdetalleArray3 = [];
var rdetPlaMovdetPlaMov = {};

var rdetComprobanteArray1 = [];
var rdetComprobanteArray2 = [];
var rdetComprobanteArray3 = [];

var rconceptoDetalleArray1 = [];
var rconceptoDetalleArray2 = [];
var rconceptoDetalleArray3 = [];

var rrconceptoCabeceraArray1 = [];
var rrconceptoCabeceraArray2 = [];
var rrconceptoCabeceraArray3 = [];

var rconceptoCabArray1 = [];
var rconceptoCabArray2 = [];
var rconceptoCabArray3 = [];

var rtabSeleccionado = 1; // 1: Factura y Boleta, 2: Planilla Movilidad, 3: Planilla de Movilidada

var rTIPO_REGISTRO = "";


$(document).ready(function () {

    Inicializar();


    function Inicializar() {

        // Sin solicitud

        let sparametros = { nivelInterfaz: rNIVEL_INTERFAZ };
        let now = new Date();

        rvIdSolicitud = 0;

        let tabla = $("#rtblDetalle").DataTable();
        tabla.destroy();
        var tr = "";
        $("#rtblDetalleContent").html(tr);
        $("#rtblDetalle").DataTable(
            {
                'language': { 'url': '/libs/datatables/spanish.json' },
                lengthMenu: [[10, 20, 30, -1], [10, 20, 30, 'Todos']]
            }
        );

        $("#restado-desc").text("");

        $("#restado-icon").removeClass("icon-liberado");
        $("#restado-icon").removeClass("icon-aprobado");
        $("#restado-icon").removeClass("icon-rechazado");
        $("#restado-icon").removeClass("icon-pendiente");

        $("#restado-desc").removeClass("desc-est-liberado");
        $("#restado-desc").removeClass("desc-est-aprobado");
        $("#restado-desc").removeClass("desc-est-rechazado");
        $("#restado-desc").removeClass("desc-est-pendiente");

        document.getElementById("ridtxtcodigo").disabled = false;
        document.getElementById("ridtxtcodigo").focus();

        document.getElementById("ridCbxSede").disabled = true;
        document.getElementById("ridCbxTipo").disabled = true;
        document.getElementById("ridCbxTipoMod").disabled = true;
        document.getElementById("ridtxtaobs").disabled = true;
        document.getElementById("rcbxCeCo1").disabled = true;


        $("#ridtxtcodigo").val("");
        $("#riddttFechaHora").val(new Date(now.getTime() - now.getTimezoneOffset() * 60000).toISOString().substring(0, 19).toString());

        document.getElementById("rbtnBuscarCodigo").disabled = true;
        $("#rbtnBuscarCodigo").removeClass("btn-primary");
        $("#rbtnBuscarCodigo").addClass("btn-secondary");

        document.getElementById("rbtnAnularSolicitud").disabled = true;
        $("#rbtnAnularSolicitud").removeClass("btn-danger");
        $("#rbtnAnularSolicitud").addClass("btn-secondary");

        document.getElementById("rbtnGenerarComprobante").disabled = true;
        $("#rbtnGenerarComprobante").removeClass("btn-success");
        $("#rbtnGenerarComprobante").addClass("btn-secondary");



        rlimpiarCabecera();

        rgetTipos();
        rgetPermisosXCeCo(sparametros);
        rgetUnidadesMedida();
        rgetPeriodos();
        rgetSede();

        document.getElementById("ridtxtcodigo").disabled = false;
        document.getElementById("ridtxtcodigo").focus();

    }

});




// *********************    REEMBOLSO   ********************

// Funcionalidades.

function rlimpiarCabecera() {
    let now = new Date();

    $("#riddttFechaHora").val("");
    $("#ridtxtcodigo").val("");
    $("#ridtxtcodigo").removeClass("ridtxtcodigo-ok");
    $("#ridCbxTipo").val(4);
    $("#ridCbxTipoMod").val(1);
    $("#ridtxtaobs").val("");
}

function rvalidacionCabecera() {
    var respuesta = false;

    if (rvIdTipoMod == 0) {
        respuesta = false;

        Swal.fire({
            title: 'Seleccione un Tipo de Moneda',
            text: "Textile Sourcing Company",
            icon: 'warning',
            showConfirmButton: true,
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Ok'
        });
    }
    else if ($("#ridtxtaobs").val().trim() == "") {
        respuesta = false;

        Swal.fire({
            title: 'Ingrese una descripción de la solicitud por favor',
            text: "Textile Sourcing Company",
            icon: 'warning',
            showConfirmButton: true,
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Ok'
        });
    }
    else if (rvIdTipo == 0) {
        respuesta = false;

        Swal.fire({
            title: 'Seleccione el tipo de Solicitud, por favor',
            text: "Textile Sourcing Company",
            icon: 'warning',
            showConfirmButton: true,
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Ok'
        });
    }
    else if (rvctpava_bnfcol == "" || rvctpava_bnfcol == null || typeof (rvctpava_bnfcol) == "undefined") {
        respuesta = false;

        Swal.fire({
            title: 'Registre el colaborador beneficierio',
            text: "Textile Sourcing Company",
            icon: 'warning',
            showConfirmButton: true,
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Ok'
        });
    }

    else if (rvCodSede == "" || rvCodSede == null || typeof (rvCodSede) == "undefined") {
        respuesta = false;

        Swal.fire({
            title: 'Seleccione una Sede',
            text: "Textile Sourcing Company",
            icon: 'warning',
            showConfirmButton: true,
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Ok'
        });
    }
    else {
        respuesta = true;
    }

    return respuesta;
}

function rvalidacionDetalle() {
    let respuesta = true;
    let isFalseLoop = false;
    let cantConceptpDetArray = 0;

    cantConceptpDetArray = rconceptoDetArray.filter(x => x.isChecked == true).length;

    if (rvIdSolicitud == 0) {

        Swal.fire({
            title: 'Debe generar el codigo de solicitud para continuar',
            text: "Textile Sourcing Company",
            icon: 'warning',
            showConfirmButton: true,
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Ok'

        });

        return false;
    }
    else if (cantConceptpDetArray == 0 || cantConceptpDetArray == null || typeof (cantConceptpDetArray) == "undefined") {

        Swal.fire({
            title: 'Seleccione un concepto, por favor',
            text: "Textile Sourcing Company",
            icon: 'warning',
            showConfirmButton: true,
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Ok'
        });

        return false;
    }
    else if (rvCodCeCo == 0 || rvCodCeCo == null) {

        Swal.fire({
            title: 'Seleccione un centro de costo',
            text: "Textile Sourcing Company",
            icon: 'warning',
            showConfirmButton: true,
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Ok'
        });

        return false;
    }

    $("#rdetalleConceptoInsertarEditar input[type='number']").each(function (i, obj) {

        let valorIngresado = $(obj).val();
        let valorMaximoS = $(obj).attr("data-valor-maxs");
        let valorMaximoD = $(obj).attr("data-valor-maxd");

        let nombreID = obj.id;
        let idConceptoDet = $(obj).attr("data-idconceptodet");

        let estaSeleccionado = rconceptoDetArray.find(x => x.idConceptoDet == idConceptoDet && x.isChecked == true);


        if (typeof (estaSeleccionado) != "undefined") {

            if (valorIngresado.toString().trim() == "" || valorIngresado == null || typeof (valorIngresado) == "undefined" || valorIngresado.toString().trim() == "0") {
                Swal.fire({
                    title: 'Ingrese el monto por favor',
                    text: "Textile Sourcing Company",
                    icon: 'warning',
                    showConfirmButton: true,
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'Ok'
                });

                document.getElementById(nombreID).focus();
                isFalseLoop = true;
                return false;

            }

            else if (parseFloat(valorIngresado) < 0) {

                Swal.fire({
                    title: 'El monto ingresado no debe ser negativo',
                    text: "Textile Sourcing Company",
                    icon: 'warning',
                    showConfirmButton: true,
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'Ok'
                });

                document.getElementById(nombreID).focus();
                isFalseLoop = true;
                return false;
            }

            else if (vIdTipoMod == 1) {

                if (parseFloat(valorIngresado) > valorMaximoS) {
                    isFalseLoop = true;

                    $("#txtMontoSol" + idConceptoDet.toString() + "").val(0);
                    valorActual = 0;

                    Swal.fire({
                        title: 'El monto ingresado no puede ser mayor al máximo permitido por día S/.' + valorMaximoS.toString(),
                        text: "Textile Sourcing Company",
                        icon: 'warning',
                        showConfirmButton: true,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Ok'
                    });

                    document.getElementById("txtMontoSol" + idConceptoDet.toString() + "").focus();

                    return false;
                }
            }
            else if (vIdTipoMod == 2) {
                // DOLAR
                if (parseFloat(valorIngresado) > valorMaximoD) {
                    isFalseLoop = true;

                    $("#txtMontoSol" + idConceptoDet.toString() + "").val(0);
                    valorActual = 0;

                    Swal.fire({
                        title: 'El monto ingresado no puede ser mayor al máximo permitido por día U$ ' + valorMaximoD,
                        text: "Textile Sourcing Company",
                        icon: 'warning',
                        showConfirmButton: true,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Ok'
                    });

                    document.getElementById("txtMontoSol" + idConceptoDet.toString() + "").focus();
                    return false;
                }
            }
        }

    });

    // Servicio
    if (rvIdConcepCab == "5") {
        $("#rdetalleConceptoInsertarEditar input.cobsdet").each(function (i, obj) {
            let obsIngresado = $(this).val();

            if (obsIngresado == "" || obsIngresado == null || typeof (obsIngresado) == "undefined") {
                Swal.fire({
                    title: 'Debe ingresar una descripción para el tipo de Solicitud',
                    text: "Textile Sourcing Company",
                    icon: 'warning',
                    showConfirmButton: true,
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'Ok'
                });

                isFalseLoop = true;
                return false;
            }
        });
    }


    if (isFalseLoop) {
        respuesta = false;
    }
    else {
        respuesta = true;
    }

    return respuesta;

}

function rsetPeriodo() {

    let fechaFormato = new Date();

    let month = '' + (fechaFormato.getMonth() + 1);
    let year = fechaFormato.getFullYear();

    if (month.length < 2) { month = '0' + month; }

    $("#rcbxPeriodoModal1").val("" + [year, month].join('-'));
    $("#rcbxPeriodoModal2").val("" + [year, month].join('-'));
    $("#rcbxPeriodoModal3").val("" + [year, month].join('-'));

    document.getElementById("rcbxPeriodoModal1").disabled = true;
    document.getElementById("rcbxPeriodoModal2").disabled = true;
    document.getElementById("rcbxPeriodoModal3").disabled = true;
}


// Listados.

function rgetPermisosXCeCo(parametros) {
    $.ajax({
        type: "GET",
        url: '/Finanzas/RG_PermisosXCeCo',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: parametros,
        success: function (data) {
            let primerValorBool = true;

            $("#rcbxCeCo1").empty();

            $.each(data.lista, function () {
                if (primerValorBool) {
                    rvCodCeCo = this.codCeCo;
                    rvCeCo = this.descripcion;

                    primerValorBool = false;
                }
                $("#rcbxCeCo1").append($("<option/>").val(this.codCeCo).text(this.descripcion));
            });

            $("#rcbxCeCo1").val(rvCodCeCo);
        },
        failure: function () {
            console.error('error al cargar tipos periodos');
        }
    });
}

function rconceptoCabXTipo(pIdTipo, pIdConceptoDet) {

    let firstConceptoCab = 0;
    let firstLoop = true;
    let conceptoArray = [];

    let v_codConceptoCab = 0;
    let v_cantidadDetalle = 0;

    let newCabeceraArray = [];
    let newrdetalleArray = [];


    if (pIdConceptoDet > 0) {

        $("#cbxConcepCab").empty();

        v_codConceptoCab = conceptordetalleArray.filter(x => x.idConceptoDet == pIdConceptoDet).idConceptoCab;

        v_cantidadDetalle = conceptordetalleArray.filter(x => x.idConcepDet != pIdConceptoDet).length;

        if (v_cantidadDetalle > 0) {
            $("#cbxConcepCab").val(v_codConceptoCab);
            newrdetalleArray = conceptordetalleArray.filter(x => x.idConcepDet != pIdConceptoDet);
        }
        else {
            newCabeceraArray = rconceptoCabeceraArray.filter(x => x.idConcepCab != v_codConceptoCab);
            newrdetalleArray = conceptordetalleArray.filter(x => x.idConcepDet != pIdConceptoDet);
        }


        rllenarCbxDetalle(newrdetalleArray);

        $.each(conceptoArray, function () {
            if (firstLoop) {
                firstConceptoCab = this.idConceptoCab;

                firstLoop = false;
            }
            $("#cbxConcepCab").append($("<option/>").val(this.idConceptoCab).text(this.conceptoCab));
        });

        rvIdConcepCab = firstConceptoCab;

        $("#cbxConcepCab").val(firstConceptoCab);
        rgetConceptoDet(firstConceptoCab);
    }

}

function rgetTipoDeMoneda() {
    $.ajax({
        type: "GET",
        url: '/Finanzas/RG_TiposDeMoneda',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            rtipoMonedaArray = data.lista;

            $("#ridCbxTipoMod").empty();
            $("#rcbxMonedaModal1").empty();
            $("#rcbxMonedaModal2").empty();
            $("#rcbxMonedaModal3").empty();

            $.each(data.lista, function () {

                $("#ridCbxTipoMod").append($("<option/>").val(this.idTipoMod).text(this.descripcion));

                $("#rcbxMonedaModal1").append($("<option/>").val(this.idTipoMod).text(this.descripcion));
                $("#rcbxMonedaModal2").append($("<option/>").val(this.idTipoMod).text(this.descripcion));
                $("#rcbxMonedaModal3").append($("<option/>").val(this.idTipoMod).text(this.descripcion));

            });

            $("#ridCbxTipoMod").val(1);
            $("#rcbxMonedaModal1").val(1);
            $("#rcbxMonedaModal2").val(1);
            $("#rcbxMonedaModal3").val(1);

            rvSimboloMoneda = rtipoMonedaArray.find(x => x.idTipoMod == 1).simbolo;
            rvIdTipoMod = 1;

            rgetTipoComprobantes();
        },
        failure: function () {
            console.error('error al cargar tipos periodos');
        }
    });
}

function rgetTipos() {

    $.ajax({
        type: "GET",
        url: '/Finanzas/RG_TiposAprobGstsFnzs',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            $("#ridCbxTipo").empty();
            $.each(data.lista, function () {
                $("#ridCbxTipo").append($("<option/>").val(this.idTipo).text(this.tipo));
            });

            $("#ridCbxTipo").val(4);
            rvIdTipo = 4;

            rgetTipoDeMoneda();
        },
        failure: function () {
            console.error('error al cargar tipos periodos');
        }
    });
}

function rgetUnidadesMedida() {
    $.ajax({
        type: "GET",
        url: '/Finanzas/RG_UnidadesMedida',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#rcbxUM1").empty();
            $.each(data.lista, function () {
                $("#rcbxUM1").append($("<option/>").val(this.codum).text(this.um));
            });
            $("#rcbxUM1").val("UN");
        },
        failure: function () {
            console.error('error al cargar tipos periodos');
        }
    });
}

function rgetPeriodos() {
    $.ajax({
        type: "GET",
        url: '/Finanzas/RG_Periodos',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#rcbxPeriodoModal1").empty();
            $("#rcbxPeriodoModal2").empty();
            $("#rcbxPeriodoModal3").empty();

            $.each(data.lista, function () {
                $("#rcbxPeriodoModal1").append($("<option/>").val(this.periodo).text(this.periodo));
                $("#rcbxPeriodoModal2").append($("<option/>").val(this.periodo).text(this.periodo));
                $("#rcbxPeriodoModal3").append($("<option/>").val(this.periodo).text(this.periodo));
            });

            rsetPeriodo();
        },
        failure: function () {
            console.error('error al cargar tipos periodos');
        }
    });
}

function rgetSede() {

    $.ajax({
        type: "GET",
        url: '/Finanzas/RG_Sedes',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log(data);
            $("#ridCbxSede").empty();
            $.each(data.lista, function () {
                $("#ridCbxSede").append($("<option/>").val(this.codSede).text(this.sede));
            });
            $("#ridCbxSede").val("C");
            rvCodSede = "C";
        },
        failure: function () {
            console.error('error al cargar tipos periodos');
        }
    });

}

function rresetControles() {

    document.getElementById("ridCbxSede").disabled = false;
    document.getElementById("ridCbxTipoMod").disabled = false;
    document.getElementById("ridtxtaobs").disabled = false;
    document.getElementById("rcbxCeCo1").disabled = true;

    $("#ridusuario").val(USUARIO_COMPLETO);

    let now = new Date();
    $("#riddttFechaHora").val(new Date(now.getTime() - now.getTimezoneOffset() * 60000).toISOString().substring(0, 19).toString());

    $("#rkpi-cantidad").text("0");
    $("#rkpi-total").text("0");

    let tabla = $("#rtblDetalle").DataTable();
    tabla.destroy();
    var tr = "";
    $("#rtblDetalleContent").html(tr);
    $("#rtblDetalle").DataTable(
        {
            'language': { 'url': '/libs/datatables/spanish.json' },
            lengthMenu: [[10, 20, 30, -1], [10, 20, 30, 'Todos']]
        }
    );
    $("#restado-desc").text("");

    $("#restado-icon").removeClass("icon-liberado");
    $("#restado-icon").removeClass("icon-aprobado");
    $("#restado-icon").removeClass("icon-rechazado");
    $("#restado-icon").removeClass("icon-pendiente");

    $("#restado-desc").removeClass("desc-est-liberado");
    $("#restado-desc").removeClass("desc-est-aprobado");
    $("#restado-desc").removeClass("desc-est-rechazado");
    $("#restado-desc").removeClass("desc-est-pendiente");

    rvCodColSofya = "";
    rvColaboradorSofya = "";
    rvctpava_bnfcol = "";
    rvcanvar_bnfcol = "";
    rvcolaborador = "";

    $("#rtxtcolaborador").val("");

}

function rajaxConceptoEdit(idSolicitud) {
    return $.ajax({
        type: "GET",
        url: "/Finanzas/RG_ConceptoCabEdit",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { idSolicitud: idSolicitud }
    });
}

function rajaxDetalleEditar(idSolicitud, idConceptoCab) {
    return $.ajax({
        type: "GET",
        url: "/Finanzas/RG_DetalleEditar",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { idSolicitud: idSolicitud, idConceptoCab: idConceptoCab }
    });
}


// Eventos Click

$("#rbtnCancelar").click(function () {
    document.getElementById("ridmCbxCeCo").focus();
});

$("#rbtnGenerarComprobante").click(function () {


    $.ajax({
        url: "/finanzas/RG_TienePendienteRend",
        type: "GET",
        dataType: "json",
        success: function (resultado) {

            if (resultado.isRedirect) {
                window.location.href = resultado.redirectUrl;
            }
            else {
                if (resultado.estado == "N") {


                    if (rvalidacionCabecera()) {

                        $("#rtxtCeCoModal1").val(rvCeCo);
                        $("#rtxtCeCoModal2").val(rvCeCo);
                        $("#rtxtCeCoModal3").val(rvCeCo);


                        if (rvIdSolicitud > 0) {

                            // Si ya existe una solicitud.
                            rlimpiarControlModal(1);
                            rlimpiarControlModal(2);
                            rlimpiarControlModal(3);

                            $("#rfacturaboleta-tab").removeClass("disabled");
                            $("#rplanillamovilidad-tab").removeClass("disabled");
                            $("#rdeclaracionjur-tab").removeClass("disabled");

                            $("#rregComprobModal .nav-tabs #rfacturaboleta-tab").tab('show');
                            rtabSeleccionado = 1;

                            rTIPO_REGISTRO = "INSERTAR";

                            rconceptoCabXTipo(rvIdTipo, 0);
                            $("#rregComprobModal").modal("show");
                        }
                        else {

                            let parametro = {
                                'opcion': 1,
                                'idSolicitud': 0,
                                'codigo': 0,
                                'idEmpresa': 1,
                                'idEstado': 10,
                                'idAnulado': 0,
                                'idTipoMod': rvIdTipoMod,
                                'observacion': $("#ridtxtaobs").val().trim(),

                                'codCeCo': rvCodCeCo,
                                'idConceptoDet': 0,
                                'valor': 0,
                                'secuencia': 0,
                                'usuario': USUARIO,
                                'usuarioCompleto': USUARIO_COMPLETO,
                                'nivelInterfaz': 0,
                                'idTipo': rvIdTipo,
                                'seleccionadoDet': 0,
                                'idConceptoCab': 0,

                                'cantDias': 0,
                                'montoSolicitado': 0,
                                'observacionDet': '',
                                'ctpava_bnf': rvctpava_bnfcol,
                                'canvar_bnf': rvcanvar_bnfcol,
                                'colaborador': rvcolaborador,

                                'codSede': rvCodSede
                            }



                            $.ajax({
                                url: "/finanzas/RG_SetRendicionGastos",
                                type: "post",
                                data: parametro,
                                dataType: "json",
                                success: function (resultado) {

                                    if (resultado.isRedirect) {
                                        window.location.href = resultado.redirectUrl;
                                    }
                                    else {

                                        if (resultado.status.idResponse > 0) {

                                            rvIdSolicitud = resultado.status.idResponse;
                                            rsolicitudCabCreada = resultado.responseEntity;

                                            $("#riddttFechaHora").val(resultado.responseEntity.fechaEmision);
                                            $("#ridtxtcodigo").val(resultado.responseEntity.codigo);
                                            $("#ridtxtcodigo").addClass("ridtxtcodigo-ok");

                                            document.getElementById("ridCbxSede").disabled = true;
                                            document.getElementById("ridtxtcodigo").disabled = true;
                                            document.getElementById("ridCbxTipo").disabled = true;
                                            document.getElementById("ridCbxTipoMod").disabled = true;
                                            document.getElementById("ridtxtaobs").disabled = true;
                                            document.getElementById("rcbxCeCo1").disabled = true;
                                            document.getElementById("rbtnBuscarColaborador").disabled = true;


                                            // MOSTRAR MODAL DE REGISTRO DE COMPROBANTE.

                                            let valorRestante = 0;

                                            rlimpiarControlModal(1);
                                            rlimpiarControlModal(2);
                                            rlimpiarControlModal(3);


                                            // Factura y boleta
                                            $("#rfacturaboleta-tab").removeClass("disabled");
                                            $("#rplanillamovilidad-tab").removeClass("disabled");
                                            $("#rdeclaracionjur-tab").removeClass("disabled");

                                            $("#rregComprobModal .nav-tabs #rfacturaboleta-tab").tab('show');

                                            rTIPO_REGISTRO = "INSERTAR";

                                            $("#rtxtCeCoModal1").val(rvCeCo);
                                            $("#rtxtCeCoModal2").val(rvCeCo);
                                            $("#rtxtCeCoModal3").val(rvCeCo);


                                            $("#rcbxMonedaModal1").val(rsolicitudCabCreada.idTipoMod);
                                            $("#rcbxMonedaModal2").val(rsolicitudCabCreada.idTipoMod);
                                            $("#rcbxMonedaModal3").val(rsolicitudCabCreada.idTipoMod);

                                            $("#rtxtDNIModal2").val(rsolicitudCabCreada.canvar_bnf);
                                            $("#rtxtDNIModal3").val(rsolicitudCabCreada.canvar_bnf);
                                            $("#rtxtApeNomModal2").val(rsolicitudCabCreada.colaborador);
                                            $("#rtxtApeNomModal3").val(rsolicitudCabCreada.colaborador);

                                            $("#rregComprobModal .kpi-modal-monto1").css("background-color", "#ABB2B9");
                                            $("#rregComprobModal .kpi-modal-monto2").css("background-color", "#ABB2B9");
                                            $("#rregComprobModal .kpi-modal-monto3").css("background-color", "#ABB2B9");

                                            rconceptoCabXTipo(rvIdTipo, 0);

                                            $("#rbtnAnularSolicitud").removeClass("btn-secondary");
                                            $("#rbtnAnularSolicitud").addClass("btn-danger");
                                            document.getElementById("rbtnAnularSolicitud").disabled = false;

                                            $("#rregComprobModal").modal("show");
                                        }
                                        else {
                                            rmensajePrincipal(false, "Ocurrio un problema");
                                        }
                                    }
                                },
                                error: function (xhr, ajaxOptions, thrownError) {

                                }
                            });

                        }

                    }



                }
                else {
                    Swal.fire({
                        title: 'Tiene rendiciones Pendientes',
                        text: "Textile Sourcing Company",
                        icon: 'warning',
                        showConfirmButton: true,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Ok'
                    });
                }
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });




});

function rvalidarAgregarDetalle() {
    let respuesta = false;
    let codigoGenerado = $("#ridtxtcodigo").val();

    if (vIdTipoMod == 0) {
        respuesta = false;

        Swal.fire({
            title: 'Debe seleccionar el tipo de Moneda',
            text: "Textile Sourcing Company",
            icon: 'warning',
            showConfirmButton: true,
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Ok'
        });

    }
    else if (codigoGenerado == "" || codigoGenerado == null || typeof (codigoGenerado) == "undefined") {
        respuesta = false;
        Swal.fire({
            title: 'Debe Generar la cabecera de la Solicitud para continuar',
            text: "Textile Sourcing Company",
            icon: 'warning',
            showConfirmButton: true,
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Ok'
        });

    }
    else {
        respuesta = true;
    }

    return respuesta;
}

$("#rbtnGuardarDetalle").click(function () {

    if (rvalidacionDetalle()) {

        $("#rdetalleConceptoInsertarEditar input[type='number']").each(function (i, obj) {
            let idConceptoDet = $(obj).attr("data-idconceptodet");
            let estaSeleccionado = rconceptoDetArray.find(x => x.idConceptoDet == idConceptoDet && x.isChecked == true);
            if (typeof (estaSeleccionado) != "undefined") {
                for (var i = 0; i < rconceptoDetArray.length; i++) {
                    if (rconceptoDetArray[i].isChecked == true && rconceptoDetArray[i].idConceptoDet == idConceptoDet) {
                        rconceptoDetArray[i].montoSolicitado = parseFloat($("#rdetalleConceptoInsertarEditar #txtMontoSol" + idConceptoDet.toString() + "").val());
                        rconceptoDetArray[i].observacionDet = $("#rdetalleConceptoInsertarEditar #txtObsDet" + idConceptoDet.toString() + "").val();
                    }
                }
            }
        });

        rconceptoDetArray = rconceptoDetArray.filter(x => x.isChecked == true);

        let parametro = {
            'opcion': 2,
            'idSolicitud': rvIdSolicitud,
            'codigo': 0,
            'idEmpresa': 1,
            'idEstado': 1,
            'idAnulado': 0,
            'idTipoMod': 0,
            'observacion': '',
            'codCeCo': rvCodCeCo,
            'idConceptoDet': rvIdConcepDet,
            'valor': $("#sidmvalor").val(),
            'secuencia': 0,
            'usuario': USUARIO,
            'usuarioCompleto': USUARIO_COMPLETO,
            'nivelInterfaz': 0,
            'idTipo': 0,
            'conceptoDetArray': rconceptoDetArray
        }

        $.ajax({
            url: "/finanzas/RG_SetRendicionGastosDet",
            type: "post",
            data: parametro,
            dataType: "json",
            success: function (resultado) {

                if (resultado.isRedirect) {
                    window.location.href = resultado.redirectUrl;
                }
                else {

                    $('#rmodalSolDetInsertar').modal('hide');

                    if (resultado.status.idResponse > 0) {
                        rmensajePrincipal(true, "Actualizando Detalle");
                    }
                    else {
                        rmensajePrincipal(false, "Ocurrio un problema al agregar el motivo");
                    }

                    rgetListaDetalle(rvIdSolicitud);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {

            }
        });
    }
});

$("#rbtnNuevo").click(function () {

    rvIdSolicitud = 0;
    rsolicitudCabCreada = {};

    rlimpiarControlModal(1);
    rlimpiarControlModal(2);
    rlimpiarControlModal(3);

    rlimpiarCabecera();
    rresetControles();

    document.getElementById("rbtnBuscarCodigo").disabled = false;
    $("#rbtnBuscarCodigo").removeClass("btn-secondary");
    $("#rbtnBuscarCodigo").addClass("btn-primary");


    document.getElementById("rbtnGenerarComprobante").disabled = false;
    $("#rbtnGenerarComprobante").removeClass("btn-secondary");
    $("#rbtnGenerarComprobante").addClass("btn-success");



    document.getElementById("rbtnBuscarColaborador").disabled = false;
    document.getElementById("ridtxtcodigo").disabled = true;
    document.getElementById("ridCbxSede").focus();
    $("#ridtxtcodigo").val("0");

});

$("#rtblDetalle").on('click', '.solicitudDetalleItem', function () {

    let idSolicitud = $(this).attr("data-idsolicitud");
    let idConceptoCab = $(this).attr("data-idconceptoCab");

    $.ajax({
        type: "GET",
        url: '/Finanzas/RG_SolicitudDetalle',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { idSolicitud: idSolicitud, idConceptoCab: idConceptoCab },
        success: function (data) {

            if (data.isRedirect) {
                window.location.href = data.redirectUrl;
            }
            else {

                let detHTML = "";
                let checkeHTML = "";

                $("#rdetalleConceptoVisualizar").empty();

                $("#ridmdCodigo").val(data.entidad.codigo);
                $("#ridmdConcepCab").val(data.entidad.conceptoCab);
                $("#ridmdCeCo").val(data.entidad.ceco);


                $.each(data.entidad.conceptoDetalle, function () {

                    detHTML += "<div class='row mt-1'>";

                    //if (this.existe == 0) {
                    //    checkeHTML = "<div class='col-1'>"
                    //        + "     <input type='checkbox' class='cseleccion' data-idConceptoDet=" + this.idConceptoDet + " disabled>"
                    //        + "</div>";
                    //}
                    //else {
                    //    checkeHTML = "<div class='col-1'>"
                    //        + "     <input type='checkbox' class='cseleccion' data-idConceptoDet=" + this.idConceptoDet + " readonly checked='checked'>"
                    //        + "</div>";
                    //}

                    detHTML = detHTML + checkeHTML;

                    detHTML = detHTML + "<div class='col-2'>"
                        + "      <p style='padding: 3px; border:1px solid #CCD1D1; border-radius: 3px;'>" + this.conceptoDet + "</p>"
                        + "</div>"
                        + "<div class='col-1'>"
                        + "     <p style='padding: 3px; border:1px solid #CCD1D1; border-radius: 3px;'>" + formatoNumero(this.montoSolicitado) + "</p>"
                        + "</div>"
                        + "<div class='col-4'>"
                        + "     <p style='padding: 3px; border:1px solid #CCD1D1; border-radius: 3px;'>" + this.observacionDet + "</p>"
                        + "</div>"
                        + "<div class='col-4'>"
                        + "     <p style='padding: 3px; border:1px solid #CCD1D1; border-radius: 3px;'>" + this.consideracion + "</p>"
                        + "</div> ";

                    detHTML = detHTML + "</div>";

                });

                $("#rdetalleConceptoVisualizar").html(detHTML);
                $('#rmodalSolDetVisualizar').modal('show');
            }
        },
        failure: function () {
            console.error('error al cargar tipos periodos');
        }
    });
});

$("#rtblDetalle").on('click', '.solicitudRendirItem', function () {
    $("#rregComprobModal").modal("show");
});

$("#rtblDetalle").on('click', '.solicitudEditarItem', function () {

    let idSolicitud = $(this).attr("data-idsolicitud");
    let idConceptoCab = $(this).attr("data-idconceptoCab");
    rswitchInsertarEditar = 2; // 2: Editar

    $.when(rajaxConceptoEdit(idSolicitud), rajaxDetalleEditar(idSolicitud, idConceptoCab)).done(function (dataConcepto, dataDetalle) {

        rentidadEditar = {};

        if (dataConcepto.isRedirect) {
            window.location.href = dataConcepto[0].redirectUrl;
        }
        else {

            let detHTML = "";
            let cbxDiasHTML = "";
            let cantChecked = 0;
            let primerLoopCbxDia = true;
            let cantDiaSeleccionado = 0;

            rdiasArrayEditar = [];
            rentidadEditar = dataDetalle[0].entidad;
            rvCodCeCo = dataDetalle[0].entidad.codCeCo;
            rvIdSolicitud = dataDetalle[0].entidad.idSolicitud;

            $("#rdetalleConceptoInsertarEditar").empty();


            // Concepto
            $("#ridmCbxConcepCab").empty();
            $.each(dataConcepto[0].lista, function () {
                $("#ridmCbxConcepCab").append($("<option/>").val(this.idConceptoCab).text(this.conceptoCab));
            });


            // Detalle
            $.each(rdiasArray, function () {

                //if (primerLoopCbxDia) {
                //    cbxDiasHTML += "<option value='" + this.codDia + "' selected>" + this.dia + "</option>";
                //    cantDiaSeleccionado = this.codDia;
                //}
                //else {
                //    cbxDiasHTML += "<option value='" + this.codDia + "'>" + this.dia + "</option>";
                //}


                if (objDet.tieneDias == "S") {
                    if (objDia.codDia.toString() != "0") {
                        if (primerLoopCbxDia) {
                            cbxDiasHTML += "<option value='" + objDia.codDia + "' selected>" + objDia.dia + "</option>";
                            cantDiaSeleccionado = objDia.codDia;

                            primerLoopCbxDia = false;
                        }
                        else {
                            cbxDiasHTML += "<option value='" + objDia.codDia + "'>" + objDia.dia + "</option>";
                        }
                    }
                }
                else if (objDet.tieneDias == "N") {
                    cbxDiasHTML = "<option value='1' selected>(SIN DIAS)</option>";
                    cantDiaSeleccionado = 1;
                }

            });



            $.each(dataDetalle[0].entidad.conceptoDetalle, function () {

                detHTML += "<div class='row mt-1'>";
                // editarrr
                if (this.existe == 0) {
                    checkeHTML = "<div class='col-1'>"
                        + "     <input type='checkbox' class='cseleccion' data-tienedias='" + obj.tieneDias + "'  data-id=" + this.idConceptoDet + " data-valor=" + this.montoSolicitado + " >"
                        + "</div>";
                    rconceptoDetArray.push({ idConceptoDet: this.idConceptoDet, seleccionadoDet: 0, codCeCo: rvCodCeCo, isChecked: false, cantDias: this.cantDias, montoSolicitado: this.montoSolicitado, observacionDet: this.observacionDet });
                }
                else {
                    checkeHTML = "<div class='col-1'>"
                        + "     <input type='checkbox' class='cseleccion' data-id=" + this.idConceptoDet + " data-valor=" + this.montoSolicitado + "  checked='checked'>"
                        + "</div>";
                    rconceptoDetArray.push({ idConceptoDet: this.idConceptoDet, seleccionadoDet: 1, codCeCo: rvCodCeCo, isChecked: true, cantDias: this.cantDias, montoSolicitado: this.montoSolicitado, observacionDet: this.observacionDet });

                    rsumTotalDetalle = rsumTotalDetalle + parseFloat(this.montoSolicitado * this.cantDias);
                    cantChecked++;
                }

                detHTML = detHTML + checkeHTML;

                detHTML = detHTML
                    + "<div class='col-2'>"
                    + "      <p style='padding: 4px; border:1px solid #CCD1D1; border-radius: 3px;'>" + this.conceptoDet + "</p>"
                    + "</div>"

                    + "<div class='col-1'>"
                    + "     <input class='form-control form-control-sm cmontosolicitar' type='number' id='txtMontoSol" + this.idConceptoDet + "' min='0' data-valor-maxs = '" + this.montoMaximoS + "' data-valor-maxd = '" + this.montoMaximoD + "'  data-idconceptodet='" + this.idConceptoDet + "' value='" + this.montoSolicitado + "'>"
                    + "</div>"

                    + "<div class='col-2'>"
                    + "     <input class='form-control form-control-sm cobsdet' type='text' id='txtObsDet" + this.idConceptoDet + "' data-idconceptodet='" + this.idConceptoDet + "' value='" + this.observacionDet + "'>"
                    + "</div>"

                    + "<div class='col-5'>"
                    + "     <p style='padding: 4px; border:1px solid #CCD1D1; border-radius: 3px;'>" + this.consideracion + "</p>"
                    + "</div> "

                    + "<div class='col-1'>"
                    + "   <select class='form-control form-control-sm cdias cbxDias" + this.idConceptoDet + "' data-valor-maxs = '" + this.montoMaximoS + "' data-valor-maxd = '" + this.montoMaximoD + "' data-id=" + this.idConceptoDet + "" + ((this.existe == 0 || obj.tieneDias == 'N') ? " disabled " : " ") + ">"
                    + cbxDiasHTML
                    + "</select>"

                    + "</div>"
                    + "</div>"


                rdiasArrayEditar.push({ idConceptoDet: this.idConceptoDet, cantDias: this.cantDias });

            });

            if (cantChecked == dataDetalle[0].entidad.conceptoDetalle.length) {
                $("#rseleccionar-todo").prop("checked", true);
            }


            let codigoGenerado = $("#ridtxtcodigo").val();

            $("#ridmcodigo").val(codigoGenerado);
            $("#ridmCbxConcepCab").val(rentidadEditar.idConceptoCab);
            $("#ridmCbxCeCo").val(rentidadEditar.codCeCo);

            rvCodCeCo = rentidadEditar.codCeCo;

            $("#sum-total-det").text(formatoNumero(rsumTotalDetalle * cantDiaSeleccionado));
            $("#rdetalleConceptoInsertarEditar").html(detHTML);
            $('#rmodalSolDetInsertar').modal('show');
        }

    });

});

$("#rtblDetalle").on('click', '.solicitudEliminarItem', function () {

    let idsolcitud = $(this).attr("data-idsolicitud");
    let idconceptocab = $(this).attr("data-idconceptocab");

    MostrarCarga("Eliminando registro...");

    Swal.fire({
        title: '¿Esta seguro de eliminar el registro?',
        text: "Textile Sourcing Company",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si',
        cancelButtonText: 'No'
    }).then((result) => {

        if (result.value) {

            let parametro = {
                'opcion': 3,
                'idSolicitud': idsolcitud,
                'codigo': 0,
                'idEmpresa': 1,
                'idEstado': 1,
                'idAnulado': 0,
                'idTipoMod': rvIdTipoMod,
                'observacion': '',

                'codCeCo': rvCodCeCo,
                'idConceptoDet': rvIdConcepDet,
                'valor': 0,
                'secuencia': 0,
                'usuario': USUARIO,
                'usuarioCompleto': USUARIO_COMPLETO,
                'nivelInterfaz': 0,
                'idTipo': rvIdTipo,
                'seleccionadoDet': 0,
                'idConceptoCab': idconceptocab,
                'observacionDet': "",
                'ctpava_bnf': "",
                'canvar_bnf': "",
                'colaborador': ""
            }


            $.ajax({
                url: "/finanzas/RG_EliminarSolicitud",
                type: "post",
                data: parametro,
                dataType: "json",
                success: function (resultado) {
                    console.log(resultado);

                    if (resultado.isRedirect) {
                        window.location.href = resultado.redirectUrl;
                    }
                    else {
                        if (resultado.status.idResponse > 0) {
                            rmensajePrincipal(true, "Item Eliminado Correctamente");
                        }
                        else {
                            rmensajePrincipal(false, "Ocurrio un problema");
                        }
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {

                }
            });

            rgetListaDetalle(rvIdSolicitud);
        }
    })

});

$("#rbtnBuscar").click(function () {

    let fechaIni = $('#idfechaInicio').val();
    let fechaFin = $('#idfechaFin').val();

    if (fechaIni == null ||
        fechaFin == null ||
        typeof (fechaIni) == "undefined" ||
        typeof (fechaFin) == "undefined" ||
        fechaIni.trim() == "" ||
        fechaFin.trim() == "") {

        Swal.fire('Seleccione la fecha, por favor')
    }
    else {

        var datos =
        {
            'fechaInicio': fechaIni,
            'fechaFin': fechaFin
        }

        $.ajax({
            type: "GET",
            url: '/finanzas/RG_BuscarHistorial',
            contentType: "application/json; charset=utf-8",
            data: datos,
            dataType: "json",
            success: function (data) {

                let tabla = $("#rtblHistorial").DataTable();

                tabla.destroy();
                var tr = "";

                data.lista.forEach((obj) => {

                    tr += "<tr> "
                        + " <td class='ocultar-columna'>" + obj.idSolicitud + "</td>"
                        + " <td> " + obj.fecha + "</td>"
                        + " <td> " + obj.codigo + "</td>"
                        + " <td> " + obj.observacion + "</td>"
                        + " <td> " + obj.estado + "</td>"
                        + " <td> " + obj.tipo + "</td>"
                        + " <td> " + obj.nivelAprob + "</td>"
                        + " <td> " + obj.moneda + "</td>"
                        + " <td> " + formatoNumero(obj.valor) + "</td>"
                        + "</tr>";
                });

                $("#rtblHistorialContent").html(tr);


                $("#rtblHistorial").DataTable(
                    {
                        dom: 'Bfrtip',
                        buttons: [
                            'copy', 'excel', 'pdf', 'print'
                        ],
                        'language': { 'url': '/libs/datatables/spanish.json' },
                        lengthMenu: [[10, 20, 30, -1], [10, 20, 30, 'Todos']]
                    });

            },
            failure: function () {
                console.error('error al cargar los periodos');
            }
        });

    }

});

$("#rbtnBuscarCodigo").click(function () {
    rlimpiarCabecera();

    document.getElementById("ridCbxSede").disabled = true;
    document.getElementById("ridCbxTipo").disabled = true;
    document.getElementById("ridCbxTipoMod").disabled = true;
    document.getElementById("ridtxtaobs").disabled = true;
    document.getElementById("rcbxCeCo1").disabled = true;

    rvIdSolicitud = 0;
    $("#ridtxtcodigo").val("");

    let tabla = $("#rtblDetalle").DataTable();
    tabla.destroy();
    var tr = "";
    $("#rtblDetalleContent").html(tr);
    $("#rtblDetalle").DataTable(
        {
            'language': { 'url': '/libs/datatables/spanish.json' },
            lengthMenu: [[10, 20, 30, -1], [10, 20, 30, 'Todos']]
        }
    );

    $("#restado-desc").text("");

    $("#restado-icon").removeClass("icon-liberado");
    $("#restado-icon").removeClass("icon-aprobado");
    $("#restado-icon").removeClass("icon-rechazado");
    $("#restado-icon").removeClass("icon-pendiente");

    $("#restado-desc").removeClass("desc-est-liberado");
    $("#restado-desc").removeClass("desc-est-aprobado");
    $("#restado-desc").removeClass("desc-est-rechazado");
    $("#restado-desc").removeClass("desc-est-pendiente");

    document.getElementById("ridtxtcodigo").disabled = false;
    document.getElementById("ridtxtcodigo").focus();
});



// Otros Eventos

$('#ridCbxTipoMod').change(function () {
    var $option = $(this).find('option:selected');
    rvIdTipoMod = parseInt($option.val());
    rvSimboloMoneda = rtipoMonedaArray.find(x => x.idTipoMod == rvIdTipoMod).simbolo;
});

$('#ridCbxSede').change(function () {
    var $option = $(this).find('option:selected');
    rvCodSede = $option.val();
});

$('#rcbxCeCo1').change(function () {
    var $option = $(this).find('option:selected');

    rvCodCeCo = parseInt($option.val());
    rCodCeCo = $option.text();
});

$('#ridCbxTipo').change(function () {
    var $option = $(this).find('option:selected');
    rvIdTipo = parseInt($option.val());

    if (rvIdTipo == 1 || rvIdTipo == 2) {
        $("#ridCbxTipoMod").val(1);
        rvIdTipoMod = $("#ridCbxTipoMod").val();
        rvSimboloMoneda = rtipoMonedaArray.find(x => x.idTipoMod == 1).simbolo;

        document.getElementById("ridCbxTipoMod").disabled = true;
    }
    else if (rvIdTipo == 3) {
        document.getElementById("ridCbxTipoMod").disabled = false;
    }
});

$('#ridtxtcodigo').keypress(function (event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);

    if (keycode == '13') {

        let codigo = $("#ridtxtcodigo").val().trim();

        if (codigo == "") {
            Swal.fire({
                title: 'Ingrese un código',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                showCancelButton: false,
                confirmButtonText: 'Ok',
                cancelButtonText: 'Cancel',
                focusConfirm: true
            }).then(result => {
                if (result.value) {
                    document.getElementById("ridtxtcodigo").focus();
                }
            });
        }
        else if (codigo.length < 10 || codigo.length > 10) {
            Swal.fire({
                title: 'Ingrese un código',
                title: 'Ingrese un código valido por favor',
                icon: 'warning',
                showConfirmButton: true,
                showCancelButton: false,
                confirmButtonText: 'Ok',
                cancelButtonText: 'Cancel',
                focusConfirm: true
            }).then(result => {
                if (result.value) {
                    document.getElementById("ridtxtcodigo").focus();
                }
            });
        }
        else {

            MostrarCarga("Buscando...");


            $.ajax({
                url: "/finanzas/RG_SolicitudXCodigo",
                type: "GET",
                data: { 'codigo': codigo },
                dataType: "json",
                success: function (resultado) {

                    if (resultado.isRedirect) {
                        window.location.href = resultado.redirectUrl;
                    }
                    else {
                        if (resultado.entidad.idSolicitud > 0) {

                            rvIdSolicitud = resultado.entidad.idSolicitud;
                            rsolicitudCabCreada = resultado.entidad;

                            $("#ridtxtcodigo").val(resultado.entidad.codigo);
                            $("#ridtxtcodigo").addClass("ridtxtcodigo-ok");
                            $("#riddttFechaHora").val(resultado.entidad.fecha);
                            $("#ridusuario").val(resultado.entidad.usuSolicitante);
                            $("#ridCbxTipo").val(resultado.entidad.idTipo);
                            $("#ridCbxTipoMod").val(resultado.entidad.idTipoMod);

                            $("#ridtxtaobs").val(resultado.entidad.observacion);
                            $("#restado-desc").text(resultado.entidad.estado);

                            rvctpava_bnfcol = resultado.entidad.ctpava_bnf;
                            rvcanvar_bnfcol = resultado.entidad.canvar_bnf;
                            rvcolaborador = resultado.entidad.colaborador;

                            $("#rtxtcolaborador").val(resultado.entidad.colaborador);

                            $("#restado-icon").removeClass("icon-liberado");
                            $("#restado-icon").removeClass("icon-aprobado");
                            $("#restado-icon").removeClass("icon-rechazado");
                            $("#restado-icon").addClass("icon-pendiente");

                            $("#restado-desc").removeClass("desc-est-liberado");
                            $("#restado-desc").removeClass("desc-est-aprobado");
                            $("#restado-desc").removeClass("desc-est-rechazado");
                            $("#restado-desc").addClass("desc-est-pendiente");


                            rgetListaDetalle(rvIdSolicitud);

                            rmensajePrincipal(true, "Mostrando Solicitud");


                            // Si tiene estado mayor a 0, tiene aprobación
                            if (resultado.entidad.idEstado > 10 || resultado.entidad.idAnulado > 0) {

                                $("#rbtnGenerarComprobante").addClass("btn-secondary");
                                $("#rbtnGenerarComprobante").removeClass("btn-success");
                                document.getElementById("rbtnGenerarComprobante").disabled = true;

                                $("#rbtnAnularSolicitud").removeClass("btn-danger");
                                $("#rbtnAnularSolicitud").addClass("btn-secondary");
                                document.getElementById("rbtnAnularSolicitud").disabled = true;

                            }
                            else {

                                $("#rbtnGenerarComprobante").addClass("btn-success");
                                $("#rbtnGenerarComprobante").removeClass("btn-secondary");
                                document.getElementById("rbtnGenerarComprobante").disabled = false;

                                $("#rbtnAnularSolicitud").removeClass("btn-secondary");
                                $("#rbtnAnularSolicitud").addClass("btn-danger");
                                document.getElementById("rbtnAnularSolicitud").disabled = false;

                            }

                        }
                        else {
                            rmensajePrincipal(false, "Solicitud no encontrada");

                            rlimpiarCabecera();
                            rresetControles();

                            $("#ridtxtcodigo").val("");
                            document.getElementById("ridCbxSede").disabled = false;
                            document.getElementById("ridtxtcodigo").disabled = false;
                            document.getElementById("ridCbxTipo").disabled = true;
                            document.getElementById("ridCbxTipoMod").disabled = true;
                            document.getElementById("ridtxtaobs").disabled = true;
                            document.getElementById("rcbxCeCo1").disabled = true;


                            document.getElementById("rbtnBuscarCodigo").disabled = true;
                            $("#rbtnBuscarCodigo").removeClass("btn-primary");
                            $("#rbtnBuscarCodigo").addClass("btn-secondary");

                            document.getElementById("ridtxtcodigo").focus();
                        }
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {

                }
            });

        }

    }
});

function rgetConceptoCab() {

    $.ajax({
        type: "GET",
        url: '/Finanzas/RG_ConceptoCabReembolso',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            let firstConceptoCab = 0;
            let firstLoop = true;

            $("#rcbxConcepCabModal1").empty();
            $("#rcbxConcepCabModal2").empty();
            $("#rcbxConcepCabModal3").empty();

            rconceptoCabeceraArray = data.lista;

            rconceptoCabArray1 = rconceptoCabeceraArray.filter(x => x.idTipoComp == parseInt($("#rcbxTipoCpbteCompModal1").val()));
            rconceptoCabArray2 = rconceptoCabeceraArray.filter(x => x.idTipoComp == parseInt($("#rcbxTipoCpbteCompModal2").val()));
            rconceptoCabArray3 = rconceptoCabeceraArray.filter(x => x.idTipoComp == parseInt($("#rcbxTipoCpbteCompModal3").val()));


            firstConceptoCab = 0;
            firstLoop = true;
            $.each(rconceptoCabArray1, function () {
                if (firstLoop) {
                    firstConceptoCab = this.idConceptoCab;
                    firstLoop = false;
                }
                $("#rcbxConcepCabModal1").append($("<option/>").val(this.idConceptoCab).text(this.conceptoCab));
            });
            rvIdConceptoCab1 = firstConceptoCab;


            firstConceptoCab = 0;
            firstLoop = true;
            $.each(rconceptoCabArray2, function () {
                if (firstLoop) {
                    firstConceptoCab = this.idConceptoCab;
                    firstLoop = false;
                }
                $("#rcbxConcepCabModal2").append($("<option/>").val(this.idConceptoCab).text(this.conceptoCab));
            });
            rvIdConceptoCab2 = firstConceptoCab;


            firstConceptoCab = 0;
            firstLoop = true;
            $.each(rconceptoCabArray3, function () {
                if (firstLoop) {
                    firstConceptoCab = this.idConceptoCab;
                    firstLoop = false;
                }
                $("#rcbxConcepCabModal3").append($("<option/>").val(this.idConceptoCab).text(this.conceptoCab));
            });
            rvIdConceptoCab3 = firstConceptoCab;


            $("#rcbxConcepCabModal1").val(rvIdConceptoCab1);
            $("#rcbxConcepCabModal2").val(rvIdConceptoCab2);
            $("#rcbxConcepCabModal3").val(rvIdConceptoCab3);

            rgetConceptoDet(parseInt($("#rcbxTipoCpbteCompModal1").val()), rvIdConceptoCab1);
            rgetConceptoDet(parseInt($("#rcbxTipoCpbteCompModal2").val()), rvIdConceptoCab2);
            rgetConceptoDet(parseInt($("#rcbxTipoCpbteCompModal3").val()), rvIdConceptoCab3);

        },
        failure: function () {
            console.error('error al cargar tipos periodos');
        }
    });
}

$('#rdetalleConceptoInsertarEditar').on('click', '.cseleccion', function (e) {

    let inputCheck = $(this);
    let idConceptoDet = $(this).attr("data-id");
    let tienedias = $(this).attr("data-tienedias");
    let valorPre = "";
    let valor = 0;

    if (inputCheck.is(":checked")) {
        // hhhhh

        for (var i = 0; i < rconceptoDetArray.length; i++) {
            if (rconceptoDetArray[i].idConceptoDet == idConceptoDet) {

                rconceptoDetArray[i].seleccionadoDet = 1;
                rconceptoDetArray[i].isChecked = true;

                if (tienedias == "S") {
                    $("#rdetalleConceptoInsertarEditar .cbxDias" + idConceptoDet.toString() + "").attr("disabled", false);
                }
                else if (tienedias == "N") {
                    $("#rdetalleConceptoInsertarEditar .cbxDias" + idConceptoDet.toString() + "").attr("disabled", true);
                }


                valorPre = $("#rdetalleConceptoInsertarEditar #txtMontoSol" + idConceptoDet.toString() + "").val();

                if (valorPre == null || typeof (valorPre) == "undefined" || valorPre.trim() == "") {
                    valor = 0;
                }
                else {
                    valor = parseFloat(valorPre);
                }

                rsumTotalDetalle = rsumTotalDetalle + (valor * parseFloat(rconceptoDetArray[i].cantDias));
            }
        }

    } else {
        for (var i = 0; i < rconceptoDetArray.length; i++) {
            if (rconceptoDetArray[i].idConceptoDet == idConceptoDet) {
                rconceptoDetArray[i].seleccionadoDet = 0;
                rconceptoDetArray[i].isChecked = false;
                rconceptoDetArray[i].cantDias = $("#rdetalleConceptoInsertarEditar .cbxDias" + idConceptoDet.toString() + "").val();
                rconceptoDetArray[i].observacionDet = $("#rdetalleConceptoInsertarEditar #txtObsDet" + idConceptoDet.toString() + "").val();

                valorPre = $("#rdetalleConceptoInsertarEditar #txtMontoSol" + idConceptoDet.toString() + "").val();

                if (valorPre == null || typeof (valorPre) == "undefined" || valorPre.trim() == "") {
                    valor = 0;
                }
                else {
                    valor = parseFloat(valorPre);
                }

                rsumTotalDetalle = rsumTotalDetalle - valor * parseFloat(rconceptoDetArray[i].cantDias);
            }
        }



        if (tienedias == "S") {
            $("#rdetalleConceptoInsertarEditar .cbxDias" + idConceptoDet.toString() + "").attr("disabled", false);
        }
        else if (tienedias == "N") {
            $("#rdetalleConceptoInsertarEditar .cbxDias" + idConceptoDet.toString() + "").attr("disabled", true);
        }

    }

    if (rconceptoDetArray.filter(x => x.isChecked == true).length == rconceptoDetArray.length) {
        $("#rseleccionar-todo").prop("checked", true);
    }
    else {
        $("#rseleccionar-todo").prop("checked", false);
    }

    $("#sum-total-det").text(formatoNumero(rsumTotalDetalle));
});

$('#rmodalSolDetInsertar').on('click', '#rseleccionar-todo', function (e) {
    let idConceptoDet = 0;
    var inputCheck = $(this);
    rsumTotalDetalle = 0;

    if (inputCheck.is(":checked")) {

        let valorXConceptDet = 0;

        $("#rdetalleConceptoInsertarEditar input[type='checkbox']").each(function (i, obj) {
            let valor = $(this).attr("data-valor");
            idConceptoDet = $(this).attr("data-id");
            valorXConceptDet = 0;

            if (rconceptoDetArray[i].idConceptoDet == idConceptoDet) {
                rconceptoDetArray[i].seleccionadoDet = 1;
                rconceptoDetArray[i].cantDias = $("#rdetalleConceptoInsertarEditar .cbxDias" + idConceptoDet.toString() + "").val();
            }

            rconceptoDetArray[i].isChecked = true;

            $(this).prop("checked", true);
            $("#rdetalleConceptoInsertarEditar .cbxDias" + idConceptoDet.toString() + "").attr("disabled", false);

            valorXConceptDet = parseFloat(valor) * rconceptoDetArray[i].cantDias;

            rsumTotalDetalle = rsumTotalDetalle + valorXConceptDet;
        });

    } else {
        $("#rdetalleConceptoInsertarEditar input[type='checkbox']").each(function (i, obj) {
            idConceptoDet = $(this).attr("data-id");

            if (rconceptoDetArray[i].idConceptoDet == idConceptoDet) {
                rconceptoDetArray[i].seleccionadoDet = 0;
                rconceptoDetArray[i].cantDias = $("#rdetalleConceptoInsertarEditar .cbxDias" + idConceptoDet.toString() + "").val();
            }

            rconceptoDetArray[i].isChecked = false;
            $(this).prop("checked", false);
            $("#rdetalleConceptoInsertarEditar .cbxDias" + idConceptoDet.toString() + "").attr("disabled", true);

            rsumTotalDetalle = 0;
        });
    }

    $("#sum-total-det").text(formatoNumero(rsumTotalDetalle));
});

$('#rcbxConcepCabModal1').change(function () {
    var $option = $(this).find('option:selected');
    rvIdConceptoCab1 = $option.val().toString();

    if (rvIdConceptoCab1 != 0 && rvIdConceptoCab1 != null && typeof (rvIdConceptoCab1) != "undefined") {
        rconceptoDetalleArray1 = [];

        rgetConceptoDet($("#rcbxTipoCpbteCompModal1").val(), rvIdConceptoCab1);
    }
});

$('#rcbxConcepCabModal2').change(function () {
    var $option = $(this).find('option:selected');
    rvIdConceptoCab2 = $option.val().toString();

    if (rvIdConceptoCab2 != 0 && rvIdConceptoCab2 != null && typeof (rvIdConceptoCab2) != "undefined") {
        rconceptoDetalleArray2 = [];

        rgetConceptoDet($("#rcbxTipoCpbteCompModal2").val(), rvIdConceptoCab2);
    }
});

$('#rcbxConcepCabModal3').change(function () {

    var $option = $(this).find('option:selected');
    rvIdConceptoCab3 = $option.val().toString();

    if (rvIdConceptoCab3 != 0 && rvIdConceptoCab3 != null && typeof (rvIdConceptoCab3) != "undefined") {
        rconceptoDetalleArray3 = [];

        rgetConceptoDet($("#rcbxTipoCpbteCompModal3").val(), rvIdConceptoCab3);
    }
});

$('#rcbxConcepDetModal1').change(function () {
    var $option = $(this).find('option:selected');
    rvIdConceptoDet1 = $option.val().toString();

    ractualizarTotalSolicitado(parseInt($("#rcbxTipoCpbteCompModal1").val()), rvIdConceptoDet1);
    rseleccionarConsideracion(parseInt($("#rcbxTipoCpbteCompModal1").val()), rvIdConceptoDet1);
});

$('#rcbxConcepDetModal2').change(function () {
    var $option = $(this).find('option:selected');
    rvIdConceptoDet2 = $option.val().toString();

    ractualizarTotalSolicitado(parseInt($("#rcbxTipoCpbteCompModal2").val()), rvIdConceptoDet2);
    rseleccionarConsideracion(parseInt($("#rcbxTipoCpbteCompModal2").val()), rvIdConceptoDet2);
});

$('#rcbxConcepDetModal3').change(function () {
    var $option = $(this).find('option:selected');
    rvIdConceptoDet3 = $option.val().toString();

    ractualizarTotalSolicitado(parseInt($("#rcbxTipoCpbteCompModal3").val()), rvIdConceptoDet3);
    rseleccionarConsideracion(parseInt($("#rcbxTipoCpbteCompModal3").val()), rvIdConceptoDet3);
});


function rgetConceptoDet(pTipoCpte, idConceptoCab) {

    $.ajax({
        type: "GET",
        url: '/Finanzas/RG_ConceptoDetalle',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: {
            idConceptoCab: idConceptoCab,
            idTipoComp: pTipoCpte
        },
        success: function (data) {

            if (pTipoCpte == 1 || pTipoCpte == 2) {
                // Factura y boleta
                rconceptoDetalleArray1 = data.lista;
                rllenarCbxDetalle(pTipoCpte, data.lista);
            }
            else if (pTipoCpte == 3) {
                // Planilla de Movilidad
                rconceptoDetalleArray2 = data.lista;
                rllenarCbxDetalle(pTipoCpte, rconceptoDetalleArray2);

            }
            else if (pTipoCpte == 4) {
                // Declaracion Jurada.
                rconceptoDetalleArray3 = data.lista;
                rllenarCbxDetalle(pTipoCpte, data.lista);
            }

        },
        failure: function () {
            console.error('error al cargar tipos periodos');
        }
    });
}

function rllenarCbxDetalle(pTipoCpte, lista) {

    let firstIdConceptoDet = 0;
    let firstConceptoDet = 0;

    let primerValorBool = true;
    let existeConceptoDetalle = false;



    if (pTipoCpte == 1 || pTipoCpte == 2) {

        // Factura y Boleta

        $("#rcbxConcepDetModal1").empty();

        $.each(lista, function () {
            existeConceptoDetalle = false;

            if (primerValorBool) {
                firstIdConceptoDet = this.idConceptoDet;
                firstConceptoDet = this.conceptoDet;

                primerValorBool = false;
            }

            //for (var i = 0; i < rdetComprobanteArray1.length; i++) {
            //    if (rdetComprobanteArray1[i].idConcepDet == this.idConceptoDet) {
            //        existeConceptoDetalle = true;
            //    }
            //}

            //if (!existeConceptoDetalle) {
            $("#rcbxConcepDetModal1").append($("<option/>").val(this.idConceptoDet).text(this.conceptoDet));
            //}
        });

        $("#rcbxConcepDetModal1").val(firstIdConceptoDet);

        rvIdConceptoDet1 = firstIdConceptoDet;
        rvConceptoDet1 = firstConceptoDet;

        ractualizarTotalSolicitado(pTipoCpte, firstIdConceptoDet);
        rseleccionarConsideracion(pTipoCpte, firstIdConceptoDet);

    }
    else if (pTipoCpte == 3) {

        // Planilla de Movilidad
        $("#rcbxConcepDetModal2").empty();

        $.each(lista, function () {
            existeConceptoDetalle = false;

            if (primerValorBool) {
                firstIdConceptoDet = this.idConceptoDet;
                firstConceptoDet = this.conceptoDet;

                primerValorBool = false;
            }

            for (var i = 0; i < rdetComprobanteArray2.length; i++) {
                if (rdetComprobanteArray2[i].idConcepDet == this.idConceptoDet) {
                    existeConceptoDetalle = true;
                }
            }

            if (!existeConceptoDetalle) {
                $("#rcbxConcepDetModal2").append($("<option/>").val(this.idConceptoDet).text(this.conceptoDet));
            }
        });

        $("#rcbxConcepDetModal2").val(firstIdConceptoDet);

        rvIdConceptoDet2 = firstIdConceptoDet;
        rvConceptoDet2 = firstConceptoDet;

        ractualizarTotalSolicitado(pTipoCpte, rvIdConceptoDet2);
        rseleccionarConsideracion(pTipoCpte, rvIdConceptoDet2);


    }
    else if (pTipoCpte == 4) {

        // Declaracion Jurada.
        $("#rcbxConcepDetModal3").empty();

        $.each(lista, function () {
            existeConceptoDetalle = false;

            if (primerValorBool) {
                firstIdConceptoDet = this.idConceptoDet;
                firstConceptoDet = this.conceptoDet;

                primerValorBool = false;
            }

            for (var i = 0; i < rdetComprobanteArray3.length; i++) {
                if (rdetComprobanteArray3[i].idConcepDet == this.idConceptoDet) {
                    existeConceptoDetalle = true;
                }
            }

            if (!existeConceptoDetalle) {
                $("#rcbxConcepDetModal3").append($("<option/>").val(this.idConceptoDet).text(this.conceptoDet));
            }
        });

        $("#rcbxConcepDetModal3").val(firstIdConceptoDet);

        rvIdConceptoDet3 = firstIdConceptoDet;
        rvConceptoDet3 = firstConceptoDet;

        ractualizarTotalSolicitado(pTipoCpte, rvIdConceptoDet3);
        rseleccionarConsideracion(pTipoCpte, rvIdConceptoDet3);
    }


}

function ractualizarTotalSolicitado(pTipoCpte, pIdConceptoDet) {

    if (pTipoCpte == 1 || pTipoCpte == 2) {
        // Factura y Boleta
        if (rvIdTipoMod == 1) {
            rvTotalSolicitado1 = rconceptoDetalleArray1.find(x => x.idConceptoDet == parseInt(pIdConceptoDet)).montoMaximoS;
        }
        else if (rvIdTipoMod == 2) {
            rvTotalSolicitado1 = rconceptoDetalleArray1.find(x => x.idConceptoDet == parseInt(pIdConceptoDet)).montoMaximoD;
        }
        //$("#ridMontoSolicitadoModal1").text(rvSimboloMoneda + " " + formatoNumero(rvTotalSolicitado1));
    }
    else if (pTipoCpte == 3) {
        // Planilla de Movilidad
        if (rvIdTipoMod == 1) {
            rvTotalSolicitado2 = rconceptoDetalleArray2.find(x => x.idConceptoDet == parseInt(pIdConceptoDet)).montoMaximoS;
        }
        else if (rvIdTipoMod == 2) {
            rvTotalSolicitado2 = rconceptoDetalleArray2.find(x => x.idConceptoDet == parseInt(pIdConceptoDet)).montoMaximoD;
        }
        //$("#ridMontoSolicitadoModal2").text(rvSimboloMoneda + " " + formatoNumero(rvTotalSolicitado2));
    }
    else if (pTipoCpte == 4) {
        // Planilla de Movilidad
        if (rvIdTipoMod == 1) {
            rvTotalSolicitado3 = rconceptoDetalleArray3.find(x => x.idConceptoDet == parseInt(pIdConceptoDet)).montoMaximoS;
        }
        else if (rvIdTipoMod == 2) {
            rvTotalSolicitado3 = rconceptoDetalleArray3.find(x => x.idConceptoDet == parseInt(pIdConceptoDet)).montoMaximoD;
        }
        //$("#ridMontoSolicitadoModal3").text(rvSimboloMoneda + " " + formatoNumero(rvTotalSolicitado3));
    }

}

function rseleccionarConsideracion(pTipoCpte, pIdConceptoDet) {

    let consideracion = "";

    if (pTipoCpte == 1 || pTipoCpte == 2) {
        // Factura y Boleta
        consideracion = rconceptoDetalleArray1.find(x => x.idConceptoDet == pIdConceptoDet).consideracion;
        $("#rtxtConsideracionModal1").val(consideracion);
    }
    else if (pTipoCpte == 3) {
        // Planilla de Movilidad
        consideracion = rconceptoDetalleArray2.find(x => x.idConceptoDet == pIdConceptoDet).consideracion;
        $("#rtxtConsideracionModal2").val(consideracion);
    }
    else if (pTipoCpte == 4) {
        // Declaracion Jurada
        consideracion = rconceptoDetalleArray3.find(x => x.idConceptoDet == pIdConceptoDet).consideracion;
        $("#rtxtConsideracionModal3").val(consideracion);
    }

}

$('#rcbxConcepDet').change(function () {

    let $option = $(this).find('option:selected');
    rvIdConcepDet = $option.val();
    rvConcepDet = $option.text();

    ractualizarTotalSolicitado(rvIdConcepDet, 1);
    rseleccionarConsideracion(rvIdConcepDet);

});

$('#rdetalleConceptoInsertarEditar').on('change', '.cdias', function (e) {

    rsumTotalDetalle = 0;

    $("#rdetalleConceptoInsertarEditar input[type='checkbox']").each(function (i, obj) {

        let valorPre = "";
        let valorActual = 0;

        let idConceptoDetCheckbox = $(obj).attr("data-id");
        let valorMaximoS = $(obj).attr("data-valor-maxs");
        let valorMaximoD = $(obj).attr("data-valor-maxd");

        if (rconceptoDetArray[i].isChecked == true) {

            rconceptoDetArray[i].seleccionadoDet = 1;
            rconceptoDetArray[i].cantDias = $("#rdetalleConceptoInsertarEditar .cbxDias" + idConceptoDetCheckbox.toString() + "").val();
            rconceptoDetArray[i].montoSolicitado = $("#rdetalleConceptoInsertarEditar #txtMontoSol" + idConceptoDetCheckbox.toString() + "").val();
            rconceptoDetArray[i].observacionDet = $("#rdetalleConceptoInsertarEditar #txtObsDet" + idConceptoDetCheckbox.toString() + "").val();


            valorPre = $("#rdetalleConceptoInsertarEditar #txtMontoSol" + idConceptoDetCheckbox.toString() + "").val();
            if (valorPre == null || typeof (valorPre) == "undefined" || valorPre.trim() == "") {
                valorActual = 0;
            }
            else {
                valorActual = parseFloat(valorPre);
            }

            // SOLES
            if (vIdTipoMod == 1) {

                if (valorActual > parseFloat(valorMaximoS)) {

                    $("#txtMontoSol" + idConceptoDetCheckbox.toString() + "").val(0);
                    valorActual = 0;

                    Swal.fire({
                        title: 'El monto ingresado no puede ser mayor al máximo permitido por día S/.' + valorMaximoS.toString(),
                        text: "Textile Sourcing Company",
                        icon: 'warning',
                        showConfirmButton: true,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Ok'
                    });

                    document.getElementById("txtMontoSol" + idConceptoDetCheckbox.toString() + "").focus();
                }

                rsumTotalDetalle = rsumTotalDetalle + (valorActual * parseFloat(rconceptoDetArray[i].cantDias));
            }
            else if (vIdTipoMod == 2) {
                // DOLAR

                if (valorActual > parseFloat(valorMaximoD)) {

                    $("#txtMontoSol" + idConceptoDetCheckbox.toString() + "").val(0);
                    valorActual = 0;

                    Swal.fire({
                        title: 'El monto ingresado no puede ser mayor al máximo permitido por día U$ ' + valorMaximoD.toString(),
                        text: "Textile Sourcing Company",
                        icon: 'warning',
                        showConfirmButton: true,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Ok'
                    });

                    document.getElementById("txtMontoSol" + idConceptoDetCheckbox.toString() + "").focus();
                }

                rsumTotalDetalle = rsumTotalDetalle + (valorActual * parseFloat(rconceptoDetArray[i].cantDias));
            }
        }
        else {

            rconceptoDetArray[i].seleccionadoDet = 0;
            rconceptoDetArray[i].cantDias = $("#rdetalleConceptoInsertarEditar .cbxDias" + idConceptoDetCheckbox.toString() + "").val();
            rconceptoDetArray[i].observacionDet = $("#rdetalleConceptoInsertarEditar #txtObsDet" + idConceptoDetCheckbox.toString() + "").val();
        }
    });

    $("#sum-total-det").text(formatoNumero(rsumTotalDetalle));

});

$('#rmodalSolDetInsertar').on('shown.bs.modal', function (e) {

    let idConceptoDet = 0;
    let primerCantDias = 0;
    let primerLoopCbxDia = true;

    if (rswitchInsertarEditar == 2) {
        $("#rdetalleConceptoInsertarEditar .cdias").each(function (i, obj) {

            idConceptoDet = $(this).attr("data-id");
            $("#ridmCbxConcepCab").prop("disabled", true);

            $.each(rdiasArray, function () {
                if (primerLoopCbxDia) {
                    primerCantDias = this.codDia;
                }
                primerLoopCbxDia = false;
            });

            let dias = rdiasArrayEditar.find(x => x.idConceptoDet == idConceptoDet).cantDias;

            if (dias > 0) {
                $("#rdetalleConceptoInsertarEditar .cbxDias" + idConceptoDet.toString() + "").val(dias);
            }
            else {
                $("#rdetalleConceptoInsertarEditar .cbxDias" + idConceptoDet.toString() + "").val(primerCantDias);
            }
        });
    }

});

$('#ridBusquedaColaborador').keypress(function (event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);

    let filtro = $("#ridBusquedaColaborador").val();

    if (keycode == '13') {

        if (filtro == "") {
            Swal.fire({
                title: 'Ingrese el filtro para la busqueda',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                showCancelButton: false,
                confirmButtonText: 'Ok',
                cancelButtonText: 'Cancel',
                focusConfirm: true
            }).then(result => {
                if (result.value) {
                    document.getElementById("ridBusquedaColaborador").focus();
                }
            });
        }
        else {

            $.ajax({
                url: "/finanzas/RG_ColaboradorSofya",
                type: "GET",
                data: { 'filtro': filtro, 'nivelInterfaz': sNIVEL_INTERFAZ },
                dataType: "json",
                success: function (resultado) {

                    if (resultado.isRedirect) {
                        window.location.href = resultado.redirectUrl;
                    }
                    else {
                        let tabla = $("#rtblColaborador").DataTable();

                        tabla.destroy();
                        var tr = "";

                        resultado.lista.forEach((obj) => {

                            tr += "<tr> "
                                + " <td> " + obj.codsofya + "</td>"
                                + " <td> " + obj.colaborador + "</td>"
                                + " <td class='text-center'>"
                                + "     <button data-ctpava='" + obj.ctpava + "' data-canvar='" + obj.canvar + "' data-codceco=" + obj.codceco + " data-codsofya='" + obj.codsofya + "' data-colaborador='" + obj.colaborador + "' data-toggle='collapse' class='btn btn-sm btn-success cseleccionar ml-1'><i class='fas fa-check'></i></button>"
                                + "</td>"
                                + "</tr>";
                        });

                        $("#rtblColaboradorContent").html(tr);

                        $("#rtblColaborador").DataTable(
                            {
                                searching: false,
                                paging: false,
                                info: false,
                                "scrollY": "250px",
                                "scrollCollapse": true,
                                'language': { 'url': '/libs/datatables/spanish.json' },
                                lengthMenu: [[10, 20, 30, -1], [10, 20, 30, 'Todos']]
                            });
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {

                }
            });

        }

    }
});

$('#rtblColaborador').on('click', '.cseleccionar', function (e) {

    rvCodColSofya = $(this).attr("data-codsofya");
    rvColaboradorSofya = $(this).attr("data-colaborador");
    rvctpava_bnfcol = $(this).attr("data-ctpava");
    rvcanvar_bnfcol = $(this).attr("data-canvar");
    rvCodCeCo = $(this).attr("data-codceco");

    rvcolaborador = $.trim(rvColaboradorSofya);

    $("#rtxtcolaborador").val($.trim(rvColaboradorSofya));

    $("#rtxtDNIModal2").val(rvcanvar_bnfcol);
    $("#rtxtDNIModal3").val(rvcanvar_bnfcol);
    $("#rtxtApeNomModal2").val(rvcolaborador);
    $("#rtxtApeNomModal3").val(rvcolaborador);

    $("#rcbxCeCo1").val(rvCodCeCo);
    rvCeCo = $("#rcbxCeCo1 option:selected").text();

    $("#rmodalColaboradorSofya").modal("hide");
});

$("#rbtnBuscarColaborador").click(function () {
    $("#rmodalColaboradorSofya").modal("show");
});

$("#rbtnAnularSolicitud").click(function () {

    if (rvIdSolicitud > 0) {

        var options = {};

        $.map(stiposAnulacionCabArray,
            function (o) {
                options[o.id] = o.name;
            });

        Swal.fire({
            title: 'Seleccione el motivo de la Anulación de solicitud',
            icon: 'info',
            input: 'select',
            inputOptions: options,
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            cancelButtonText: 'Cancelar',
            confirmButtonText: 'Ok'

        }).then((result) => {

            if (result.value) {

                let parametro = {
                    'opcion': 8,
                    'opcionTipoAprob': 0,
                    'idSolicitud': parseInt(rvIdSolicitud),
                    'secuencia': 0,
                    'valorEntrega': 0,
                    'usuario': USUARIO,
                    'nivelInterfaz': rNIVEL_INTERFAZ,
                    'idAnulado': parseInt(result.value),
                    'idAnuladoDet': 0
                };

                $.ajax({
                    url: "/finanzas/RG_AnularSolicitud",
                    type: "post",
                    data: parametro,
                    dataType: "json",
                    success: function (resultado) {

                        if (resultado.isRedirect) {
                            window.location.href = resultado.redirectUrl;
                        }
                        else {

                            if (resultado.status.idResponse > 0) {
                                rmensajePrincipal(true, resultado.status.statusResponse);
                            }
                            else {
                                rmensajePrincipal(false, resultado.status.statusResponse);
                            }


                            $("#rbtnAnularSolicitud").removeClass("btn-danger");
                            $("#rbtnAnularSolicitud").addClass("btn-secondary");
                            document.getElementById("rbtnAnularSolicitud").disabled = true;

                            $("#rbtnGenerarComprobante").removeClass("btn-success");
                            $("#rbtnGenerarComprobante").addClass("btn-secondary");
                            document.getElementById("rbtnGenerarComprobante").disabled = true;

                            rconceptoDetArray = [];

                            rvalidarrestadoSolicitud(2, rvIdSolicitud);
                            rgetListaDetalle(rvIdSolicitud);
                        }
                    },
                    error: function (xhr, ajaxOptions, thrownError) {

                    }
                });
            }

        });

    }
    else {
        Swal.fire({
            title: 'No existe solicitud para anular',
            text: "Textile Sourcing Company",
            icon: 'warning',
            showConfirmButton: true,
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Ok'
        });
    }


});




// Funciones Generales

function formatoNumero(x) {
    x = x.toFixed(2);
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function rmensajePrincipal(estado, mensaje) {
    if (estado == true) {

        const Toast = Swal.mixin({
            toast: true,
            position: 'top-end',
            showConfirmButton: false,
            timer: 2500,
            timerProgressBar: true,
            didOpen: (toast) => {
                toast.addEventListener('mouseenter', Swal.stopTimer)
                toast.addEventListener('mouseleave', Swal.resumeTimer)
            }
        })

        Toast.fire({
            icon: 'success',
            title: mensaje
        });

    }
    else if (estado == false) {

        const Toast = Swal.mixin({
            toast: true,
            position: 'top-end',
            showConfirmButton: false,
            timer: 2500,
            timerProgressBar: true,
            didOpen: (toast) => {
                toast.addEventListener('mouseenter', Swal.stopTimer)
                toast.addEventListener('mouseleave', Swal.resumeTimer)
            }
        })

        Toast.fire({
            icon: 'error',
            title: mensaje
        });

    }
}

function rgetConceptoDet_old(idConceptoCab) {
    $.ajax({
        type: "GET",
        url: '/Finanzas/RG_ConceptoDetalleInsertar',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { idConceptoCab: idConceptoCab },
        success: function (data) {

            $("#rdetalleConceptoInsertarEditar").empty();

            let detHTML = "";
            let cbxDiasHTML = "";

            let primerLoopCbxDia = true;
            let cantDiaSeleccionado = 0;

            $.each(rdiasArray, function () {

                if (primerLoopCbxDia) {
                    cbxDiasHTML += "<option value='" + this.codDia + "' selected>" + this.dia + "</option>";
                    cantDiaSeleccionado = this.codDia;
                }
                else {
                    cbxDiasHTML += "<option value='" + this.codDia + "'>" + this.dia + "</option>";
                }

                primerLoopCbxDia = false;
            });

            $.each(data.lista, function () {

                detHTML += "<div class='row mt-1'>"
                    + "<div class='col-1'>"
                    + "     <input type='checkbox' class='cseleccion' data-id=" + this.idConceptoDet + " data-valor=" + this.montoMaximo + ">"
                    + "</div>"
                    + "<div class='col-2'>"
                    + "      <p style='padding: 4px; border:1px solid #CCD1D1; border-radius: 3px;'>" + this.conceptoDet + "</p>"
                    + "</div>"
                    + "<div class='col-1'>"
                    + "     <input class='form-control form-control-sm cmontosolicitar' type='number' id='txtMontoSol" + this.idConceptoDet + "' min='0' max='" + this.montoMaximo + "' data-idconceptodet='" + this.idConceptoDet + "' data-valor-maxs = '" + this.montoMaximoS + "' data-valor-maxd='" + this.montoMaximoD + "'>"
                    + "</div>"

                    + "<div class='col-2'>"
                    + "     <input class='form-control form-control-sm cobsdet' type='text' id='txtObsDet" + this.idConceptoDet + "' data-idconceptodet='" + this.idConceptoDet + "'>"
                    + "</div>"

                    + "<div class='col-5'>"
                    + "     <p style='padding: 4px; border:1px solid #CCD1D1; border-radius: 3px;'>" + this.consideracion + "</p>"
                    + "</div>"
                    + "<div class='col-1'>"
                    + "   <select class='form-control form-control-sm cdias cbxDias" + this.idConceptoDet + "' data-id=" + this.idConceptoDet + " data-valor-maxs = '" + this.montoMaximoS + "' data-valor-maxd='" + this.montoMaximoD + "' disabled>"
                    + cbxDiasHTML
                    + "</select>"
                    + "</div>"
                    + "</div>";

                //rsumTotalDetalle = rsumTotalDetalle + parseFloat(this.montoMaximo);
                rsumTotalDetalle = 0;
                rconceptoDetArray.push({ idConceptoDet: this.idConceptoDet, seleccionadoDet: 1, codCeCo: rvCodCeCo, isChecked: false, cantDias: cantDiaSeleccionado, montoSolicitado: 0, observacionDet: this.observacionDet });
            });

            $("#rseleccionar-todo").prop("checked", false);
            $("#rdetalleConceptoInsertarEditar").html(detHTML);
            $("#sum-total-det").text(formatoNumero(rsumTotalDetalle * cantDiaSeleccionado));

        },
        failure: function () {
            console.error('error al cargar tipos periodos');
        }
    });
}

$("#rdetalleConceptoInsertarEditar").on("keyup", ".cmontosolicitar", function () {

    rsumTotalDetalle = 0;

    $("#rdetalleConceptoInsertarEditar input[type='number']").each(function (i, obj) {
        idConceptoDet = $(obj).attr("data-idconceptodet");

        let valorMaximoS = $(obj).attr("data-valor-maxs");
        let valorMaximoD = $(obj).attr("data-valor-maxd");

        if (rconceptoDetArray[i].idConceptoDet == idConceptoDet && rconceptoDetArray[i].isChecked == true) {
            let valorActual = 0;
            let cantDias = $("#rdetalleConceptoInsertarEditar .cbxDias" + idConceptoDet.toString() + "").val();


            if ($(obj).val() == null || typeof ($(obj).val()) == "undefined" || $(obj).val().trim() == "") {
                valorActual = 0;
            }
            else {
                valorActual = parseFloat($(obj).val());
            }


            // SOLES
            if (vIdTipoMod == 1) {
                if (valorActual > parseFloat(valorMaximoS)) {

                    $("#txtMontoSol" + idConceptoDet.toString() + "").val(0);
                    valorActual = 0;

                    Swal.fire({
                        title: 'El monto ingresado no puede ser mayor al máximo permitido por día S/.' + valorMaximoS.toString(),
                        text: "Textile Sourcing Company",
                        icon: 'warning',
                        showConfirmButton: true,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Ok'
                    });

                    document.getElementById("txtMontoSol" + idConceptoDet.toString() + "").focus();
                }

            }
            else if (vIdTipoMod == 2) {
                // DOLAR

                if (valorActual > parseFloat(valorMaximoD)) {

                    $("#txtMontoSol" + idConceptoDet.toString() + "").val(0);
                    valorActual = 0;

                    Swal.fire({
                        title: 'El monto ingresado no puede ser mayor al máximo permitido por día U$ ' + valorMaximoD.toString(),
                        text: "Textile Sourcing Company",
                        icon: 'warning',
                        showConfirmButton: true,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Ok'
                    });

                    document.getElementById("txtMontoSol" + idConceptoDet.toString() + "").focus();
                }
            }

            rsumTotalDetalle = rsumTotalDetalle + (valorActual * parseFloat(cantDias));
        }
    });

    $("#sum-total-det").text(formatoNumero(rsumTotalDetalle));
});

$("#rtabRendicionGastos").on("click", ".rctabRegComp", function () {
    let index = $(this).attr("data-id");
    rtabSeleccionado = parseInt(index);
});

$('.rcfecha').change(function () {

    var inputDate = new Date(this.value);
    var today = new Date();

    if (inputDate > today) {


        $(this).val(FormatoFecha(today, "-"));

        Swal.fire({
            title: 'La fecha no puede ser mayor a la fecha Actual',
            text: "Textile Sourcing Company",
            icon: 'warning',
            showConfirmButton: true,
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Ok'
        });
    }

});

$("#rtblDetalleContent").on('click', '.seleccion', function () {

    let idsolicitud = $(this).attr("data-idsolicitud");
    let secuencia = $(this).attr("data-secuencia");
    let valor = $(this).attr("data-valor");
    let total = $(this).attr("data-total");
    let codceco = $(this).attr("data-codceco");
    let ceco = $(this).attr("data-ceco");
    let totalRestante = $(this).attr("data-restante");

    if ($(this).prop("checked")) {
        rdetalleArray.push({ idsolicitud: idsolicitud, secuencia: secuencia, valor: parseFloat(valor), total: parseFloat(total), codceco: codceco, ceco: ceco, totalrestante: parseFloat(totalRestante) });

    } else {

        for (var i = 0; i < rdetalleArray.length; i++) {
            if (rdetalleArray[i].idsolicitud == idsolicitud && rdetalleArray[i].secuencia == secuencia) {
                rdetalleArray.splice(i, 1);
            }
        }
    }
});


function restadoSolicitud(pIdSolicitud) {
    $.ajax({
        url: "/finanzas/RG_restadoSolicitud",
        type: "GET",
        data: { idSolicitud: pIdSolicitud },
        dataType: "json",
        success: function (resultado) {

            if (resultado.isRedirect) {
                window.location.href = resultado.redirectUrl;
            }
            else {
                $("#restado-desc").text(resultado.entidad.estado);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
        }
    });
}

function rgetTipoComprobantes() {

    $.ajax({
        type: "GET",
        url: '/Finanzas/RG_TipoComprobantes',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            let primerValorBool = true;

            rtiposComprobantesArray = data.lista;

            $("#rcbxTipoCpbteCompModal1").empty();
            $("#rcbxTipoCpbteCompModal2").empty();
            $("#rcbxTipoCpbteCompModal3").empty();

            $.each(data.lista, function () {
                if (primerValorBool) {
                    rvCodTipoComprobante = this.idTipoComp;
                }

                if (this.idTipoComp == 1 || this.idTipoComp == 2) {
                    $("#rcbxTipoCpbteCompModal1").append($("<option/>").val(this.idTipoComp).text(this.tipoComprobante));
                }
                else if (this.idTipoComp == 3) {
                    $("#rcbxTipoCpbteCompModal2").append($("<option/>").val(this.idTipoComp).text(this.tipoComprobante));
                }
                else if (this.idTipoComp == 4) {
                    $("#rcbxTipoCpbteCompModal3").append($("<option/>").val(this.idTipoComp).text(this.tipoComprobante));
                }

                primerValorBool = false;
            });

            rcambioTipoCpte(rvCodTipoComprobante);

            $("#rcbxTipoCpbteCompModal1").val(rvCodTipoComprobante);
            $("#rcbxTipoCpbteCompModal2").val(3); // Planilla de Movilidad
            $("#rcbxTipoCpbteCompModal3").val(4); // Declaracion Jurada

            document.getElementById("rcbxTipoCpbteCompModal2").disabled = true;
            document.getElementById("rcbxTipoCpbteCompModal3").disabled = true;

            rgetConceptoCab();

        },
        failure: function () {
            console.error('error al cargar tipos periodos');
        }
    });
}

function rgetTipoMoneda() {

    $.ajax({
        type: "GET",
        url: '/Finanzas/RG_TiposDeMoneda',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#rcbxMonedaModal1").empty();
            $("#rcbxMonedaModal2").empty();
            $("#rcbxMonedaModal3").empty();

            $.each(data.lista, function () {
                $("#rcbxMonedaModal1").append($("<option/>").val(this.idTipoMod).text(this.descripcion));
            });
            $.each(data.lista, function () {
                $("#rcbxMonedaModal2").append($("<option/>").val(this.idTipoMod).text(this.descripcion));
            });
            $.each(data.lista, function () {
                $("#rcbxMonedaModal3").append($("<option/>").val(this.idTipoMod).text(this.descripcion));
            });

            $("#rcbxMonedaModal1").val(0);
            $("#rcbxMonedaModal2").val(0);
            $("#rcbxMonedaModal3").val(0);
        },
        failure: function () {
            console.error('error al cargar tipos periodos');
        }
    });
}

$('#rtxtDNIModal2').keypress(function (event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);

    if (keycode == '13') {
        let dni = $("#rtxtDNIModal2").val().trim();
        rbusquedaXDNI(dni, rtabSeleccionado)
    }
});

$('#rtxtDNIModal3').keypress(function (event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);

    if (keycode == '13') {
        let dni = $("#rtxtDNIModal3").val().trim();
        rbusquedaXDNI(dni, rtabSeleccionado)
    }
});

$('#rmodalSolDetInsertar').on('hidden.bs.modal', function (e) {
    $("#rtxtRUCModal").val("");
    $("#rtxtRSModal").val("");
    $("#rtxtDireccionModal").val("");
});

$("#rbtnAgregarMotivo1").click(function () {

    if (rvalidacionDetCmpbte(rtabSeleccionado)) {

        //let cantidad = parseFloat($("#txtCantidad1").val());
        let cantidad = 1;
        let valorUnitarioTotal = parseFloat($("#rtxtValorUnitarioTotal1").val());
        let valorUnitario = 0;

        if ($("#rcbxTipoCpbteCompModal1").val().trim() == "1") {
            valorUnitario = (valorUnitarioTotal) / ((PORCENTAJE_IGV / 100) + 1);

        }
        else if ($("#rcbxTipoCpbteCompModal1").val().trim() == "2") {
            valorUnitario = (valorUnitarioTotal);
        }

        let total = (cantidad * valorUnitarioTotal);

        const sumDetalleSol = rdetalleArray.reduce((acc, item) => {
            return acc += item.totalrestante
        }, 0);

        const sumAgregadosDet = rdetComprobanteArray1.reduce((acc, item) => {
            return acc += item.total;
        }, 0);


        riDetalle1++;

        var now = new Date();

        rdetComprobanteArray1.push({
            idSolicitud: rvIdSolicitud,
            idConcepCab: rvIdConceptoCab1,
            idConcepDet: rvIdConceptoDet1,
            conceptoDet: rvConceptoDet1,
            iDetalle: riDetalle1,
            fecha: FormatoFecha(now, "/"),
            fechaBD: FormatoFecha(now, ""),
            detalle1: ($("#rcbxTipoCpbteCompModal1").val().trim() == "1" ? $("#rtxtDescripcion1").val() : $("#rtxtObsCompModal1").val()),
            detalle2: "",
            codum: $("#rcbxUM1").val(),
            valorunit: valorUnitario,
            cantidad: cantidad,
            total: total,
            seccion: 1,
            codCeCo: rvCodCeCo,
            idTipo: rvIdTipo
        });


        let tabla = $("#rtblFacturaBoleta").DataTable();

        tabla.destroy();
        var tr = "";

        rdetComprobanteArray1.forEach((obj) => {

            tr += "<tr> "
                + " <td> </td>"
                + " <td> " + obj.codum + "</td>"
                + " <td> " + obj.conceptoDet + "</td>"
                + " <td> " + obj.detalle1 + "</td>"
                //+ " <td> " + obj.cantidad + "</td>"
                + " <td> " + formatoNumero(obj.valorunit) + "</td>"
                + " <td> " + formatoNumero(obj.total) + "</td>"
                + " <td class='text-center'>"
                + "     <button data-iDetalle='" + obj.iDetalle + "' data-toggle='collapse' class='btn btn-sm btn-danger cselectEliminar1 ml-1'><i class='fas fa-trash'></i></button>"
                + "</td>"
                + "</tr>";
        });

        $("#rtblFacturaBoletaContent").html(tr);

        $("#rtblFacturaBoleta").DataTable(
            {
                'language': { 'url': '/libs/datatables/spanish.json' },
                lengthMenu: [[3], [3]]
            });


        const importeTotal = rdetComprobanteArray1.reduce((acc, item) => {
            return acc += (item.valorunit);
        }, 0);


        if ($("#rcbxTipoCpbteCompModal1").val().trim() == "1") {
            // FACTURA
            $("#ridimportetotal1").val(formatoNumero(importeTotal));
            $("#ridigv1").val(formatoNumero(importeTotal * (PORCENTAJE_IGV / 100)));
            $("#ridtotal1").val(formatoNumero(importeTotal + (importeTotal * (PORCENTAJE_IGV / 100))));

            $("#rtxtDescripcion1").val("");
            //$("#txtCantidad1").val("");
            $("#rtxtValorUnitarioTotal1").val("");

            document.getElementById("rcbxUM1").focus = true;
        }
        else if ($("#rcbxTipoCpbteCompModal1").val().trim() == "2") {
            // BOLETA
            $("#ridimportetotal1").val(formatoNumero(importeTotal));
            $("#ridigv1").val(0);
            $("#ridtotal1").val(formatoNumero(importeTotal));

            $("#rcbxUM1").val("UN");
            $("#rtxtDescripcion1").val("");
            //$("#txtCantidad1").val("1");
            $("#rtxtValorUnitarioTotal1").val("");

            document.getElementById("rtxtValorUnitarioTotal1").focus = true;
        }


        const totalDetalle = rdetComprobanteArray1.reduce((acc, item) => {

            //if (item.idConcepDet == rvIdConcepDet) {
            acc += item.total;
            //}
            return acc;
        }, 0);

        // Descontar del monto maximo del Item de detalle.
        //const consumidoXDetalle = rdetComprobanteArray1.reduce((acc, item) => {
        //    if (item.idConcepDet == rvIdConcepDet) {
        //        acc += item.total;
        //    }
        //    return acc;
        //}, 0);


        document.getElementById("rcbxConcepCabModal1").disabled = true;
        document.getElementById("rcbxConcepDetModal1").disabled = true;
        document.getElementById("rtxtObservacionModal1").disabled = true;

        //$("#ridMontoSolicitadoModal1").text(rvSimboloMoneda + " " + formatoNumero(rvTotalSolicitado1 - totalDetalle));
        //$("#ridMontoSolicitadoModal1").text(rvSimboloMoneda + " " + formatoNumero(rvTotalSolicitado1));
    }

});

var rdetCpteArrayValid2 = [];
var ri2 = 0;

$("#rbtnAgregarMotivo2").click(function () {

    if (rvalidacionDetCmpbte(rtabSeleccionado)) {

        let importeIngresado = parseFloat($("#rtxtImporteModal2").val());

        riDetalle2++;
        ri2++;

        let v2_maximoS = 0;
        let v2_maximoD = 0;
        let estadoValidacion = false;
        let valorSigaLoop = true;

        // validar el monto maximo
        rdetCpteArrayValid2.push({ id: riDetalle2, fecha: $("#rtxtFechaModalDet2").val(), monto: parseFloat($("#rtxtImporteModal2").val()) });

        v2_TipoMod = $("#rcbxMonedaModal2").val();

        v2_maximoS = rtiposComprobantesArray.find(x => x.idTipoComp == parseInt($("#rcbxTipoCpbteCompModal2").val())).maximoSoles;
        v2_maximoD = rtiposComprobantesArray.find(x => x.idTipoComp == parseInt($("#rcbxTipoCpbteCompModal2").val())).maximoDolar;


        var result = [];
        var sumXFecha = 0;

        rdetCpteArrayValid2.reduce(function (res, value) {
            if (!res[value.fecha]) {
                res[value.fecha] = { fecha: value.fecha, monto: 0 };
                result.push(res[value.fecha])
            }
            res[value.fecha].monto += value.monto;
            return res;
        }, {});

        for (var i = 0; i < result.length; i++) {

            if (valorSigaLoop) {

                sumXFecha = result[i].monto;

                if (v2_TipoMod == "1") {
                    // Soles.
                    if (sumXFecha > v2_maximoS) {
                        estadoValidacion = false;

                        Swal.fire({
                            title: 'El monto máximo permitido es S/.' + v2_maximoS,
                            text: "Textile Sourcing Company",
                            icon: 'warning',
                            showConfirmButton: true,
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: 'Ok'
                        });

                        for (var i = 0; i < rdetCpteArrayValid2.length; i++) {
                            if (rdetCpteArrayValid2[i].id == riDetalle2) {
                                rdetCpteArrayValid2.splice(i, 1);
                            }
                        }
                        $("#rtxtImporteModal2").val("");
                        valorSigaLoop = false;
                    }
                    else {
                        estadoValidacion = true;
                    }
                }
                else if (v2_TipoMod == "2") {
                    // Dolar
                    if (sumXFecha > v2_maximoD) {
                        estadoValidacion = false;

                        Swal.fire({
                            title: 'El monto máximo permitido es U$.' + v2_maximoD,
                            text: "Textile Sourcing Company",
                            icon: 'warning',
                            showConfirmButton: true,
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: 'Ok'
                        });

                        for (var i = 0; i < rdetCpteArrayValid2.length; i++) {
                            if (rdetCpteArrayValid2[i].id == riDetalle2) {
                                rdetCpteArrayValid2.splice(i, 1);
                            }
                        }

                        $("#rtxtImporteModal2").val("");

                        valorSigaLoop = false;
                    }
                    else {
                        estadoValidacion = true;
                    }
                }
                else {
                    estadoValidacion = true;
                }
            }
        }

        if (estadoValidacion) {

            rdetComprobanteArray2.push({
                idSolicitud: rvIdSolicitud,
                idConcepCab: rvIdConceptoCab2,
                idConcepDet: rvIdConceptoDet2,
                conceptoDet: rvConceptoDet2,
                observacionDet: $("#rtxtObservacionModal2").val(),
                iDetalle: riDetalle2,
                fecha: FormatoFecha($("#rtxtFechaModalDet2").val(), "/"),
                fechaBD: FormatoFecha($("#rtxtFechaModalDet2").val(), ""),
                detalle1: $("#rtxtOrgDstModal2").val(),
                detalle2: $("#rtxtMotivoModal2").val(),
                codum: "UN", // UNIDAD
                valorunit: parseFloat($("#rtxtImporteModal2").val()),
                cantidad: 1,
                total: parseFloat($("#rtxtImporteModal2").val()),
                seccion: 1,
                codCeCo: rvCodCeCo,
                idTipo: rvIdTipo
            });

            let tabla = $("#rtblPlanillaMovilidad").DataTable();

            tabla.destroy();
            var tr = "";

            rdetComprobanteArray2.forEach((obj) => {

                tr += "<tr> "
                    + " <td> </td>"
                    + " <td> " + obj.fecha + "</td>"
                    + " <td> " + obj.detalle1 + "</td>"
                    + " <td> " + obj.detalle2 + "</td>"
                    + " <td> " + formatoNumero(obj.valorunit) + "</td>"
                    + " <td class='text-center'>"
                    + "     <button data-iDetalle='" + obj.iDetalle + "' data-toggle='collapse' class='btn btn-sm btn-danger cselectEliminar2 ml-1'><i class='fas fa-trash'></i></button>"
                    + "</td>"
                    + "</tr>";
            });

            $("#rtblPlanillaMovilidadContent").html(tr);

            $("#rtblPlanillaMovilidad").DataTable(
                {
                    'language': { 'url': '/libs/datatables/spanish.json' },
                    lengthMenu: [[3], [3]]
                });

            $("#rtxtOrgDstModal2").val("");
            $("#rtxtMotivoModal2").val("");
            $("#rtxtImporteModal2").val("");

            document.getElementById("rtxtOrgDstModal2").focus = true;

            document.getElementById("rcbxConcepCabModal2").disabled = true;
            document.getElementById("rcbxConcepDetModal2").disabled = true;
            document.getElementById("rtxtObservacionModal2").disabled = true;

            const sumAgregadosDet = rdetComprobanteArray2.reduce((acc, item) => {
                return acc += (item.valorunit * item.cantidad);
            }, 0);

            //$("#ridMontoSolicitadoModal2").text(rvSimboloMoneda + " " + formatoNumero(rvTotalSolicitado2));
        }
    }

});

$("#rbtnAgregarMotivo3_1").click(function () {

    if (rvalidacionDetCmpbte(rtabSeleccionado)) {

        let respuesta = false;
        let importeIngresado = parseFloat($("#rtxtMontoDet3_1").val());

        if (importeIngresado == "0" || importeIngresado == "" || typeof (importeIngresado) == "undefined" || isNaN(importeIngresado)) {

            Swal.fire({
                title: 'Ingrese un monto',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });
        }
        else if (parseFloat(importeIngresado) <= 0) {

            Swal.fire({
                title: 'Ingrese un monto positivo',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });

        }
        else {

            let sumTotal3 = 0;
            let v3_TipoMod = $("#rcbxMonedaModal3").val();
            let v3_maximoS = rtiposComprobantesArray.find(x => x.idTipoComp == parseInt($("#rcbxTipoCpbteCompModal3").val())).maximoSoles;
            let v3_maximoD = rtiposComprobantesArray.find(x => x.idTipoComp == parseInt($("#rcbxTipoCpbteCompModal3").val())).maximoDolar;

            // Validar si excede el monto maximo.
            if (v3_TipoMod.toString() == 1) {

                // Soles.
                if (rdetComprobanteArray3.length > 0) {
                    sumTotal3 = rdetComprobanteArray3.reduce((acc, item) => {
                        return acc += item.valorunit * item.cantidad
                    }, 0);
                }
                else {
                    sumTotal3 = parseFloat($("#rtxtMontoDet3_1").val());
                }

                if (parseFloat(sumTotal3) > v3_maximoS) {
                    respuesta = false;
                    Swal.fire({
                        title: 'El monto máximo permitido es S/.' + v3_maximoS,
                        text: "Textile Sourcing Company",
                        icon: 'warning',
                        showConfirmButton: true,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Ok'
                    });
                }
                else {
                    respuesta = true;
                }

            }
            else if (v3_TipoMod.toString() == 2) {

                // Dolar.
                if (rdetComprobanteArray2.length > 0) {
                    sumTotal3 = rdetComprobanteArray3.reduce((acc, item) => {
                        return acc += item.valorunit * item.cantidad
                    }, 0);
                }
                else {
                    sumTotal3 = parseFloat($("#rtxtMontoDet3_1").val());
                }


                if (parseFloat(sumTotal3) > v3_maximoD) {
                    respuesta = false;
                    Swal.fire({
                        title: 'El monto máximo permitido es U$ ' + v3_maximoD,
                        text: "Textile Sourcing Company",
                        icon: 'warning',
                        showConfirmButton: true,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Ok'
                    });
                }
                else {
                    respuesta = true;
                }
            }


            // Paso la validacion.
            if (respuesta) {

                const sumAgregadosDet = rdetComprobanteArray3.reduce((acc, item) => {
                    return acc += (item.valorunit * item.cantidad);
                }, 0);

                riDetalle3++;

                rdetComprobanteArray3.push({
                    idSolicitud: rvIdSolicitud,
                    idConcepCab: rvIdConceptoCab3,
                    idConcepDet: rvIdConceptoDet3,
                    conceptoDet: rvConceptoDet3,
                    iDetalle: riDetalle3,
                    fecha: FormatoFecha($("#rtxtFechaModalDet3_1").val(), "-"),
                    fechaBD: FormatoFecha($("#rtxtFechaModalDet3_1").val(), ""),
                    detalle1: $("#rcbxMonedaModal3 option:selected").text(),
                    detalle2: "",
                    codum: "UN", // UNIDAD
                    valorunit: parseFloat($("#rtxtMontoDet3_1").val()),
                    cantidad: 1,
                    total: parseFloat($("#rtxtMontoDet3_1").val()),
                    seccion: 1,
                    codCeCo: rvCodCeCo,
                    idTipo: rvIdTipo
                });

                let tabla = $("#rtblDeclaracionJurada3_1").DataTable();

                tabla.destroy();
                var tr = "";

                rdetComprobanteArray3.forEach((obj) => {
                    if (obj.seccion == 1) {

                        tr += "<tr> "
                            + " <td> </td>"
                            + " <td> " + obj.fecha + "</td>"
                            + " <td> " + obj.detalle1 + "</td>"
                            + " <td> " + formatoNumero(obj.valorunit) + "</td>"
                            + " <td class='text-center'>"
                            + "     <button data-idetalle='" + obj.iDetalle + "' data-toggle='collapse' class='btn btn-sm btn-danger cselectEliminar3_1 ml-1'><i class='fas fa-trash'></i></button>"
                            + "</td>"
                            + "</tr>";
                    }
                });

                $("#rtblDeclaracionJuradaContent3_1").html(tr);

                $("#rtblDeclaracionJurada3_1").DataTable(
                    {
                        'language': { 'url': '/libs/datatables/spanish.json' },
                        lengthMenu: [[5], [5]]
                    });

                $("#rtxtMontoDet3_1").val("");

                document.getElementById("rtxtFechaModalDet3_1").focus = true;


                // Descontar del monto maximo del Item de detalle.
                const consumidoXDetalle = rdetComprobanteArray3.reduce((acc, item) => {
                    //if (item.seccion == 1) {
                    acc += item.total;
                    //}
                    return acc;
                }, 0);

                document.getElementById("rcbxConcepCabModal3").disabled = true;
                document.getElementById("rcbxConcepDetModal3").disabled = true;
                document.getElementById("rtxtObservacionModal3").disabled = true;

                //$("#ridMontoSolicitadoModal3").text(rvSimboloMoneda + " " + formatoNumero(rvTotalSolicitado3 - consumidoXDetalle));
                //$("#ridMontoSolicitadoModal3").text(rvSimboloMoneda + " " + formatoNumero(rvTotalSolicitado3));

            }
        }
    }
});

$("#rbtnAgregarMotivo3_2").click(function () {

    if (rvalidacionDetCmpbte(rtabSeleccionado)) {

        let respuesta = false;
        let importeIngresado = parseFloat($("#rtxtMontoDet3_2").val());

        if (importeIngresado == "0" || importeIngresado == "" || typeof (importeIngresado) == "undefined" || isNaN(importeIngresado)) {

            Swal.fire({
                title: 'Ingrese un monto',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });
        }
        else if (parseFloat(importeIngresado) <= 0) {

            Swal.fire({
                title: 'Ingrese un monto positivo',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });

        }
        else {

            let sumTotal3 = 0;
            let v3_TipoMod = $("#rcbxMonedaModal3").val();
            let v3_maximoS = rtiposComprobantesArray.find(x => x.idTipoComp == parseInt($("#rcbxTipoCpbteCompModal3").val())).maximoSoles;
            let v3_maximoD = rtiposComprobantesArray.find(x => x.idTipoComp == parseInt($("#rcbxTipoCpbteCompModal3").val())).maximoDolar;

            // Validar si excede el monto maximo.
            if (v3_TipoMod.toString() == 1) {

                // Soles.
                if (rdetComprobanteArray3.length > 0) {
                    sumTotal3 = rdetComprobanteArray3.reduce((acc, item) => {
                        return acc += item.valorunit * item.cantidad
                    }, 0);
                }
                else {
                    sumTotal3 = parseFloat($("#rtxtMontoDet3_2").val());
                }

                if (parseFloat(sumTotal3) > v3_maximoS) {
                    respuesta = false;
                    Swal.fire({
                        title: 'El monto máximo permitido es S/.' + v3_maximoS,
                        text: "Textile Sourcing Company",
                        icon: 'warning',
                        showConfirmButton: true,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Ok'
                    });
                }
                else {
                    respuesta = true;
                }

            }
            else if (v3_TipoMod.toString() == 2) {

                // Dolar.
                if (zdetComprobanteArray3.length > 0) {
                    sumTotal3 = zdetComprobanteArray3.reduce((acc, item) => {
                        return acc += item.valorunit * item.cantidad
                    }, 0);
                }
                else {
                    sumTotal3 = parseFloat($("#rtxtMontoDet3_2").val());
                }

                if (parseFloat(sumTotal3) > v3_maximoD) {
                    respuesta = false;
                    Swal.fire({
                        title: 'El monto máximo permitido es U$ ' + v3_maximoD,
                        text: "Textile Sourcing Company",
                        icon: 'warning',
                        showConfirmButton: true,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Ok'
                    });
                }
                else {
                    respuesta = true;
                }
            }


            if (respuesta) {

                riDetalle3++;

                rdetComprobanteArray3.push({
                    idSolicitud: rvIdSolicitud,
                    idConcepCab: rvIdConceptoCab3,
                    idConcepDet: rvIdConceptoDet3,
                    conceptoDet: rvConceptoDet3,
                    iDetalle: riDetalle3,
                    fecha: FormatoFecha($("#rtxtFechaModalDet3_2").val(), "-"),
                    fechaBD: FormatoFecha($("#rtxtFechaModalDet3_2").val(), ""),
                    detalle1: $("#rcbxMonedaModal3 option:selected").text(),
                    detalle2: "",
                    codum: "UN", // UNIDAD
                    valorunit: parseFloat($("#rtxtMontoDet3_2").val()),
                    cantidad: 1,
                    total: parseFloat($("#rtxtMontoDet3_2").val()),
                    seccion: 2,
                    codCeCo: rvCodCeCo,
                    idTipo: rvIdTipo
                });

                let tabla = $("#rtblDeclaracionJurada3_2").DataTable();

                tabla.destroy();
                var tr = "";

                rdetComprobanteArray3.forEach((obj) => {
                    if (obj.seccion == 2) {

                        tr += "<tr> "
                            + " <td> </td>"
                            + " <td> " + obj.fecha + "</td>"
                            + " <td> " + obj.detalle1 + "</td>"
                            + " <td> " + formatoNumero(obj.valorunit) + "</td>"
                            + " <td class='text-center'>"
                            + "     <button data-idetalle='" + obj.iDetalle + "' data-toggle='collapse' class='btn btn-sm btn-danger cselectEliminar3_2 ml-1'><i class='fas fa-trash'></i></button>"
                            + "</td>"
                            + "</tr>";
                    }
                });

                $("#rtblDeclaracionJuradaContent3_2").html(tr);

                $("#rtblDeclaracionJurada3_2").DataTable(
                    {
                        'language': { 'url': '/libs/datatables/spanish.json' },
                        lengthMenu: [[5], [5]]
                    });

                $("#rtxtMontoDet3_2").val("");

                document.getElementById("rtxtFechaModalDet3_2").focus = true;


                // Descontar del monto maximo del Item de detalle.
                const consumidoXDetalle = rdetComprobanteArray3.reduce((acc, item) => {
                    //if (item.seccion == 2) {
                    acc += item.total;
                    //}
                    return acc;
                }, 0);

                document.getElementById("rcbxConcepCabModal3").disabled = true;
                document.getElementById("rcbxConcepDetModal3").disabled = true;
                document.getElementById("rtxtObservacionModal3").disabled = true;

                //$("#ridMontoSolicitadoModal3").text(rvSimboloMoneda + " " + formatoNumero(rvTotalSolicitado3 - consumidoXDetalle));
                //$("#ridMontoSolicitadoModal3").text(rvSimboloMoneda + " " + formatoNumero(rvTotalSolicitado3));

            }
        }
    }
});

$("#rtblFacturaBoleta").on("click", ".cselectEliminar1", function () {

    let vIDetalle = $(this).attr("data-idetalle");

    for (var i = 0; i < rdetComprobanteArray1.length; i++) {
        if (rdetComprobanteArray1[i].iDetalle == parseInt(vIDetalle)) {
            rdetComprobanteArray1.splice(i, 1);
        }
    }

    const sumDetalleSol = rdetalleArray.reduce((acc, item) => {
        return acc += item.totalrestante
    }, 0);

    const sumAgregadosDet = rdetComprobanteArray1.reduce((acc, item) => {
        return acc += (item.total);
    }, 0);

    $("#rkpiMontoSeleecionadoModal1").text(rsolicitudCabCreada.simbolo + " " + formatoNumero(sumDetalleSol - sumAgregadosDet));

    if ((sumDetalleSol - sumAgregadosDet) < 0) {
        $("#rregComprobModal .kpi-modal-monto1").css("background-color", "#ABB2B9");
    }
    else {
        $("#rregComprobModal .kpi-modal-monto1").css("background-color", "#ABB2B9");
    }

    let tabla = $("#rtblFacturaBoleta").DataTable();

    tabla.destroy();
    var tr = "";

    rdetComprobanteArray1.forEach((obj) => {

        tr += "<tr> "
            + " <td></td>"
            + " <td> " + obj.codum + "</td>"
            + " <td> " + obj.conceptoDet + "</td>"
            + " <td> " + obj.detalle1 + "</td>"
            + " <td> " + formatoNumero(obj.valorunit) + "</td>"
            + " <td> " + formatoNumero(obj.total) + "</td>"
            + " <td class='text-center'>"
            + "     <button data-iDetalle='" + obj.iDetalle + "' data-toggle='collapse' class='btn btn-sm btn-danger cselectEliminar1 ml-1'><i class='fas fa-trash'></i></button>"
            + "</td>"
            + "</tr>";
    });


    $("#rtblFacturaBoletaContent").html(tr);

    $("#rtblFacturaBoleta").DataTable(
        {
            'language': { 'url': '/libs/datatables/spanish.json' },
            lengthMenu: [[5], [5]]
        });


    const importeTotal = rdetComprobanteArray1.reduce((acc, item) => {
        return acc += (item.valorunit);
    }, 0);


    if ($("#rcbxTipoCpbteCompModal1").val().trim() == "1") {
        // FACTURA
        $("#rtxtDescripcion1").val("");
        //$("#txtCantidad1").val("");
        $("#rtxtValorUnitarioTotal1").val("");

        $("#ridimportetotal1").val(formatoNumero(importeTotal));
        $("#ridigv1").val(formatoNumero(importeTotal * (PORCENTAJE_IGV / 100)));
        $("#ridtotal1").val(formatoNumero(importeTotal + (importeTotal * (PORCENTAJE_IGV / 100))));
    }
    else if ($("#rcbxTipoCpbteCompModal1").val().trim() == "2") {
        // BOLETA
        $("#rtxtDescripcion1").val("");
        //$("#txtCantidad1").val("1");
        $("#rtxtValorUnitarioTotal1").val("");

        $("#ridimportetotal1").val(formatoNumero(importeTotal));
        $("#ridigv1").val(0);
        $("#ridtotal1").val(formatoNumero(importeTotal));
    }


    document.getElementById("rcbxUM1").focus = true;



    //$("#rkpiMontoSeleecionadoModal1").text(rsolicitudCabCreada.simbolo + " " + formatoNumero(sumDetalleSol - sumAgregadosDet));

    //$("#ridMontoSolicitadoModal1").text(rvSimboloMoneda + " " + formatoNumero(rvTotalSolicitado1 - sumAgregadosDet));

});

$("#rtblPlanillaMovilidad").on("click", ".cselectEliminar2", function () {
    let vIDetalle = $(this).attr("data-iDetalle");

    for (var i = 0; i < rdetComprobanteArray2.length; i++) {
        if (rdetComprobanteArray2[i].iDetalle == parseInt(vIDetalle)) {
            rdetComprobanteArray2.splice(i, 1);
        }
    }

    for (var i = 0; i < rdetCpteArrayValid2.length; i++) {
        if (rdetCpteArrayValid2[i].id == vIDetalle) {
            rdetCpteArrayValid2.splice(i, 1);
        }
    }

    const sumAgregadosDet = rdetComprobanteArray2.reduce((acc, item) => {
        return acc += item.valorunit;
    }, 0);


    let tabla = $("#rtblPlanillaMovilidad").DataTable();

    tabla.destroy();
    var tr = "";

    rdetComprobanteArray2.forEach((obj) => {

        tr += "<tr> "
            + " <td> </td>"
            + " <td> " + obj.fecha + "</td>"
            + " <td> " + obj.detalle1 + "</td>"
            + " <td> " + obj.detalle2 + "</td>"
            + " <td> " + formatoNumero(obj.valorunit) + "</td>"
            + " <td class='text-center'>"
            + "     <button data-iDetalle='" + obj.iDetalle + "' data-toggle='collapse' class='btn btn-sm btn-danger cselectEliminar2 ml-1'><i class='fas fa-trash'></i></button>"
            + "</td>"
            + "</tr>";
    });

    $("#rtblPlanillaMovilidadContent").html(tr);

    $("#rtblPlanillaMovilidad").DataTable(
        {
            'language': { 'url': '/libs/datatables/spanish.json' },
            lengthMenu: [[10, 20, 30, -1], [10, 20, 30, 'Todos']]
        });

    $("#rtxtOrgDstModal2").val("");
    $("#rtxtMotivoModal2").val("");
    $("#rtxtImporteModal2").val("0");

    document.getElementById("rtxtOrgDstModal2").focus = true;
    //$("#ridMontoSolicitadoModal2").text(rsolicitudCabCreada.simbolo + " " + formatoNumero(rvTotalSolicitado2 - sumAgregadosDet));
    //$("#ridMontoSolicitadoModal2").text(rsolicitudCabCreada.simbolo + " " + formatoNumero(rvTotalSolicitado2));
});

$("#rtblDeclaracionJurada3_1").on("click", ".cselectEliminar3_1", function () {
    let vIDetalle = $(this).attr("data-idetalle");

    for (var i = 0; i < rdetComprobanteArray3.length; i++) {
        if (rdetComprobanteArray3[i].iDetalle == parseInt(vIDetalle)) {
            rdetComprobanteArray3.splice(i, 1);
        }
    }

    const sumAgregadosDet = rdetComprobanteArray3.reduce((acc, item) => {
        return acc += item.valorunit;
    }, 0);


    let tabla = $("#rtblDeclaracionJurada3_1").DataTable();

    tabla.destroy();
    var tr = "";


    rdetComprobanteArray3.forEach((obj) => {

        if (obj.seccion == 1) {
            tr += "<tr> "
                + " <td></td>"
                + " <td> " + obj.fecha + "</td>"
                + " <td> " + obj.detalle1 + "</td>"
                + " <td> " + formatoNumero(obj.valorunit) + "</td>"
                + " <td class='text-center'>"
                + "     <button data-iDetalle='" + obj.iDetalle + "' data-toggle='collapse' class='btn btn-sm btn-danger cselectEliminar3_1 ml-1'><i class='fas fa-trash'></i></button>"
                + "</td>"
                + "</tr>";
        }
    });

    $("#rtblDeclaracionJuradaContent3_1").html(tr);

    $("#rtblDeclaracionJurada3_1").DataTable(
        {
            'language': { 'url': '/libs/datatables/spanish.json' },
            lengthMenu: [[5], [5]]
        });

    $("#rtxtMontoDet3_1").val("");

    document.getElementById("rtxtFechaModalDet3_1").focus = true;
    //$("#ridMontoSolicitadoModal3").text(rsolicitudCabCreada.simbolo + " " + formatoNumero(rvTotalSolicitado3 - sumAgregadosDet));
    //$("#ridMontoSolicitadoModal3").text(rsolicitudCabCreada.simbolo + " " + formatoNumero(rvTotalSolicitado3));
});

$("#rtblDeclaracionJurada3_2").on("click", ".cselectEliminar3_2", function () {
    let vIDetalle = $(this).attr("data-idetalle");

    for (var i = 0; i < rdetComprobanteArray3.length; i++) {
        if (rdetComprobanteArray3[i].iDetalle == parseInt(vIDetalle)) {
            rdetComprobanteArray3.splice(i, 1);
        }
    }

    const sumAgregadosDet = rdetComprobanteArray3.reduce((acc, item) => {
        return acc += item.valorunit;
    }, 0);

    //$("#ridMontoSolicitadoModal3").text(rsolicitudCabCreada.simbolo + " " + formatoNumero(rvTotalSolicitado3 - sumAgregadosDet));
    //$("#ridMontoSolicitadoModal3").text(rsolicitudCabCreada.simbolo + " " + formatoNumero(rvTotalSolicitado3));


    let tabla = $("#rtblDeclaracionJurada3_2").DataTable();

    tabla.destroy();
    var tr = "";


    rdetComprobanteArray3.forEach((obj) => {

        if (obj.seccion == 2) {
            tr += "<tr> "
                + " <td></td>"
                + " <td> " + obj.fecha + "</td>"
                + " <td> " + obj.detalle1 + "</td>"
                + " <td> " + formatoNumero(obj.valorunit) + "</td>"
                + " <td class='text-center'>"
                + "     <button data-iDetalle='" + obj.iDetalle + "' data-toggle='collapse' class='btn btn-sm btn-danger cselectEliminar3_2 ml-1'><i class='fas fa-trash'></i></button>"
                + "</td>"
                + "</tr>";
        }
    });

    $("#rtblDeclaracionJuradaContent3_2").html(tr);

    $("#rtblDeclaracionJurada3_2").DataTable(
        {
            'language': { 'url': '/libs/datatables/spanish.json' },
            lengthMenu: [[5], [5]]
        });

    $("#rtxtMontoDet3_2").val("");

    document.getElementById("rtxtFechaModalDet3_2").focus = true;
});

$("#rbtnCancelarComprobante").click(function () {

    rlimpiarControlModal(1);
    rlimpiarControlModal(2);
    rlimpiarControlModal(3);
});

$("#rbtnGuardarComprobante").click(function () {

    if (rvalidacionGuardarComp(rtabSeleccionado)) {

        let parametro = {};
        let opcion = 0;

        if (rTIPO_REGISTRO == "INSERTAR") {
            opcion = 1;
        }
        else if (rTIPO_REGISTRO == "EDITAR") {
            opcion = 6;
        }

        // 1. Facturas / Boletas
        if (rtabSeleccionado == 1) {

            parametro = {
                'opcion': opcion,
                'idRendCompte': rvIdRendCompte,
                'idTipoComp': parseInt($("#rcbxTipoCpbteCompModal1").val()),

                'nserie': $("#rtxtSerieModal1").val(),
                'ndocof': $("#rtxtNumeroModal1").val(),

                'rucDNI': $("#rtxtRucCompModal1").val(),
                'rs': $("#rtxtRSCompModal1").val(),
                'codigoPC': $("#rtxtCodSofyaPCModal1").val(),
                'obs': $("#rtxtObsCompModal1").val(),
                'fechaEmision': $("#rtxtFechaModal1").val(),
                'codceco': rvCodCeCo,
                'idTipoMod': parseInt($("#rcbxMonedaModal1").val()),
                'porcentajeIGV': (($("#rcbxTipoCpbteCompModal1").val().trim() == "1") ? PORCENTAJE_IGV : 0),
                'obs1': '',
                'obs2': '',
                'fecha1': '',
                'fecha2': '',
                'periodo': $("#rcbxPeriodoModal1").val(),
                'observacionDet': $("#rtxtObservacionModal1").val(),

                'detalleComprobanteArray': rdetComprobanteArray1
            };

        }
        else if (rtabSeleccionado == 2) {

            // 2. Planilla de Movilidad
            parametro = {
                'opcion': opcion,
                'idRendCompte': rvIdRendCompte,
                'idTipoComp': 3,

                'nserie': "000",
                'ndocof': FormatoFecha($("#rtxtFechaModal2").val()),

                'rucDNI': $("#rtxtDNIModal2").val(),
                'rs': $("#rtxtApeNomModal2").val(),
                'codigoPC': "T-" + $("#rtxtDNIModal2").val(),
                'obs': "PLANILLA DE MOVILIDAD - " + rvcolaborador,
                'fechaEmision': $("#rtxtFechaModal2").val(),
                'codceco': rvCodCeCo,
                'idTipoMod': parseInt($("#rcbxMonedaModal2").val()),
                'porcentajeIGV': 0,
                'obs1': '',
                'obs2': '',
                'fecha1': '',
                'fecha2': '',
                'periodo': $("#rcbxPeriodoModal2").val(),
                'observacionDet': $("#rtxtObservacionModal2").val(),

                'detalleComprobanteArray': rdetComprobanteArray2
            };
        }
        else if (rtabSeleccionado == 3) {

            // 2. Declaracion Jurada
            parametro = {
                'opcion': opcion,
                'idRendCompte': rvIdRendCompte,
                'idTipoComp': 4,

                'nserie': "000",
                'ndocof': FormatoFecha($("#rtxtFechaModal3").val(), ""),

                'rucDNI': $("#rtxtDNIModal3").val(),
                'rs': $("#rtxtApeNomModal3").val(),
                'codigoPC': "T-" + $("#rtxtDNIModal3").val(),
                'obs': "DECLARACIÓN JURADA - " + rvcolaborador,
                'fechaEmision': $("#rtxtFechaModal3").val(),
                'codceco': rvCodCeCo,
                'idTipoMod': parseInt($("#rcbxMonedaModal3").val()),
                'porcentajeIGV': 0,
                'obs1': $("#rtxtCiudadModal3").val(),
                'obs2': $("#rtxtPaisModal3").val(),
                'fecha1': FormatoFecha($("#rtxtFechaDesdeModal3").val(), ""),
                'fecha2': FormatoFecha($("#rtxtFechaHastaModal3").val(), ""),
                'periodo': $("#rcbxPeriodoModal3").val(),
                'observacionDet': $("#rtxtObservacionModal3").val(),

                'detalleComprobanteArray': rdetComprobanteArray3
            };
        }


        $.ajax({
            url: "/finanzas/RG_SetReembolsoComprobante",
            type: "post",
            data: parametro,
            dataType: "json",
            success: function (resultado) {

                if (resultado.isRedirect) {
                    window.location.href = resultado.redirectUrl;
                }
                else {
                    if (resultado.entidad.idResponse > 0) {

                        rvalidarrestadoSolicitud(2, rsolicitudCabCreada.idSolicitud);
                        rgetListaDetalle(rsolicitudCabCreada.idSolicitud);

                        rlimpiarControlModal(1);
                        rlimpiarControlModal(2);
                        rlimpiarControlModal(3);

                        $("#rregComprobModal").modal("hide");
                    }
                    else {
                        rmensajePrincipal(false, resultado.entidad.statusResponse);

                        console.log(resultado.entidad.statusResponse);
                    }
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {

            }
        });
    }
});

function FormatoFecha(pFecha, separador) {
    let fechaFormato = new Date(pFecha).toISOString();
    fechaFormato = fechaFormato.substring(0, 10);

    let fechaPre = fechaFormato.replace("-", "");
    let fechaFinal = fechaPre.replace("-", "");

    let anio_valor = fechaFinal.substr(0, 4);
    let mes_valor = fechaFinal.substr(4, 2);
    let dia_valor = fechaFinal.substr(6, 2);

    return [anio_valor, mes_valor, dia_valor].join(separador);
}

$('#rcbxTipoCpbteCompModal1').change(function () {

    var $option = $(this).find('option:selected');
    rvCodTipoComprobante = parseInt($option.val());

    rdefinirValoresTipoCpte(vCodTipoComprobante);

    $("#ridimportetotal1").val("0");
    $("#ridigv1").val("0");
    $("#ridtotal1").val("0");

    rdetComprobanteArray1 = [];

    let tabla = $("#rtblFacturaBoleta").DataTable();
    tabla.destroy();
    var tr = "";
    $("#rtblFacturaBoletaContent").html(tr);
    $("#rtblFacturaBoleta").DataTable(
        {
            'language': { 'url': '/libs/datatables/spanish.json' },
            lengthMenu: [[3], [3]]
        }
    );
});

function rdefinirValoresTipoCpte(vCodTipoComprobante) {
    // Limpiar TAB Factura Boleta

    if (vCodTipoComprobante == 1) {
        // Factura
        document.getElementById("rcbxConcepCabModal1").disabled = true;
        document.getElementById("rcbxConcepDetModal1").disabled = true;
        document.getElementById("rcbxTipoCpbteCompModal1").disabled = true;
        document.getElementById("rtxtObservacionModal1").disabled = true;


        document.getElementById("rtxtObsCompModal1").disabled = false;

        document.getElementById("rcbxUM1").disabled = false;
        document.getElementById("rtxtDescripcion1").disabled = false;
        //document.getElementById("rtxtCantidad1").disabled = false;
        document.getElementById("rtxtValorUnitarioTotal1").disabled = false;

        document.getElementById("ridimportetotal1").disabled = true;
        document.getElementById("ridigv1").disabled = true;
        document.getElementById("ridtotal1").disabled = true;
    }
    else if (vCodTipoComprobante == 2) {
        // Boleta
        document.getElementById("rcbxConcepCabModal1").disabled = true;
        document.getElementById("rcbxConcepDetModal1").disabled = true;
        document.getElementById("rcbxTipoCpbteCompModal1").disabled = true;
        document.getElementById("rtxtObsCompModal1").disabled = false;
        document.getElementById("rcbxUM1").disabled = true;
        document.getElementById("rtxtDescripcion1").disabled = true;
        //document.getElementById("rtxtCantidad1").disabled = true;
        document.getElementById("rtxtValorUnitarioTotal1").disabled = false;

        $("#rtxtDescripcion1").val("");
        $("#rcbxUM1").val("UN");
        //$("#rtxtCantidad1").val(1);

        document.getElementById("rtxtValorUnitarioTotal1").focus = true;
    }

    document.getElementById("ridimportetotal1").disabled = true;
    document.getElementById("ridigv1").disabled = true;
    document.getElementById("ridtotal1").disabled = true;

}

function rcambioTipoCpte(pCodTipoCpte) {


    // Limpiar TAB Factura Boleta

    if (pCodTipoCpte == 1) {
        // Factura
        document.getElementById("rcbxTipoCpbteCompModal1").disabled = false;
        document.getElementById("rtxtSerieModal1").disabled = false;
        document.getElementById("rtxtNumeroModal1").disabled = false;
        document.getElementById("rtxtObsCompModal1").disabled = false;

        document.getElementById("rcbxUM1").disabled = false;
        document.getElementById("rtxtDescripcion1").disabled = false;
        //document.getElementById("txtCantidad1").disabled = false;
        document.getElementById("rtxtValorUnitarioTotal1").disabled = false;

        document.getElementById("ridimportetotal1").disabled = true;
        document.getElementById("ridigv1").disabled = true;
        document.getElementById("ridtotal1").disabled = true;

        document.getElementById("rtxtSerieModal1").focus = true;
    }
    else if (pCodTipoCpte == 2) {
        // Boleta
        document.getElementById("rcbxTipoCpbteCompModal1").disabled = false;
        document.getElementById("rtxtSerieModal1").disabled = false;
        document.getElementById("rtxtObsCompModal1").disabled = false;

        document.getElementById("rcbxUM1").disabled = true;
        document.getElementById("rtxtDescripcion1").disabled = false;
        document.getElementById("rtxtValorUnitarioTotal1").disabled = false;

        $("#rtxtDescripcion1").val("");
        $("#rcbxUM1").val("UN");

        document.getElementById("rtxtValorUnitarioTotal1").focus = true;

    }

    rdetComprobanteArray1 = [];

    let tabla = $("#rtblFacturaBoleta").DataTable();
    tabla.destroy();
    var tr = "";
    $("#rtblFacturaBoletaContent").html(tr);
    $("#rtblFacturaBoleta").DataTable(
        {
            'language': { 'url': '/libs/datatables/spanish.json' },
            lengthMenu: [[3], [3]]
        }
    );

    document.getElementById("ridimportetotal1").disabled = true;
    document.getElementById("ridigv1").disabled = true;
    document.getElementById("ridtotal1").disabled = true;

    $("#ridimportetotal1").val("0");
    $("#ridigv1").val("0");
    $("#ridtotal1").val("0");
}

$("#rtabRendicionGastos").on("click", ".rctabRegComp", function () {
    let index = $(this).attr("data-id");
    rtabSeleccionado = parseInt(index);
});


$('#rregComprobModal').on('shown.bs.modal', function (e) {

    var now = new Date();
    var today = FormatoFecha(now, "-");

    $('#rtxtFechaModal1').val(today);
    $('#rtxtFechaModal2').val(today);
    $('#rtxtFechaModal3').val(today);
    $('#rtxtFechaModalDet2').val(today);
    $('#rtxtFechaModalDet3').val(today);
    $('#rtxtFechaDesdeModal3').val(today);
    $('#rtxtFechaHastaModal3').val(today);
    $('#rtxtFechaModalDet3_1').val(today);
    $('#rtxtFechaModalDet3_2').val(today);

    $("#rcbxMonedaModal1").val(rsolicitudCabCreada.idTipoMod);
    $("#rcbxMonedaModal2").val(rsolicitudCabCreada.idTipoMod);
    $("#rcbxMonedaModal3").val(rsolicitudCabCreada.idTipoMod);

});

function rvalidacionDetCmpbte(tabSeleccion) {
    let respuesta = false;

    if (tabSeleccion == 1) {

        let tipoComprobante = $('#rcbxTipoCpbteCompModal1').val();

        let testvalue = $("#rtxtValorUnitarioTotal1").val();

        if ($("#rcbxTipoCpbteCompModal1").val().trim() == "0") {
            respuesta = false;

            Swal.fire({
                title: 'Seleccione un Tipo de Comprobante',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });
        }

        else if (($("#rtxtSerieModal1").val() == "" || $("#rtxtSerieModal1").val() == null) && tipoComprobante == "1") {
            respuesta = false;

            Swal.fire({
                title: 'Ingrese la serie del comprobante',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });
        }
        else if (($("#rtxtNumeroModal1").val() == "" || $("#rtxtNumeroModal1").val() == null) && tipoComprobante == "1") {
            respuesta = false;

            Swal.fire({
                title: 'Ingrese el numero de comprobante',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });
        }
        else if ($("#rcbxUM1").val() == "0" && tipoComprobante == "1") {
            respuesta = false;

            Swal.fire({
                title: 'Seleccione una unidad de medida por favor',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });
        }
        else if ($("#rtxtDescripcion1").val().trim() == "" && (tipoComprobante == "1")) {
            respuesta = false;
            Swal.fire({
                title: 'Ingrese la descripción por favor',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });
        }
        else if (($("#rtxtValorUnitarioTotal1").val().trim() == "" || $("#rtxtValorUnitarioTotal1").val() == null || typeof ($("#rtxtValorUnitarioTotal1").val()) == "undefined") && (tipoComprobante == "1")) {
            respuesta = false;
            Swal.fire({
                title: 'Ingrese el valor Unitario',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });
        }
        else if ((parseFloat($("#rtxtValorUnitarioTotal1").val()) == 0 || parseFloat($("#rtxtValorUnitarioTotal1").val()) < 0 || parseFloat($("#rtxtValorUnitarioTotal1").val()) > 1500) && (tipoComprobante == "1" || tipoComprobante == "2")) {
            respuesta = false;
            Swal.fire({
                title: 'Ingrese un Valor unitario correcto por favor',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });
        }
        else if (rvIdConceptoCab1.toString() == "5") {
            if ($("#rtxtObservacionModal1").val().trim() == "" || $("#rtxtObservacionModal1").val() == null || typeof ($("#rtxtObservacionModal1").val()) == "undefined") {
                respuesta = false;
                Swal.fire({
                    title: 'Ingrese una descripción por favor',
                    text: "Textile Sourcing Company",
                    icon: 'warning',
                    showConfirmButton: true,
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'Ok'
                });
            }
            else {
                respuesta = true;
            }
        }
        else {
            respuesta = true;
        }
    }
    else if (tabSeleccion == 2) {

        //v2_TipoMod = $("#rcbxMonedaModal2").val();
        //let sumTotal2 = 0;

        //v2_maximoS = rtiposComprobantesArray.find(x => x.idTipoComp == parseInt($("#rcbxTipoCpbteCompModal2").val())).maximoSoles;
        //v2_maximoD = rtiposComprobantesArray.find(x => x.idTipoComp == parseInt($("#rcbxTipoCpbteCompModal2").val())).maximoDolar;



        if ($("#rtxtApeNomModal2").val().trim() == "") {
            respuesta = false;

            Swal.fire({
                title: 'Ingrese Apellidos y Nombres',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });
        }
        else if ($("#rtxtDNIModal2").val().trim() == "") {
            respuesta = false;

            Swal.fire({
                title: "Ingrese el número de DNI",
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });
        }
        else if ($("#rtxtApeNomModal2").val().trim() == "0 Ninguno" || $("#rtxtApeNomModal2").val().trim() == "0 Varios") {
            respuesta = false;

            Swal.fire({
                title: 'Colaborador no existe',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });
        }
        else if ($("#rtxtOrgDstModal2").val().trim() == "") {
            respuesta = false;

            Swal.fire({
                title: 'Ingrese una descripción de Origen/Destino',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });
        }
        else if ($("#rtxtMotivoModal2").val().trim() == "") {
            respuesta = false;

            Swal.fire({
                title: 'Ingrese una descripción del motivo',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });
        }
        else if ($("#rtxtImporteModal2").val().trim() == "" || $("#rtxtImporteModal2").val().trim() == "0") {
            respuesta = false;

            Swal.fire({
                title: 'Ingrese el importe por favor',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });
        }

        //else if (v2_TipoMod.toString() == 1) {




        //    if (rdetComprobanteArray2.length > 0) {

        //        var result = [];
        //        rdetComprobanteArray2.reduce(function (res, value) {
        //            if (!res[value.fecha]) {
        //                res[value.fecha] = { fecha: value.fecha, valorunit: 0 };
        //                result.push(res[value.fecha])
        //            }
        //            res[value.fecha].valorunit += value.valorunit;
        //            return res;
        //        }, {});


        //        console.log("resultadoooo")
        //        console.log(result)



        //    }
        //    else {
        //        sumTotal2 = $("#rtxtImporteModal2").val();
        //    }

        //    if (parseFloat(sumTotal2) > v2_maximoS) {
        //        respuesta = false;
        //        Swal.fire({
        //            title: 'El monto máximo permitido es S/.' + v2_maximoS,
        //            text: "Textile Sourcing Company",
        //            icon: 'warning',
        //            showConfirmButton: true,
        //            confirmButtonColor: '#3085d6',
        //            confirmButtonText: 'Ok'
        //        });
        //    }
        //    else {
        //        respuesta = true;
        //    }
        //}
        else {
            respuesta = true;
        }
    }
    else if (tabSeleccion == 3) {

        if ($("#rtxtCiudadModal3").val().trim() == "") {
            respuesta = false;

            Swal.fire({
                title: 'Ingrese el nombre de la Ciudad',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });
        }
        else if ($("#rtxtPaisModal3").val().trim() == "") {
            respuesta = false;

            Swal.fire({
                title: "Ingrese el nombre del País",
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });
        }
        else {
            respuesta = true;
        }

    }

    return respuesta;
}

function rvalidacionGuardarComp(indexTab) {
    var respuesta = false;

    // Registro de Factura y Boleta

    if (indexTab == 1) {

        let tipoComprobante = $('#rcbxTipoCpbteCompModal1').val();


        if ($("#rcbxTipoCpbteCompModal1").val().trim() == "0" && (tipoComprobante == "1" || tipoComprobante == "2")) {
            respuesta = false;

            Swal.fire({
                title: 'Seleccione un Tipo de Comprobante',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });
        }
        else if ($("#rtxtSerieModal1").val().trim() == "" && (tipoComprobante == "1" || tipoComprobante == "2")) {
            respuesta = false;

            Swal.fire({
                title: 'Ingrese un numero y serie',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });
        }
        else if ($("#rtxtNumeroModal1").val().trim() == "" && (tipoComprobante == "1" || tipoComprobante == "2")) {
            respuesta = false;

            Swal.fire({
                title: 'Ingrese un numero y serie',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });
        }
        else if ($("#rtxtRucCompModal1").val().trim() == "" && (tipoComprobante == "1" || tipoComprobante == "2")) {
            respuesta = false;

            Swal.fire({
                title: 'Ingrese el número de RUC / DNI',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });
        }
        else if ($("#rtxtCodSofyaPCModal1").val().trim() == "" && (tipoComprobante == "1" || tipoComprobante == "2")) {
            respuesta = false;

            Swal.fire({
                title: 'El proveedor debe estar registrado en Sofya para continuar',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });
        }
        else if (rdetComprobanteArray1.length == 0 && (tipoComprobante == "1" || tipoComprobante == "2")) {
            respuesta = false;

            Swal.fire({
                title: 'Debe agregar el detalle del comprobante',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });
        }
        else {
            respuesta = true;
        }
    }
    if (indexTab == 2) {

        // Planilla de Movilidad
        if ($("#rtxtDNIModal2").val().trim() == "") {
            respuesta = false;

            Swal.fire({
                title: 'No se encuentra el colaborador',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });
        }
        else if (rdetComprobanteArray2.length == 0) {
            respuesta = false;

            Swal.fire({
                title: 'Debe agregar el detalle de Planilla de Movilidad',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });
        }
        else {
            respuesta = true;
        }
    }
    else if (indexTab == 3) {

        // Planilla de Movilidad
        if ($("#rcbxMonedaModal3").val().trim() == "0") {
            respuesta = false;

            Swal.fire({
                title: 'Ingrese el tipo de Moneda',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });

        }
        else if ($("#rtxtDNIModal3").val().trim() == "") {
            respuesta = false;

            Swal.fire({
                title: 'Ingrese el número de DNI por favor',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });
        }
        else if (rdetComprobanteArray3.length == 0) {
            respuesta = false;

            Swal.fire({
                title: 'Debe agregar el detalle de la Declaración Jurada',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok'
            });
        }
        else {
            respuesta = true;
        }
    }


    return respuesta;
}

function rlimpiarControlModal(tabSeleccion) {

    var now = new Date();
    var today = FormatoFecha(now, "-");

    if (tabSeleccion == 1) {

        // 1. Factura y Boleta
        $("#rcbxTipoCpbteCompModal1").val(rvCodTipoComprobante);
        //rcambioTipoCpte(rvCodTipoComprobante);
        $("#rtxtObservacionModal1").val("");
        $("#rtxtFechaModal1").val(today);
        $("#rtxtSerieModal1").val("");
        $("#rtxtNumeroModal1").val("");
        $("#rtxtRucCompModal1").val("");
        $("#rtxtRSCompModal1").val("");
        $("#rtxtCodSofyaPCModal1").val("");
        $("#rtxtObsCompModal1").val("");

        $("#rcbxUM1").val("UN");
        $("#rtxtDescripcion1").val("");
        //$("#txtCantidad1").val("");
        $("#rtxtValorUnitarioTotal1").val("");

        //$("#ridMontoSolicitadoModal1").text("0.00");

        let tabla = $("#rtblFacturaBoleta").DataTable();
        tabla.destroy();
        var tr = "";
        $("#rtblFacturaBoletaContent").html(tr);
        $("#rtblFacturaBoleta").DataTable(
            {
                'language': { 'url': '/libs/datatables/spanish.json' },
                lengthMenu: [[3], [3]]
            }
        );

        $("#ridimportetotal1").val("0");
        $("#ridigv1").val("0");
        $("#ridtotal1").val("0");

        rdetComprobanteArray1 = [];

        document.getElementById("rtxtSerieModal1").focus = true;

        document.getElementById("rcbxConcepCabModal1").disabled = false;
        document.getElementById("rcbxConcepDetModal1").disabled = false;
        document.getElementById("rtxtObservacionModal1").disabled = false;

        ractualizarTotalSolicitado($("#rcbxConcepCabModal1").val(), $("#rcbxConcepDetModal1").val());

    }
    else if (tabSeleccion == 2) {

        // 2. Planilla de Movilidad
        $("#txtNumPlanillaModal2").val("");
        //$("#rtxtApeNomModal2").val("");
        //$("#rtxtDNIModal2").val("");
        $("#rtxtObservacionModal2").val("");
        //$("#rtxtCeCoModal2").val("");

        $("#rtxtFechaModal2").val(today);
        $("#rtxtOrgDstModal2").val("");
        $("#rtxtMotivoModal2").val("");
        $("#rtxtImporteModal2").val("");

        let tabla = $("#rtblPlanillaMovilidad").DataTable();
        tabla.destroy();
        var tr = "";
        $("#rtblPlanillaMovilidadContent").html(tr);
        $("#rtblPlanillaMovilidad").DataTable(
            {
                'language': { 'url': '/libs/datatables/spanish.json' },
                lengthMenu: [[3], [3]]
            }
        );

        rdetComprobanteArray2 = [];
        rdetCpteArrayValid2 = [];

        document.getElementById("rcbxConcepCabModal2").disabled = false;
        document.getElementById("rcbxConcepDetModal2").disabled = false;
        document.getElementById("rtxtObservacionModal2").disabled = false;

        ractualizarTotalSolicitado($("#rcbxConcepCabModal2").val(), $("#rcbxConcepDetModal2").val());
    }
    else if (tabSeleccion == 3) {

        // 3. Declaración Jurada.
        $("#rtxtCodigoModal3").val("");
        //$("#rtxtApeNomModal3").val("");
        //$("#rtxtDNIModal3").val("");
        $("#rtxtObservacionModal3").val("");
        $("#rtxtFechaModal3").val(today);

        $("#rtxtMotivoModal3").val("");
        $("#rtxtPaisModal3").val("");

        $("#rtxtMontoDet3_1").val("");
        $("#rtxtMontoDet3_2").val("");


        let tabla1 = $("#rtblDeclaracionJurada3_1").DataTable();
        tabla1.destroy();
        var tr1 = "";
        $("#rtblDeclaracionJuradaContent3_1").html(tr1);
        $("#rtblDeclaracionJurada3_1").DataTable(
            {
                'language': { 'url': '/libs/datatables/spanish.json' },
                lengthMenu: [[3], [3]]
            }
        );


        let tabla2 = $("#rtblDeclaracionJurada3_2").DataTable();
        tabla2.destroy();
        var tr2 = "";
        $("#rtblDeclaracionJuradaContent3_2").html(tr2);
        $("#rtblDeclaracionJurada3_2").DataTable(
            {
                'language': { 'url': '/libs/datatables/spanish.json' },
                lengthMenu: [[3], [3]]
            }
        );


        rdetComprobanteArray3 = [];

        document.getElementById("rcbxConcepCabModal3").disabled = false;
        document.getElementById("rcbxConcepDetModal3").disabled = false;
        document.getElementById("rtxtObservacionModal3").disabled = false;

        ractualizarTotalSolicitado($("#rcbxConcepCabModal3").val(), $("#rcbxConcepDetModal3").val());
    }

    rdetComprobanteArray = [];
}

function rvalidarrestadoSolicitud(pOpcion, pIdSolicitud) {

    $.ajax({
        url: "/finanzas/RG_ValidarEstadoRendicion",
        type: "POST",
        data: {
            opcion: pOpcion,
            idSolicitud: pIdSolicitud,
            codigo: "",
            secuencia: 0,
            idRendCompte: 0
        },
        dataType: "json",
        success: function (resultado) {

            if (resultado.isRedirect) {
                window.location.href = resultado.redirectUrl;
            }
            else {
                rindicadorEstado(resultado.status.idResponse);
                $("#restado-desc").text(resultado.status.statusResponse);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}

function rbusquedaXDNI(dni, opcionModal) {

    if (dni == "") {
        Swal.fire({
            title: 'Ingrese el número de DNI',
            text: "Textile Sourcing Company",
            icon: 'warning',
            showConfirmButton: true,
            showCancelButton: false,
            confirmButtonText: 'Ok',
            cancelButtonText: 'Cancel',
            focusConfirm: true
        }).then(result => {
            if (result.value) {
                // Planilla de Movilidad
                if (opcionModal == 2) {
                    document.getElementById("rtxtDNIModal2").focus = true;
                }
                else if (opcionModal == 3) {
                    // Declaracion Jurada
                    document.getElementById("rtxtDNIModal3").focus = true;
                }
            }
        });
    }
    else if (dni.length < 7) {
        Swal.fire({
            title: 'Ingrese un número RUC valido por favor',
            icon: 'warning',
            showConfirmButton: true,
            showCancelButton: false,
            confirmButtonText: 'Ok',
            cancelButtonText: 'Cancel',
            focusConfirm: true
        }).then(result => {
            if (result.value) {
                // Planilla de Movilidad
                if (opcionModal == 2) {
                    document.getElementById("rtxtDNIModal2").focus = true;
                }
                else if (opcionModal == 3) {
                    // Declaracion Jurada
                    document.getElementById("rtxtDNIModal3").focus = true;
                }
            }
        });
    }
    else {
        $.ajax({
            type: "GET",
            url: '/Finanzas/RG_BuscarColaboradorXDNI',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { dni: dni },
            success: function (data) {

                if (opcionModal == 2) {
                    // Planilla de Movilidad
                    $("#rtxtApeNomModal2").val(data.entidad.colaborador);
                }
                else if (opcionModal == 3) {
                    // Declaracion Jurada
                    $("#rtxtApeNomModal3").val(data.entidad.colaborador);
                }

                if (data.entidad.codcolaborador == "0") {

                    const Toast = Swal.mixin({
                        toast: true,
                        position: 'top-end',
                        showConfirmButton: false,
                        timer: 2500,
                        timerProgressBar: true,
                        didOpen: (toast) => {
                            toast.addEventListener('mouseenter', Swal.stopTimer)
                            toast.addEventListener('mouseleave', Swal.resumeTimer)
                        }
                    })

                    Toast.fire({
                        icon: 'warning',
                        title: data.entidad.mensaje
                    });

                    if (opcionModal == 2) {
                        // Planilla de Movilidad
                        document.getElementById("rtxtDNIModal2").focus = true;
                    }
                    else if (opcionModal == 3) {
                        // Declaracion Jurada
                        document.getElementById("rtxtDNIModal3").focus = true;
                    }
                }
                else {
                    if (opcionModal == 2) {
                        // Planilla de Movilidad
                        document.getElementById("rtxtOrgDstModal2").focus = true;
                    }
                    else if (opcionModal == 3) {
                        // Declaracion Jurada
                        // document.getElementById("rtxtDNIModal3").focus = true;
                    }
                }
            },
            failure: function () {
                console.error('error al cargar tipos periodos');
            }
        });
    }
}

function rlistarDetalle(idRendCompte) {

    $.ajax({
        url: "/finanzas/RG_RendicionDetalle",
        type: "GET",
        data: { idRendCompte: idRendCompte },
        dataType: "json",
        success: function (resultado) {

            if (resultado.isRedirect) {
                window.location.href = resultado.redirectUrl;
            }
            else {
                let tabla = $("#rtblPlanillaMovilidad").DataTable();

                tabla.destroy();
                var tr = "";

                resultado.lista.forEach((obj) => {

                    tr += "<tr> "
                        + " <td> " + obj.fecha + "</td>"
                        + " <td> " + obj.detalle1 + "</td>"
                        + " <td> " + obj.detalle2 + "</td>"
                        + " <td> " + formatoNumero(obj.importe) + "</td>"
                        + " <td class='text-center'>"
                        + "     <button data-idRendCompteDet='" + obj.idRendCompteDet + "' data-toggle='collapse' class='btn btn-sm btn-danger cselectEliminar ml-1'><i class='fas fa-trash'></i></button>"
                        + "</td>"
                        + "</tr>";
                });

                $("#rtblPlanillaMovilidadContent").html(tr);


                $("#rtblPlanillaMovilidad").DataTable(
                    {
                        'language': { 'url': '/libs/datatables/spanish.json' },
                        lengthMenu: [[10, 20, 30, -1], [10, 20, 30, 'Todos']]
                    });
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });


}

function rindicadorEstado(idEstado) {

    if (idEstado == 1) {
        // Pendiente
        $("#restado-icon").removeClass("icon-liberado");
        $("#restado-icon").removeClass("icon-aprobado");
        $("#restado-icon").removeClass("icon-rechazado");
        $("#restado-icon").removeClass("icon-rend-completa");
        $("#restado-icon").removeClass("icon-rend-incompleta");
        $("#restado-icon").addClass("icon-pendiente");

        $("#restado-desc").removeClass("desc-est-liberado");
        $("#restado-desc").removeClass("desc-est-aprobado");
        $("#restado-desc").removeClass("desc-est-rechazado");
        $("#restado-desc").removeClass("desc-est-rend-completa");
        $("#restado-desc").removeClass("desc-est-rend-incompleta");
        $("#restado-desc").addClass("desc-est-pendiente");

    }
    else if (idEstado == 2) {
        // Liberado
        $("#restado-icon").removeClass("icon-pendiente");
        $("#restado-icon").removeClass("icon-aprobado");
        $("#restado-icon").removeClass("icon-rechazado");
        $("#restado-icon").removeClass("icon-rend-completa");
        $("#restado-icon").removeClass("icon-rend-incompleta");
        $("#restado-icon").addClass("icon-liberado");

        $("#restado-desc").removeClass("desc-est-pendiente");
        $("#restado-desc").removeClass("desc-est-aprobado");
        $("#restado-desc").removeClass("desc-est-rechazado");
        $("#restado-desc").removeClass("desc-est-rend-completa");
        $("#restado-desc").removeClass("desc-est-rend-incompleta");
        $("#restado-desc").addClass("desc-est-liberado");

    }
    else if (idEstado == 3) {
        // Aprobado
        $("#restado-icon").removeClass("icon-pendiente");
        $("#restado-icon").removeClass("icon-liberado");
        $("#restado-icon").removeClass("icon-rechazado");
        $("#restado-icon").removeClass("icon-rend-completa");
        $("#restado-icon").removeClass("icon-rend-incompleta");
        $("#restado-icon").addClass("icon-aprobado");

        $("#restado-desc").removeClass("desc-est-pendiente");
        $("#restado-desc").removeClass("desc-est-liberado");
        $("#restado-desc").removeClass("desc-est-rechazado");
        $("#restado-desc").removeClass("desc-est-rend-completa");
        $("#restado-desc").removeClass("desc-est-rend-incompleta");
        $("#restado-desc").addClass("desc-est-aprobado");

    }
    else if (idEstado == 4) {
        // Aprobado
        $("#restado-icon").removeClass("icon-pendiente");
        $("#restado-icon").removeClass("icon-liberado");
        $("#restado-icon").removeClass("icon-rechazado");
        $("#restado-icon").removeClass("icon-rend-completa");
        $("#restado-icon").removeClass("icon-rend-incompleta");
        $("#restado-icon").addClass("icon-aprobado");

        $("#restado-desc").removeClass("desc-est-pendiente");
        $("#restado-desc").removeClass("desc-est-liberado");
        $("#restado-desc").removeClass("desc-est-rechazado");
        $("#restado-desc").removeClass("desc-est-rend-completa");
        $("#restado-desc").removeClass("desc-est-rend-incompleta");
        $("#restado-desc").addClass("desc-est-aprobado");

    }
    else if (idEstado == 5) {
        // Rechazado
        $("#restado-icon").removeClass("icon-pendiente");
        $("#restado-icon").removeClass("icon-liberado");
        $("#restado-icon").removeClass("icon-aprobado");
        $("#restado-icon").removeClass("icon-rend-completa");
        $("#restado-icon").removeClass("icon-rend-incompleta");
        $("#restado-icon").addClass("icon-rechazado");

        $("#restado-desc").removeClass("desc-est-pendiente");
        $("#restado-desc").removeClass("desc-est-liberado");
        $("#restado-desc").removeClass("desc-est-aprobado");
        $("#restado-desc").removeClass("desc-est-rend-completa");
        $("#restado-desc").removeClass("desc-est-rend-incompleta");
        $("#restado-desc").addClass("desc-est-rechazado");
    }
    else if (idEstado == 6) {
        // Rendicion Completa
        $("#restado-icon").removeClass("icon-pendiente");
        $("#restado-icon").removeClass("icon-liberado");
        $("#restado-icon").removeClass("icon-rechazado");
        $("#restado-icon").removeClass("icon-rend-incompleta");
        $("#restado-icon").addClass("icon-rend-completa");

        $("#restado-desc").removeClass("desc-est-pendiente");
        $("#restado-desc").removeClass("desc-est-liberado");
        $("#restado-desc").removeClass("desc-est-rechazado");
        $("#restado-desc").removeClass("desc-est-rend-incompleta");
        $("#restado-desc").addClass("desc-est-rend-completa");
    }
    else if (idEstado == 7) {
        // Rendicion Incompleta
        $("#restado-icon").removeClass("icon-pendiente");
        $("#restado-icon").removeClass("icon-liberado");
        $("#restado-icon").removeClass("icon-rechazado");
        $("#restado-icon").removeClass("icon-rend-completa");
        $("#restado-icon").addClass("icon-rend-incompleta");

        $("#restado-desc").removeClass("desc-est-pendiente");
        $("#restado-desc").removeClass("desc-est-liberado");
        $("#restado-desc").removeClass("desc-est-rechazado");
        $("#restado-desc").removeClass("desc-est-rend-completa");
        $("#restado-desc").addClass("desc-est-rend-incompleta");
    }

}

function formatoNumero(x) {
    x = x.toFixed(2);
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function rarmarTablaDetalle(lista) {

    let tabla = $("#rtblSolicitud").DataTable();

    tabla.destroy();
    var tr = "";

    lista.forEach((obj) => {

        tr += "<tr> "
            + " <td class='ocultar-columna'>" + obj.idSolicitud + "</td>"
            + " <td class='ocultar-columna'> " + obj.secuencia + "</td>"
            + " <td> " + obj.serieNumero + "</td>"
            + " <td> " + obj.tipo + "</td>"
            + " <td> " + obj.conceptoCab + "</td>"
            + " <td> " + obj.conceptoDet + "</td>"
            + " <td> " + obj.cantDias + "</td>"
            + " <td> " + formatoNumero(obj.valor) + "</td>"
            + " <td> " + obj.simbolo + " " + formatoNumero(obj.totalSolicitado) + "</td>"
            + " <td> " + obj.simbolo + " " + formatoNumero(obj.totalRendido) + "</td>"
            + " <td> " + obj.simbolo + " " + formatoNumero(obj.totalSolicitado - obj.totalRendido) + "</td>"

            + " <td class='text-center'>"
            + "     <button data-idtipocomp = " + obj.idTipoComp + "  data-idrendcompte=" + obj.idRendCompte + " data-toggle='collapse' class='btn btn-sm btn-danger cImprimirPDF' " + (obj.idRendCompte == 0 ? " disabled " : " ") + "><i class='fas fa-file-pdf'></i></button>"
            + "</td>"

            + " <td class='text-center'>"
            + "     <input type='checkbox' data-idsolicitud='" + obj.idSolicitud + "' data-secuencia='" + obj.secuencia + "' data-valor=" + obj.valor + " data-total=" + obj.total + " data-restante=" + (obj.totalSolicitado - obj.totalRendido) + " class='seleccion ml-2' " + (obj.idEstadoDet == 6 ? " disabled " : " ") + ">"
            + "</td>"

            + "</tr>";
    });


    $("#rtblSolicitudContent").html(tr);

    $("#rtblSolicitud").DataTable(
        {
            'language': { 'url': '/libs/datatables/spanish.json' },
            lengthMenu: [[10, 20, 30, -1], [10, 20, 30, 'Todos']]
        });
}

$("#rbtnVerRegistrados").click(function () {

    if (rvalidacionVerCmptes()) {

        $.ajax({
            url: "@Url.Action("RG_BuscarIdSolicitud", "Finanzas")",
            data: {
                idSolicitud: rsolicitudCabCreada.idSolicitud,
            },
            success: function (data) {
                window.location.href = '@Url.Action("MapearReporteCXSPDF", "Finanzas")'
            }
        });
    }
});

function rvalidacionVerCmptes() {
    var respuesta = false;

    let idSolicitud = rsolicitudCabCreada.idSolicitud;

    if (idSolicitud == null || typeof (idSolicitud) == "undefined") {
        respuesta = false;

        Swal.fire({
            title: 'Seleccione una Solicitud por favor',
            text: "Textile Sourcing Company",
            icon: 'warning',
            showConfirmButton: true,
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Ok'
        });
    }
    else {
        respuesta = true;
    }

    return respuesta;
}

$("#list-comprobantes").on("click", "a", function () {

    let idRendCompte = $(this).attr("data-idrendcompte");

    rcargarComprobanteCabecera(idRendCompte);


});

function rmensajePrincipal(estado, mensaje) {
    if (estado == true) {

        const Toast = Swal.mixin({
            toast: true,
            position: 'top-end',
            showConfirmButton: false,
            timer: 2500,
            timerProgressBar: true,
            didOpen: (toast) => {
                toast.addEventListener('mouseenter', Swal.stopTimer)
                toast.addEventListener('mouseleave', Swal.resumeTimer)
            }
        })

        Toast.fire({
            icon: 'success',
            title: mensaje
        });

    }
    else if (estado == false) {

        const Toast = Swal.mixin({
            toast: true,
            position: 'top-end',
            showConfirmButton: false,
            timer: 2500,
            timerProgressBar: true,
            didOpen: (toast) => {
                toast.addEventListener('mouseenter', Swal.stopTimer)
                toast.addEventListener('mouseleave', Swal.resumeTimer)
            }
        })

        Toast.fire({
            icon: 'error',
            title: mensaje
        });

    }
}

$("#rtblDetalleContent").on("click", ".cImprimirPDF", function () {

    let idTipoComp = $(this).attr("data-idtipocomp");
    let idRendCompte = $(this).attr("data-idrendcompte");

    $.ajax({
        url: "@Url.Action("RG_BuscarIdCompte", "Finanzas")",
        data: {
            idTipoComp: idTipoComp,
            idRendCompte: idRendCompte
        },
        success: function (data) {

            if (idTipoComp == 3) {
                // Planilla de Movilidad
                window.location.href = '@Url.Action("MapearReportePLPDF", "Finanzas")'
            }
            else if (idTipoComp == 4) {
                // Declaración Jurada
                window.location.href = '@Url.Action("MapearReporteDJPDF", "Finanzas")'
            }

        }
    });

});

$("#rtblDetalleContent").on("click", ".cEditarCpte", function () {

    rdetalleArray = [];
    rvIdSolicitud = $(this).attr("data-idsolicitud");

    let secuencia = $(this).attr("data-secuencia");
    let valor = $(this).attr("data-valor");
    let totalRestante = $(this).attr("data-restante");
    let totalsolicitado = $(this).attr("data-totalsolicitado");

    rvIdRendCompte = $(this).attr("data-idrendcompte");

    rdetalleArray.push({ idsolicitud: rvIdSolicitud, secuencia: secuencia, valor: parseFloat(valor), totalrestante: parseFloat(totalsolicitado) });

    rTIPO_REGISTRO = "EDITAR";
    $("#rregComprobModal").modal("show");

    rcargarComprobanteCabecera(rvIdRendCompte);

});

$('#rtxtRucCompModal1').keypress(function (event) {
    //zzzz
    var keycode = (event.keyCode ? event.keyCode : event.which);

    if (keycode == '13') {

        let ruc = $("#rtxtRucCompModal1").val().trim();

        if (ruc == "") {
            Swal.fire({
                title: 'Ingrese RUC',
                text: "Textile Sourcing Company",
                icon: 'warning',
                showConfirmButton: true,
                showCancelButton: false,
                confirmButtonText: 'Ok',
                cancelButtonText: 'Cancel',
                focusConfirm: true
            }).then(result => {
                if (result.value) {
                    document.getElementById("rtxtRucCompModal1").focus = true;
                }
            });
        }
        else if (ruc.length < 7 || ruc.length > 15) {
            Swal.fire({
                title: 'Ingrese un número RUC valido por favor',
                icon: 'warning',
                showConfirmButton: true,
                showCancelButton: false,
                confirmButtonText: 'Ok',
                cancelButtonText: 'Cancel',
                focusConfirm: true
            }).then(result => {
                if (result.value) {
                    //document.getElementById("ridtxtcodigo").focus = true;
                }
            });

        }
        else {

            MostrarCarga("Buscando...");


            $.ajax({
                url: "/finanzas/RG_SetproveedorValidar",
                type: "POST",
                data: { 'opcion': '1', 'tipoPersona': 'J', 'ruc': ruc, 'rs': '', 'direccion': '' },
                dataType: "json",
                success: function (resultado) {

                    if (resultado.isRedirect) {
                        window.location.href = resultado.redirectUrl;
                    }
                    else {
                        rproveedor = resultado.entidad;

                        if (resultado.entidad.idResponse == 0) {

                            Swal.fire({
                                title: '¿Proveedor no existe, desea crearlo?',
                                text: "Textile Sourcing Company",
                                icon: 'warning',
                                showCancelButton: true,
                                confirmButtonColor: '#3085d6',
                                cancelButtonColor: '#d33',
                                confirmButtonText: 'Si',
                                cancelButtonText: 'No'
                            }).then((result) => {
                                if (result.value) {

                                    $("#rtxtRUCModal").val($("#rtxtRucCompModal1").val());
                                    document.getElementById("rtxtRUCModal").disabled = true;

                                    $("#rregComprobModal").modal("hide");
                                    $('#rproveedorModal').modal('show');
                                }
                            });
                        }
                        else {

                            $("#rtxtRSCompModal1").val(resultado.entidad.danvar);
                            $("#rtxtCodSofyaPCModal1").val(resultado.entidad.ctpava + "-" + resultado.entidad.canvar);

                            rmensajePrincipal(true, "proveedor correcto");
                        }
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {

                }
            });

        }

    }
});

$('#rproveedorModal').on('shown.bs.modal', function (e) {
    document.getElementById("rtxtRSModal").focus = true;
});

function rgetListaDetalle(pidSolicitud) {

    $.ajax({
        type: "GET",
        url: '/finanzas/RG_SolicitudDetReembolso',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { idSolicitud: pidSolicitud },
        success: function (data) {

            if (data.isRedirect) {
                window.location.href = data.redirectUrl;
            }
            else {
                rgenerarTablaDetalle(data.lista);
            }
        },
        failure: function () {

            Swal.fire({
                icon: 'error',
                title: 'Error al mostrar datos',
                text: 'Textile Sourcing Company',
                showConfirmButton: false,
                timer: 2500,
            });

        }
    });
}

function rgenerarTablaDetalle(lista) {

    let vTotal = 0;
    let tabla = $("#rtblDetalle").DataTable();
    tabla.destroy();
    var tr = "";
    let tr_btn_pdf = "";
    let tr_fin = "";

    // Nueva Lista

    console.log("lista");
    console.log(lista);

    lista.forEach((obj) => {

        vTotal = vTotal + (obj.totalcpte);

        tr += "<tr> "
            + " <td> </td> "
            + " <td class='ocultar-columna'> " + obj.idSolicitud + "</td> "
            //+ " <td> " + obj.codigo + "</td> "
            //+ " <td> " + obj.fecha + "</td>"

            + " <td> " + obj.serieNumero + "</td>"
            + " <td> " + obj.ceco + "</td> "
            + " <td> " + obj.conceptoCab + "</td>"
            //+ " <td> " + obj.conceptoDet + "</td> "

            //+ " <td> " + obj.cantDias + "</td> "
            //+ " <td> " + obj.simbolo + " " + formatoNumero(obj.valor) + "</td>"
            //+ " <td> " + obj.simbolo + " " + formatoNumero(obj.total) + "</td>"
            //+ " <td> " + obj.simbolo + " " + formatoNumero(obj.totalSolicitado - obj.totalRendido) + "</td>"

            + " <td> " + obj.simbolo + " " + formatoNumero(obj.totalcpte) + "</td>"


            + " <td class='text-center'>"
            + "     <button data-idsolicitud=" + obj.idSolicitud + " data-secuencia=" + obj.secuencia + " data-idrendcompte=" + obj.idRendCompte + " class='btn btn-sm btn-danger cEliminarCpte'><i class='fas fa-trash'></i></button>"
            + "</td>"

            + " <td class='text-center'>"
            + "     <button data-idrendcompte=" + obj.idRendCompte + "  data-idsolicitud='" + obj.idSolicitud + "' data-secuencia='" + obj.secuencia + "'  data-valor=" + obj.valor + " data-totalsolicitado = " + obj.totalSolicitado + " data-restante=" + (obj.totalSolicitado - obj.totalRendido) + " data-toggle='collapse' class='btn btn-sm btn-success cEditarCpte' " + ((obj.idRendCompte > 0) ? "  " : " disabled ") + "><i class='fas fa-edit'></i></button>"
            + "</td>"


        if (obj.idTipoComp == 3 || obj.idTipoComp == 4) {

            tr_btn_pdf = " <td class='text-center'>"
                + "     <button data-idtipocomp = " + obj.idTipoComp + "  data-idrendcompte=" + obj.idRendCompte + " data-toggle='collapse' class='btn btn-sm btn-danger cImprimirPDF' " + ((obj.idRendCompte == 0 || obj.idTipoComp == 1 || obj.idTipoComp == 2) ? " disabled " : " ") + "><i class='fas fa-file-pdf'></i></button>"
                + "</td>";
        }
        else {
            tr_btn_pdf = " <td class='text-center'></td> ";
        }

        tr = tr + tr_btn_pdf;

        tr = tr + + "</tr>";



        //+ " <td class='text-center'>"
        //+ "     <button data-idsolicitud='" + obj.idSolicitud + "' data-idconceptocab='" + obj.idConceptoCab + "' data-toggle='collapse' class='btn btn-sm btn-primary solicitudDetalleItem'><i class='fas fa-eye'></i></button>"
        ////+ "     <button data-idsolicitud='" + obj.idSolicitud + "' data-idconceptocab='" + obj.idConceptoCab + "' data-toggle='collapse' class='btn btn-sm btn-success ml-1 solicitudEditarItem'" + (idEstado > 1 ? " disabled " : " ") + "><i class='fas fa-edit'></i></button>"
        ////+ "     <button data-idsolicitud='" + obj.idSolicitud + "' data-idconceptocab='" + obj.idConceptoCab + "' data-toggle='collapse' class='btn btn-sm btn-danger ml-1 solicitudEliminarItem'" + (idEstado > 1 ? " disabled " : " ") + "><i class='fas fa-trash'></i></button>"
        //+ " </td>"
        //+ " <td class='text-center'>"
        //+ "     <input type='checkbox' data-codceco='" + obj.codceco + "' data-ceco = '" + obj.ceco + "'  data-idsolicitud='" + obj.idSolicitud + "' data-secuencia='" + obj.secuencia + "' data-valor=" + obj.valor + " data-total=" + obj.total + "   data-restante=" + (obj.totalSolicitado - obj.totalRendido) + "  class='seleccion' " + (obj.idEstadoDet == 5 ? " disabled " : " ") + ">"
        //+ "</td>"

    });


    $("#rkpi-cantidad").text(lista.length.toString());


    $("#rkpi-total").text(formatoNumero(vTotal).toString());

    $("#rtblDetalleContent").html(tr);

    $("#rtblDetalle").DataTable(
        {
            'language': { 'url': '/libs/datatables/spanish.json' },
            lengthMenu: [[10, 20, 30, -1], [10, 20, 30, 'Todos']]
        }
    );
}

$("#rtblDetalleContent").on("click", ".cImprimirPDF", function () {

    let idTipoComp = $(this).attr("data-idtipocomp");
    let idRendCompte = $(this).attr("data-idrendcompte");

    $.ajax({
        url: "@Url.Action("RG_BuscarIdCompte", "Finanzas")",
        data: {
            idTipoComp: idTipoComp,
            idRendCompte: idRendCompte
        },
        success: function (data) {

            if (idTipoComp == 3) {
                // Planilla de Movilidad
                window.location.href = '@Url.Action("MapearReportePLPDF", "Finanzas")'
            }
            else if (idTipoComp == 4) {
                // Declaración Jurada
                window.location.href = '@Url.Action("MapearReporteDJPDF", "Finanzas")'
            }

        }
    });
});

$("#rtblDetalleContent").on("click", ".cEliminarCpte", function () {

    let idRendCompte = $(this).attr("data-idrendcompte");
    let idSolicitud = $(this).attr("data-idsolicitud");
    let secuencia = $(this).attr("data-secuencia");

    Swal.fire({
        title: '¿Desea Eliminar el Comprobante?',
        text: 'Textile Sourcing Company',
        icon: 'info',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Ok'

    }).then((result) => {

        if (result.value) {

            parametro = {
                'opcion': 11,
                'idRendCompte': idRendCompte,
                'idTipoComp': 0,
                'serieNumero': "",
                'rucDNI': "",
                'rs': "",
                'codigoPC': "",
                'obs': "",
                'fechaEmision': "",
                'codceco': 0,
                'idTipoMod': 0,
                'porcentajeIGV': 0,
                'obs1': '',
                'obs2': '',
                'fecha1': '',
                'periodo': "",
                'solitudSeqArray': null,
                'detalleComprobanteArray': null,
                'idSolicitud': idSolicitud,
                'secuencia': secuencia
            };

            $.ajax({
                url: "/finanzas/RG_SetRendicionComprobante",
                type: "post",
                data: parametro,
                dataType: "json",
                success: function (resultado) {

                    if (resultado.isRedirect) {
                        window.location.href = resultado.redirectUrl;
                    }
                    else {
                        if (resultado.entidad.idResponse > 0) {

                            rvalidarrestadoSolicitud(2, rsolicitudCabCreada.idSolicitud);
                            rgetListaDetalle(rsolicitudCabCreada.idSolicitud);

                            rlimpiarControlModal(1);
                            rlimpiarControlModal(2);
                            rlimpiarControlModal(3);

                            $("#rregComprobModal").modal("hide");

                            rmensajePrincipal(true, resultado.entidad.statusResponse);
                        }
                        else {
                            rmensajePrincipal(false, resultado.entidad.statusResponse);
                        }
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {

                }
            });

        }

    });

});

$("#rbtnVerRegistrados").click(function () {

    if (rvalidacionVerCmptes()) {

        $.ajax({
            url: "@Url.Action("RG_BuscarIdSolicitud", "Finanzas")",
            data: {
                idSolicitud: rsolicitudCabCreada.idSolicitud,
            },
            success: function (data) {
                window.location.href = '@Url.Action("MapearReporteCXSPDF", "Finanzas")'
            }
        });

    }
});

function rvalidacionVerCmptes() {
    var respuesta = false;

    let idSolicitud = rsolicitudCabCreada.idSolicitud;

    if (idSolicitud == null || typeof (idSolicitud) == "undefined") {
        respuesta = false;

        Swal.fire({
            title: 'Seleccione una Solicitud por favor',
            text: "Textile Sourcing Company",
            icon: 'warning',
            showConfirmButton: true,
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Ok'
        });
    }
    else {
        respuesta = true;
    }

    return respuesta;
}

function rcargarComprobanteCabecera(pIdRendCompte) {

    $.ajax({
        url: "/finanzas/RG_ReembolsoCpteCabecera",
        type: "GET",
        data: { idRendCompte: pIdRendCompte },
        dataType: "json",
        success: function (resultado) {

            if (resultado.isRedirect) {
                window.location.href = resultado.redirectUrl;
            }
            else {

                rlistaComptesIdTipoComp = resultado.entidad.idTipoComp;
                rsolicitudCabCreada = resultado.entidad;

                if (resultado.entidad.idTipoComp == 1 || resultado.entidad.idTipoComp == 2) {

                    // FACTURA Y BOLETA
                    $("#rfacturaboleta-tab").removeClass("disabled");
                    $("#rplanillamovilidad-tab").addClass("disabled");
                    $("#rdeclaracionjur-tab").addClass("disabled");

                    $("#rregComprobModal .nav-tabs #rfacturaboleta-tab").tab('show');
                    rtabSeleccionado = 1;

                    $("#rcbxTipoCpbteCompModal1").val(resultado.entidad.idTipoComp);
                    document.getElementById("rcbxTipoCpbteCompModal1").disabled = true;

                    $("#rcbxConcepCabModal1").val(resultado.entidad.idConceptoCab);
                    $("#rcbxConcepDetModal1").val(resultado.entidad.idConceptoDet);
                    $("#rtxtObservacionModal1").val(resultado.entidad.observacionDet);


                    $("#rtxtSerieModal1").val(resultado.entidad.nserie);
                    $("#rtxtNumeroModal1").val($.trim(resultado.entidad.ndocof));

                    $("#rtxtFechaModal1").val(resultado.entidad.fechaEmision);
                    $("#rcbxMonedaModal1").val(resultado.entidad.idTipoMod);
                    $("#rcbxPeriodoModal1").val(resultado.entidad.periodo);
                    $("#rtxtRucCompModal1").val(resultado.entidad.rucDNI);
                    $("#rtxtRSCompModal1").val(resultado.entidad.rs);
                    $("#rtxtCodSofyaPCModal1").val(resultado.entidad.codigoPC);
                    $("#rtxtObsCompModal1").val(resultado.entidad.obs);
                    $("#rtxtCeCoModal1").val(resultado.entidad.ceco);

                    $("#rkpiMontoSeleecionadoModal1").text(resultado.entidad.simbolo + " " + formatoNumero(resultado.entidad.totalCmpbnteSolicitud - resultado.entidad.totalCmpbnte));


                    if (resultado.entidad.idTipoComp == 1) {
                        // Factura
                        $("#ridimportetotal1").val(formatoNumero(resultado.entidad.totalCmpbnteBI));
                        $("#ridigv1").val(formatoNumero(resultado.entidad.totalCmpbnteBI * (PORCENTAJE_IGV / 100)));
                        $("#ridtotal1").val(formatoNumero(resultado.entidad.totalCmpbnte));
                    }
                    else if (resultado.entidad.idTipoComp == 2) {
                        // Boleta
                        $("#ridimportetotal1").val(formatoNumero(resultado.entidad.totalCmpbnteBI));
                        $("#ridigv1").val(formatoNumero(0));
                        $("#ridtotal1").val(formatoNumero(resultado.entidad.totalCmpbnte));
                    }

                    $("#rcbxUM1").val("UN");
                    $("#rtxtDescripcion1").val("");
                    $("#txtCantidad1").val("");
                    $("#rtxtValorUnitarioTotal1").val("");

                    rdefinirValoresTipoCpte($("#rcbxTipoCpbteCompModal1").val());
                    rcargarComprobanteDetalle(resultado.entidad.idRendCompte, resultado.entidad.idTipoComp);
                }
                else if (resultado.entidad.idTipoComp == 3) {

                    // PLANILLA DE MOVILIDADA
                    $("#rfacturaboleta-tab").addClass("disabled");
                    $("#rplanillamovilidad-tab").removeClass("disabled");
                    $("#rdeclaracionjur-tab").addClass("disabled");

                    $('#rregComprobModal .nav-tabs #rplanillamovilidad-tab').tab('show');
                    rtabSeleccionado = 2;

                    $("#rtxtObservacionModal2").val(resultado.entidad.observacionDet);


                    $("#rtxtFechaModal2").val(resultado.entidad.fechaEmision);
                    $("#txtNumPlanillaModal2").val(resultado.entidad.serieNumero);
                    $("#rcbxMonedaModal2").val(resultado.entidad.idTipoMod);
                    $("#rcbxPeriodoModal2").val(resultado.entidad.periodo);

                    $("#rtxtDNIModal2").val(resultado.entidad.rucDNI);
                    $("#rtxtApeNomModal2").val(resultado.entidad.rs);
                    $("#rtxtCeCoModal2").val(resultado.entidad.ceco);

                    $("#rkpiMontoSeleecionadoModal2").text(resultado.entidad.simbolo + " " + formatoNumero(resultado.entidad.totalCmpbnteSolicitud - resultado.entidad.totalCmpbnte));

                    rcargarComprobanteDetalle(resultado.entidad.idRendCompte, resultado.entidad.idTipoComp);

                }
                else if (resultado.entidad.idTipoComp == 4) {

                    // DECLARACION JURADA.
                    $("#rfacturaboleta-tab").addClass("disabled");
                    $("#rplanillamovilidad-tab").addClass("disabled");
                    $("#rdeclaracionjur-tab").removeClass("disabled");

                    $("#rtxtObservacionModal3").val(resultado.entidad.observacionDet);

                    $('#rregComprobModal .nav-tabs #rdeclaracionjur-tab').tab('show');
                    rtabSeleccionado = 3;

                    $("#rtxtFechaModal3").val(resultado.entidad.fechaEmision);
                    $("#rtxtCodigoModal3").val(resultado.entidad.serieNumero);
                    $("#rcbxMonedaModal3").val(resultado.entidad.idTipoMod);
                    $("#rcbxPeriodoModal3").val(resultado.entidad.periodo);
                    $("#rtxtDNIModal3").val(resultado.entidad.rucDNI);
                    $("#rtxtApeNomModal3").val(resultado.entidad.rs);
                    $("#rtxtCeCoModal3").val(resultado.entidad.ceco);

                    $("#rtxtCiudadModal3").val(resultado.entidad.obs1);
                    $("#rtxtPaisModal3").val(resultado.entidad.obs2);
                    $("#rtxtFechaDesdeModal3").val(resultado.entidad.fecha1);
                    $("#rtxtFechaHastaModal3").val(resultado.entidad.fecha2);

                    $("#rkpiMontoSeleecionadoModal3").text(resultado.entidad.simbolo + " " + formatoNumero(resultado.entidad.totalCmpbnteSolicitud - resultado.entidad.totalCmpbnte));

                    rcargarComprobanteDetalle(resultado.entidad.idRendCompte, resultado.entidad.idTipoComp);
                }

            }
        },
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });

}

function rcargarComprobanteDetalle(pIdRendCompte, pIdTipoComp) {

    $.ajax({
        url: "/finanzas/RG_ReembolsoCpteDetalle",
        type: "GET",
        data: { idRendCompte: pIdRendCompte },
        dataType: "json",
        success: function (resultado) {

            if (resultado.isRedirect) {
                window.location.href = resultado.redirectUrl;
            }
            else {

                if (pIdTipoComp == 1 || pIdTipoComp == 2) {

                    // FACTURA Y BOLETA

                    let tablaFB = $("#rtblFacturaBoleta").DataTable();

                    tablaFB.destroy();
                    var trFB = "";
                    rdetComprobanteArray1 = [];

                    resultado.lista.forEach((obj) => {

                        riDetalle1++;

                        rdetComprobanteArray1.push({
                            conceptoDet: obj.conceptoDet,
                            iDetalle: riDetalle1,
                            fecha: obj.fecha,
                            fechaBD: obj.fechaBD,
                            detalle1: obj.detalle1,
                            detalle2: "",
                            codum: obj.codum,
                            valorunit: obj.valorunit,
                            cantidad: obj.cantidad,
                            total: obj.total,

                            idConceptoDet: obj.idConceptoDet,
                            idConceptoCab: obj.idConceptoCab,
                            idSolicitud: obj.idSolicitud,
                            idTipo: obj.idTipo

                        });
                        //dddddd

                        trFB += "<tr> "
                            + " <td></td>"
                            + " <td> " + obj.um + "</td>"
                            + " <td> " + obj.conceptoDet + "</td>"
                            + " <td> " + obj.detalle1 + "</td>"
                            + " <td> " + formatoNumero(obj.valorunit) + "</td>"
                            + " <td> " + formatoNumero(obj.total) + "</td>"
                            + " <td class='text-center'>"
                            + "     <button data-iDetalle ='" + riDetalle1 + "' class='btn btn-sm btn-danger cselectEliminar1 ml-1'><i class='fas fa-trash'></i></button>"
                            + "</td>"
                            + "</tr>";
                    });


                    $("#rtblFacturaBoletaContent").html(trFB);

                    $("#rtblFacturaBoleta").DataTable(
                        {
                            'language': { 'url': '/libs/datatables/spanish.json' },
                            lengthMenu: [[3], [3]]
                        });

                }
                else if (pIdTipoComp == 3) {

                    // PLANILLA DE MOVILIDAD

                    let tablaPM = $("#rtblPlanillaMovilidad").DataTable();

                    tablaPM.destroy();
                    var trPM = "";
                    rdetComprobanteArray2 = [];
                    rdetCpteArrayValid2 = [];

                    resultado.lista.forEach((obj) => {

                        riDetalle2++;

                        rdetComprobanteArray2.push({
                            iDetalle: riDetalle2,
                            fecha: obj.fecha,
                            fechaBD: obj.fechaBD,
                            detalle1: obj.detalle1,
                            detalle2: obj.detalle2,
                            codum: obj.codum, // UNIDAD
                            valorunit: obj.valorunit,
                            cantidad: obj.cantidad,

                            idConceptoDet: obj.idConceptoDet,
                            idConceptoCab: obj.idConceptoCab,
                            idSolicitud: obj.idSolicitud,
                            idTipo: obj.idTipo
                        });

                        trPM += "<tr> "
                            + " <td></td>"
                            + " <td> " + obj.fecha + "</td>"
                            + " <td> " + obj.detalle1 + "</td>"
                            + " <td> " + obj.detalle2 + "</td>"
                            + " <td> " + formatoNumero(obj.valorunit) + "</td>"
                            + " <td class='text-center'>"
                            + "     <button data-idrendcomptedet='" + riDetalle2 + "' data-toggle='collapse' class='btn btn-sm btn-danger cselectEliminar2 ml-1'><i class='fas fa-trash'></i></button>"
                            + "</td>"
                            + "</tr>";
                    });

                    $("#rtblPlanillaMovilidadContent").html(trPM);

                    $("#rtblPlanillaMovilidad").DataTable(
                        {
                            'language': { 'url': '/libs/datatables/spanish.json' },
                            lengthMenu: [[3], [3]]
                        });

                }
                else if (pIdTipoComp == 4) {

                    // DECLARACION JURADA

                    let tablaDJ_1 = $("#rtblDeclaracionJurada3_1").DataTable();
                    let tablaDJ_2 = $("#rtblDeclaracionJurada3_2").DataTable();

                    tablaDJ_1.destroy();
                    tablaDJ_2.destroy();

                    var trDJ_1 = "";
                    var trDJ_2 = "";

                    rdetComprobanteArray3 = [];


                    resultado.lista.forEach((obj) => {

                        riDetalle3++;

                        rdetComprobanteArray3.push({
                            iDetalle: riDetalle3,
                            fecha: obj.fecha,
                            fechaBD: obj.fechaBD,
                            detalle1: obj.detalle1,
                            detalle2: "",
                            codum: obj.codum, // UNIDAD
                            valorunit: obj.valorunit,
                            cantidad: obj.cantidad,
                            seccion: obj.seccion,
                            idConceptoDet: obj.idConceptoDet,
                            idConceptoCab: obj.idConceptoCab,
                            idSolicitud: obj.idSolicitud,
                            idTipo: obj.idTipo
                        });

                        if (obj.seccion == 1) {
                            trDJ_1 += "<tr> "
                                + " <td></td>"
                                + " <td> " + obj.fecha + "</td>"
                                + " <td> " + obj.detalle1 + "</td>"
                                + " <td> " + formatoNumero(obj.valorunit) + "</td>"
                                + " <td class='text-center'>"
                                + "     <button data-iDetalle='" + riDetalle3 + "' data-toggle='collapse' class='btn btn-sm btn-danger cselectEliminar3_1 ml-1'><i class='fas fa-trash'></i></button>"
                                + "</td>"
                                + "</tr>";
                        }
                        else if (obj.seccion == 2) {
                            trDJ_2 += "<tr> "
                                + " <td></td>"
                                + " <td> " + obj.fecha + "</td>"
                                + " <td> " + obj.detalle1 + "</td>"
                                + " <td> " + formatoNumero(obj.valorunit) + "</td>"
                                + " <td class='text-center'>"
                                + "     <button data-iDetalle='" + riDetalle3 + "' data-toggle='collapse' class='btn btn-sm btn-danger cselectEliminar3_2 ml-1'><i class='fas fa-trash'></i></button>"
                                + "</td>"
                                + "</tr>";
                        }

                    });

                    $("#rtblDeclaracionJuradaContent3_1").html(trDJ_1);
                    $("#rtblDeclaracionJuradaContent3_2").html(trDJ_2);

                    $("#rtblDeclaracionJurada3_1").DataTable(
                        {
                            'language': { 'url': '/libs/datatables/spanish.json' },
                            lengthMenu: [[5], [5]]
                        });

                    $("#rtblDeclaracionJurada3_2").DataTable(
                        {
                            'language': { 'url': '/libs/datatables/spanish.json' },
                            lengthMenu: [[5], [5]]
                        });
                }
            }

        },
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}

$("#rbtnRegistrarProveedor").click(function () {

    if (rvalidacionRegistro()) {

        MostrarCarga("Guardando...");

        $.ajax({
            url: "/finanzas/RG_SetproveedorRegistrar",
            type: "POST",
            data: {
                'opcion': '2',
                'tipoPersona': 'J',
                'ruc': $("#rtxtRUCModal").val(),
                'rs': $("#rtxtRSModal").val(),
                'direccion': $("#rtxtDireccionModal").val()
            },
            dataType: "json",
            success: function (resultado) {

                rproveedor = resultado.entidad;

                if (resultado.isRedirect) {
                    window.location.href = resultado.redirectUrl;
                }
                else {

                    if (resultado.entidad.idResponse == 1) {
                        rmensajePrincipal(true, resultado.entidad.statusResponse);
                    }
                    else {
                        rmensajePrincipal(false, resultado.entidad.statusResponse);
                    }
                }

                $("#rtxtRucCompModal1").val(rproveedor.creguc);
                $("#rtxtRSCompModal1").val(rproveedor.danvar);
                $("#rtxtCodSofyaPCModal1").val(rproveedor.ctpava + "-" + rproveedor.canvar);

                $('#rproveedorModal').modal('hide');
                $("#rregComprobModal").modal("show");
            },
            error: function (xhr, ajaxOptions, thrownError) {

            }
        });
    }
});

function rvalidacionRegistro() {
    var respuesta = false;

    if ($("#rtxtRUCModal").val().trim() == "") {
        respuesta = false;
        Swal.fire({
            title: 'Ingrese un código de RUC',
            text: "Textile Sourcing Company",
            icon: 'warning',
            showConfirmButton: true,
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Ok'

        });
    }
    else if ($("#rtxtRSModal").val().trim() == "") {
        respuesta = false;

        Swal.fire({
            title: 'Ingrese Razon Social',
            text: "Textile Sourcing Company",
            icon: 'warning',
            showConfirmButton: true,
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Ok'
        });
    }
    else {
        respuesta = true;
    }

    return respuesta;
}


