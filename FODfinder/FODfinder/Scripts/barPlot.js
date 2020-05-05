var data = [{
    type: 'bar',
    x: [2, 10, 2],
    y: ['a ', 'b ', 'c '],
    orientation: 'h'
}];

var layout = {
    plot_bgcolor: "#F5EEEB",
    paper_bgcolor: "#F5EEEB"
}

var chart = document.getElementById('chart');
Plotly.newPlot(chart, data, layout);
