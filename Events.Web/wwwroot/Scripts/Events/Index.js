var Events = {

    variables: {
        oTable: null,
        srcGetEventsList: '/Events/GetEventList'
    },
    controls: {
        tblevents: '#tblevents',
        txteventsearchbox: "#txteventsearchbox",
        tblevents_Rows: '#tblevents tbody tr',
    },

    IntializeEventExpenseTable: function () {

        Events.variables.oTable = $(Events.controls.tblevents).dataTableEvents({
            "sAjaxSource": Events.variables.srcGetEventsList,
            "aaSorting": [[1, "desc"]],// default sorting
            "sDom": "frtlip",
            "autoWidth": false,
            "bLengthChange": true,
            "fixedHeader": true,
            "aoColumnDefs": [
                {
                    "aTargets": [0],
                    "className": "text-center",
                    "bSortable": false,
                    //"mRender": function (data, type, row, meta) {
                    //    return '<span data-ExpenseId=' + row[3] + '>' + parseInt(meta.row + meta.settings._iDisplayStart + 1) + '</span';
                    //},
                },
                {
                    "aTargets": [1],
                    "className": "text-center",
                },
                {
                    "aTargets": [2],
                    "className": "text-center",
                    //"mRender": function (data, type, full) {
                    //    return '<div>' + full[2] + '</div>';
                    //},
                },
                {
                    "aTargets": [3],
                    "className": "text-center",
                },
                {
                    "aTargets": [4],
                    "className": "text-center",
                },
                {
                    "aTargets": [5],
                    "className": "text-center",
                },
                {
                    "aTargets": [6],
                    "className": "text-center",
                },
                {
                    "aTargets": [7],
                    "className": "text-center",
                    //"mRender": function (data, type, full) {
                    //    var row = '';
                    //    row += '<a href= "javascript:void(0);" title="Remove Expense Details" onclick=Expenses.DeleteExpenseDetails(' + full[3] + ')> <i class="fa fa-times red"></i></a >';

                    //    return row;
                    //},
                    "bSortable": false,

                }
            ],

            "oLanguage": {
                "sEmptyTable": $('#hdnNodataavailable').val(),
                "sLengthMenu": "Page Size: _MENU_",
                "oPaginate": {
                    "sNext": $('#hdnNext').val(),
                    "sPrevious": $('#hdnPrevious').val()
                }
            },
            "fnServerParams": function (aoData) {

                $("div").data("srchParams",
                    [
                        { name: 'iDisplayLength', value: $("#hdnGeneralPageSize").val() },
                        { name: 'srchTxt', value: encodeURIComponent($(Events.controls.txteventsearchbox).val().trim() == '' ? '' : $(Events.controls.txteventsearchbox).val().trim()) },
                        { name: 'srchBy', value: 'ALL' },
                    ]);

                var srchParams = $("div").data("srchParams");
                if (srchParams) {
                    for (var i = 0; i < srchParams.length; i++) {
                        aoData.push({ "name": "" + srchParams[i].name + "", "value": "" + srchParams[i].value + "" });
                    }
                }
            },
            "fnInfoCallback": function (oSettings, iStart, iEnd, iMax, iTotal, sPre) {
                //smallTable();
                return common.pagingText(iStart, iEnd, iTotal, $('#hdnRecordsText').val(), oSettings._iDisplayLength);
            },
        });
    },



}


$(document).ready(function () {


});