define(function() {
    var cache = [];

    return {
        pub : function(id) {
            var args = [].splice.call(arguments, 1);
            if (!cache[id]) {
                cache[id] = [];
            }
            
            for (var i = 0, l = cache[id].length; i < l; i++) {
                cache[id][i].apply(null, args);
            }
        },
        
        sub: function(id, fn) {
            if (!cache[id]) {
                cache[id] = [fn];
            } else {
                cache[id].push(fn);
            }
        }
    };
});