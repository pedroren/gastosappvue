function bindDatePicker(target) {
    target.datepicker({
                    showOtherMonths: true,
                    selectOtherMonths: true,
                    dateFormat: "yy-mm-dd"
                });
}

(function ($) {
    $.fn.styleTable = function (options) {
        this.addClass("table-hover table-striped table-bordered");
        return true;
        //Codigo debajo no se usa
        var defaults = {
            css: 'ui-styled-table'
        };
        options = $.extend(defaults, options);

        return this.each(function () {
            $this = $(this);
            $this.addClass(options.css);

            $this.on('mouseover mouseout', 'tbody tr', function (event) {
                $(this).children().toggleClass("ui-state-hover", event.type == 'mouseover');
            });

            $this.find("th").addClass("ui-state-default");
            $this.find("td").addClass("ui-widget-content");
            $this.find("tr:last-child").addClass("last-child");
        });
    };
    
    $("table").styleTable();
})(jQuery);