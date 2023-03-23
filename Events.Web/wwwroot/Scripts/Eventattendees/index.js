
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
                    "data": "id",
                },
                {
                    "data": "eventId",
                },
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
                    "data": "couponTypeId",
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
                    "render": function (data, type, row, meta) {
                        var date = new Date(row.createdOn);
                        var month = date.getMonth();
                        return date.getDay() + "/" + date.getMonth() + "/" + date.getFullYear();
                    }
                },
                {
                    "data": "createdBy",
                },

                {
                    "render": function (data, type, row, meta) {
                        var date = new Date(row.modifiedOn);
                        var month = date.getMonth();
                        return date.getDay() + "/" + date.getMonth() + "/" + date.getFullYear();
                    }

                },
                {
                    "data": "modifiedBy"
                },
                {
                    render: function (data, type, row, meta) {
                        return ' <a class="btn btn-danger" onclick="Delete(' + row.id + ')" >Delete</a> | <a class="btn btn-primary" onclick="edit_attendee(' + row.id + ')" >Edit</a>';
                    }
                },

            ]
        });

}

$(document).ready(function () {
   
});



function create_attendee(id) {
    $.ajax({

        url: '/Eventattendees/CreateEdit/' + id,
        success: function (resonce) {
            $('#Attendees').html(resonce);
            $("#addeditattendee").modal('show');
            
        }
    })
}


function save_Attendee() {
    $("#formAddAttendees").validate({
        rules: {
            AttendeeName: "required",
            ContactNo: {
                required: true,
                minlength: 10,
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
            },
            CouponTypeId: {
                required: true,
                number: true
            },
            RemainingCoupons: {
                required: true,
                number: true
            }

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
            }
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
            CreatedBy: $("#Createdby").val(),
            CreatedOn: $("#crearedon").val()
        }
        $.ajax({
            type: "post",
            url: '/Eventattendees/CreateEdit1',
            data: data,
            success: function (resonce) {
                alert(resonce);
                window.location.reload();
            }
        })
    }
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
            $('#attendeestitle').text('Edit Attendee Detail');
            $.ajax({
                type: "get",
                url: '/Eventattendees/Edit/' + id,
                success: function (resonce) {
                    var now = new Date(resonce.purchasedOn);
                    var day = ("0" + now.getDate()).slice(-2);
                    var month = ("0" + (now.getMonth() + 1)).slice(-2);
                    var today = now.getFullYear() + "-" + month + "-" + day;

                    var now = new Date(resonce.createdOn);
                    var day = ("0" + now.getDate()).slice(-2);
                    var month = ("0" + (now.getMonth() + 1)).slice(-2);
                    var todayq = now.getFullYear() + "-" + month + "-" + day;

                    $("#attenid").val(resonce.id);
                    $("#EventId").val(resonce.eventId);
                    $("#AttendeeName").val(resonce.attendeeName);
                    $("#ContactNo").val(resonce.contactNo);
                    $("#CouponsPurchased").val(resonce.couponsPurchased);
                    $("#PurchasedOn").val(today);
                    $("#TotalAmount").val(resonce.totalAmount);
                    $("#Remarks").val(resonce.remarks);
                    $("#CouponTypeId").val(resonce.couponTypeId);
                    $("#RemainingCoupons").val(resonce.remainingCoupons);
                    $("#Createdby").val(resonce.createdBy);
                    $("#crearedon").val(todayq);

                }
            })
        }
    })

}

function Delete(id) {
    var confirmation = confirm("Are you sure to delete this Member...");
    if (confirmation) {
        $.ajax({
            type: "post",
            url: '/Eventattendees/Delete/' + id,
            success: function (resonce) {
                alert("Record Deleted Successfuly..");
                window.location.reload();
            }
        })
    }
}
