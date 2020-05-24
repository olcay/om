var URLModule = function() {
    var get = function (name) {
        var urlParams = new URLSearchParams(window.location.hash.substr(1));

        return urlParams.get(name);
    };

    var getQuery = function () {
        return get("q");
    };

    var getPage = function () {
        return get("p");
    };

    var getCurrentHash = function () {
        var hash = "#";

        var q = getQuery();

        if (q) {
            hash += "q=" + q;
        }

        hash += "&p=";

        return hash;
    };

    return {
        getQuery: getQuery,
        getPage: getPage,
        getCurrentHash: getCurrentHash
    };
}();