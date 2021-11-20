//window.gastosApp = window.gastosApp || {};
//window.gastosApp.importViewModel = (function (ko) {
    ko.bindingHandlers.date = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            //debugger;
            var jsonDate = valueAccessor();
            var value = new Date(parseInt(jsonDate().substr(6)));
            var ret = value.getMonth() + 1 + "/" + value.getDate() + "/" + value.getFullYear();
            $(element).val(ret);
            var value2 = valueAccessor();
            value2($(element).val());
            $(element).blur(function () {
                var value = valueAccessor();
                value($(element).val());
            });
        },
        update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            // First get the latest data that we're bound to
            var value = valueAccessor(), allBindings = allBindingsAccessor();

            // Next, whether or not the supplied model property is observable, get its current value
            var valueUnwrapped = ko.utils.unwrapObservable(value);
            //debugger;
            if ((valueUnwrapped === null) || (valueUnwrapped === undefined)) {
                valueUnwrapped = "";
            }
        }
    };

    //debugger;
    var viewModel =  {
        self : this,
        transaccion: ko.mapping.fromJS(initialData),
        listaNombres : ko.observableArray(["Fecha","Descripcion","Monto"])

    }

    viewModel.addConceptoItem = function () 
    {
        this.transaccion.Campos.push(ko.mapping.fromJS(nuevoCampoTemplate));
    };
    viewModel.removeConceptoItem = function (conceptoItem) 
    {
        this.transaccion.Campos.remove(conceptoItem);
    };
    viewModel.save = function () 
    {
        //debugger;
        ko.utils.postJson(location.href, { transImportMap: ko.mapping.toJS(viewModel.transaccion) });
    };

    ko.applyBindings(viewModel);
//    return viewModel;
//});

//ko.applyBindings(window.gastosApp.importViewModel);