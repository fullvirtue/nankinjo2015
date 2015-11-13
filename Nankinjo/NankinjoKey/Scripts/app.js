$(function () {
    //date picker
    $('.cal').datepicker({
        format: 'yyyy/mm/dd',
        language: 'ja',
        autoclose: true,
        todayHighlight: true
    });

    $('.cal-yyyymm').datepicker({
        format: 'yyyy/mm',
        language: 'ja',
        minViewMode: 'months',
        autoclose: true
    });

});



