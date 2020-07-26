var ShelfController = function (itemService, shelfService, itemModule) {
    var pageData;

    var fail = function () {
        swal("Something failed!", "We could not process your request.", "error");
    };

    var init = function (data) {
        pageData = data;
        itemModule.init();

        $(".js-remove-book").click(removeBook);
        $("#js-remove-shelf").click(removeShelf);
        $("#js-rename-shelf").click(renameShelf);
        $("#js-operation").on("click", "#js-makePublic-shelf", makeShelfPublic);
        $("#js-operation").on("click", "#js-makePrivate-shelf", makeShelfPrivate);
    };

    var makeShelfPublic = function (e) {
        e.preventDefault();

        shelfService.makePublic(pageData.id, makeShelfPublicDone, fail);
    };

    var makeShelfPrivate = function (e) {
        e.preventDefault();

        shelfService.makePrivate(pageData.id, makeShelfPrivateDone, fail);
    };

    var renameShelf = function (e) {
        e.preventDefault();

        bootbox.prompt({
            title: "Rename Shelf",
            onEscape: true,
            backdrop: true,
            value: pageData.title,
            required: true,
            callback: function (result) {
                if (result) {
                    shelfService.rename(pageData.id, result,
                        function () {
                            $("#spnTitle").html(result);
                            pageData.title = result;
                        }, fail);
                }
            }
        });
    };

    var makeShelfPublicDone = function () {
        $("#js-makePublic-shelf").hide();
        $("#js-makePrivate-shelf").show();
        $("#lblIsPublic").hide();
    };

    var makeShelfPrivateDone = function () {
        $("#js-makePublic-shelf").show();
        $("#js-makePrivate-shelf").hide();
        $("#lblIsPublic").show();
    };

    var removeShelf = function (e) {
        e.preventDefault();

        swal({
            title: "Are you sure?",
            text: "Once deleted, you will not be able to recover this shelf!",
            icon: "warning",
            buttons: true,
            dangerMode: true
        })
            .then((willDelete) => {
                if (willDelete) {
                    shelfService.remove(pageData.id, removeShelfDone, fail);
                }
            });
    };

    var removeShelfDone = function () {
        window.location = "/p/" + pageData.userName;
    };

    var removedBook;

    var removeBook = function () {
        removedBook = $(this);

        swal({
            title: "Are you sure?",
            text: "Once deleted, you will not be able to recover this book!",
            icon: "warning",
            buttons: true,
            dangerMode: true
        })
            .then((willDelete) => {
                if (willDelete) {
                    itemService.remove(removedBook.data("book-id"), removeBookDone, fail);
                }
            });
    };

    var removeBookDone = function () {
        removedBook.parents(".thumbnail").fadeOut(function () {
            $(this).remove();
            salvattore.recreateColumns(document.querySelector("#grid"));
        });
    };

    return {
        init: init
    };
}(ItemService, ShelfService, ItemModule);