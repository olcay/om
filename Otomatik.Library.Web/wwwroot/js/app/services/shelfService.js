﻿var ShelfService = function () {
    var getForm = function (done, fail) {
        $.get("/shelves")
            .done(done)
            .fail(fail);
    };

    var get = function (userId, page, done, fail) {
        $.get({
            url: "/api/" + userId + "/shelves?page=" + page,
            headers:
            {
                RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
            }
        })
            .done(done)
            .fail(fail);
    };

    var getPublic = function (page, query, done, fail) {
        $.get({
            url: "/api/shelves?page=" + page + (query ? "&query=" + query : ""),
            headers:
            {
                RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
            }
        })
            .done(done)
            .fail(fail);
    };

    var getStarred = function (userId, page, done, fail) {
        $.get({
            url: "/api/" + userId + "/starredShelves?page=" + page,
            headers:
            {
                RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
            }
        })
            .done(done)
            .fail(fail);
    };

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

    var rename = function (shelfId, title, done, fail) {
        $.ajax({
            url: "/api/shelves/" + shelfId,
            method: "PATCH",
            data: { title },
            headers:
            {
                RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
            }
        })
            .done(done)
            .fail(fail);
    };

    return {
        getForm: getForm,
        get: get,
        getPublic: getPublic,
        getStarred: getStarred,
        remove: remove,
        makePublic: makePublic,
        makePrivate: makePrivate,
        rename: rename
    };
}();