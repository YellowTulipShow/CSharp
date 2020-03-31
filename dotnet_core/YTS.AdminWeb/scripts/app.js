(function () {
    try {
        var browser = navigator.appName
        var b_version = navigator.appVersion
        var version = b_version.split(";");
        var trim_Version = version[1].replace(/[ ]/g, "");
        var isie = browser == "Microsoft Internet Explorer";
        if (isie && trim_Version == "MSIE6.0") {
            window.location.href = '/ie6update.html';
        } else if (isie && trim_Version == "MSIE7.0") {
            window.location.href = '/ie6update.html';
        } else if (isie && trim_Version == "MSIE8.0") {
            window.location.href = '/ie6update.html';
        } else if (isie && trim_Version == "MSIE9.0") {
            window.location.href = '/ie6update.html';
        }
    } catch(err) { }
})();

(function() {
    $.ajaxSetup({
        beforeSend: function (xhr) {
            var token = window.localStorage.getItem('jwtToken');
            if (token) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token);
            }
        },
        complete: function(xhr) {
            if(xhr.status == 401){
                window.localStorage.setItem('jwtToken', null);
                var uri_encode = encodeURIComponent(window.location.href);
                window.location.href = '/login.html?callback=' + uri_encode;
            }
        }
    });
})();

(function() {
    var domain = 'https://localhost:6121';
    window.Api = {
        BaseUrl: domain,
        Call: function(url) {
            url = (url || '').trim('/');
            return domain + '/' + url;
        },
    }
})();
