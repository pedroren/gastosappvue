function isIphone() {
    var ua = navigator.userAgent.toLowerCase();
    if ((ua.match(/iphone/i)) || (ua.match(/ipod/i))) {
        return true;
    }
    else {
        return false;
    }
}

function isAndroid() {
    var ua = navigator.userAgent.toLowerCase();
    if ((ua.match(/android/i))) {
        return true;
    }
    else {
        return false;
    }
}

function isMobile() {
    var ua = navigator.userAgent.toLowerCase();
    if (isIphone() || isAndroid()) {
        return true;
    }
    else {
        return false;
    }
}

function cleanCellString(s) {
    clean = s.replace(/\s+/g, '');
    return clean;
}

(function ($) {
    $.QueryString = (function (a) {
        if (a == "") return {};
        var b = {};
        for (var i = 0; i < a.length; ++i) {
            var p = a[i].split('=');
            if (p.length != 2) continue;
            b[p[0]] = decodeURIComponent(p[1].replace(/\+/g, " "));
        }
        return b;
    })(window.location.search.substr(1).split('&'))
})(jQuery);

jQuery.extend({
   postJSON: function( url, data, callback) {
      return jQuery.post(url, data, callback, "json");
   }
});

String.format = function() {
  var s = arguments[0];
  for (var i = 0; i < arguments.length - 1; i++) {       
    var reg = new RegExp("\\{" + i + "\\}", "gm");             
    s = s.replace(reg, arguments[i + 1]);
  }

  return s;
}

withCommas = function (original) {
    original += '';
    x = original.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;

} 
