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
                    "data": "id",
                },
                {
                    "data": "eventId",
                },
                {
                    "data": "couponName"
                },
                {
                    "data": "couponPrice",
                },
                {
                    "data": "active",
                },
                {
                    "data": "createdBy",
                },
                {
                      "data": "createdOn",
                    "render": function (data) {
                        var date = new Date(data);
                        var month = date.getMonth() + 1;
                        return (month.toString().length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                    }
                },
                {
                    "data": "modifiedBy"
                },
                {
                  
                        "data": "modifiedOn",
                    "render": function (data) {
                        var date = new Date(data);
                        var month = date.getMonth() + 1;
                        return (month.toString().length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                    }

                },
                {
                    render: function (data, type, row, meta) {
                        return ' <a class="btn btn-info"  onclick="edit_ct(' + row.id + ')" >Edit</a> |  <a class="btn btn-danger" onclick="Delete(' + row.id + ')" >Delete</a>';
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
        }
    })
}



function save_ctype() {

    $("#ctypeform").validate({
        rules: {
            CouponName: {
                required: true,
            },
            CouponPrice: {
                required: true,
                number:true
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
        }
    });

 

    if ($('#ctypeform').valid()) {
        if ($("#Active").is(':checked')) {
            $("#Active").attr('value', 'true');
        } else {
            $("#Active").attr('value', 'false');
        }

        var data = {
            Id: $("#Coupontype").val(),
            EventId: $("#EventId").val(),
            CouponName: $("#CouponName").val(),
            CouponPrice: $("#CouponPrice").val(),
            Active: $("#Active").val(),
            CreatedBy: $("#CreatedBy").val(),
            CreatedOn: $("#CreatedOn").val(),
        }
        $.ajax({
            type: "post",
            url: '/Eventcoupontypes/CreateCType',
            data: data,
            success: function (resonce) {
                alert(resonce);
                window.location.reload();
            }
        })
    }
}


function edit_ct(id) {
    $.ajax({
        type: "get",
        url: '/Eventcoupontypes/CreateCType',
        success: function (resonce) {
            $('#Ctype').html(resonce);
            $("#addCTypee").modal('show');

            $.ajax({
                type: "get",
                url: '/Eventcoupontypes/Edit/' + id,
                success: function (resonce) {
                    var now = new Date(resonce.createdOn);
                    var day = ("0" + now.getDate()).slice(-2);
                    var month = ("0" + (now.getMonth() + 1)).slice(-2);
                    var today = now.getFullYear() + "-" + month + "-" + day;

                    $("#Coupontype").val(resonce.id);
                    $("#EventId").val(resonce.eventId);
                    $("#CouponName").val(resonce.couponName);
                    $("#CouponPrice").val(resonce.couponPrice);
                    $("#Active").prop("checked", resonce.active);
                    $("#CreatedBy").val(resonce.createdBy);
                    $("#CreatedOn").val(today);
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
            url: '/Eventcoupontypes/Delete/' + id,
            success: function (resonce) {
                alert("Record Deleted Successfuly..");
                window.location.reload();
            }
        })
    }
}
