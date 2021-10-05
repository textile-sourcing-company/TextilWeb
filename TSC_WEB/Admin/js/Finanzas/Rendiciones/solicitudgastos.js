// Con Solicitud

const sNIVEL_INTERFAZ = 10;
var sswitchInsertarEditar = 0; // 1: Insertar, 2: Editar

var svIdSolicitud = 0;
var svCodigo = "";
var svIdTipoMod = 0;
var svCodCeCo = 0;
var svIdConcepCab = 0;
var svIdConcepDet = 0;
var svIdTipo = 0;
var svCodSede = '';

var svChecked = false;

var sdiasArray = [];
var sentidadEditar = {};
var sdiasArrayEditar = [];
var sconceptoDetArray = [];
var stipoMonedaArray = [];

var ssumTotalDetalle = 0;

var svCodColSofya = "";
var ssvcolaboradorSofya = "";

var svctpava_bnfcol = "";
var svcanvar_bnfcol = "";
var svcolaborador = "";

var stiposAnulacionCabArray = [];
var sTipoRegistroCabDet = "C";


$(document).ready(function () {

    Inicializar();


    function Inicializar() {

        let parametros = { nivelInterfaz: sNIVEL_INTERFAZ };
        sgetPermisosXCeCo(parametros);

        sgetDias();
        sgetTiposAnulacionCabecera();
        sgetSede();

        slimpiarCabecera();

        document.getElementById("sidtxtcodigo").focus();

        document.getElementById("sbtnAgregarDetalle").disabled = true;
        $("#sbtnAgregarDetalle").removeClass("btn-warning");
        $("#sbtnAgregarDetalle").addClass("btn-secondary");

        document.getElementById("sbtnGuardarCabecera").disabled = true;
        $("#sbtnGuardarCabecera").removeClass("btn-success");
        $("#sbtnGuardarCabecera").addClass("btn-secondary");

        document.getElementById("sbtnAnularSolicitud").disabled = true;
        $("#sbtnAnularSolicitud").removeClass("btn-danger");
        $("#sbtnAnularSolicitud").addClass("btn-secondary");

        document.getElementById("sbtnBuscarCodigo").disabled = true;
        $("#sbtnBuscarCodigo").removeClass("btn-primary");
        $("#sbtnBuscarCodigo").addClass("btn-secondary");


        document.getElementById("sidCbxTipo").disabled = true;
        document.getElementById("sidCbxTipoMod").disabled = true;
        document.getElementById("sidtxtaobs").disabled = true;
        document.getElementById("sbtnBuscarColaborador").disabled = true;

    }


});




