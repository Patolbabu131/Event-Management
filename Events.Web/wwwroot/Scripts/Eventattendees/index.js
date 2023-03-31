
function functionToCall(id) {

    datatable = $('#Eventattendeestable')
        .DataTable
        ({
            "sAjaxSource": "/Eventattendees/GetEventattendees/" + id,
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
                    "data": "attendeeName",
                },
                {
                    "data": "contactNo",
                },
                {
                    "data": "couponsPurchased",
                },
                {
                    "data": "purchasedOn",
                    "render": function (data) {
                        var date = new Date(data);
                        var month = date.getMonth() + 1;
                        return (month.toString().length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                    }

                },
                {

                    "data": "totalAmount",
                },

                {
                    "data": "modeOfPayment"
                },

                {
                    "data": "remainingCoupons"
                },
                {
                    "data": "remarks",
                },
                {
                    render: function (data, type, row, meta) {
                        return ' <a class="btn btn-danger" onclick="Delete(' + row.id + ')" >Delete</a> | <a class="btn btn-primary" onclick="edit_attendee(' + row.id + ')" >Edit</a>';
                    }
                },
            ]
        });
}

$(document).ready(function ()
{
   
});



function create_attendee(id) {
    $.ajax({
        url: '/Eventattendees/CreateEdit/' + id,
        success: function (resonce) {
            $('#Attendees').html(resonce);
            $("#addeditattendee").modal('show');
            onlynumber();
            $("#PurchasedOn").datepicker();
        }
    })
}
function onlynumber() {
    $('.numberonly').keypress(function (e) {

        var charCode = (e.which) ? e.which : event.keyCode

        if (String.fromCharCode(charCode).match(/[^0-9]/g))
            return false;
    });
}

function save_Attendee() {
    $("#formAddAttendees").validate({
        rules: {
            AttendeeName: {
                required: true,
                maxlength: 200
            },
            ContactNo: {
                required: true,
                maxlength: 10
            },
            CouponsPurchased: {
                required: true,
                number: true
            },
            PurchasedOn: {
                required: true,
            },
            TotalAmount: {
                required: true,
                number: true
            },
            Remarks: {
                required: true,
                maxlength:500
            },
            CouponTypeId: {
                required: true,
                number: true
            },
            RemainingCoupons: {
                required: true,
                number: true
            },
            Remarks:{
                 required: true,
            },

        },
        messages: {
            AttendeeName: " Please enter AttendeeName",
            ContactNo: " Please enter valid Contact Number",
            CouponsPurchased: {
                required: " Please enter Purchased Coupons",
                number: "Invalid input"
            },
            PurchasedOn: {
                required: "Please enter Date",
            },
            TotalAmount: {
                required: " Please enter Amount ",
                number: "Invalid input"
            },
            Remarks: "Please enter Remark",
            CouponTypeId: {
                required: " Please Select Id ",
                number: "Invalid input"
            },
            RemainingCoupons: {
                required: " Please Remaining Coupons ",
                number: "Invalid input"
            },
            Remarks: {
                required: " Please Enter Remarks ",
            },
        },
    });
    if ($('#formAddAttendees').valid()) {
        var data = {
            Id: $("#attenid").val(),
            EventId: $("#EventId").val(),
            AttendeeName: $("#AttendeeName").val(),
            ContactNo: $("#ContactNo").val(),
            CouponsPurchased: $("#CouponsPurchased").val(),
            PurchasedOn: $("#PurchasedOn").val(),
            TotalAmount: $("#TotalAmount").val(),
            Remarks: $("#Remarks").val(),
            CouponTypeId: $("#CouponTypeId").val(),
            RemainingCoupons: $("#RemainingCoupons").val(),
        }
        $.ajax({
            type: "post",
            url: '/Eventattendees/CreateEdit1',
            data: data,
            success: function ConfirmDialog(message) {
                $("#addeditattendee").modal('hide');
                CallDialog(message);                                   
            }             
        })
    }
}

function selectcoupon(cname) {

    $("#CouponTypeId select").val(cname);

}



function details_event(id) {
    $.ajax({
        type: "post",
        data: id,
        url: '/Events/EventsDetails/' + id,
        success: function (resonce) {
            $('#CreateContainer').html(resonce);
            $("#DetailsEventsModal").modal('show');
        }
    })
}

function edit_attendee(id) {
    $.ajax({
        type: "get",
        url: '/Eventattendees/Edit/' + id,
        success: function (resonce) {
            $('#Attendees').html(resonce);
            $("#addeditattendee").modal('show');
            $("#PurchasedOn").datepicker();
            $('#attendeestitle').text('Edit Attendee Detail');
            onlynumber();

            $.ajax({
                type: "get",
                url: '/Eventattendees/Edit1/' + id,
                success: function (resonce) {
                    var now = new Date(resonce.purchasedOn);
                    var day = ("0" + now.getDate()).slice(-2);
                    var month = ("0" + (now.getMonth() + 1)).slice(-2);
                    var today = day + "/" + month + "/" + now.getFullYear() ;


                    $("#attenid").val(resonce.id);
                    $("#EventId").val(resonce.id);
                    $("#AttendeeName").val(resonce.attendeeName);
                    $("#ContactNo").val(resonce.contactNo);
                    $("#CouponsPurchased").val(resonce.couponsPurchased);
                    $("#PurchasedOn").val(today);
                    $("#TotalAmount").val(resonce.totalAmount);
                    $("#Remarks").val(resonce.remarks);
                    $("#CouponTypeId").val(resonce.couponTypeId);
                    $("#RemainingCoupons").val(resonce.remainingCoupons);

                }
            })
        }
    })

}

//function Delete(id) {
//    var confirmation = confirm("Are you sure to delete this Member...");
//    if (confirmation) {
//        $.ajax({
//            type: "post",
//            url: '/Eventattendees/Delete/' + id,
//            //success: function (resonce) {
//            //    alert("Record Deleted Successfuly..");
//            //    window.location.reload();

//            success: function ConfirmDialog(message) {
//                    //$("#addeditattendee").modal('hide');
//                    CallDialog(message);
//                }

//        })
//    }
//}


function Delete(id) {
    $('#Attendees').appendTo('body')
        .html('<div id="dailog"><h6>' + "Are You Sure Want To Delete This Member ?..." + '</h6></div>')
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
                        url: '/Eventattendees/Delete/' + id,
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