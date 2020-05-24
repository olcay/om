var HomeController = function (starModule, shelfService, paginationModule, urlModule) {
    var _shelvesTemplate;

    var fail = function (data) {
        swal("Something failed!", "We could not process your request. " + data.responseText, "error");
    };

    var displayShelves = function (data) {
        var render = _.template($(_shelvesTemplate).html());

        $("#divShelves").html(render({ shelves: data.list }));

        paginationModule.init(data.currentPage, data.pageCount, "#navShelves");
    };

    var getShelves = function () {
        var page = urlModule.getPage();
        var query = urlModule.getQuery();

        if (!page || page <= 0) {
            page = 1;
        }

        shelfService.getPublic(page, query, displayShelves, fail);
    };

    var init = function (container, shelvesTemplate) {
        starModule.init(container);
        _shelvesTemplate = shelvesTemplate;

        window.onhashchange = getShelves;
        getShelves();

        $("#frmSearch").on("submit",
            function(e) {
                e.preventDefault();

                location.hash = "q=" + $("#inputQuery").val();
            });
    };

    return {
        init: init
    };
}(StarModule, ShelfService, PaginationModule, URLModule);