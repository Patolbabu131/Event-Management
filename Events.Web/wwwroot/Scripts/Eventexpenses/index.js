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
        }
    })
}




function save_eexpenses() {
    $("#formAddExpenses").validate({
        rules: {
            ExpenseName: "required",
            ExpenseSubject: {
                required: true,

            },
            PurchasedOn: {
                required: true,
            },
            AmountSpent: {
                required: true,
                number: true,

            },
            Remarks: {
                required: true,
            },
        },
        messages: {
            ExpenseName: " Please enter ExpenseName",
            ContactNo: " Please enter valid Contact Number",
            ExpenseSubject: {
                required: " Please enter ExpenseSubject",
            },
            AmountSpent: {
                required: " Please enter Amount ",
                number: "Invalid input"
            },
            AmountSpent: {
                required: " Please AmountSpent ",
            },
            Remarks: "Please enter Remark",
        },
        highlight: function (element) {
            $(element).parent().addClass('error')
        },
        unhighlight: function (element) {
            $(element).parent().removeClass('error')
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
            success: function (resonce) {
                alert(resonce);
                window.location.reload();
            }
        })
    }
}





function edit_expenses(id) {
    $.ajax({
        type: "get",
        url: '/Eventexpenses/CreateEdit' ,
        success: function (resonce) {
            $('#expenses').html(resonce);
            $("#createexpenses").modal('show');


            $.ajax({
                type: "get",
                url: '/Eventexpenses/Edit/' + id,
                success: function (resonce) {
                    var now = new Date(resonce.createdOn);
                    var day = ("0" + now.getDate()).slice(-2);
                    var month = ("0" + (now.getMonth() + 1)).slice(-2);
                    var today = now.getFullYear() + "-" + month + "-" + day;

                    $("#expensesid").val(resonce.id);
                    $("#EventId").val(resonce.eventId);
                    $("#ExpenseName").val(resonce.expenseName);
                    $("#ExpenseSubject").val(resonce.expenseSubject);
                    $("#AmountSpent").val(resonce.amountSpent);
                    $("#Remarks").val(resonce.remarks);
                    $("#CreatedOn").val(today);
                    $("#CreatedBy").val(resonce.createdBy);
                }
            })
        }
    })

}

function Delete(id) {
    var confirmation = confirm("Are you sure to delete this Member...");
    if (confirmation) {
        $.ajax({
            type: "get",
            url: '/Eventexpenses/Delete/' + id,
            success: function (resonce) {
                alert("Record Deleted Successfuly..");
                window.location.reload();
            }
        })
    }
}
