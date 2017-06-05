define(['jquery', 'pubsub'], function ($, ps) {
    return {
        init: function (el, events) { // name - event name
            ps.sub(events.completed, function (promise) {
                promise.success(function (data) {
                    el.empty();
                    $.each(data, function () {
                        $('<option>').attr('value', this.Value).html(this.Text).appendTo(el);
                    });
                    el.change();
                });
            });

            ps.pub(events.start);
        }
    };
});