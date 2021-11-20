    ko.bindingHandlers.date = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
    
            var jsonDate = valueAccessor();
            var value = new Date(parseInt(jsonDate().substr(6)));
            var ret = value.getMonth() + 1 + "/" + value.getDate() + "/" + value.getFullYear();
            $(element).val(ret);
            var value2 = valueAccessor();
            value2($(element).val());
            $(element).blur(function() {
                var value = valueAccessor();
                value($(element).val());
            }); 
        },
        update: function(element, valueAccessor, allBindingsAccessor, viewModel) {
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



    var viewModel =  {
        self : this,
        listaTags: ko.mapping.fromJS(initialData),
        listaConcepto : ko.observableArray(supportData1)

    }
    viewModel.addConceptoItem = function () 
    {
        this.listaTags.push(ko.mapping.fromJS(newItemTemplate));
};
viewModel.removeConceptoItem = function (conceptoItem) 
{
    this.listaTags.remove(conceptoItem);
};
viewModel.save = function () 
{
       
    ko.utils.postJson(postURL, { listaTags: ko.mapping.toJS(viewModel.listaTags) });
};


$("form").validate({ submitHandler: function () { viewModel.save() } });

ko.applyBindings(viewModel);