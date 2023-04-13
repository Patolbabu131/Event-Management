
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
                "processing":'<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> '
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
                    "data": "paymentStatus",
                },
                {
                    "data": "modeOfPayment",
                },
                {
                    "data": "paymentReference",
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

function create_attendee(id) {
    $.ajax({
        url: '/Eventattendees/CreateEdit/' + id,
        success: function (resonce) {
            $('#Attendees').html(resonce);
            $("#addeditattendee").modal('show');
            $("#PurchasedOn").datepicker();
        
       
        }
    })
}


function myExecutiveMember() {
    var eid = document.getElementById("ExecutiveMember").value;
    $.get("/Eventattendees/fetchcoupon/"+eid,
        function (data) {
            //$("#CouponTypeIdd").prop("disabled", false);
            $.each(data, function (index, value) {
                $('.CouponTypeId').append(new Option(value.couponName, value.id));
            });
        }
    )
}

function selectNocoupon() {
    var couponid = document.getElementById("CouponTypeIdd").value;
    var mid = document.getElementById("ExecutiveMember").value;
    $.ajax({

        url: "/Eventattendees/getnoofcoupon",
        data: {
            id: couponid,
            mid: mid
       
            },
        success: function (value) {
            //$("#selectmultiplecoupons").prop("disabled", false);

         
            $.each(value, function (index, value) {
             
                $('.nocoupon').append($('<option>').val(value.id).text(value.couponNumber))
            });
            $('.nocoupon').prop('multiple', true)
            var multipleCancelButton = new Choices('.nocoupon', {
                removeItemButton: true,
            }); 
        }
    })
 
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
            PurchasedOn: {
                required: true
            },
            ExecutiveMember: {
                required: true               
            },
            CouponTypeId: {
                required: true
            },
            CouponsPurchased: {
                required: true
            },
            ModeOfPayment: {
                required: true
            },
            PaymentStatus: {
                required: true
            },
            PaymentReference: {
                required: true
            },
            Remarks:{
                 required: true,
            },
        },
        messages: {
            AttendeeName: " Please enter AttendeeName",

            ContactNo: " Please enter valid Contact Number",
            
            PurchasedOn: {
                required: "Please enter Date",
            },
            ExecutiveMember: {
                required: "Please enter Executive Member "
            },
            CouponTypeId: {
                required: " Please Select Id "
            },
            CouponsPurchased: {
                required: " Please enter Purchased Coupons"
            },
            ModeOfPayment: {
                required:"Please enter Mode Of Paymnet"
            },
            PaymentStatus: {
                required:"Please enter Paymnet Status"
            },
            PaymentReference: {
                required: "Please enter Paymnet Refrence"
            },
            Remarks: {
                required: " Please Enter Remarks ",
            },
        },
    });
    if ($('#formAddAttendees').valid()) {

        var assign = [];
        var count = $('.nocoupon').val();
        for (var i = 0; i < count.length; i++) {
            assign.push({
                Id: count[i],
                Booked: "true"
            });
        }
        var data = {
            Id: $("#attenid").val(),
            EventId: $("#EventId").val(),
            AttendeeName: $("#AttendeeName").val(),
            ContactNo: $("#ContactNo").val(),
            PurchasedOn: $("#PurchasedOn").val(),
            ExecutiveMember: $("#ExecutiveMember").val(),
            CouponTypeId: $("#CouponTypeIdd").val(),
            CouponsPurchased: $(".nocoupon").val().join(","),
            ModeOfPayment: $("#ModeOfPayment").val(),
            PaymentStatus: $("#PaymentStatus").val(),
            PaymentReference: $("#PaymentReference").val(),
            Remarks: $("#Remarks").val(),
            Eventcouponassignmentmappings:assign
        }

        $.ajax({
            type: "post",
            url: '/Eventattendees/CreateEdit1',
            data: data,
            success: function ConfirmDialog(message)
            {
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
        url: '/Eventattendees/CreateEdit/' + id,
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

                    $("#ExecutiveMember").val(resonce.executiveMember);
                    $("#CouponTypeIdd").val(resonce.couponTypeIdd);
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

function CallDialog(message) {
    $('#Attendees').appendTo('body')
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