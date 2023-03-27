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
            document.getElementById("Active").checked = true;
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

    if ($("#Active").is(':checked')) {
        $("#Active").attr('value', 'true');
    } else {
        $("#Active").attr('value', 'false');
    }


    if ($('#ctypeform').valid()) {
        var data = {
            Id: $("#Coupontype").val(),
            EventId: $("#EventId").val(),
            CouponName: $("#CouponName").val(),
            CouponPrice: $("#CouponPrice").val(),
            Active: $("#Active").val()
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
            $('.modal-title').text('Edit Coupon Type');
            $.ajax({
                type: "get",
                url: '/Eventcoupontypes/Edit/' + id,
                success: function (resonce) {
                    $("#Coupontype").val(resonce.id);
                    $("#EventId").val(resonce.eventId);
                    $("#CouponName").val(resonce.couponName);
                    $("#CouponPrice").val(resonce.couponPrice);
                    $("#Active").prop("checked", resonce.active);
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
                alert(resonce);
                window.location.reload();
            }
        })
    }
}
