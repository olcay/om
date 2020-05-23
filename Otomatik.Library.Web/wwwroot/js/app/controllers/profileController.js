var ProfileController = function (followingModule, starModule, shelfService, followingService, paginationModule) {
    var _followerTemplate, _shelvesTemplate, _tab, _userId;

    var fail = function (data) {
        swal("Something failed!", "We could not process your request. " + data.responseText, "error");
    };

    var displayFollowList = function (title, followers) {
        var render = _.template($(_followerTemplate).html());

        bootbox.dialog({
            title: title,
            message: render({ followers: followers }),
            backdrop: true,
            onEscape: true,
            scrollable: true
        });
    };

    var displayShelves = function (data) {
        var render = _.template($(_shelvesTemplate).html());

        $("#divShelves").html(render({ shelves: data.list }));

        paginationModule.init(data.currentPage, data.pageCount, "#navShelves");
    };

    var displayFollowers = function (data) {
        displayFollowList("Followers", data);
    };

    var displayFollowings = function (data) {
        displayFollowList("Followings", data);
    };

    var getShelves = function () {
        var page = location.hash.substr(1);

        if (!page || page <= 0) {
            page = 1;
        }

        if (_tab === "Shelves") {
            shelfService.get(_userId, page, displayShelves, fail);
        } else {
            shelfService.getStarred(_userId, page, displayShelves, fail);
        }
    };

    var init = function (container, userId, followerTemplate, shelvesTemplate, tab) {
        _followerTemplate = followerTemplate;
        _shelvesTemplate = shelvesTemplate;
        _userId = userId;
        _tab = tab;

        starModule.init(container);
        followingModule.init(container);

        window.onhashchange = getShelves;
        getShelves();

        $(document).on("click", "#btnFollowers", function (e) {
            followingService.getFollowers(userId, displayFollowers, fail);
        });

        $(document).on("click", "#btnFollowing", function (e) {
            followingService.getFollowings(userId, displayFollowings, fail);
        });
    };

    return {
        init: init
    };
}(FollowingModule, StarModule, ShelfService, FollowingService, PaginationModule);