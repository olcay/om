var ItemModule = function (itemService) {
    var shelfId;

    var fail = function (data) {
        swal("Something failed!", "We could not process your request. " + data.responseText, "error");
    };

    var done = function (data) {
        var url = `/shelves/${shelfId}/items/${data}/`;

        window.location = url;
    };

    var showForm = function () {
        shelfId = $(this).data("shelfid");

        bootbox.prompt({
            title: "Title for the new item",
            onEscape: true,
            backdrop: true,
            required: true,
            callback: function (result) {
                if (result) {
                    itemService.add(null, null, shelfId, result, done, fail);
                }
            }
        });
    }

    var init = function () {
        $(document).on("click", ".js-add-item", showForm);
    };

    return {
        init: init
    };
}(ItemService);