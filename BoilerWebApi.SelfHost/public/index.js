function callGet(obj) {
    var results = document.getElementById("results");
    obj.addEventListener("click", function (e) {
    var bustCache = "&" + new Date().getTime(),
        oReq = new XMLHttpRequest();
    oReq.onreadystatechange = function () {
        if (oReq.readyState !== 4) {
            results.innerHTML = "Loading ...";
            return;
        }
        if (oReq.status !== 200) {
            results.innerHTML = "Error : " + oReq.responseText;
            return;
        }
        results.innerHTML = "Result : " + oReq.responseText;
    };
    oReq.open("GET", e.target.dataset.url + bustCache, true);
    oReq.setRequestHeader("X-Requested-With", "XMLHttpRequest");
    oReq.setRequestHeader("x-vanillaAjaxWithoutjQuery-version", "1.0");
    oReq.send();
    });
}

function callPost(obj) {
    var results = document.getElementById("results");
    obj.addEventListener("click", function (e) {
        var oReq = new XMLHttpRequest();
        oReq.onreadystatechange = function () {
            if (oReq.readyState !== 4) {
                results.innerHTML = "Loading ...";
                return;
            }
            if (oReq.status !== 200) {
                results.innerHTML = "Error : " + oReq.responseText;
                return;
            }
            results.innerHTML = "Result : " + oReq.responseText;
        };
        oReq.open("POST", e.target.dataset.url, true);
        oReq.setRequestHeader("Content-Type", "application/json; charset=UTF-8");
        oReq.setRequestHeader("X-Requested-With", "XMLHttpRequest");
        oReq.setRequestHeader("x-vanillaAjaxWithoutjQuery-version", "1.0");
        oReq.send("{'Id': '1'}");
    });
}

(function () {
    var retrieveOk = document.getElementById("retrieveOk");
    var retrieveKo = document.getElementById("retrieveKo");
    var retrieveBug = document.getElementById("retrieveBug");

    callGet(retrieveOk);
    callGet(retrieveKo);
    callPost(retrieveBug);
}());
