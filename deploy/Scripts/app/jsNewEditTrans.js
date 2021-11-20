
var availableTags = new Array();

function calculaMontoTransf() {
    var tasatransf, montotransf, monto;
    tasatransf = 0;
    montotransf = 0;
    monto = 0;
    tasatransf = $("[name=TasaTransf]").val();
    monto = $("[name=Monto]").val();
    if (tasatransf != 0)
        montotransf = monto / tasatransf;
    $("[name=MontoTransf]").val(montotransf.toFixed(2));

}

function cambiaTipoTrans() {
    var tipotrans = $("input[name=TipoTransaccion]:checked").val();
    //$("[name=TipoTransaccionId]").val(tipotrans);
    //alert(tipotrans);
    if (tipotrans == "1") {
        $('#lblConceptoId').show();
        $('#pnlConceptoId').show();
        $('#pnlTransf').hide();
        $('#pnlCuentaIdTransf').hide();
        $('#lblCuentaIdTransf').hide();
        $('#pnlTasaTransf').hide();
        $('#lblTasaTransf').hide();
        $('#pnlMontoTransf').hide();
        $('#lblMontoTransf').hide();
        $("[name=CuentaTransf]").val("");
        $("[name=TasaTransf]").val("0");
        $("[name=MontoTransf]").val("0");
    }
    else {
        $('#lblConceptoId').hide();
        $('#pnlConceptoId').hide();
        $('#pnlTransf').show();
        $('#pnlCuentaIdTransf').show();
        $('#lblCuentaIdTransf').show();
        $('#pnlTasaTransf').show();
        $('#lblTasaTransf').show();
        $('#pnlMontoTransf').show();
        $('#lblMontoTransf').show();
    };
}

function cambiaCuentaTransf(actionUrl) {
    if ($("[name=CuentaTransf]").val() != "0") {
        $.ajax(
            {
                type: "POST",
                url: actionUrl,
                data: "CuentaId=" + $("[name=CuentaTransf]").val(),
                success: function (result) {
                    if (result.success) {
                        $("[name=TasaTransf]").val(result.Tasa);
                        calculaMontoTransf();
                    }
                },
                error: function (req, status, error) {
                        alert("Error BuscaTasaMonedaCuenta: " + req.responseText);
                }
            });
    }
}
function buscaDescripFrecuente(actionUrl) {
    if ($("[name=Descripcion]").val().length > 3) {
        $.ajax(
            {
                type: "POST",
                url: actionUrl,
                data: "pDescripFrecuente=" + $("[name=Descripcion]").val(),
                success: function (result) {
                    if (result.success) {
                        if ($("[name=Monto]").val() == '0.00' || $("[name=Monto]").val() == '0') { $("[name=Monto]").val(result.Monto); }
                        if (result.CuentaId != null) {
                            $("[name=Cuenta]").val(result.CuentaId);
                            if (isMobile()) {
                                $("[name=Cuenta]").selectmenu('refresh');
                            }
                        }
                        if (result.CuentaIdTransf != null) {
                            $("[name=CuentaTransf]").val(result.CuentaIdTransf);
                            $("[name=CuentaTransf]").change();
                            $("#radio :input")[1].click();
                            $("#radio :input").button("refresh");
                            cambiaTipoTrans();
                        }
                        if (result.ConceptoId != null) {
                            $("[name=Concepto]").val(result.ConceptoId);
                            if (isMobile()) {
                                $("[name=Concepto]").selectmenu('refresh');
                            }
                        }
                        $("[name=Monto]").focus();
                        $("[name=Monto]").select();
                    }
                },
                error: function (req, status, error) {
                    alert("Error DescripFrecuente: " + req.responseText);
                }
            });
    }
    }

    function asigna_autocomplete(listado, actionUrl) {
        availableTags = listado;
        if (isMobile()) {
            $("#Descripcion").autocomplete( {
                target: $('#suggestions'), // the listview to receive results
                source: availableTags, // URL return JSON data
                minLength: 1,
                matchFromStart: true,
                callback: function (e) {
                    var $a = $(e.currentTarget); // access the selected item
                    $('#Descripcion').val($a.text()); // place the value of the selection into the search box
                    $("#Descripcion").autocomplete('clear'); // clear the listview
                    buscaDescripFrecuente(actionUrl);
                },
                link: 'target.html?term='// optional callback function fires upon result selection
            });
        }
        else {
            $("#Descripcion").autocomplete({
                source: availableTags,
                change: function (event, ui) {
                    buscaDescripFrecuente(actionUrl);
                },
                minLength: 0
            });
        }
       
    }

    function openDate(valor) {
    	//SpinningWheel mobile plugin, no en uso
        var now = new Date(valor);
        var days = {};
        var years = {};
        var months = { 1: 'Gen', 2: 'Feb', 3: 'Mar', 4: 'Apr', 5: 'May', 6: 'Jun', 7: 'Jul', 8: 'Aug', 9: 'Sep', 10: 'Oct', 11: 'Nov', 12: 'Dec' };

        for (var i = 1; i < 32; i += 1) {
            days[i] = i;
        }

        for (i = now.getFullYear() - 3; i < now.getFullYear() + 2; i += 1) {
            years[i] = i;
        }

        SpinningWheel.addSlot(years, 'right', now.getFullYear());
        SpinningWheel.addSlot(months, '', now.getMonth()+1);
        SpinningWheel.addSlot(days, 'right', now.getDay()+7);

        SpinningWheel.setCancelAction(cancel);
        SpinningWheel.setDoneAction(done);

        SpinningWheel.open();
    }

    