var NotificationService = function () {
    var get = function (done) {
        $.getJSON("/api/notifications/", done);
    };

    return {
        get: get
    };
}();