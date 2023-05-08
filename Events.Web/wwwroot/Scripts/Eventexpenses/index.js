function callbindexpenese(id) {
    datatable = $('#expensestable')
        .DataTable
        ({
            "sAjaxSource": "/Eventexpenses/GetEventwxpenses/"+id,
            "bServerSide": true,
            "bProcessing": true,
            "bSearchable": true,
            "filter": true,
            "autoWidth": true,
            "language": {
                "emptyTable": "No record found.",
                "processing":
                    '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> '
            },
            "columns": [
                {
                    "data": "expenseName",
                },
                {
                    "data": "expenseSubject",
                },
                {
                    "data": "amountSpent",
                },
                {
                    "data": "remarks",
                },
                {
                    render: function (data, type, row, meta) {
                        return '<a class="btn btn-info" onclick="edit_expenses(' + row.id + ')" >Edit</a> | <a class="btn btn-danger" onclick="Delete(' + row.id + ')" >Delete</a>';
                    }
                },
            ]
        });
}


function addEventListener_expenses(id) {    
    $.ajax({
        type: "get",
        url: '/Eventexpenses/CreateEdit/'+id,
        success: function (resonce) {
            $('#expenses').html(resonce);
            $("#createexpenses").modal('show');
            onlynumber();
        }
    })
}


function save_eexpenses() {

    $("#formAddExpenses").validate({
        rules: {
            ExpenseName: {
                required: true,
                maxlength: 100
            },
            ExpenseSubject: {
                required: true,
                maxlength: 300
            },
            AmountSpent: {
                required: true,
                number: true,

            },
            Remarks: {
                required: true,
                maxlength: 500
            },
        },
        messages: {
            ExpenseName: {
                required: "Expense Name is a required field!!!"
            },
            ExpenseSubject: {
                required: "Expense Subject is a required field!!!"
            },
            AmountSpent: {
                required: "Amount Spent is a required field!!!"
            },
            Remarks: {
                required: "Remarks is a required field!!!"
            },
        }
    });
    if ($('#formAddExpenses').valid()) {
        var data = {
            Id: $("#expensesid").val(),
            EventId: $("#EventId").val(),
            ExpenseName: $("#ExpenseName").val(),
            ExpenseSubject: $("#ExpenseSubject").val(),
            AmountSpent: $("#AmountSpent").val(),
            CreatedOn: $("#CreatedOn").val(),
            CreatedBy: $("#CreatedBy").val(),
            Remarks: $("#Remarks").val(),
        }
        $.ajax({
            type: "post",
            url: '/Eventexpenses/CreateEdit',
            data: data,
            success: function ConfirmDialog(message)
            {              

                $("#createexpenses").modal('hide');
                CallDialog(message);   
            }             
        })
    }
}


function CallDialog(message) {
    $('#expenses').appendTo('body')
        .html('<div><h6>' + message + '</h6></div>')
        .dialog({
            modal: true,
            title: 'Save Message',
            zIndex: 10000,
            autoOpen: true,
            width: 'auto',
            icon: 'fa fa- close',
            click: function () {
                $(this).dialog('destroy');
            },
            buttons: [
                {
                    text: "Ok",
                    icon: "ui-icon-heart",
                    click: function () {
                        $(this).dialog('destroy');
                        window.location.reload();
                    }
                }
            ]
        });
}



function onlynumber() {
    $('.numberonly').keypress(function (e) {

        var charCode = (e.which) ? e.which : event.keyCode

        if (String.fromCharCode(charCode).match(/[^0-9]/g))
            return false;
    });
}

function edit_expenses(id) {
    $.ajax({
        type: "get",
        url: '/Eventexpenses/CreateEdit' ,
        success: function (resonce) {
            $('#expenses').html(resonce);
            $("#createexpenses").modal('show');
            $('.modal-title').text('Edit Expenese');
            onlynumber();
            $.ajax({
                type: "get",
                url: '/Eventexpenses/Edit/' + id,
                success: function (resonce) {
                    $("#expensesid").val(resonce.id);
                    $("#EventId").val(resonce.eventId);
                    $("#ExpenseName").val(resonce.expenseName);
                    $("#ExpenseSubject").val(resonce.expenseSubject);
                    $("#AmountSpent").val(resonce.amountSpent);
                    $("#Remarks").val(resonce.remarks);
                }
            })
        }
    })

}

function Delete(id) {
    $('#expenses').appendTo('body')
        .html('<div id="dailog"><h6>' + "Are You Sure Want To Delete This Expense ?... " + '</h6></div>')
        .dialog({
            modal: true,
            title: 'Delete Message',
            zIndex: 10000,
            autoOpen: true,
            width: 'auto',
            icon: 'fa fa- close',
            click: function () {
                $(this).dialog('destroy');
            },
            buttons: {
                Yes: function () {
                    $.ajax({
                        url: '/Eventexpenses/Delete/' + id,
                        success: function () {
                            $('#dailog').appendTo('body')
                                .html('<div><h6>' + "Deleted Successfully ... " + '</h6></div>')
                                .dialog({
                                    modal: true,
                                    title: 'Delete Message',
                                    zIndex: 10000,
                                    autoOpen: true,
                                    width: 'auto',
                                    icon: 'fa fa- close',
                                    click: function () {
                                        $(this).dialog('destroy');
                                    },
                                    buttons: [
                                        {
                                            text: "Ok",
                                            icon: "ui-icon-heart",
                                            click: function () {
                                                $(this).dialog('destroy');
                                                window.location.reload();
                                            }
                                        }
                                    ]
                                });
                        }

                    })
                },
                No: function () {

                    $(this).dialog('destroy');
                }
            }
        });
}
