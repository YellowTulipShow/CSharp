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
