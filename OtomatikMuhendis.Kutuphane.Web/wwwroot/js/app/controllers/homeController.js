var HomeController = function(starModule) {
    var init = function (container) {
        starModule.init(container);
    };

    return {
        init: init
    };
}(StarModule);