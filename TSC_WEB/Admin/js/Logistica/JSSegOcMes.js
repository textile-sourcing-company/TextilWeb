
//LOAD
$(document).ready(function () {
    ListarClientes();
});
//Listar Centro de Costo
function ListarClientes() {
    $.ajax({
        url: '/Logistica/ListarCCosto/',
        type: 'GET',
        datatype: 'json',
        success: function (e) {
            let option = "<option value='0'>SELECCIONE LOS CCOSTO</option>";
            e.forEach(function (obj) {
                option += "<option "
                    + " data-codccosto  =   " + obj.codcentrocosto
                    + ">" + obj.centrocosto
                    + "</option>";
            });

            $("#cbccosto").html(option);
        }
    });
}
//
$("#checkccosto").change(function () {

    let check = $(this).prop("checked");

    if (check) {
        $("#cbccosto").prop('disabled', false);
    }
    else {
        $("#cbccosto").prop('disabled', true);

    }

});

$("#checkmovi").change(function () {

    let check = $(this).prop("checked");

    if (check) {
        $("#dtfechaini").prop('disabled', false);
        $("#dtfechafin").prop('disabled', false); 
    }
    else {
        $("#dtfechaini").prop('disabled', true);
        $("#dtfechafin").prop('disabled', true);
   
    }

});

$("#btnsearch").click(function () {
    Listarinformacion();
});
function Listarinformacion() {
    let check1 = $("#checkmovi").prop("checked");
    if (check1)
    {
        
        var v_mesfin = $("#dtfechafin").val();
        var v_mesini = $("#dtfechaini").val();
    }
    else
    {
        var v_mesfin = "";
        var v_mesini = "";
    }

    var v_Prove = $("#txtproveedor").val();
    var v_Compr = $("#txtcomprador").val();
    var v_Famil = $("#txtfamilia").val();
    var v_ccosto = $("#cbccosto").find(":selected").attr("data-codccosto");
    //.getMonth()
    

    let datos = {
        'P_OC': "",
        'P_PROVEEDOR': v_Prove,
        'P_COMPRADOR': v_Compr,
        'P_FAMILIA': v_Famil,
        'P_NIVEL': "",
        'P_GRUPO': "",
        'P_SUBGRUPO': "",
        'P_ITEM': "",
        'P_MES_INI': v_mesini,
        'P_MES_FIN': v_mesfin,
        'P_ANIO_INI': v_mesini,
        'P_ANIO_FIN': v_mesfin,
        'P_CCO': v_ccosto
    }

    MostrarCarga("Cargando...");
    $.ajax({
        url: '/Logistica/ListarSeg_OcporMes/',
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
                        <td>${obj.CLIENTE}</td>   
                        <td>${obj.MES}</td> 
                        <td>${obj.PROGRAMA}</td> 
                        <td>${obj.FAMILIA}</td>
                        <td>${obj.COD_PROD}</td> 
                        <td>${obj.DSC_PROD}</td> 
                        <td>${obj.OC}</td>      
                        <td>${obj.PROVEEDOR}</td>
                        <td>${obj.COMPRADOR}</td>
                        <td>${obj.FECHA_EMI_OC}</td> 
                        <td>${obj.FECHA_COMP_OC}</td> 
                        <td>${obj.DESCRIPCIONPAGO}</td>
                        <td>${obj.CNT_OC}</td>
                        <td>${obj.PREC_UNIT}</td>
                        <td>${obj.TOTAL}</td>
                        <td>${obj.COD_CENTROCOSTO}</td>
                        <td>${obj.DESC_CENTROCOSTO}</td>
                   </tr>`;

            });
            $("#TablaCuerpoSegPro").html(tr);
            //ArmarDataTable("TablaSegPro", false, false);
            $("#TablaSegPro").DataTable(
                {
                    "language": { 'url': '/libs/datatables/spanish.json' },
                    "scrollX": true,
                    "bSort": false,
                    scrollY: '60vh',
                    scrollCollapse: true,
                    ordering : false,
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