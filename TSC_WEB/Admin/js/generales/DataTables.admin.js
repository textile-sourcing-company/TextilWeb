function ArmarDataTable(tabla,exportar = false,minimo = true,scrool = false) {

    if (!scrool) {
        var objeto = {};
        objeto.language = { 'url': "/Libs/datatables/Spanish.json" };
        objeto.ordering = false;
        //objeto.scrollbar = true;
        if (minimo) {
            objeto.lengthMenu = [[5, 10, 20, -1], [5, 10, 20, 'Todos']];
        }
        if (exportar) {
            objeto.dom = 'Bfrtip';
            objeto.buttons = ['excel', 'pdf', 'print'];
        }

        $(`#${tabla}`).DataTable(objeto);
    } else {
        $(`#${tabla}`).DataTable(
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
                //    'excel', 'pdf', 'print'
                //],
                scrollY: '60vh',
                scrollCollapse: true,
                paging: true
            });
    }

    

}

// SET DATATABLE SIMPLE
function setDataTableSimple(tabla,scroolx = false,dato = false) {
    var tbl = $(`#table${tabla}`).DataTable();
    tbl.destroy();

    if (dato) {
        $(`#tbody${tabla}`).html(dato);
    }

    var objeto = {};
    objeto.language = { 'url': "/Libs/datatables/Spanish.json" };
    objeto.ordering = false;

    objeto.scrollX = scroolx;
    //objeto.processing = true;

    //if (minimo) {
        objeto.lengthMenu = [[5, 10, 20, -1], [5, 10, 20, 'Todos']];
    //}

    //if (exportar) {
    //    objeto.dom = 'Bfrtip';
    //    objeto.buttons = ['excel', 'pdf', 'print'];
    //}

    $(`#table${tabla}`).DataTable(objeto);
    


}

// ARMAR DATABLA NEW
function ArmarDataTable_New(tabla, dato, ordenar = false, minimo = true, exportar = false, scroolx = false) {

    // REFRESCAMOS
    var tbl = $(`#table${tabla}`).DataTable();
    tbl.destroy();



    // LLENAMOS DATOS
    $(`#tbody${tabla}`).html(dato);


    var objeto = {};
    objeto.language = { 'url': "/Libs/datatables/Spanish.json" };
    objeto.ordering = ordenar;

    objeto.scrollX = scroolx;

    if (minimo) {
        objeto.lengthMenu = [[5, 10, 20, -1], [5, 10, 20, 'Todos']];
    }

    if (exportar) {
        objeto.dom = 'Bfrtip';
        objeto.buttons = ['excel', 'pdf', 'print'];
    }

    $(`#table${tabla}`).DataTable(objeto);


}