function getCookie(name) {
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
    if (arr = document.cookie.match(reg))
        return unescape(arr[2]);
    else
        return null;
}

function setCookie(name, value) {
    var Time = config.cookie_keeptime;
    var exp = new Date();
    exp.setTime(exp.getTime() + Time);
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
}

function clearCookie(name) {
    document.cookie = name + '=0;expires=' + new Date(0).toUTCString();
}