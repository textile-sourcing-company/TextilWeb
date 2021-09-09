
// SOLICITUDES GET PARA OBTENER UN JSON O UN HTML
async function get(controller, operacion, parameters, html = false) {

    // DATOS QUE VAMOS A DEVOLVER
    let response;
    // PARAMETROS 
    let datos = {};

    datos = parameters;
    //datos.operacion = operacion;



    //  EJECUTAMOS
    response = await $.ajax({
        url: `/${controller}/${operacion}`,
        type: 'get',
        data: datos
    })
    // PARSEAMOS RESULTADO
    response = !html ? JSON.parse(response) : response;
    // DEVOLVEMOS
    return response;
}

// SOLICITUDES POST
async function post(controller, operacion, parameters, html = false) {


    // DATOS QUE VAMOS A DEVOLVER
    let response;
    // PARAMETROS 
    let datos = {};


    //datos.parameters = parameters;
    datos = parameters;

    //  EJECUTAMOS
    response = await $.ajax({
        url: `/${controller}/${operacion}`,
        type: 'post',
        data: datos,
        dataType:'json'
    })

    // PARSEAMOS RESULTADO
    // response = JSON.parse(response);
    response = !html ? JSON.parse(response) : response;

    // DEVOLVEMOS
    return response;
}
