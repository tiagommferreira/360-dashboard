
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

var ctx = $("#products-stock-chart").get(0).getContext("2d");
var produtosStockChart;


function produtosStockAjax(outOfStock, url) {
    return $.ajax({
        url: url,
        data: { out_of_stock: outOfStock }
    });
}

produtosStockChart();


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
            
            produtosStockChart = new Chart(ctx).Doughnut(produtosData, optionsDonutChart);
    });
}


