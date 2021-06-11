
// CONVIERTE IMAGENES A BASE 64
function ConvertImageToBase64(imagen) {
    let dataimagen1 = document.getElementById(imagen).toDataURL("image/png");
    let BASE64_MARKER = ';base64,';
    let base64Index = dataimagen1.indexOf(BASE64_MARKER) + BASE64_MARKER.length;
    let base64 = dataimagen1.substring(base64Index);
    return base64;
}