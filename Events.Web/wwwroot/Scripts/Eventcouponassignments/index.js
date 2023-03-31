function Coupenassign(id) {
    datatable = $('#Cassignmenttable')
        .DataTable
        ({
            "sAjaxSource": "/Eventcouponassignments/GetCAssign/" + id,
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
                    "data": "couponsFrom",
                },
                {
                    "data": "couponsTo",
                },
                {
                    "data": "totalCoupons",
                },
                {
                    render: function (data, type, row, meta) {
                        return ' <a class="btn btn-info" onclick="edit_cassign(' + row.id + ')" >Edit</a> | <a class="btn btn-danger" onclick="Delete(' + row.id + ')" >Delete</a>';
                    }
                },
            ]
        });
}


function create_cassign(id) {
    $.ajax({
        type: "get",
        url: '/Eventcouponassignments/CreateCAssign/' + id,
        success: function (resonce) {
            $('#Cassign').html(resonce);
            $("#addCAssign").modal('show');
        }
    })
}

function save_Cassign() {
    $("#CAssignform").validate({
        rules: {

            CouponsFrom: {
                required: true,
                number: true
            },
            CouponsTo: {
                required: true,
                number: true
            },
            TotalCoupons: {
                required: true,
                number: true
            },
        },
        messages: {
            CouponsFrom: {
                required: " Please enter Coupons From",
                number: "Invalid input"
            },
            CouponsTo: {
                required: " Please enter Coupons To ",
                number: "Invalid input"
            },
            TotalCoupons: {
                required: " Please enter Total Coupons",
                number: "Invalid input"
            }

        },
    });
    if ($('#CAssignform').valid()) {
        var data = {
            Id: $("#Cassignid").val(),
            EventId: $("#EventId").val(),
            ExecutiveMemberId: $("#ExecutiveMemberId").val(),
            CouponsFrom: $("#CouponsFrom").val(),
            CouponsTo: $("#CouponsTo").val(),
            TotalCoupons: $("#TotalCoupons").val(),
        }
        $.ajax({
            type: "post",
            url: '/Eventcouponassignments/CreateCAssign',
            data: data,
            success: function ConfirmDialog(message) {
                $("#addCAssign").modal('hide');
                $('#Cassign').appendTo('body')
                    .html('<div><h6>' + message + '</h6></div>')
                    .dialog({
                        modal: true,
                        title: 'Save Message',
                        zIndex: 10000,
                        autoOpen: true,
                        width: 'auto',
                        icon: 'fa fa- close',
                        click: function () {
                            $(this).dialog("close");
                        },
                        buttons: [
                            {
                                text: "Ok",
                                icon: "ui-icon-heart",
                                click: function () {
                                    $(this).dialog("close");
                                    window.location.reload();
                                }
                            }
                        ]
                    });
            }
        })
    }
}


function edit_cassign(id) {
    $.ajax({
        type: "get",
        url: '/Eventcouponassignments/CreateCAssign',
        success: function (resonce) {
            $('#Cassign').html(resonce);
            $("#addCAssign").modal('show');
            $('.modal-title').text('Edit Assigned coupon');

            $.ajax({
                type: "get",
                url: '/Eventcouponassignments/getEdit/' + id,
                success: function (resonce) {
                    $("#Cassignid").val(resonce.id);
                    $("#EventId").val(resonce.eventId);
                    $("#ExecutiveMemberId").val(resonce.executiveMemberId);
                    $("#CouponsFrom").val(resonce.couponsFrom);
                    $("#CouponsTo").val(resonce.couponsTo);
                    $("#TotalCoupons").val(resonce.totalCoupons);
                }
            })
        }
    })

}

function Delete(id) {
    $('#Cassign').appendTo('body')
        .html('<div id="dailog"><h6>' + "Are You Sure Want To Delete This Member ?... " + '</h6></div>')
        .dialog({
            modal: true,
            title: 'Delete Message',
            zIndex: 10000,
            autoOpen: true,
            width: 'auto',
            icon: 'fa fa- close',
            click: function () {
                $(this).dialog("close");
            },
            buttons: {
                Yes: function () {
                    $.ajax({
                        url: '/Eventcouponassignments/Delete/' + id,
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
                                        $(this).dialog("close");
                                    },
                                    buttons: [
                                        {
                                            text: "Ok",
                                            icon: "ui-icon-heart",
                                            click: function () {
                                                $(this).dialog("close");
                                                window.location.reload();
                                            }
                                        }
                                    ]
                                });
                        }

                    })
                },
                No: function () {

                    $(this).dialog("close");
                }
            }
        });
}



function myFunction(val) {
    var from = $("#CouponsFrom").val();
    var toval = $("#CouponsTo").val();
    debugger
    if (from < toval) {
        var totlvl = toval - from + 1;
        $("#TotalCoupons").val(totlvl);
    }
    else if (from && toval > 0 ) {

        alert("Coupon From number Should be smaller than Coupon To");
    }
}

