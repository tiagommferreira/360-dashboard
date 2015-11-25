
var donutColors = ["#47D147", "#F7464A"];
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


var barChartOptions = {
    //Boolean - Whether the scale should start at zero, or an order of magnitude down from the lowest value
    scaleBeginAtZero: true,

    //Boolean - Whether grid lines are shown across the chart
    scaleShowGridLines: true,

    //String - Colour of the grid lines
    scaleGridLineColor: "rgba(0,0,0,.05)",

    //Number - Width of the grid lines
    scaleGridLineWidth: 1,

    //Boolean - Whether to show horizontal lines (except X axis)
    scaleShowHorizontalLines: true,

    //Boolean - Whether to show vertical lines (except Y axis)
    scaleShowVerticalLines: true,

    //Boolean - If there is a stroke on each bar
    barShowStroke: true,

    //Number - Pixel width of the bar stroke
    barStrokeWidth: 2,

    //Number - Spacing between each of the X value sets
    barValueSpacing: 5,

    //Number - Spacing between data sets within X values
    barDatasetSpacing: 1,

    //String - A legend template
    legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].fillColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>"

};

var ctx = $("#products-stock-chart").get(0).getContext("2d");
var bctx = $("#products-out_of_stock-chart").get(0).getContext("2d");
var produtosStockChart;
var outOfStockChart;


function produtosStockAjax(outOfStock, url) {
    return $.ajax({
        url: url,
        data: { out_of_stock: outOfStock }
    });
}

produtosStockChart();
outOfStockBarChart();

function produtosStockChart() {
    $.when(produtosStockAjax("False", $("#products-stock-chart").data("url")),
        produtosStockAjax("true", $("#products-stock-chart").data("url"))).done(function (dataInStockProdutos, dataOutStockProdutos) {
            var produtosInStock = [];
            var produtosOutStock = [];

            produtosInStock = dataInStockProdutos[0];
            produtosOutStock = dataOutStockProdutos[0];

            var inStockData = 0;
            for (i = 0; i < produtosInStock.length; i++) {
                inStockData = inStockData + parseInt(produtosInStock[i].StockAtual);
            }


            var outStockData = 0;
            for (i = 0; i < produtosOutStock.length; i++) {
                outStockData = outStockData + parseInt(produtosOutStock[i].StockAtual);
            }

            outStockData = -outStockData;

            var produtosData = [
                {
                    value: inStockData,
                    color: donutColors[0],
                    highlight: donutColors[0],
                    label: "In Stock"
                },{
                    value: outStockData,
                    color: donutColors[1],
                    highlight: donutColors[1],
                    label: "Out of Stock"
                },
            ];
            
            outOfStockChart = new Chart(ctx).Doughnut(produtosData, optionsDonutChart);
    });
}


function outOfStockBarChart() {
    $.when(produtosStockAjax("true", $("#products-out_of_stock-chart").data("url"))).done(function (dataOutStockProdutos) {
            var produtosOutStock = [];
            produtosOutStock = dataOutStockProdutos;

            console.log(produtosOutStock);
            var outStockData = [];
            var outStockLabels = [];
            for (i = 0; i < produtosOutStock.length && i < 10; i++) {
                outStockData.push(-parseInt(produtosOutStock[i].StockAtual));
                outStockLabels.push(produtosOutStock[i].CodProduto);
            }

            var data = {
                labels: outStockLabels,
                datasets: [
                    {
                        label: "Out of Stock Products",
                        fillColor: "rgba(255,10,20,0.5)",
                        strokeColor: "rgba(220,220,220,0.8)",
                        highlightFill: "rgba(220,220,220,0.75)",
                        highlightStroke: "rgba(220,220,220,1)",
                        data: outStockData
                    }
                ]
            };

            outOfStockChart = new Chart(bctx).Bar(data, barChartOptions);
        });
}

