
var optionsLineChart = {

    ///Boolean - Whether grid lines are shown across the chart
    scaleShowGridLines: true,

    //String - Colour of the grid lines
    scaleGridLineColor: "rgba(0,0,0,.05)",

    //Number - Width of the grid lines
    scaleGridLineWidth: 1,

    //Boolean - Whether to show horizontal lines (except X axis)
    scaleShowHorizontalLines: true,

    //Boolean - Whether to show vertical lines (except Y axis)
    scaleShowVerticalLines: true,

    //Boolean - Whether the line is curved between points
    bezierCurve: true,

    //Number - Tension of the bezier curve between points
    bezierCurveTension: 0.4,

    //Boolean - Whether to show a dot for each point
    pointDot: true,

    //Number - Radius of each point dot in pixels
    pointDotRadius: 4,

    //Number - Pixel width of point dot stroke
    pointDotStrokeWidth: 1,

    //Number - amount extra to add to the radius to cater for hit detection outside the drawn point
    pointHitDetectionRadius: 20,

    //Boolean - Whether to show a stroke for datasets
    datasetStroke: true,

    //Number - Pixel width of dataset stroke
    datasetStrokeWidth: 2,

    //Boolean - Whether to fill the dataset with a colour
    datasetFill: true,

    //String - A legend template
    legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].strokeColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>"

};

var donutColors = ["#F7464A", "#4D88FF", "#47D147"];
var optionsDonutChart = {
    //Boolean - Whether we should show a stroke on each segment
    segmentShowStroke: true,

    //String - The colour of each segment stroke
    segmentStrokeColor: "#fff",

    //Number - The width of each segment stroke
    segmentStrokeWidth: 2,

    //Number - The percentage of the chart that we cut out of the middle
    percentageInnerCutout: 50, // This is 0 for Pie charts

    //Number - Amount of animation steps
    animationSteps: 100,

    //String - Animation easing effect
    animationEasing: "easeOutBounce",

    //Boolean - Whether we animate the rotation of the Doughnut
    animateRotate: true,

    //Boolean - Whether we animate scaling the Doughnut from the centre
    animateScale: false,

    //String - A legend template
    legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++){%><li><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>"

};


var monthNames = ["January", "February", "March", "April", "May", "June",
  "July", "August", "September", "October", "November", "December"
];

var lctx = $("#transactions-chart").get(0).getContext("2d");
var ctx = $("#top-products-chart").get(0).getContext("2d");
var myLineChart;
var myDoughnutChart;

$("#transacoes-ano").change(function () {
    transactionsChart($("#transacoes-ano").val());
});
transactionsChart($("#transacoes-ano").val());

function vendasAjax(ano, url) {
    return $.ajax({
        url: url,
        data: { year: ano }
    });
}
function comprasAjax(ano, url) {
    return $.ajax({
        url: url,
        data: { year: ano }
    });
}

function getValorDoMes(array, mes) {
    var i = 0;
    for (; i < array.length; i++) {
        if (array[i].Mes == mes)
            return parseFloat(array[i].Valor)
    }
    return 0;
}

function transactionsChart(ano) {
    $.when(vendasAjax(ano, $("#transactions-chart").data("vendasurl")), comprasAjax(ano,$("#transactions-chart").data("comprasurl"))).done(function (dataVendas, dataCompras) {
        var vendas = [];
        var compras = [];
        vendas = dataVendas[0];
        compras = dataCompras[0];

        var vendasData = [];
        var vendasLabels = [];
        var counter = 1;

        var comprasData = [];
        var comprasLabels = [];

        for (counter = 1; counter < 13; counter++) {
            vendasLabels.push(monthNames[counter - 1]);
            vendasData.push(getValorDoMes(vendas, counter));
        }
        for (counter = 1; counter < 13; counter++) {
            comprasLabels.push(monthNames[counter - 1]);
            comprasData.push(Math.abs(getValorDoMes(compras, counter)));
        }

        var transactionsDataLine = {
            labels: comprasLabels,
            datasets: [
                {
                    label: "Vendas",
                    fillColor: "rgba(152,230,0,0.2)",
                    strokeColor: "rgba(152,230,0,1)",
                    pointColor: "rgba(152,230,0,1)",
                    pointStrokeColor: "#fff",
                    pointHighlightFill: "#fff",
                    pointHighlightStroke: "rgba(152,230,0,1)",
                    data: vendasData
                },
                 {
                     label: "Compras",
                     fillColor: "rgba(151,187,205,0.2)",
                     strokeColor: "rgba(151,187,205,1)",
                     pointColor: "rgba(151,187,205,1)",
                     pointStrokeColor: "#fff",
                     pointHighlightFill: "#fff",
                     pointHighlightStroke: "rgba(151,187,205,1)",
                     data: comprasData
                 }
            ]
        };
        if (myLineChart != null)
            myLineChart.destroy();
        myLineChart = new Chart(lctx).Line(transactionsDataLine, optionsLineChart);
    });


}

function topProductsAjax() {
    return $.ajax({
        url: $("#top-products-chart").data("url"),
        data: { required: 3 }
    });
}

function topProductsChart() {
    $.when(topProductsAjax()).done(function (dataProducts) {
        var products = dataProducts;
        var topProdutosData = [];

        var i = 0;
        for (; i < products.length; i++) {
            topProdutosData.push({
                value: products[i].Quantidade,
                color: donutColors[i % donutColors.length],
                highlight: donutColors[i % donutColors.length],
                label: products[i].ArtigoDescricao
            });
        }

        if (myDoughnutChart != null)
            myDoughnutChart.destroy();
        myDoughnutChart = new Chart(ctx).Doughnut(topProdutosData, optionsDonutChart);

    });
}

topProductsChart();
