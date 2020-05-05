function ajaxCall() {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/HighRiskLabelledIngredients/GetIngredientsForPlot',
        success: successAjax,
        error: errorAjax
    })
};

function successAjax(data) {
    var xValues = [];
    var yValues = [];
    for(i = data.length - 1; i >= 0; i--) {
        xValues.push(data[i]['countOfLabelOccurences']);
        yValues.push(data[i]['ingredientName']);
    }

    var data = [{
        type: 'bar',
        marker: { color: 'CCBDBD' },
        x: xValues,
        y: yValues,
        orientation: 'h'
    }];

    var layout = {
        plot_bgcolor: '#F5EEEB',
        paper_bgcolor: '#F5EEEB',
        yaxis: { fixedrange: true },
        xaxis: { fixedrange: true, dtick: 1 },
        hovermode: 'closest'
    }

    var chart = document.getElementById('chart');
    Plotly.newPlot(chart, data, layout);
}

function errorAjax() {
    console.log("Error")
}

ajaxCall();