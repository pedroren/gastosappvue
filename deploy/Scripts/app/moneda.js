//--Monedas - Viewmodel Knockout
function monedaViewModel() {
    var self = this;
    self.monedas = ko.observableArray();
    self.moneda = ko.observable();

    //--get list------
    self.getMonedas = function () {
        //hack to get i.e to work :pass in ?breakcache='+Math.random() 
        //as querystring --otherwise i,e responds with 304:nothings changed
        //$.getJSON('Monedas/GetList' + '?breakcache=' + Math.random(), function (data) {
        $.postJSON(appURL+'Monedas/GetList',  function (data) {
            //debugger;
            //self.monedas(ko.mapping.fromJS(data.Records));
            //self.monedas = ko.mapping.fromJS(data.Records);
            ko.mapping.fromJS(data.Records, {}, self.monedas);
        });
    };

    //New blank record
    self.newrecord = function () {
        var newrecord = ko.mapping.fromJS(newItemTemplate);
        self.moneda(newrecord);
        self.showDialog(self.moneda, "Nuevo");
    };

    //Edit record
    self.getdetails = function (_record) {
        self.moneda(_record);
        self.showDialog(_record, "Editar");
    };

    //Close record
    self.closeDetails = function (_record) {
        self.moneda(undefined);
    };

    //Delete record
    self.remove = function (_record) {
        //debugger;
        $("#dialog-confirm").dialog({
            resizable: false,
            height: 140,
            modal: true,
            buttons: {
                "Borrar": function () {
                    $(this).dialog("close");
                    var jsonData = ko.mapping.toJS(_record);
                    $.post(appURL + 'Monedas/DeleteEntity', jsonData, function (returnedData) {
                        // This callback is executed if the post was successful     
                    })
                    self.monedas.remove(_record);
                    self.closeDetails();
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });
        
    };

    //Save record
    self.save = function (_record) {
        //debugger;
        var jsonData = ko.mapping.toJS(_record); 

        jsonData = ko.toJSON(jsonData); 

        var isNew = false;
        if (_record.MonedaId() == 0)
            isNew = true;

        $.ajax({
            type: 'POST',
            //url: monedaURL + '/SaveEntity',
            url: appURL + 'api/monedasapi/post',
            data: jsonData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (returnedData) {
                //debugger;
                if (isNew) {
                    _record.MonedaId(returnedData.MonedaId);
                    self.monedas.push(_record);
                }
                self.closeDetails();
            },
            error: function (error) {
                //debugger;
                jsonValue = jQuery.parseJSON(error.responseText);
                alert('Error: ' + jsonValue);
            }
        });
    };

    //Refresh list
    self.refresh = function () {
        self.monedas([]);
        self.getMonedas();
    }

    //Find By Id
    self.objectById = function (id) {
        var match = ko.utils.arrayFirst(self.monedas(), function (item) {
            return item.MonedaId() === id;  //note the ()
        });
        if (!match)
            return null;
        else
            return match;
    };

    self.showDialog = function (_record, pretitulo) {
        $("#recordPanel").dialog({
            height: "auto",
            width: "auto",
            modal: true,
            title: pretitulo + " moneda",
            closeOnEscape: true,
            buttons: {
                "Salvar": function() {
                    self.save(_record);
                    $( this ).dialog( "close" );
                
                 },
                Cancel: function() {
                    $( this ).dialog( "close" );
                }
            },
            close: function() {
                self.closeDetails();
                //allFields.val( "" ).removeClass( "ui-state-error" );
            }
    });
    }

    //debugger;
    //Init
    self.getMonedas();
}

