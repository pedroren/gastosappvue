var graphticks = [];

function parseTableData(ptable, pvaluetype){


    var rowHeader = [];
    var rowData = [];
    var includeData = [];
    var cantcolmax = 1;

    //Recorre Filas
    ptable.find('tbody tr').each(function (i) {

        //Busca label fila
        $(this).find('th').each(function () {
            var thisValue = $(this).text();
            rowHeader.push(thisValue);
        });

        //Busca celdas datos
        var cantcol = 1;
        $(this).find('td').each(function () {
            if (cantcol++ <= cantcolmax) {
                if ($.trim($(this).text()).length)
                    var thisValue = parseFloat($(this).text().replace("$", "").replace("(", "-").replace(")", "").replace(",", ""));
                else
                    var thisValue = 0;
                if ((pvaluetype < 0 && thisValue <= 0) || (pvaluetype > 0 && thisValue >= 0))
                    includeData.push(true);
                else
                    includeData.push(false);
                rowData.push(thisValue);

            }
        });

    });

var data = [];
for (var i = 0; i < rowHeader.length; i++) {
    if(includeData[i])
        data[i] = { label: rowHeader[i], data: rowData[i] }
}
return data;
}

function parseTableDataSeries(ptable, conceptos) {
    debugger;
    var tableData = {};

    //define xLabels array
    tableData.xLabels = [];
    graphticks = [];
    //loop through each th element in the table's thead
    ptable.find('thead th').each(function (i) {
        //push each th's text value into the xLabels array
        if (i != 0) {
            var html = cleanCellString($(this).html());
            var aniomes = parseInt(html.split("-")[0]) * 12 + parseInt(html.split("-")[1]);
            //tableData.xLabels.push(html);
            //tableData.xLabels.push(parseInt(html.replace("-", "")));
            tableData.xLabels.push(aniomes);
            graphticks.push([aniomes, html]);
        }
    });


    //define legend array
    tableData.legend = [];
    //loop through each th in the table's tbody
    ptable.find('tbody th').each(function () {
        //push each th's text value into the legend array
        if (jQuery.inArray($(this).html().trim(),conceptos) > -1)
            tableData.legend.push(cleanCellString($(this).html()));
        else
            tableData.legend.push(null);
    });

    //define dataGroups array
    tableData.dataGroups = [];
    //loop through each tr in the tbody
    ptable.find('tbody tr').each(function (i) {
        //define next item in dataGroups as a new array
        tableData.dataGroups[i] = [];
        //loop through each td in this tr
        $(this).find('td').each(function (x) {
            //convert the td text into a number 
            var tdNumberValue = parseFloat($(this).text().replace("$", "").replace("(", "-").replace(")", "").replace(",", ""));
            //add the value to the dataGroups[i] array
            //if ((pvaluetype < 0 && tdNumberValue <= 0) || (pvaluetype > 0 && tdNumberValue >= 0))
                tableData.dataGroups[i].push([tableData.xLabels[x], tdNumberValue]);
            //else
            //    tableData.dataGroups[i].push([tableData.xLabels[x], 0]);
            
        });
    });


    var data = [];
    var d = 0;
    for (var i = 0; i < tableData.legend.length; i++) {
        if (tableData.legend[i]) {
            data[d] = { label: tableData.legend[i], data: tableData.dataGroups[i] };
            d++;
        }
        
    }
    return data;
}

function showChartTooltip(x, y, contents) {
    $('<div id="charttooltip">' + contents + '</div>').css({
        position: 'absolute',
        display: 'none',
        top: y + 5,
        left: x + 5,
        border: '1px solid #bfbfbf',
        padding: '2px',
        'background-color': '#ffffff',
        opacity: 1
    }).appendTo("body").fadeIn(200);
}

function pieHover(event, pos, item) {
    $("#x").text(pos.pageX);
    $("#y").text(pos.pageY);
    if (item) {
        $("#charttooltip").remove();
        var x = item.datapoint[0].toFixed(2),
        y = item.datapoint[1];
        showChartTooltip(pos.pageX, pos.pageY, item.series.label+"("+y[0][1]+")");
    } else {
        $("#charttooltip").remove();
    }
};

function lineHover(event, pos, item) {
    $("#x").text(pos.pageX);
    $("#y").text(pos.pageY);
    if (item) {
        $("#charttooltip").remove();
        var x = item.datapoint[0].toFixed(2),
        y = item.datapoint[1];
        showChartTooltip(pos.pageX, pos.pageY, item.series.label + "(" + y + ")");
    } else {
        $("#charttooltip").remove();
    }
};