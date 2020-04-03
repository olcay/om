var ItemService = function () {
    var remove = function (bookId, done, fail) {
        $.ajax({
        url: "/api/items/" + bookId,
            method: "DELETE",
            headers:
            {
                RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
            }
        })
        .done(done)
        .fail(fail);
    };

    var add = function (gBookId, gameId, shelfId, title, done, fail) {
        $.post({
            url: "/api/items/",
            data: { shelfId: shelfId, gBookId: gBookId, gameId: gameId, title: title },
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
        add: add
    };
}();