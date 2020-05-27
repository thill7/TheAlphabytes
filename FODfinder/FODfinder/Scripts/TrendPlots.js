function ajaxCall1() {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/HighRiskLabelledIngredients/GetAgeChart1',
        success: successAjax1,
        error: errorAjax
    })
};

function successAjax1(data) {
    var xValues = [];
    var yValues = [];
    var textValues = [];
    for (i = data.length - 1; i >= 0; i--) {
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

    var chart = document.getElementById('chartAgeIngredientsGroup1');
    Plotly.newPlot(chart, data, layout);
}

function ajaxCall2() {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/HighRiskLabelledIngredients/GetAgeChart2',
        success: successAjax2,
        error: errorAjax
    })
};

function successAjax2(data) {
    var xValues = [];
    var yValues = [];
    var textValues = [];
    for (i = data.length - 1; i >= 0; i--) {
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

    var chart = document.getElementById('chartAgeIngredientsGroup2');
    Plotly.newPlot(chart, data, layout);
}

function ajaxCall3() {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/HighRiskLabelledIngredients/GetAgeChart3',
        success: successAjax3,
        error: errorAjax
    })
};

function successAjax3(data) {
    var xValues = [];
    var yValues = [];
    var textValues = [];
    for (i = data.length - 1; i >= 0; i--) {
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

    var chart = document.getElementById('chartAgeIngredientsGroup3');
    Plotly.newPlot(chart, data, layout);
}

function ajaxCall4() {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/HighRiskLabelledIngredients/GetAgeChart4',
        success: successAjax4,
        error: errorAjax
    })
};

function successAjax4(data) {
    var xValues = [];
    var yValues = [];
    var textValues = [];
    for (i = data.length - 1; i >= 0; i--) {
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

    var chart = document.getElementById('chartAgeIngredientsGroup4');
    Plotly.newPlot(chart, data, layout);
}

function ajaxCall5() {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/HighRiskLabelledIngredients/GetAgeChart5',
        success: successAjax5,
        error: errorAjax
    })
};

function successAjax5(data) {
    var xValues = [];
    var yValues = [];
    var textValues = [];
    for (i = data.length - 1; i >= 0; i--) {
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

    var chart = document.getElementById('chartAgeIngredientsGroup5');
    Plotly.newPlot(chart, data, layout);
}

function errorAjax() {
    console.log("Error")
}

ajaxCall1();
ajaxCall2();
ajaxCall3();
ajaxCall4();
ajaxCall5();