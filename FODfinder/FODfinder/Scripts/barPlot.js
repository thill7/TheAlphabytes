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
    var data = [{
        type: 'bar',
        x: [data[0]['countOfLabelOccurences'], data[1]['countOfLabelOccurences'], data[2]['countOfLabelOccurences']],
        y: [data[0]['ingredientName'], data[1]['ingredientName'], data[2]['ingredientName']],
        orientation: 'h'
    }];

    var layout = {
        plot_bgcolor: '#F5EEEB',
        paper_bgcolor: '#F5EEEB'
    }

    var chart = document.getElementById('chart');
    Plotly.newPlot(chart, data, layout);
}

function errorAjax() {
    console.log("Error")
}

ajaxCall();