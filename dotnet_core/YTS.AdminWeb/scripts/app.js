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
    var appConfig = Object.get(window, 'appConfig', {});
    window.Api = {
        BaseUrl: Object.get(appConfig, 'BaseUrl', ''),
        UrlFormat: function(url) {
            url = (url || '').trim('/');
            return this.BaseUrl + '/' + url;
        },
        CallGet: function(args) {
            var self = this;
            args = args || {};
            var url = Object.get(args, 'url', '');
            if (!url) {
                throw "url is null!";
                return;
            }
            $.ajax({
                type: 'get',
                url: this.UrlFormat(url),
                data: Object.get(args, 'data', {}),
                contentType: Object.get(args, 'contentType', 'application/json'),
                success: Object.get(args, 'success', function(result) {}),
                error: Object.get(args, 'error', self.funcError),
                complete: Object.get(args, 'complete', function(xhr) {})
            });
        },
        CallPost: function(args) {
            var self = this;
            args = args || {};
            var url = Object.get(args, 'url', '');
            if (!url) {
                throw "url is null!";
                return;
            }
            url = this.UrlFormat(url);
            var data = Object.get(args, 'data', {});
            var model = null;
            for (var key in data) {
                var value = data[key];
                if (typeof value === "object") {
                    if (model == null) {
                        model = value;
                    } else {
                        throw "Not allowed to pass in two model/array parameters!";
                        return;
                    }
                    delete data[key];
                }
            }
            url = url.trimEnd('\\?') + "?";
            if (model) {
                url += PageInfo.ToHttpGETParameter(data);
            }
            $.ajax({
                type: 'post',
                url: url,
                data: model ? JSON.stringify(model) : data,
                contentType: Object.get(args, 'contentType', 'application/json'),
                success: Object.get(args, 'success', function(result) {}),
                error: Object.get(args, 'error', self.funcError),
                complete: Object.get(args, 'complete', function(xhr) {})
            });
        },
        funcError: function(xhr) {
            var responseJSON = Object.get(xhr, 'responseJSON', {});
            var responseStatus = Object.get(responseJSON, 'status', 0);
            var responseTitle = Object.get(responseJSON, 'title', '');
            var status = Object.get(xhr, 'status', 0);
            var statusText = Object.get(xhr, 'statusText', '');
            console.log('status: {0}, errorMessage: {1}'.format(
                responseStatus || status,
                responseTitle || statusText));
        },
    }
})();
