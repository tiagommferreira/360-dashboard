

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



function topProdutosAjax() {
    return $.ajax({
        url: $("#encomendas-chart").data("url")
    });
}

topClientsChart();

var myDoughnutChart;

var ctx = $("#encomendas-chart").get(0).getContext("2d");
function topClientsChart() {
    $.when(topProdutosAjax()).done(function (dataEncomendas) {
        var orders = dataEncomendas;
        var topClientesData = [];

        var i = 0;
        for (; i < orders.length; i++) {
            topClientesData.push({
                value: orders[i].Total.toFixed(2),
                color: donutColors[i % donutColors.length],
                highlight: donutColors[i % donutColors.length],
                label: orders[i].Cliente
            });
        }

        if (myDoughnutChart != null)
            myDoughnutChart.destroy();
        myDoughnutChart = new Chart(ctx).Doughnut(topClientesData, optionsDonutChart);

    });
}