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
    var textValues = [];
    for(i = data.length - 1; i >= 0; i--) {
        xValues.push(data[i]['countOfLabelOccurences']);
        yValues.push(data[i]['ingredientName']);
        textValues.push(data[i]['ingredientName']);
    }

    var data = [{
        type: 'bar',
        marker: { color: 'CCBDBD' },
        x: xValues,
        y: yValues,
        text: textValues,
        textposition: 'auto',
        orientation: 'h'
    }];

    var layout = {
        plot_bgcolor: '#F5EEEB',
        paper_bgcolor: '#F5EEEB',
        yaxis: { fixedrange: true, dtick: 1, visible: false },
        xaxis: { fixedrange: true, dtick: 1 },
        margin: { l: 130, t: 70, b: 70 }
    }

    var chart = document.getElementById('chart');
    Plotly.newPlot(chart, data, layout);
}

function errorAjax() {
    console.log("Error")
}

ajaxCall();