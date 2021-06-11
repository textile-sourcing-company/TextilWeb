//ARMA GRAFICO INDICADOR
function setGrafico(chartname, contenedorchart, valorprogramado, valortizado, valortendido, valorcortado, valorlabels, tituloleyenda = "", mostrarleyenda = false) {

    // VALORES PORCENTAJE
    let valorporcentaje = [];

    //QUITAMOS CANVAS
    $(`#${chartname}`).remove();
    $(`#${contenedorchart}`).append(`<canvas id='${chartname}'></canvas>`);

    var ctx = document.getElementById(chartname).getContext('2d');
    //ctx.height = 500;
    let MAX = Math.max(...valorprogramado);
    let PORCENTAJE = Math.round(MAX * 0.30);
    MAX = MAX + PORCENTAJE;
    let STEP = MAX >= 1000 ? 200 : 100;
    let nuevomax = MAX % STEP;
    nuevomax = STEP - nuevomax;
    nuevomax = nuevomax == STEP ? 0 : nuevomax;
    MAX += nuevomax;

    // OBTENEMOS VALOR DE PORCENTAJE
    for (let i = 0; i < valorprogramado.length; i++) {
        let valor = Math.round((valorcortado[i] / valorprogramado[i]) * 100) ;
        valorporcentaje.push(valor);
    }

    // CREAMOS CHART
    var mixedChart = new Chart(ctx, {
        type: 'bar',
        data: {
            datasets: [
                {
                    label: 'Cumplimiento de fichas programadas',
                    data: valorprogramado,
                    order: 1,
                    backgroundColor: '#ffc600',
                    borderColor: '#ffc600',
                },
                {
                    label: 'Fichas ingresadas en la plataforma - Tizado',
                    data: valortizado,
                    order: 1,
                    backgroundColor: '#597fcb',
                    borderColor: '#597fcb',
                },
                {
                    label: 'Fichas Liquidadas (Online) - Tendido',
                    data: valortendido,
                    order: 1,
                    backgroundColor: '#ee7420',
                    borderColor: '#ee7420',
                },
                {
                    label: 'Fichas Liquidadas (Online) - Cortado',
                    data: valorcortado,
                    order: 1,
                    backgroundColor: '#70ad47',
                    borderColor: '#70ad47',
                },
                {
                    label: '% Cumplimiento de Liquidación - Corte ',
                    data: valorporcentaje,
                    type: 'line',
                    fill: false,
                    backgroundColor: 'red',
                    borderColor: 'red',
                    yAxisID: 'B',
                    lineTension: 0,   
                }],
            labels: valorlabels
        },
        options: {
            maintainAspectRatio: false,
            //responsive: true,
            legend: {
                display:  mostrarleyenda,
                position: 'bottom',
            },
            title: {
                display: mostrarleyenda,
                text: tituloleyenda
            },
            scales: {
                yAxes: [
                    {
                        id: 'A',
                        type: 'linear',
                        position: 'left',
                        ticks: {
                            min: 0,
                            max: MAX,
                            stepSize: STEP
                        },
                        gridLines: {
                            display: false
                        }
                    },
                    {
                        id: 'B',
                        type: 'linear',
                        position: 'right',
                        ticks: {
                            min: -70,
                            max: 100,
                            stepSize: 10,
                            display:false
                        },
                        gridLines: {
                            display: false
                        }
                    },
                ]
            },
            tooltips: {
                enabled: false,
            },
            animation: {
                duration: 1,
                onComplete : function () {
                    var ctx = this.chart.ctx;
                    ctx.font = Chart.helpers.fontString(Chart.defaults.global.defaultFontFamily, 'normal', Chart.defaults.global.defaultFontFamily);
                    ctx.fillStyle = "black";
                    ctx.textAlign = 'center';
                    ctx.textBaseline = 'bottom';

                    this.data.datasets.forEach(function (dataset) {

                        for (var i = 0; i < dataset.data.length; i++) {
                            for (var key in dataset._meta) {
                                var model = dataset._meta[key].data[i]._model;
                                var valor = 0;
                                if (dataset.type == "line") {
                                    valor = dataset.data[i] + "%";
                                } else {
                                    valor = dataset.data[i];
                                }

                                ctx.fillText(valor, model.x, model.y - 5);
                            }
                        }


                    });
                }
            }
        }
    });




}