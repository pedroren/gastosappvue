//--Conceptos - Viewmodel Knockout
var SingleConceptoViewModel = function (concepto) {
    var model = ko.mapping.fromJS(concepto);
    var _dummyObservable = ko.observable();

    model.NombrePadre = ko.computed(function () {
        //debugger;
        _dummyObservable(); //retrieve and ignore the value
        var idPadre = 0;

        try {
            if (model.ConceptoPadre.ConceptoId != null) {
                if (ko.isObservable(model.ConceptoPadre))
                    return model.ConceptoPadre().Nombre();
                else
                    return model.ConceptoPadre.Nombre();
            }
            else
                return "";
        } catch (err) {
            return ""
        }
    });

    model.forceUpdateComputed = function () {
        _dummyObservable.notifySubscribers(); //fake a change notification
    };

    return model;
}

function conceptoViewModel() {
    var self = this;
    self.conceptos = ko.observableArray();
    self.concepto = ko.observable();

    //--get list------
    self.getConceptos = function () {
        //hack to get i.e to work :pass in ?breakcache='+Math.random() 
        //as querystring --otherwise i,e responds with 304:nothings changed
        //$.getJSON('Conceptos/GetList' + '?breakcache=' + Math.random(), function (data) {
        $.getJSON(appURL+'Conceptos/GetList',  function (data) {
            //debugger;
            //self.conceptos(ko.mapping.fromJS(data.Records));
            //self.conceptos = ko.mapping.fromJS(data.Records);
            //ko.mapping.fromJS(data.Records, {}, self.conceptos);
			self.conceptos($.map(data.Records, SingleConceptoViewModel));
        });
    };

    //New blank record
    self.newrecord = function () {
        debugger;
        //var newrecord = ko.mapping.fromJS(newItemTemplate);
        //var newrecord = $.map(newItemTemplate, SingleConceptoViewModel);
        var newrecord = new SingleConceptoViewModel(ko.mapping.fromJS(newItemTemplate));
        self.concepto(newrecord);
        self.showDialog(newrecord, "Nuevo");
    };

    //Edit record
    self.getdetails = function (_record) {
        self.concepto(_record);
        self.showDialog(_record, "Editar");
    };

    //Close record
    self.closeDetails = function (_record) {
        self.concepto(undefined);
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
                    $.post(appURL+'Conceptos/DeleteEntity', jsonData, function (returnedData) {
                        // This callback is executed if the post was successful     
                    })
                    self.conceptos.remove(_record);
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
        debugger;
        var jsonData = ko.mapping.toJS(_record);
        //Si se asginó el Concepto Padre lo actualiza en el registro
        if (_record.ConceptoPadre != undefined) {
            if (_record.ConceptoPadre.ConceptoId != null && jsonData.ConceptoPadre == null) {
                _record.ConceptoPadre(self.objectById(_record.ConceptoPadre.ConceptoId));
                jsonData = ko.mapping.toJS(_record);
            }
        }
        //Si se eliminó el Concepto Padre lo actualiza en el item observable
        //if (_record.ConceptoPadre.ConceptoId === undefined && _record.ConceptoPadre.Nombre() != undefined) {
        //    _record.ConceptoPadre(undefined);
        //}

        jsonData = ko.toJSON(jsonData);

        var isNew = false;
        if (_record.ConceptoId() == 0)
            isNew = true;

        $.ajax({
            type: 'POST',
            //url: appURL+'Conceptos/SaveEntity',
            url: appURL + 'api/conceptosapi/post',
            data: jsonData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (returnedData) {
                debugger;
                if (isNew) {
                    _record.ConceptoId(returnedData.ConceptoId);
                    self.conceptos.push(_record);
                }
                else {
                    if (typeof _record.ConceptoPadre == "object")
                        _record.ConceptoPadre = ko.mapping.fromJS(returnedData.ConceptoPadre);
                    else
                        ko.mapping.fromJS(returnedData.ConceptoPadre, _record.ConceptoPadre); //??
                    _record.forceUpdateComputed();
                    //self.conceptos.replace(self.conceptos()[self.conceptos.indexOf(_record)], {
                    //    Concepto: _record
                    //});
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

    //Close detail & refresh list
    self.refresh = function () {
        self.conceptos([]);
        self.getConceptos();
    }

    //Find By Id
    self.objectById = function (id) {
        var match = ko.utils.arrayFirst(self.conceptos(), function (item) {
            return item.ConceptoId() === id;  //note the ()
        });
        if (!match)
            return null;
        else
            return match;
    };

    //Get Concepto Padre Nombre
    self.getNombrePadre = function (_record) {
        //debugger;
        var idPadre = 0;
        
        try{
            if(_record.ConceptoPadre.ConceptoId!=null){
                if(ko.isObservable(_record.ConceptoPadre.ConceptoId))
                    idPadre = _record.ConceptoPadre.ConceptoId();
                else
                    idPadre = _record.ConceptoPadre.ConceptoId;
                if(idPadre != undefined)
                    return self.objectById(idPadre).Nombre;
                else
                    return "";
            }
            else
                return "";
        }catch(err){
            return ""
        }
    };

    self.showDialog = function (_record, pretitulo) {
        $("#recordPanel").dialog({
            height: "auto",
            width: "auto",
            modal: true,
            title: pretitulo + " concepto",
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
    self.getConceptos();
}

