define(['jquery', 'pubsub'], function ($, ps) {
    return {
        init: function (settings, events) {
            ps.sub(events.gamesCountCompleted, function (promise) {
                promise.success(function (data) {
                    if (data.ErrorMessage === null) {
                        $(document).find('#' + settings.gamesCountId).text(data.Count);
                    }
                });
            });

            ps.pub(events.gamesCountStart);
        }
    };
});