$("#btnsearch").click(function(){
   
    LstarUsuarios();
});
function LstarUsuarios(){
    var v_dni = $("#txtdni").val();
    var v_fotocheck = $("#txtfotocheck").val();
    var v_usuario = $("#txtusuario").val();
    
    let datos = {
        'PDNI': v_dni,
        'PFOTOCHECK' : v_fotocheck,
        'PUSUARIO' : v_usuario,
    }

    MostrarCarga("Cargando..."); 
    $.ajax({
        url: '/Sistema/ListarUsuarios/',
        type: 'GET',
        contenType: 'json',
        data: datos,
        success: function (e) {        
             
            let tabla = $("#TablaUsuarios").DataTable();
            
            tabla.destroy();
            let tr = "";
            e.forEach(function (obj) {
                
                tr += `
                    <tr>
                        <td>${obj.dni}</td>                       
                        <td>${obj.nombre}</td> 
                        <td>${obj.desc_cargo}</td> 
                        <td>${obj.ccosto}</td> 
                        <td>${obj.FOTOSHECK}</td> 
                        <td>${obj.usuario}</td> 
                        <td>${obj.pass}</td> 
                        <td>${obj.accesos}</td> 
                   </tr>`;
            });

            $("#TablacurpoUsuarios").html(tr);
            ArmarDataTable("TablaUsuarios", true, false);    
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