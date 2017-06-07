define(['jquery', 'pubsub'], function ($, ps) {
    var moduleSettings = {};
    return {
        init: function (settings, events) {
            moduleSettings = $.extend({}, settings, moduleSettings);
            var newComment = function (item) {
                var result = $.ajax({
                    type: "POST",
                    url: moduleSettings.newCommentUrl,
                    data: item
                });

                ps.pub(events.newCommentCompleted, result);
            };

            var gamesCount = function(item) {
                var result = $.ajax({
                    type: "GET",
                    url: moduleSettings.gamesCountUrl
                });
                
                ps.pub(events.gamesCountCompleted, result);
            };

            var loadListData = function (loadUrl, event) {
                var result = $.ajax({
                    type: "GET",
                    url: loadUrl
                });

                ps.pub(event, result);
            };

            ps.sub(events.newCommentStart, newComment);
            ps.sub(events.gamesCountStart, gamesCount);
            ps.sub(events.loadGenresStart, function(){
                loadListData(moduleSettings.genresUrl, events.loadGenresCompleted);
            });
        }
    };
});