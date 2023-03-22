function Coupenassign(id) {
    datatable = $('#Cassignmenttable')
        .DataTable
        ({
            "sAjaxSource": "/Eventcouponassignments/GetCAssign/"+id,
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
                    "data": "id"
                },
                {
                    "data": "eventId",
                },
                {
                    "data": "executiveMemberId",
                },
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
                    "data": "createdOn",
                    "render": function (data)
                    {
                        var date = new Date(data);
                        var month = date.getMonth() + 1;
                        return (month.toString().length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                    }

                },
                {
                    "data": "createdBy",
                },
               
                {
                    "data": "modifiedBy",
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
                        return ' <a class="btn btn-info" onclick="edit_cassign(' + row.id + ')" >Edit</a> | <a class="btn btn-danger" onclick="Delete(' + row.id + ')" >Delete</a>';
                    }
                },
            ]
        });
}


function create_cassign(id) {
    $.ajax({
        type: "get",
        url: '/Eventcouponassignments/CreateCAssign/'+id,
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
            CreatedOn: $("#CreatedOn").val(),
            CreatedBy: $("#CreatedBy").val(),
        }
        $.ajax({
            type: "post",
            url: '/Eventcouponassignments/CreateCAssign',
            data: data,
            success: function (resonce) {
                alert(resonce);
                window.location.reload();
            }
        })
    }
    
}


function edit_cassign(id) {
    $.ajax({
        type: "get",
        url: '/Eventcouponassignments/CreateCAssign' ,
        success: function (resonce) {
            $('#Cassign').html(resonce);
            $("#addCAssign").modal('show');


            $.ajax({
                type: "get",
                url: '/Eventcouponassignments/getEdit/' + id,
                success: function (resonce) {
                    var now = new Date(resonce.createdOn);
                    var day = ("0" + now.getDate()).slice(-2);
                    var month = ("0" + (now.getMonth() + 1)).slice(-2);
                    var today = now.getFullYear() + "-" + month + "-" + day;

                    $("#Cassignid").val(resonce.id);
                    $("#EventId").val(resonce.eventId);
                    $("#ExecutiveMemberId").val(resonce.executiveMemberId);
                    $("#CouponsFrom").val(resonce.couponsFrom);
                    $("#CouponsTo").val(resonce.couponsTo);
                    $("#TotalCoupons").val(resonce.totalCoupons);
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
            url: '/Eventcouponassignments/Delete/' + id,
            success: function (resonce) {
                alert("Record Deleted Successfuly..");
                window.location.reload();
            }
        })
    }
}
