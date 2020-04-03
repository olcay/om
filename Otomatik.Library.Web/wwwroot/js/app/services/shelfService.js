var ShelfService = function() {
    var remove = function (shelfId, done, fail) {
        $.ajax({
            url: "/api/shelves/" + shelfId,
            method: "DELETE",
            headers:
            {
                RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
            }
        })
        .done(done)
        .fail(fail);
    };

    var makePublic = function (shelfId, done, fail) {
        $.ajax({
            url: "/api/shelves/" + shelfId + "/public",
            method: "PATCH",
            headers:
            {
                RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
            }
        })
        .done(done)
        .fail(fail);
    };

    var makePrivate = function (shelfId, done, fail) {
        $.ajax({
            url: "/api/shelves/" + shelfId + "/private",
            method: "PATCH",
            headers:
            {
                RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
            }
        })
        .done(done)
        .fail(fail);
    };

    return {
        remove: remove,
        makePublic: makePublic,
        makePrivate: makePrivate
    };
}();