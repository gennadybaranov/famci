define(['jquery', 'pubsub'], function ($, ps) {
    return {
        init: function (settings) {
            
            $('form').submit(function (e) {
                e.preventDefault();
                if ($('form').valid()) {
                    ps.pub(settings.newCommentStart, {
                        Comment: $(this).find('#' + settings.newCommentId).val()
                    });
                }
            });

            ps.sub(settings.newCommentCompleted, function (promise) {
                promise.success(function (result) {
                    var errorIndex = result.indexOf('class="error"');
                    if (errorIndex !== -1) {
                        //add error message
                        var errors = $(document).find('form .error');
                        $(document).find('form .error').empty();
                        errors.append(result);
                    } else {
                        // add li to ul & clear textbox
                        var comments = $(document).find('#' + settings.commentListId);
                        comments.append(result);
                        $(document).find('form #' + settings.newCommentId).val('');
                        $(document).find('form .error').empty();
                    }
                });
            });
        }
    };
});