
$(function () {
    var timer;

    var $table = $('#tableData').DataTable({
        'destroy': true,
        'processing': true,
        'serverSide': true,
        'filter': true,
        'stateSave': true,
        'searchDelay': 2000,
        'ajax': {
            'url': 'Home/DataHandler',
            'type': 'POST',
            'datatype': 'json'
        },
        'columns': [
            { 'data': 'id', },
            { 'data': 'name', },
            { 'data': 'surname' },
            { 'data': 'email' },
            { 'data': 'street' },
            { 'data': 'city' }
        ]
    });

    $(".dataTables_filter input")
        .unbind()
        .bind("input", function (e) {
            let $this = $(this);

            clearTimeout(timer);

            timer = setTimeout(() => {
                $table.search($this.val()).draw();
            }, 1000);

            return;
        });
})