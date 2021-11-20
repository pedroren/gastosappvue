//--TipoCuentas - Viewmodel Knockout
function tipocuentaViewModel() {
    var self = this;
    self.tipocuentas = ko.observableArray();
    self.tipocuenta = ko.observable();

    //--get list------
    self.getTipoCuentas = function () {
        //hack to get i.e to work :pass in ?breakcache='+Math.random() 
        //as querystring --otherwise i,e responds with 304:nothings changed
        //$.getJSON('TipoCuentas/GetList' + '?breakcache=' + Math.random(), function (data) {
        $.getJSON(tipocuentaURL+'/GetList',  function (data) {
            //debugger;
            //self.tipocuentas(ko.mapping.fromJS(data.Records));
            //self.tipocuentas = ko.mapping.fromJS(data.Records);
            ko.mapping.fromJS(data.Records, {}, self.tipocuentas);
        });
    };

    //New blank record
    self.newrecord = function () {
        var newrecord = ko.mapping.fromJS(newItemTemplate);
        self.tipocuenta(newrecord);
        self.showDialog(self.tipocuenta, "Nuevo");
    };

    //Edit record
    self.getdetails = function (_record) {
        self.tipocuenta(_record);
        self.showDialog(_record, "Editar");
    };

    //Close record
    self.closeDetails = function (_record) {
        self.tipocuenta(undefined);
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
                    $.post(tipocuentaURL + '/DeleteEntity', jsonData, function (returnedData) {
                        // This callback is executed if the post was successful     
                    })
                    self.tipocuentas.remove(_record);
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
        if (_record.TipoCuentaId() == 0)
            isNew = true;

        $.ajax({
            type: 'POST',
            //url: tipocuentaURL + '/SaveEntity',
            url: appURL + 'api/tipocuentasapi',
            data: jsonData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (returnedData) {
                //debugger;
                if (isNew) {
                    _record.TipoCuentaId(returnedData.TipoCuentaId);
                    self.tipocuentas.push(_record);
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
        self.tipocuentas([]);
        self.getTipoCuentas();
    }

    //Find By Id
    self.objectById = function (id) {
        var match = ko.utils.arrayFirst(self.tipocuentas(), function (item) {
            return item.TipoCuentaId() === id;  //note the ()
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
            title: pretitulo + " tipocuenta",
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
    self.getTipoCuentas();
}

