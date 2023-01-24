var EventExpenses = {

    variables: {
        oTable: null,
    },
    controls: {
        tblExpenseDetails: '#tblExpenseDetails'
    },

    IntializeEventExpenseTable: function () {

        EventExpenses.variables.oTable = $(Expenses.controls.tblExpenseDetails).dataTableEvents({
            "sAjaxSource": Expenses.variables.srcGetExpenseList,
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
                    "mRender": function (data, type, row, meta) {
                        return '<span data-ExpenseId=' + row[3] + '>' + parseInt(meta.row + meta.settings._iDisplayStart + 1) + '</span';
                    },
                },
                {
                    "aTargets": [1],
                    "className": "text-center",
                },
                {
                    "aTargets": [2],
                    "className": "text-center",
                    "mRender": function (data, type, full) {
                        return '<div>' + full[2] + '</div>';
                    },
                },
                {
                    "aTargets": [3],
                    "className": "text-center",
                    "mRender": function (data, type, full) {
                        var row = '';
                        row += '<a href= "javascript:void(0);" title="Remove Expense Details" onclick=Expenses.DeleteExpenseDetails(' + full[3] + ')> <i class="fa fa-times red"></i></a >';

                        return row;
                    },
                    "bSortable": false,

                }
            ],

            "oLanguage": {
                "sEmptyTable": $('#hdnNodataavailable').val(),
                "sLengthMenu": "Page Size: _MENU_",
                //"sInfoEmpty": $('#hdnEmptyInfo').val(),
                "oPaginate": {
                    //"sFirst": $(CustomerList.textvariables.hdntblPageFirst).val(),
                    //"sLast": $(CustomerList.textvariables.hdntblPageLast).val(),
                    "sNext": $('#hdnNext').val(),
                    "sPrevious": $('#hdnPrevious').val()
                }
            },
            "fnServerParams": function (aoData) {

                $("div").data("srchParams",
                    [
                        { name: 'iDisplayLength', value: $("#hdnGeneralPageSize").val() },
                        { name: 'srchTxt', value: encodeURIComponent($(Expenses.controls.txtExpenseSearchBox).val().trim() == '' ? '' : $(Expenses.controls.txtExpenseSearchBox).val().trim()) },
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

                if (($(Expenses.controls.tblExpenseDetails).dataTable().fnSettings().fnRecordsTotal() > 0)) {

                    var row = $(Expenses.controls.tblExpenseDetails_Rows)[0];
                    if (!$(row).find('td').hasClass('dataTables_empty')) {
                        $(row).addClass('selected');
                        var expenseid = $(row).find('td span').attr('data-expenseid');
                        Expenses.GetExpenseDetails(expenseid);
                    }
                    $(Expenses.controls.btnSendEmail).show();
                }
                else {
                    $(Expenses.controls.btnSendEmail).hide();
                    Expenses.ClearExpenseTargetDetails();
                }
                smallTable();

                return common.pagingText(iStart, iEnd, iTotal, $('#hdnRecordsText').val(), oSettings._iDisplayLength);
            },
        });
    },



}


$(document).ready(function () {


});