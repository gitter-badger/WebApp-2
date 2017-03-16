var setlang = function (culture) {
    $.post('/api/setLang', $.param({ culture: culture }), function (data) {
        if (data.code == 0) {
            location.reload();
        }
    });
};