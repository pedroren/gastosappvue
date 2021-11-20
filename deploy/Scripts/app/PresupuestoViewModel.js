(function (root) {
    var breeze = root.breeze,
      ko = root.ko,
      app = root.app = root.app || {};
    Logger.show();        // show logger initially  
    log("Window is loaded.");

    // define Breeze namespace  
    //var entityModel = breeze.entityModel;

    // service name is route to the Web API controller  
    var serviceName = appURL + 'api/PresupuestosApiEF';

    // manager is the service gateway and cache holder  
    var manager = new breeze.EntityManager(serviceName);

    // define the viewmodel  
    var vm = {
        presupuestos: ko.observableArray(),
        save: saveChanges,
        editPresupuesto: editPresupuesto,
        deletePresupuesto: deletePresupuesto,
        newPresupuesto: ko.observable(  
        {  
            Nombre: "",
            FechaDesde: "",
            FechaHasta: ""
        }),  
        addPresupuesto: addNewPresupuesto
    };

    // start fetching Presupuestos  
    getPresupuestos();

    // bind view to the viewmodel  
    ko.applyBindings(vm);

    function getPresupuestos() {
        log("querying Presupuestos");
        var query = new breeze.EntityQuery.from("GetPresupuestos");
        return manager
          .executeQuery(query)
          .then(querySucceeded)
          .fail(queryFailed);

        // clear observable array and load the results   
        function querySucceeded(data) {
            log("queried Presupuestos");
            vm.presupuestos.removeAll();
            var presupuestos = data.results;
            presupuestos.forEach(function (presupuesto) {
                vm.presupuestos.push(presupuesto);
            });
        }
    };

    function saveChanges() {
        return manager.saveChanges()
          .then(function () { log("changes saved"); })
          .fail(saveFailed);
    }

    function queryFailed(error) {
        log("Query failed: " + error.message);
    }

    function saveFailed(error) {
        log("Save failed: " + error.message);
    }

    function editPresupuesto(presupuesto) {
        presupuesto.entityAspect.setModified();
        vm.save();
    }

    function addNewPresupuesto() {
        log("Adding New");
        var item = createPresupuesto({
            Nombre: vm.newPresupuesto().Nombre,
            FechaDesde: vm.newPresupuesto().FechaDesde,
            FechaHasta: vm.newPresupuesto().FechaHasta
        });
        vm.presupuestos.push(item);
        vm.save();
    };

    function createPresupuesto(newPresupuesto) {
        return manager.createEntity('Presupuesto', newPresupuesto);
    };

    function deletePresupuesto(presupuesto) {
        presupuesto.entityAspect.setDeleted();
        vm.presupuestos.remove(presupuesto);
        vm.save();
    }

    //$(document).delegate(".presupuestoAdd", "click", function () {
    //    log("Adding New");
    //    vm.addPresupuesto();
    //});

    $("#presupuestoList").delegate(".editable", "dblclick", function () {
        $(".modal", this).modal();
    });

    $("#presupuestoList").delegate(".presupuestoSaver", "click", function () {
        log("sad ga lomi...:)");
        var presupuesto = ko.dataFor(this);
        vm.editPresupuesto(presupuesto);
    });

    $("#presupuestoList").delegate(".presupuestoCancel", "click", function () {
        log("cancel");
        var presupuesto = ko.dataFor(this);
        presupuesto.entityAspect.rejectChanges();
    });

}(window));