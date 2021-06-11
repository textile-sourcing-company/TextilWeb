// Obtenemos la ejecucion del boton Buscar
$("#btnsearch").click(function () {
    Listarinformacion();
});
$("#btnexprt").click(function () {
    var v_programa = $("#txtprogrma").val();
    var v_pedido = $("#txtpedido").val();
    var v_Po = $("#txtPO").val();


    let datos = {
        'PPROG': v_programa,
        'PPEDIDO': v_pedido,
        'PPO': v_Po,
    }
    exportarResumen(datos)
});
function exportarResumen(parametros) {

    MostrarCarga("Cargando...");

    $.ajax({
        url: '/Logistica/ListarSeguimientoAvioProgr',
        contentType: 'application/json; charset=utf-8',
        datatype: 'json',
        type: "GET",
        data: parametros,
        success: function () {
            Swal.fire({
                icon: 'success',
                title: 'Reporte Generado con Éxito',
                text: 'Textile Sourcing Company',
                showConfirmButton: false,
                timer: 1500,
                onClose: () => {
                    window.location = "/Logistica/ListarSeguimientoAvioProgr?PPROG=" + parametros.PPROG + "&PPEDIDO=" + parametros.PPEDIDO + "&PPO=" + parametros.PPO;
                }
            });
        }
    });
}
function convertDateFormat(string) {
    var info = string.split('-').reverse().join('/');
    return info;
}
function Listarinformacion() {
    var v_programa = $("#txtprogrma").val();
    var v_pedido = $("#txtpedido").val();
    var v_Po = $("#txtPO").val();


    let datos = {
        'PPROG': v_programa,
        'PPEDIDO': v_pedido,
        'PPO': v_Po,
    }

    MostrarCarga("Cargando...");
    $.ajax({
        url: '/Logistica/ListarSeguimientoAvioProgr/',
        type: 'GET',
        contenType: 'json',
        data: datos,
        success: function (e) {

            let tabla = $("#TablaSegPro").DataTable();

            tabla.destroy();
           
            let tr = "";
            e.forEach(function (obj) {
                tr += `
                    <tr>
                        <td>${obj.Cliente}</td>                       
                        <td>${obj.Fecha_creacion_pedido}</td> 
                        <td>${obj.Fecha_Exfactory}</td> 
                        <td>${obj.Programa}</td> 
                        <td>${obj.Estilo_cliente}</td>
                        <td>${obj.Pedido}</td> 
                        <td>${obj.Fecha_emision_req}</td> 
                        <td>${obj.Requerimiento}</td> 
                        <td>${obj.Po}</td> 
                        <td>${obj.Fecha_asig_avio}</td> 
                        <td>${obj.Codigo_avio}</td> 
                        <td>${obj.Descripcion_avio}</td> 
                        <td>${obj.Orden_compra}</td> 
                        <td>${obj.Fecha_Emision_Oc}</td>
                        <td>${obj.Fecha_compromiso_Oc}</td>
                        <td>${obj.Cantidad_req_programa}</td>
                        <td>${obj.Cantidad_requerimiento}</td>
                        <td>${obj.Cantidad_Oc}</td>
                        <td>${obj.Cantidad_Recibida}</td>
                        <td>${obj.Fecha_Ingreso_alm}</td>
                        <td>${obj.Periodo_booking}</td>
                        <td>${obj.Orden_Planeamiento}</td>
                   </tr>`;

            });
            $("#TablaCuerpoSegPro").html(tr);
            //ArmarDataTable("TablaSegPro", false, false);
            $("#TablaSegPro").DataTable(
                {
                    "language": { 'url': '/libs/datatables/spanish.json' },
                    "initComplete": function (settings, json) {
                        // var api = this.api();

                    },
                    "footerCallback": function (row, data, start, end, display) {
                        //var api = this.api(), data;
                        //sumaMontos(this);
                        return;
                    },
                    "scrollX": true,
                    "bSort": false,
                    //dom: 'Bfrtip',
                    //buttons: [
                    //    'excel','pdf', 'print'
                    //],
                    scrollY: '60vh',
                    scrollCollapse: true,
                    paging: true
                });
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