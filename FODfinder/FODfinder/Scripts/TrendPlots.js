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

function ajaxCall6() {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/HighRiskLabelledIngredients/GetGenderChart1',
        success: successAjax6,
        error: errorAjax
    })
};

function successAjax6(data) {
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

    var chart = document.getElementById('chartGenderIngredientsFemale');
    Plotly.newPlot(chart, data, layout);
}

function ajaxCall7() {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/HighRiskLabelledIngredients/GetGenderChart2',
        success: successAjax7,
        error: errorAjax
    })
};

function successAjax7(data) {
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

    var chart = document.getElementById('chartGenderIngredientsMale');
    Plotly.newPlot(chart, data, layout);
}

function ajaxCall8() {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/HighRiskLabelledIngredients/GetGenderChart3',
        success: successAjax8,
        error: errorAjax
    })
};

function successAjax8(data) {
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

    var chart = document.getElementById('chartGenderIngredientsNonbinary');
    Plotly.newPlot(chart, data, layout);
}

function ajaxCall9() {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/HighRiskLabelledIngredients/GetEthnicityChart1',
        success: successAjax9,
        error: errorAjax
    })
};

function successAjax9(data) {
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

    var chart = document.getElementById('chartEthnicityBlack');
    Plotly.newPlot(chart, data, layout);
}

function ajaxCall10() {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/HighRiskLabelledIngredients/GetEthnicityChart2',
        success: successAjax10,
        error: errorAjax
    })
};

function successAjax10(data) {
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

    var chart = document.getElementById('chartEthnicityNativeAmerican');
    Plotly.newPlot(chart, data, layout);
}

function ajaxCall11() {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/HighRiskLabelledIngredients/GetEthnicityChart3',
        success: successAjax11,
        error: errorAjax
    })
};

function successAjax11(data) {
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

    var chart = document.getElementById('chartEthnicityAsian');
    Plotly.newPlot(chart, data, layout);
}

function ajaxCall12() {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/HighRiskLabelledIngredients/GetEthnicityChart4',
        success: successAjax12,
        error: errorAjax
    })
};

function successAjax12(data) {
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

    var chart = document.getElementById('chartEthnicityPacific');
    Plotly.newPlot(chart, data, layout);
}

function ajaxCall13() {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/HighRiskLabelledIngredients/GetEthnicityChart5',
        success: successAjax13,
        error: errorAjax
    })
};

function successAjax13(data) {
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

    var chart = document.getElementById('chartEthnicityLatino');
    Plotly.newPlot(chart, data, layout);
}

function ajaxCall14() {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/HighRiskLabelledIngredients/GetEthnicityChart6',
        success: successAjax14,
        error: errorAjax
    })
};

function successAjax14(data) {
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

    var chart = document.getElementById('chartEthnicityWhite');
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
ajaxCall6();
ajaxCall7();
ajaxCall8();
ajaxCall9();
ajaxCall10();
ajaxCall11();
ajaxCall12();
ajaxCall13();
ajaxCall14();