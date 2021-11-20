//--Cuentas - Viewmodel Knockout

var SingleCuentaViewModel = function (cuenta) {
    var model = ko.mapping.fromJS(cuenta);

    model.FormattedBalance = ko.computed(function () {
        return formatCurrency(model.Balance);
    });

    return model;
}

function cuentaViewModel() {
    var self = this;
    self.cuentas = ko.observableArray();
    self.cuenta = ko.observable();
    self.tipocuentas = ko.observableArray();
    self.monedas = ko.observableArray();

    

    //--get list------
    self.getCuentas = function () {
        //hack to get i.e to work :pass in ?breakcache='+Math.random() 
        //as querystring --otherwise i,e responds with 304:nothings changed
        //$.getJSON('Cuentas/GetList' + '?breakcache=' + Math.random(), function (data) {
        $.postJSON(appURL + 'Cuentas/GetList', function (data) {
            //debugger;
            //ko.mapping.fromJS(data.Records, {}, self.cuentas);
            self.cuentas($.map(data.Records, SingleCuentaViewModel));
        });
        
    };

    //--get list Tipos Cuentas
    self.getTipoCuentas = function () {
        $.postJSON(appURL + 'cuentas/GetTipoCuentaList', function (data) {
            ko.mapping.fromJS(data.Records, {}, self.tipocuentas);
        });
    };

    //--get list Monedas
    self.getMonedas = function () {
        $.postJSON(appURL + 'Monedas/GetList', function (data) {
            ko.mapping.fromJS(data.Records, {}, self.monedas);
        });
    };

    //New blank record
    self.newrecord = function () {
        var newrecord = new SingleCuentaViewModel(ko.mapping.fromJS(newItemTemplate));
        self.cuenta(newrecord);
        self.showDialog(newrecord, "Nuevo");
    };

    //Edit record
    self.getdetails = function (_record) {
        self.cuenta(_record);
        self.showDialog(_record, "Editar");
    };

    //Close record
    self.closeDetails = function (_record) {
        self.cuenta(undefined);
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
                    $.post(appURL + 'Cuentas/DeleteEntity', jsonData, function (returnedData) {
                        // This callback is executed if the post was successful     
                    })
                    self.cuentas.remove(_record);
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
        if (_record.CuentaId() == 0)
            isNew = true;

        $.ajax({
            type: 'POST',
            //url: cuentaURL + '/SaveEntity',
            url: appURL + 'api/cuentasapi/post',
            data: jsonData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (returnedData) {
                //debugger;
                if (isNew) {
                    _record.CuentaId(returnedData.CuentaId);
                    self.cuentas.push(_record);
                }
                //else {
                //    self.replaceIt(_record);
                //}
                self.closeDetails();
                //self.refresh();
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
        self.cuentas([]);
        self.getCuentas();
    }

    //Find By Id
    self.objectById = function (id) {
        var match = ko.utils.arrayFirst(self.cuentas(), function (item) {
            return item.CuentaId() === id;  //note the ()
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
            title: pretitulo + " cuenta",
            closeOnEscape: true,
            buttons: {
                "Salvar": function() {
                    self.save(_record);
                    $(this).dialog("close");
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

    self.replaceIt = function (_record) {
        var idx = self.cuentas.indexOf(_record);
        debugger;
        self.cuentas.replace(self.cuentas()[idx], {
            cuenta: _record
        });
    };

    //debugger;
    //Init
    self.getCuentas();
    self.getTipoCuentas();
    self.getMonedas();
}

ko.bindingHandlers.money = {
    update: function (element, valueAccessor, allBindingsAccessor) {
        var value = valueAccessor(), allBindings = allBindingsAccessor();
        var valueUnwrapped = ko.utils.unwrapObservable(value);

        var m = "";
        if (valueUnwrapped) {
            m = parseInt(valueUnwrapped);
            if (m) {
                m = String.format("{0:n0}", m);
            }
        }
        $(element).text(m);
    }
};

ko.bindingHandlers.numericText = {
    update: function (element, valueAccessor, allBindingsAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        var positions = ko.utils.unwrapObservable(allBindingsAccessor().positions) || ko.bindingHandlers.numericText.defaultPositions;
        var formattedValue = value.toFixed(positions);
        var finalFormatted = ko.bindingHandlers.numericText.withCommas(formattedValue); 

        ko.bindingHandlers.text.update(element, function () { return finalFormatted; });
    },
    defaultPrecision: 2,

    withCommas: function(original){
       original+= '';
     x = original.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
     
    } 
};

formatCurrency = function (value) {
    //debugger;
    if (value != undefined) {
    try{
        return "$" + withCommas(value().toFixed(2));
        }
        catch(e){
            return "$" + withCommas(parseFloat(value()).toFixed(2));
        }
    }
}
