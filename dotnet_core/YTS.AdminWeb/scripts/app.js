// (function() {
//     // 检测IE
//     if ('undefined' == typeof (document.body.style.maxHeight)) {
//         window.location.href = '/ie6update.html';
//     }
// })();

(function() {
    window.Api = function(url) {
        url = url.trim('/');
        domain = 'https://localhost:6121';
        return domain + '/' + url;
    }
})();

(function() {
    //jquery全局配置
    $.ajaxSetup({
        // dataType: "json",
        // cache: false,
        // headers: {
        //     "token": token
        // },
        beforeSend: function (xhr) {
            // 可以设置自定义标头
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
