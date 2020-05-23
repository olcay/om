var NotificationService = function () {
    var get = function (done) {
        $.getJSON("/api/notifications/", done);
    };

    var read = function (done) {
        $.post({
                url: "/api/notifications/",
                headers:
                {
                    RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
                }
            })
            .done(done);
    };

    return {
        get: get,
        read: read
    };
}();