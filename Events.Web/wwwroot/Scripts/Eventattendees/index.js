
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
                "processing": '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> '
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
                    "render": function (row) {
                        var arr = row.split(",");
                        return arr.length;
                    }
                },
                {
                    "data": "purchasedOn",
                    "render": function (data, type, row, meta) {
                        var date = new Date(row.purchasedOn);
                        const year = date.getFullYear();
                        const month = String(date.getMonth() + 1).padStart(2, '0');
                        const day = String(date.getDate()).padStart(2, '0');
                        const joined = [day, month, year].join('/');
                        return joined;
                    }

                },
                {
                    "data": "paymentStatus",
                    "render": function (row) {
                        if (row == 1) {
                            return 'Paid';
                        } else {
                            return 'Pending';
                        }
                    }
                },
                {
                    "data": "modeOfPayment",
                    "render": function (row) {
                        if (row == 1)
                            return "Cash";
                        else if (row == 2)
                            return "UPI";
                        else if (row == 3)
                            return "Bank_Transfer";
                        else if (row == 4)
                            return "Others";

                    }
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
            $('.nocoupon').select2({
                'width': '100%',
                closeOnSelect: false,
                placeholder: "Select Numbers of Coupons",
                multiple: true
            });
        }
    })
}

function myUser(tmp) {
    var eid = document.getElementById("User").value;
    $.get("/Eventattendees/fetchcoupon/" + eid,
        function (data) {
            //$("#CouponTypeIdd").prop("disabled", false);
            if (tmp =   = 1) {
                $('.CouponTypeId option').remove();
                $('.CouponTypeId').append("<option selected disable> Select Coupon</option > ");
            }
   
            $.each(data, function (index, value) {
                $('.CouponTypeId').append(new Option(value.couponName, value.id));
            });

        }
    )
}

function selectNocoupon() {
    var couponid = document.getElementById("CouponTypeIdd").value;
    var mid = document.getElementById("User").value;
    var aid = $("#attenid").val();
    $.ajax({

        url: "/Eventattendees/getnoofcoupon",
        data: {
            id: couponid,
            mid: mid,
            aid: aid,
        },
        success: function (value) {
            $('#selectmultiplecoupons option').remove();

            $.each(value, function (index, value) {
                $('.nocoupon').append($('<option id="option' + value.id + '">').val(value.id).text(value.couponNumber))
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
                maxlength: 10,
                minlength: 10
            },
            PurchasedOn: {
                required: true
            },
            User: {
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

            Remarks: {
                required: true
            },
        },
        messages: {
            AttendeeName: " Please Enter AttendeeName",

            ContactNo: " Please Enter valid Contact Number",

            PurchasedOn: {
                required: "Please Select Date",
            },
            User: {
                required: "Please Select Executive Member "
            },
            CouponTypeId: {
                required: " Please Select Coupon "
            },
            CouponsPurchased: {
                required: " Please Select Coupons Numbers"
            },
            ModeOfPayment: {
                required: "Please Select Mode Of Paymnet"
            },
            PaymentStatus: {
                required: "Please Select Paymnet Status"
            },
            PaymentReference: {
                required: "Please Select Paymnet Refrence"
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
            User: $("#User").val(),
            CouponTypeId: $("#CouponTypeIdd").val(),
            CouponsPurchased: $(".nocoupon").val().join(","),
            ModeOfPayment: $("#ModeOfPayment").val(),
            PaymentStatus: $("#PaymentStatus").val(),
            PaymentReference: $("#PaymentReference").val(),
            Remarks: $("#Remarks").val(),
            Eventcouponassignmentmappings: assign
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
    $("#CouponTypeIdd").val(cname);
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
       
            $("#PurchasedOn").datepicker();
            $('#attendeestitle').text('Edit Attendee Detail');

            $("#CouponTypeIdd").prop('disabled', true);
            $("#User").prop('disabled', true);
            $("#loaderDiv").show();

            setTimeout(function () {
                $.ajax({
                    type: "get",
                    url: '/Eventattendees/Edit1/' + id,
                    success: function (resonce) {
                        var now = new Date(resonce.purchasedOn);
                        var day = ("0" + now.getDate()).slice(-2);
                        var month = ("0" + (now.getMonth() + 1)).slice(-2);
                        var today = day + "/" + month + "/" + now.getFullYear();

                        $("#attenid").val(resonce.id);
                        $("#EventId").val(resonce.id);
                        $("#AttendeeName").val(resonce.attendeeName);
                        $("#ContactNo").val(resonce.contactNo);
                        $("#CouponsPurchased").val(resonce.couponsPurchased);
                        $("#PurchasedOn").val(today);
                        $("#TotalAmount").val(resonce.totalAmount);
                        $("#Remarks").val(resonce.remarks);
                        $("#ModeOfPayment").val(resonce.modeOfPayment);
                        $("#PaymentStatus").val(resonce.paymentStatus);
                        $("#PaymentReference").val(resonce.paymentReference);

                     
              
                        function1();
                        function function1() {
                            $("#User").val(resonce.user);
                            function2();
                        }
                        function function2() {
                            myUser();
                            setTimeout(function () {
                                function3();
                            }, 470)

                        }
                        function function3() {
                            $("#CouponTypeIdd").val(resonce.couponTypeId);
                            function4();
                        }

                        function function4() {
                            selectNocoupon();
                            setTimeout(function () {
                                function5();
                            }, 300)
                       
                        }
                        function function5() {
                            var nameArr = resonce.couponsPurchased.split(',');
                            $(nameArr).each(function (k, v) {
                                $("#selectmultiplecoupons option[value='" + v + "']").attr('selected', 'selected');
                            }); function6();
                        }
                        function function6() {
                            $('.nocoupon').select2({
                                'width': '100%',
                                closeOnSelect: false,
                                placeholder: "Select Numbers of Coupons",
                            });
                            $("#loaderDiv").hide();
                            $("#addeditattendee").modal('show');
                        }   
                    }
                })
            }, 150)


    
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
                $(this).dialog('destroy');
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

function matchCustom(params, data) {
    // If there are no search terms, return all of the data
    if ($.trim(params.term) === '') {
        return data;
    }

    // Do not display the item if there is no 'text' property
    if (typeof data.text === 'undefined') {
        return null;
    }

    // `params.term` should be the term that is used for searching
    // `data.text` is the text that is displayed for the data object
    if (data.text.indexOf(params.term) > -1) {
        var modifiedData = $.extend({}, data, true);
        modifiedData.text += ' (matched)';

        // You can return modified objects from here
        // This includes matching the `children` how you want in nested data sets
        return modifiedData;
    }

    // Return `null` if the term should not be displayed
    return null;
}
