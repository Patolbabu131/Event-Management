$(document).ready(function () {
    bindmember();

});

$('#create_member').click(function () {
    $.ajax({
        type: "post",
        url: '/Users/CreateMember',
        success: function (resonce) {
            $('#member').html(resonce);
            $("#addECMemberModal").modal('show');
            $("#addAppointedOn").datepicker();
        }
    })
});





function save_member() {
    $("#formAddECMember").validate({
        rules: {
            addName: "required",
            addDesignation: {
                required: true,
            },
            PurchasedOn: {
                required: true,
            },
            addAppointedOn: {
                required: true,
            },
            addDuties: {
                required: true,
            },
            LoginName: {
                required: true,
            },
            adddPassword: {
                required: true,
            },
            addRole: {
                required: true,
            },

        },
        messages: {
            addName: " Please enter Name",
            addDesignation: {
                required: " Please enter Designation",
            },
            addAppointedOn: {
                required: " Please AppointedOn ",
            },
            addDuties: "Please enter Duties",
            LoginName: "Please enter LoginName",
            adddPassword: "Please enter valide email",
            addRole: "Please select Role",
        },
        highlight: function (element) {
            $(element).parent().addClass('error')
        },
        unhighlight: function (element) {
            $(element).parent().removeClass('error')
        }
    });
    if ($('#formAddECMember').valid()) {
        if ($("#addActive").is(':checked')) {
            $("#addActive").attr('value', 'true');
        } else {
            $("#addActive").attr('value', 'false');
        }
        var data = {
            Id: $("#EventId").val(),
            FullName: $("#addrName").val(),
            Designation: $("#addDesignation").val(),
            AppointedOn: $("#addAppointedOn").val(),                    
            Duties: $("#addDuties").val(),
            loginName: $("#addLoginName").val(),
            password: $("#addPassword").val(),
            Role: $("#addRole").val(),
            Active: $("#addActive").val(),
        }

        $.ajax({
            type: "post",
            url: '/Users/CreateMembers',
            data: data,

            success: function
                ConfirmDialog(message) {
                $('<div></div>').appendTo('body')
                    .html('<div><h6>' + message + '?</h6></div>')
                    .dialog({
                        modal: true,
                        title: 'Events Data Is Saved...',
                        zIndex: 10000,
                        autoOpen: true,
                        width: 'auto',
                        resizable: false,
                        close: function (event, ui) {
                            $(this).remove();
                            window.location.reload();
                        }
                    })
                window.location.reload(); 
            }
                   
        })


    }
}



function bindmember() {
    datatable = $('#tblecmembers')
        .DataTable
        ({

            "sAjaxSource": "/Users/GetECMember",
            "bServerSide": true,
            "bProcessing": true,
            "bSearchable": true,
            "filter": true,
            "language": {
                "emptyTable": "No record found.",
                "processing": '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> '
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

                    "data": "appointedOn",
                    "render": function (data, type, row, meta) {
                        var date = new Date(row.appointedOn);
                        const year = date.getFullYear();
                        const month = String(date.getMonth() + 1).padStart(2, '0');
                        const day = String(date.getDate()).padStart(2, '0');
                        const joined = [day, month, year].join('/');
                        return joined;
                    }
                },
                {
                    "data": "duties",
                    render: function (data, type, row, meta) {
                        return row.duties
                    }
                },
                {
                    "data": "loginName",
                    render: function (data, type, row, meta) {
                        return row.loginName
                    }
                },
                {
                    "data": "role",
                    render: function (data, type, row, meta) {
                        return row.role
                    }
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
                        return '<a class="btn btn-info"  onclick="edit_member(' + row.id + ')" >Edit</a> ';
                    }
                }
            ]
        });

}

function details_member(id) {
    $.ajax({
        type: "post",
        data: id,
        url: '/Users/ECMemberDetails/' + id,
        success: function (resonce) {
            $('#member').html(resonce);
            $("#DetailsECMemberModal").modal('show');
        }
    })
}

function edit_member(id) {
    $.ajax({
        type: "post",
        url: '/Users/CreateMember',
        success: function (resonce) {
            $('#member').html(resonce);
            $("#addECMemberModal").modal('show');
            $("#addAppointedOn").datepicker();

            $.ajax({
                type: "post",
                url: '/Users/GetEdit/' + id,
                success: function (resonce) {

                    var now = new Date(resonce.appointedOn);
                    var day = ("0" + now.getDate()).slice(-2);
                    var month = ("0" + (now.getMonth() + 1)).slice(-2);
                    var today = now.getFullYear() + "-" + (month) + "-" + (day);



                    $('#EventId').val(resonce.id);
                    $('#addrName').val(resonce.fullName);
                    $('#addDesignation').val(resonce.designation);
                    $('#addAppointedOn').val(today);
                    $('#addDuties').val(resonce.duties);
                    $("#addLoginName").val(resonce.loginName),
                    $("#addPassword").val(resonce.password),
                    $("#addRole").val(resonce.role),
                    $("#addActive").prop("checked", resonce.active)
                }
            })
        }
    })


}

function delete_member(id) {
    var confirmation = confirm("Are you sure to delete this Member...");
    if (confirmation) {
        $.ajax({
            type: "post",
            url: '/Users/DeteleMember/' + id,
            success: function (resonce) {
                alert("Record Deleted Successfuly..");
                window.location.reload();
            }
        })
    }
}
