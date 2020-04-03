var ProfileController = function(followingModule, starModule) {
    var init = function(container) {
        starModule.init(container);
        followingModule.init(container);
    };
    
    return {
        init: init
    };
}(FollowingModule, StarModule);