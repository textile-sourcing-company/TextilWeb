// ##############
// ## MENSAJES ##
// ##############

// ADVERTIR
function Advertir(mensaje) {
    swal.fire(mensaje, "Textile Web", "error");
}
//INFORMAR -- SUCCESS
//function Informar(mensaje) {
//    swal.fire(mensaje, "Textile Web", "success");
//}
function Informar(mensaje, autocierre = false, reload = false) {

    if (reload) {
        setTimeout(() => {
            location.reload();
        }, autocierre + 100);
    }

    if (autocierre) {

        Swal.fire({
            icon: 'success',
            title: mensaje,
            text: 'Textile Web',
            timer: autocierre,
            allowEscapeKey: false,
            allowOutsideClick: false
            // timerProgressBar: true,
        })

    } else {
        Swal.fire(mensaje, "Textile Web", "success");
    }

}


// TOAST
function InformarMini(mensaje, tiempo = false) {


    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: tiempo ? tiempo : 3000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer)
            toast.addEventListener('mouseleave', Swal.resumeTimer)
        }
    })

    Toast.fire({
        icon: 'success',
        title: mensaje
    })
}

// TOAST ADVERTIR
function AdvertirMini(mensaje, tiempo = false) {


    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: tiempo ? tiempo : 3000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer)
            toast.addEventListener('mouseleave', Swal.resumeTimer)
        }
    })

    Toast.fire({
        icon: 'error',
        title: mensaje
    })
}

//PREGUNTAR
async function Preguntar(pregunta) {
    let resultado = await Swal.fire({
        title: pregunta,
        text: "Textile Web",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Confirmar'
    });

    return resultado;
}

function MostrarCarga(mensaje) {

    Swal.fire({
        title: mensaje,
        html: 'Esto puede tardar unos minutos',
        //timer: 2000,
        allowEscapeKey: false,
        allowOutsideClick: false,
        timerProgressBar: true,
        onBeforeOpen: () => {
            Swal.showLoading()
        }
    })
}

function MostrarCarga_new(mensaje) {

    Swal.fire({
        title: mensaje,
        html: 'Esto puede tardar unos minutos',
        //timer: 2000,
        allowEscapeKey: false,
        allowOutsideClick: false,
        timerProgressBar: true,
        onBeforeOpen: () => {
            Swal.showLoading()
        }
    })
}

function OcultarCarga() {
    Swal.hideLoading();
    Swal.clickConfirm();
}

//LOGIN ACCESO
function EntrarSistema(mensaje) {
    let timerInterval;

    Swal.fire({
        title: mensaje,
        icon: 'success',
        html: 'Cargando...',
        timer: 1000,
        onBeforeOpen: () => {
            Swal.showLoading()
        },
        onClose: () => {
            clearInterval(timerInterval)
        }
    }).then((result) => {
        if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.timer
        ) {
            //REDIRECCIONANDO 
            location.href = "/dashboard/index";
        }
    })
}

// GET DATOS PREGUNTAR
async function getDatosPreguntar(pregunta) {


    //let valor = null;
    const { value: valor } = await Swal.fire({
        title: pregunta,
        input: 'text',
        //inputLabel: 'Your IP address',
        //inputValue: inputValue,
        showCancelButton: true,
        inputValidator: (value) => {
            if (!value) {
                return 'Es necesario ingresar la información'
            }
        }
    })
    return valor;

    /*
    if (ipAddress) {
        Swal.fire(`Your IP address is ${ipAddress}`)
    }*/


}   