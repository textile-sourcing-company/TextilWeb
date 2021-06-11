//VARIABLES GLOBALES
var cgc9 = null,
    cgc4 = null,
    cgc2 = null,
    sequ = 0,
    seqmas = 0;
//LOAD
$(document).ready(function () {
    ListarClientes();
});
/********************************************************

    a s i g a c i o n  d e  f u n c i o n e s
            P R O C E S O  M A S I V O

*********************************************************/
//CLICK BOTON AGREGAR MASIVO
$("#btnaddmas").click(function () {
    $('#ModalMasivo').modal('show')
    let combo2 = "#cbclientemas";
    LimpiarDatatable('#TablaMermasmodalMasivo');
    ListarClientespop(combo2);
   
});
$("#btnaddclimasivo").click(function(){
    var valida = Validacionselcliente("#cbclientemas");
    if (valida) {
        return;
    }
    AgregarClienteMasivo();
    $("#TablaMermasmodalMasivo").DataTable().clear();
    LstarClientesparaMasivo();
});
//click en registrar
$("#btnregmas").click(function () {
    if (AgregMermaMasiva() == false) {
        LimpiarTexto("txtCantini","txtCantfin","txtcantper","txtprcprdmas");
        $("#ModalMasivo").modal("hide");
    } ;
    
});
//ACCIONAR EL BOTON ELIMINAR BTON MODAL MASIVO
$("#TablaMermasmodalMasivo").on('click', '.eliminarMasi', function () {
    //LLENANDO VARIABLES GLOBALES
    seqmas = $(this).data("seq");
    var datos = {
        'pseq' : seqmas,
    }

    $.ajax({
        url: '/Planeamiento/DeleteClientesMermaMas',
        type: 'GET',
        data: datos,
        datatype: 'json',
        success: function (e) {
           
            if (e.success) {
                window.location.href = e.redurl;
            }
            else {
                if (e.rsperror == "F") {
                    Swal.fire({ 
                        icon: 'success',
                        title: e.mensaje,
                        text: 'Textil Sourcing Company',
                        allowEscapeKey: false,
                        allowOutsideClick: false
                    });
                    
                    LstarClientesparaMasivo();
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


        }
    });

})
function AgregMermaMasiva(){
    let qtdeini = $("#txtCantini").val();
    let qtdefin = $("#txtCantfin").val();
    let qtdperd_ = $("#txtcantper").val();
    let percmer_ = $("#txtprcprdmas").val();
    let verror = false;
    var valid = ValidacionCantidad(qtdeini,qtdefin,qtdperd_,percmer_);
    if (valid) {
        return;
    }
    var datos = {
        'pqtdini': qtdeini,
        'pqtdfin': qtdefin,
        'pqtdperd': qtdperd_,
        'pporcperd': percmer_,
    }
    $.ajax({
        url: '/Planeamiento/GenerarMermaClienteMasiva',
        type: 'GET',
        data: datos,
        datatype: 'json',
        success: function (e) {
            if (e.success) {
                window.location.href = e.redurl;
            }
            else {
                if (e.rsperror == "F") {
                    verror = false;
                }else {
                    verror = true;
                }

                if (e.rsperror == "F") {
                    Swal.fire({
                        icon: 'success',
                        title: e.mensaje,
                        text: 'Textil Sourcing Company',
                        allowEscapeKey: false,
                        allowOutsideClick: false
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


        }
    });
    return verror;
}
//Listamos Los clientes agregados
function LstarClientesparaMasivo(){
    let cliente9_ = $("#cbclientemas").find(":selected").attr("data-cli9");
    let cliente4_ = $("#cbclientemas").find(":selected").attr("data-cli4");
    let cliente2_ = $("#cbclientemas").find(":selected").attr("data-cli2");

    let datos = {
        'pcgc9': cliente9_,
        'pcgc4': cliente4_,
        'pcgc2': cliente2_,
    }
    $.ajax({
        url: '/Planeamiento/ListarClientesMermaMas/',
        type: 'GET',
        contenType: 'json',
        data: datos,
        success: function (e) {        
            console.log(e);
            let tabla = $("#TablaMermasmodalMasivo").DataTable();
            
            tabla.destroy();
            let tr = "";
            e.forEach(function (obj) {
                tr += `
                    <tr>SEQ_LIMITE
                        <td>${obj.CLIENTE}</td>                       
                        <td class='text-center'>
                            <a class='eliminarMasi iconelim ml-1' data-seq  ='${obj.SEQ_LIMITE}'  href='#'>  
                                <i class='fas fa-trash'></i> 
                            </a>           
                        </td>
                   </tr>`;
            });

            $("#tablaCuerpoMermasmodalMasivo").html(tr);
            ArmarDataTable("TablaMermasmodalMasivo");            
        }
    });
}
//Agregamos los Pedidos a Procesar
function AgregarClienteMasivo(){
    let cliente9_ = $("#cbclientemas").find(":selected").attr("data-cli9");
    let cliente4_ = $("#cbclientemas").find(":selected").attr("data-cli4");
    let cliente2_ = $("#cbclientemas").find(":selected").attr("data-cli2");
    
    var datos = {
        'pcgc9': cliente9_,
        'pcgc4': cliente4_,
        'pcgc2': cliente2_,
    }
    console.log(datos);
    $.ajax({
        url: '/Planeamiento/AgregaClientesMerma',
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
                        icon: 'error',
                        title: e.mensaje,
                        text: 'Textil Sourcing Company',
                        allowEscapeKey: false,
                        allowOutsideClick: false
                    });
                }
            }


        }
    });
};
/********************************************************

    a s i g a c i o n  d e  f u n c i o n e s
        P R O C E S O  I N D I V I D U A L

*********************************************************/
//EJECUTAR CLICK DEL BOTON AGREGAR
$("#btnadd").click(function () {
    $('#exampleModal').modal('show')
    let combo1 = "#cbclientepop";
    LimpiarTexto("txtCantinimdl","txtCantfinmdl","txtcantpermdl","txtprcmermdl")
    LimpiarDatatable('#TablaMermasmodal');
    ListarClientespop(combo1); 
});
//CLIENTES PARA MODAL
function ListarClientespop(combo) {
    $.ajax({
        url: '/operaciones/ListarClientesFtec/',
        type: 'GET',
        datatype: 'json',
        success: function (e) {
            console.log(e);
            let option = "<option value='0'>SELECCIONE</option>";
            e.forEach(function (obj) {
                option += "<option "
                       + " data-cli9  =   " + obj.cliente9
                       + " data-cli4  =   " + obj.cliente4
                       + " data-cli2  =   " + obj.cliente2
                       + ">" + obj.nombre_cliente
                       + "</option>";
            });

            $(combo).html(option);
        }
    });
}
//click en registrar
$("#btnreg").click(function () {
   
    var valida = Validacionselcliente("#cbclientepop");
    if (valida) {
        return;
    }
    GenerMerma();
    //BuscarRegistrosModal();
});
//INGRESAR LAS MERMAS INGRESADAS
function GenerMerma() {
    let cliente9_ = $("#cbclientepop").find(":selected").attr("data-cli9");
    let cliente4_ = $("#cbclientepop").find(":selected").attr("data-cli4");
    let cliente2_ = $("#cbclientepop").find(":selected").attr("data-cli2");
    let qtdeini = $("#txtCantinimdl").val();
    let qtdefin = $("#txtCantfinmdl").val();
    let qtdperd_ = $("#txtcantpermdl").val();
    let percmer_ = $("#txtprcmermdl").val();
    
    var valid = ValidacionCantidad(qtdeini,qtdefin,qtdperd_,percmer_);
    if (valid) {
        return;
    }
    var datos = {
        'pcgc9': cliente9_,
        'pcgc4': cliente4_,
        'pcgc2': cliente2_,
        'pqtdini': qtdeini,
        'pqtdfin': qtdefin,
        'pqtdperd': qtdperd_,
        'pporcperd': percmer_,
    }
    console.log(datos);
    $.ajax({
        url: '/Planeamiento/GenerarMermaCliente',
        type: 'GET',
        data: datos,
        datatype: 'json',
        success: function (e) {
            if (e.success) {
                window.location.href = e.redurl;
            }
            else {
                if (e.rsperror == "F") {
                    Swal.fire({
                        icon: 'success',
                        title: e.mensaje,
                        text: 'Textil Sourcing Company',
                        allowEscapeKey: false,
                        allowOutsideClick: false
                    });
                    //ACTUALIZANDO
                    BuscarRegistrosModal();
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


        }
    });
}
//BUSCAR REGISTRO PARA MODAL
function BuscarRegistrosModal()
{
    let cliente9 = $("#cbclientepop").find(":selected").attr("data-cli9");
    let cliente4 = $("#cbclientepop").find(":selected").attr("data-cli4");
    let cliente2 = $("#cbclientepop").find(":selected").attr("data-cli2");
    let datos = {
        'pcgc9': cliente9,
        'pcgc4': cliente4,
        'pcgc2': cliente2,
    }
    $.ajax({
        url: '/Planeamiento/BuscMermaxCliente',
        type: 'GET',
        contenType: 'json',
        data: datos,
        success: function (e) {
            let tabla = $("#TablaMermasmodal").DataTable();
            tabla.destroy();
            let tr = "";
            e.forEach(function (obj) {
                tr += "<tr>"
                         + "<td>" + obj.LIMITE_INFERIOR + "</td>"
                         + "<td>" + obj.LIMITE_SUPERIOR + "</td>"
                         + "<td>" + obj.QTDE_PERDA + "</td>"
                         + "<td>" + obj.PERC_PERDA + "</td>"
                       + "</tr>";
            });
            $("#tablaCuerpoMermasmodal").html(tr);
            ArmarDataTable("TablaMermasmodal");
        }
    });
}
/********************************************************

    a s i g a c i o n  d e  f u n c i o n e s
 B U S Q U E D A  G E N E R A L  D E  M E R M A S 

*********************************************************/
//LLENAR DATATBLE PRINCIPAL           
$("#btnsearch").click(function () {
    BuscarRegistros();
});
//BOTNO EDITAR
$("#btnedit").click(function()
{
    EditMerma();
})
//ACCIONAR EL BOTON EDITAR DEL DATATABLE PRINCIPAL
$("#tablaCuerpoMermas").on('click', '.editar', function () {
    var ini = $(this).data("limiteini");
    var fin = $(this).data("limitefin");
    var cliente = $(this).data("cliente");
    var qtdperd = $(this).data("qtdperdida");
    var prcperd = $(this).data("prcperdida");
    //LLENANDO VARIABLES GLOBALES
    cgc9 = $(this).data("cgc9");
    cgc4 = $(this).data("cgc4");
    cgc2 = $(this).data("cgc2");
    sequ = $(this).data("seq");
 

    $("#txtCantiniedit").val(ini);
    $("#txtCantfinedit").val(fin);
    $("#lblcliente").val(cliente);
    $("#txtcantperedit").val(qtdperd);
    $("#txtprcedit").val(prcperd);
    $("#ModalEdicion").modal("show");
})

//ACCIONAR BOTON ELIMINAR
$("#tablaCuerpoMermas").on('click','.eliminar',function(){   
    //LLENANDO VARIABLES GLOBALES
    cgc9 = $(this).data("cgc9");
    cgc4 = $(this).data("cgc4");
    cgc2 = $(this).data("cgc2");
    sequ = $(this).data("seq");
    DeletMerma();
   
});
//LISTAR CLIENTES FORM PRINCIPAL
function ListarClientes() {
    $.ajax({
        url: '/operaciones/ListarClientesFtec/',
        type: 'GET',
        datatype: 'json',
        success: function (e) {
            let option = "<option value='0'>SELECCIONE TODOS LOS CLIENTES</option>";
            e.forEach(function (obj) {
                option += "<option "
                       + " data-cli9  =   " + obj.cliente9
                       + " data-cli4  =   " + obj.cliente4
                       + " data-cli2  =   " + obj.cliente2
                       + ">" + obj.nombre_cliente
                       + "</option>";
            });

            $("#cbcliente").html(option);
        }
    });
}
//EDITAR MERMAS REGISTRADAS
function EditMerma()
{
    let qtdeini = $("#txtCantiniedit").val();
    let qtdefin = $("#txtCantfinedit").val();
    let qtdeprd = $("#txtcantperedit").val();
    let prcperd = $("#txtprcedit").val();
    //VALIDACIONES
 
    var valid = ValidacionCantidad(qtdeini,qtdefin,qtdeprd,prcperd);
    if (valid) {
        return;
    }
    var datos = {
        'pcgc9': cgc9,
        'pcgc4': cgc4,
        'pcgc2': cgc2,
        'pseq' : sequ,
        'pqtdini': qtdeini,
        'pqtdfin': qtdefin,
        'pqtdperd': qtdeprd,
        'pporcperd': prcperd,
    }

    $.ajax({
        url: '/Planeamiento/EditMermaCliente',
        type: 'GET',
        data: datos,
        datatype: 'json',
        success: function (e) {
           
            if (e.success) {
                window.location.href = e.redurl;
            }
            else {
                if (e.rsperror == "F") {
                    Swal.fire({ 
                        icon: 'success',
                        title: e.mensaje,
                        text: 'Textil Sourcing Company',
                        allowEscapeKey: false,
                        allowOutsideClick: false
                    });
                    
                    $("#ModalEdicion").modal('hide');
                    BuscarRegistros();
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


        }
    });
}
function DeletMerma(){
    
    var datos = {
        'pcgc9': cgc9,
        'pcgc4': cgc4,
        'pcgc2': cgc2,
        'pseq' : sequ,
    }

    $.ajax({
        url: '/Planeamiento/DeletMermaCliente',
        type: 'GET',
        data: datos,
        datatype: 'json',
        success: function (e) {
           
            if (e.success) {
                window.location.href = e.redurl;
            }
            else {
                if (e.rsperror == "F") {
                    Swal.fire({ 
                        icon: 'success',
                        title: e.mensaje,
                        text: 'Textil Sourcing Company',
                        allowEscapeKey: false,
                        allowOutsideClick: false
                    });
                    BuscarRegistros();
                  
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


        }
    });
}
//BUSCAR LISTADO MERMA POR CLIENTES
function BuscarRegistros() {
    let cliente9 = $("#cbcliente").find(":selected").attr("data-cli9");
    let cliente4 = $("#cbcliente").find(":selected").attr("data-cli4");
    let cliente2 = $("#cbcliente").find(":selected").attr("data-cli2");
    //debugger;
    let datos = {
        'pcgc9': cliente9,
        'pcgc4': cliente4,
        'pcgc2': cliente2,
    }
    $.ajax({
        url: '/Planeamiento/BuscMermaxCliente',
        type: 'GET',
        contenType: 'json',
        data: datos,
        success: function (e) {

            let tabla = $("#TablaMermas").DataTable();
            tabla.destroy();
            let tr = "";
            e.forEach(function (obj) {             
                tr += `
                    <tr>
                        <td>${obj.CLIENTE}</td>
                        <td>${obj.LIMITE_INFERIOR}</td>
                        <td>${obj.LIMITE_SUPERIOR}</td>
                        <td>${obj.QTDE_PERDA}</td>
                        <td>${obj.PERC_PERDA}</td>
                        <td class='text-center'>
                            <a class='editar iconedi mr-1' 
                                data-limiteini  ='${obj.LIMITE_INFERIOR}' 
                                data-cliente    ='${obj.CLIENTE}'
                                data-limitefin  ='${obj.LIMITE_SUPERIOR}'
                                data-qtdperdida ='${obj.QTDE_PERDA}'
                                data-prcperdida ='${obj.PERC_PERDA}'
                                data-cgc9       ='${obj.CGC9}'
                                data-cgc4       ='${obj.CGC4}'
                                data-cgc2       ='${obj.CGC2}'
                                data-seq        ='${obj.SEQ_LIMITE}'
                                href='#'>  
                                <i class='fas fa-edit'></i> 
                            </a>
                            <a class='eliminar iconelim ml-1' 
                                data-cgc9       ='${obj.CGC9}'
                                data-cgc4       ='${obj.CGC4}'
                                data-cgc2       ='${obj.CGC2}'
                                data-seq        ='${obj.SEQ_LIMITE}' 
                                href='#'>  
                                <i class='fas fa-trash'></i> 
                            </a>
                        </td>
                </tr>`;
            })
            $("#tablaCuerpoMermas").html(tr);
            ArmarDataTable("TablaMermas", true, false);
        }
    });
}


/********************************************************

        a s i g a c i o n  d e  f u n c i o n e s
     V A L I D A C I O N   Y   U T I L I T A R I O S

*********************************************************/
function ValidacionCantidad(pcantini,pcantfin,pqtdeperd,pprcperd){
  
    var vicantini = parseInt(pcantini);
    var vicantfin = parseInt(pcantfin);
    var viqtdeperd = parseInt(pqtdeperd);
    var viprcperd = parseInt(pprcperd);
    //VALIDAMOS QUE TODOS LOS CAMPOS SEAN DIFERENTE A CERO
    if ( vicantini== 0 && vicantfin == 0 && viqtdeperd == 0 && viprcperd == 0) {
        Swal.fire({
            icon: 'error',
            title: "Las cantidades no pueden estar en '0' ",
            text: 'Textil Sourcing Company',
            allowEscapeKey: false,
            allowOutsideClick: false
        });
        return true;
    }
    //VALIDAMOS QUE LA CANTIDAD INICIAL SEA DIFERENTE A CERO
    if ( vicantini== 0) {
        Swal.fire({
            icon: 'error',
            title: "La cantidad Inicial debe ser mayor a '0' ",
            text: 'Textil Sourcing Company',
            allowEscapeKey: false,
            allowOutsideClick: false
        });
        return true;
    }
    //VALIDAMOS QUE LA CANTIDAD FINAL SEA DIFERENTE A CERO
    if ( vicantfin == 0) {
        Swal.fire({
            icon: 'error',
            title: "La cantidad Final debe ser mayor a '0' ",
            text: 'Textil Sourcing Company',
            allowEscapeKey: false,
            allowOutsideClick: false
        });
        return true;
    }
    //VALIDAMOS QUE ALMENOS TENGA CANTIDAD O PORCENTAJE
    if ( viqtdeperd == 0 && viprcperd == 0) {
        Swal.fire({
            icon: 'error',
            title: "Debe asignar una Cantidad o Porcentaje",
            text: 'Textil Sourcing Company',
            allowEscapeKey: false,
            allowOutsideClick: false
        });
        return true;
    }
    //VALIDAMOS QUE LA CANTIDAD INICIAL SEA MENOR QUE LA FINAL
    if ( vicantini >= vicantfin) {
        Swal.fire({
            icon: 'error',
            title: "Cantidad inicial no debe ser MAYOR o IGUAL a la cantidad final",
            text: 'Textil Sourcing Company',
            allowEscapeKey: false,
            allowOutsideClick: false
        });
        return true;
    }
}
//VALIDAMOS QUE EL CLIENTE ESTE SELECCIONADO
function Validacionselcliente(combo){
    let cliente9_ = $(combo).find(":selected").attr("data-cli9");
    let cliente4_ = $(combo).find(":selected").attr("data-cli4");
    let cliente2_ = $(combo).find(":selected").attr("data-cli2");

    if (cliente9_ === undefined && cliente4_ === undefined && cliente2_ === undefined) {
        Swal.fire({
            icon: 'error',
            title: "Seleccionar Cliente",
            text: 'Textil Sourcing Company',
            allowEscapeKey: false,
            allowOutsideClick: false
        });
        return true;
    }
}
function LimpiarTexto(pcantini,pcantfin,pcantperd,pporc){
    debugger;
    document.getElementById(pcantini).value="0";
    document.getElementById(pcantfin).value="0";
    document.getElementById(pcantperd).value="0";
    document.getElementById(pporc).value="0.000";


} 
function LimpiarDatatable(pdatatable){
    var table = $(pdatatable).DataTable();
 
    table
        .clear()
        .draw();
}
