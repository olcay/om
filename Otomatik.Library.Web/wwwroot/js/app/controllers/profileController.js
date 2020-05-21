var ProfileController = function(followingModule, starModule, shelfService) {
    var init = function(container, userId, page) {
        starModule.init(container);
        followingModule.init(container);

        shelfService.get(userId, page);
    };
    
    return {
        init: init
    };
}(FollowingModule, StarModule, ShelfService);