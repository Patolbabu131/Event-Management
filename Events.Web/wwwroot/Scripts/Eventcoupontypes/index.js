function LoadCtypetable(id) {

    datatable = $('#CtypeTable')
        .DataTable
        ({
            "sAjaxSource": "/Eventcoupontypes/Getctype/" + id,
            "bServerSide": true,
            "bProcessing": true,
            "bSearchable": true,
            "filter": true,
            "language": {
                "emptyTable": "No record found.",
                "processing":
                    '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> '
            },
            columns: [
                {
                    "data": "couponName",
                },
                {
                    "data": "couponPrice",
                },
                {
                    "data": "totalCoupon",
                },
                {
                    render: function (data, type, row) {
                        if (row.active == 0) {
                            return 'No'
                        }
                        else {
                            return 'Yes'
                        }
                    },
                },  
                {
                    render: function (data, type, row, meta) {
                        return ' <a class="btn btn-info"  onclick="edit_ct(' + row.id + ')" hidden>Edit</a>            <a class="btn btn-danger" onclick="Delete(' + row.id + ')" >Delete</a>';
                    }
                },
            ]
        });
}

function create_Ctype(id) {
    $.ajax({
        type: "get",
        url: '/Eventcoupontypes/CreateCType/'+id,
        success: function (resonce) {
            $('#Ctype').html(resonce);
            $("#addCTypee").modal('show');
            document.getElementById("Active").checked = true;
        }
    })
}


function save_ctype() {

    $("#ctypeform").validate({
        rules: {
            CouponName: {
                required: true,
                maxlength: 100
            },
            CouponPrice: {
                required: true,
                number:true
            },
            TotalCoupon: {
                required: true,
                number: true
            },
        },
        messages: {
            CouponName: {
                required: "Coupon Name is a required field!!!"
            },
            CouponPrice: {
                required: "Coupon Price is a required field!!!",
                number:"Invalid input"
            },
            TotalCoupon: {
                required: "Total Coupon is a required field!!!",
                number: "Invalid input"
            },
        }
    });
     

    if ($('#ctypeform').valid()) {
        if ($("#Active").is(':checked')) {
            $("#Active").attr('value', 'true');
        } else {
            $("#Active").attr('value', 'false');
        }
        var assign = [];
        for (var i = 1; i <= $("#TotalCoupon").val(); i++) {
            assign.push({
                CouponNumber: i,
                EventId: $("#EventId").val()
            });
        }
        var data = {
            Id: $("#Coupontype").val(),
            EventId: $("#EventId").val(),
            CouponName: $("#CouponName").val(),
            CouponPrice: $("#CouponPrice").val(),
            TotalCoupon: $("#TotalCoupon").val(),
            Active: $("#Active").val()
        }
        $.ajax({
            type: "post",
            url: '/Eventcoupontypes/CreateCType',
            data: data,
            success: function ConfirmDialog(message) {
                $("#addCTypee").modal('hide');
                CallDailog(message);
            }             
        })
    }
}

function CallDailog(message) {
    $('#Ctype').appendTo('body')
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


function edit_ct(id) {
    $.ajax({
        type: "get",
        url: '/Eventcoupontypes/CreateCType',
        success: function (resonce) {
            $('#Ctype').html(resonce);
            $("#addCTypee").modal('show');
            $('.modal-title').text('Edit Coupon Type');
            $.ajax({
                type: "get",
                url: '/Eventcoupontypes/Edit/' + id,
                success: function (resonce) {
                    $("#Coupontype").val(resonce.id);
                    $("#EventId").val(resonce.eventId);
                    $("#CouponName").val(resonce.couponName);
                    $("#CouponPrice").val(resonce.couponPrice);
                    $("#TotalCoupon").val(resonce.totalCoupon);
                    $("#Active").prop("checked", resonce.active);
                }
            })
        }
    })

}

//function Delete(id) {
//    var confirmation = confirm("Are you sure to delete this Member...");
//    if (confirmation) {
//        $.ajax({
//            type: "get",
//            url: '/Eventcoupontypes/Delete/' + id,
//            success: function (resonce) {
//                alert("Record Deleted Successfuly..");
//                window.location.reload();
//            }
//        })
//    }
//}


function Delete(id) {
    $('#Ctype').appendTo('body')
        .html('<div id="dailog"><h6>' + "Are You Sure Want To Delete This Member ?... " + '</h6></div>')
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
                        url: '/Eventcoupontypes/Delete/' + id,
                        success: function (response) {
                            $('#dailog').appendTo('body')
                                .html('<div><h6>' + response + '</h6></div>')
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