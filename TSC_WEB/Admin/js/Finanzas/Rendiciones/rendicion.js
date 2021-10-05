// RENDICION DE GASTOS

var zvTipoComprobante1 = [];

var zsolicitudSeleccionada = {};
var zvIdSolicitud = 0;
var zvCeCo = 0;
var zvIdRendCompte = 0;

var ziDetalle1 = 0;
var ziDetalle2 = 0;
var ziDetalle3 = 0;

var zproveedor = {};
var zdetalleArray = [];
var zdetalleArray3 = [];
var zdetPlaMov = {};

var zdetComprobanteArray1 = [];
var zdetComprobanteArray2 = [];
var zdetComprobanteArray3 = [];

var ztabSeleccionado = 1; // 1: Factura y Boleta, 2: Planilla Movilidad, 3: Planilla de Movilidada

var zlistaComptesIdTipoComp = 0;

var zidsolicitud_dev = 0;
var zidsecuencia_dev = 0;

var zTIPO_REGISTRO = "";
var ztiposComprobantesArray = [];
var rtiposComprobantesArray = [];



$(document).ready(function () {

    Inicializar();


    function Inicializar() {

        // Rendicion de Gastos

        zgetTipoComprobantes();
        zgetTipoMoneda();
        zgetUnidadesMedida();
        zgetPeriodos();

        $("#zregComprobModal .kpi-modal-monto1").css("background-color", "#ABB2B9");
        $("#zregComprobModal .kpi-modal-monto2").css("background-color", "#ABB2B9");
        $("#zregComprobModal .kpi-modal-monto3").css("background-color", "#ABB2B9");

        $("#ztxtCodigoModal3").val("0");
        $("#ztxtNumPlanillaModal2").val("0");
    }
});

