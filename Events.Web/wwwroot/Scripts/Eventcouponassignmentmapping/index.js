$("#mySelect").change(function () { 
    var x = document.getElementById("mySelect").value;
    datatable = $('#Cassignmentmappingtable')
        .DataTable
        ({
            "sAjaxSource": "/Eventcouponassignmentmappings/Getctype/" + x,
            "bServerSide": true,
            destroy: true,
            "bProcessing": true,
            "bSearchable": true,
            "filter": true,
            "autoWidth": true,
            "language": {
                "emptyTable":
                    "No record found.",
                "processing": '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> '
            },
            "columns": [
                {
                    "data": "couponNumber",
                },
                {
                    data: function (row, type, set) {

                        var drop = '<select name = "list"  id="member_' + row.id + '" class="selectmember form-control form-select-sm " value="' + row.user + '" >'
                        drop += '<option value = "" disabled selected>Select Member</option>'
                        $.each(memberslist, function (i, v) {
                            if (v.id == row.User) {
                                drop += '<option value="' + v.Id + '">' + v.FullName + '</option>';
                            }
                            else {
                                drop += '<option value="' + v.Id + '"selected>' + v.FullName + '</option>';
                            }

                        })
                        return drop;
                    }

                },
                {
                    data: function (row, type, set) {
                        if (row.attendee.length === 0) {
                            return "_____________";
                        }
                        else {
                            return row.attendee;
                        }
                    }
                },

                {
                    "data": "booked",
                },

                {
                    render: function (data, type, row, meta) {
                       
                        if (row.booked === "true") {
                            return '<button class=" button-31" id="savebtn" onclick="update(' + row.id + ')" disabled>Save</button>';
                        } else {
                            return '<button class=" button-31" id="savebtn" onclick="update(' + row.id + ')">Save</button>';
                        }
                    }
                },

                {
                    data: "active",
                    render: function (data, type, row) {

                        if (row.booked === "true") {
                            return '<input type="checkbox" class="checkbox editor-active save_value" id="chkddl_' + row.id + '"  value="' + row.id + '" disabled>';
                        } else {
                            return '<input type="checkbox" class="checkbox editor-active save_value" id="chkddl_' + row.id + '" value="' + row.id + '">';
                        }
                           

                    },
                    className: "dt-body-center"
                },

            ],

        });

})

function selectexmember() {
    $.ajax({
        type: "get",
        url: '/Eventcouponassignmentmappings/getmembers',
        success: function (members) {
            $.each(members, function (index, value) {
                $('.selectmember').append($('<option>').val(value.id).text(value.fullName));
            });
        }
    })
}


var update = function (id) {

    var cno = $("#member_" + id).val();

    var data = {
        Id: id,
        User: cno
    }
    $.ajax({
        type: 'post',
        url: "/Eventcouponassignmentmappings/Edit",
        data: data,
        success: function (result) {
            $("#mySelect").trigger("change");
        },
        error: function (xhr) {
            CallDialog('Request Status: ' + xhr.status + ' Status Text: ' + xhr.statusText + ' ' + xhr.responseText);
        }
    });
}

function dropdownval() {
    if ($(".selectmultiplemember").val()===null) {
        CallDialog("Select Executive Member");
    } 
    else {
        var ExeVal = $("#DDL").val();

        var val = '';
        $(':checkbox:checked').each(function (i) {
            val = (val == "" ? "" : val + ",") + $(this).val();
        });


        var data = {
            User: ExeVal,
            strids: val

        }
        $.ajax({
            type: 'post',
            url: "/Eventcouponassignmentmappings/Edit2",
            data: data,
            success: function (result) {
                CallDialog(result);
                $("#mySelect").trigger("change");
            },
            error: function (xhr) {
                CallDialog('Request Status: ' + xhr.status + ' Status Text: ' + xhr.statusText + ' ' + xhr.responseText);
            }

        });
    }

}

function CallDialog(message) {
    $('#Cassign').appendTo('body')
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
                        $("#mySelect").trigger("change");
                    }
                }
            ]
        });

}