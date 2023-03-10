$(document).ready(function () {
    bindmember();

});

$('#create_member').click(function () {
    $.ajax({
        type: "post",
        
        url: '/ExecutiveMembers/CreateMember',
        success: function (resonce) {
            $('#member').html(resonce);
            $("#addECMemberModal").modal('show');
        }
    })
});

function save_member() {
    var data = {
        Id: $("#EventId").val(),
        FullName: $("#addrName").val(),
        Designation: $("#addDesignation").val(),
        AppointedOn: $("#addAppointedOn").val(),
        Duties: $("#addDuties").val(),
        CreatedOn:$("#createon").val()
    }
    savemember(data);
}

function savemember(data) {
    $.ajax({
        type: "post",
        data: data,
        url: '/ExecutiveMembers/CreateMembers',
        success: function (resonce) {
            alert(resonce);
        }
    })
}

function bindmember() {
    datatable = $('#tblecmembers')
        .DataTable
        ({

            "sAjaxSource": "/ExecutiveMembers/GetECMember",
            "bServerSide": true,
            "bProcessing": true,
            "bSearchable": true,
            "filter": true,
            "language": {
                "emptyTable": "No record found.",
                "processing":
                    '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> '
            },
            "columns": [
                {
                    "data": "fullname",
                    render: function (data, type, row, meta) {
                        return row.fullName
                    }
                },
                {
                    "data": "designation",
                    render: function (data, type, row, meta) {
                        return row.designation
                    }
                },
                {
                    "data": 'appointedon',
                    "render": function (data, type, row, meta) {
                        var date = new Date(row.appointedOn);
                        var month = date.getMonth() ;
                        return date.getDay() + "/" + date.getMonth() + "/" + date.getFullYear();
                    }
                },
                {
                    "data": "duties",
                    render: function (data, type, row, meta) {
                        return row.duties
                    }
                },
                {
                    render: function (data, type, row, meta) {
                        return ' <a class="btn btn-primary" onclick="details_member(' + row.id + ')" >Details</a> |  <a class="btn btn-info"  onclick="edit_member(' + row.id + ')" >Edit</a> |  <a class="btn btn-danger" onclick="delete_member(' + row.id + ')" >Delete</a>';
                    }
                }
            ]
        });

}

function details_member(id) {
    $.ajax({
        type: "post",
        data: id,
        url: '/ExecutiveMembers/ECMemberDetails/'+id,
        success: function (resonce) {
            $('#member').html(resonce);
            $("#DetailsECMemberModal").modal('show');
        }
    })
}

function edit_member(id) {
    $.ajax({
        type: "post",
        url: '/ExecutiveMembers/CreateMember',
        success: function (resonce) {
            $('#member').html(resonce);
            $("#addECMemberModal").modal('show');
        }
    })

    $.ajax({
        type: "post",
        data: id,
        url: '/ExecutiveMembers/GetEdit/'+id,
        success: function (resonce) {
            var now = new Date(resonce.appointedOn);
            var day = ("0" + now.getDate()).slice(-2);
            var month = ("0" + (now.getMonth() + 1)).slice(-2);
            var today = now.getFullYear() + "-" + (month) + "-" + (day);


            var ok = new Date(resonce.createdOn);
            var day1 = ("0" + ok.getDate()).slice(-2);
            var month1 = ("0" + (ok.getMonth() + 1)).slice(-2);
            var create = ok.getFullYear() + "-" + (month1) + "-" + (day1);

            $('#EventId').val(resonce.id);
            $('#addrName').val(resonce.fullName);
            $('#addDesignation').val(resonce.designation);
            $('#addAppointedOn').val(today);
            $('#addDuties').val(resonce.duties);
            $('#createon').val(create)
        }
    })
}

function delete_member(id) {
    var confirmation = confirm("Are you sure to delete this Member...");
    if (confirmation) {
        $.ajax({
            type: "post",
            url: '/ExecutiveMembers/DeteleMember/'+id,
            success: function (resonce) {
                alert("Record Deleted Successfuly..");
                window.location.reload();
            }
        })
    }
}