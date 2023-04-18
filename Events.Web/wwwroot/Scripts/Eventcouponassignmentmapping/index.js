

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

                        var drop = '<select name = "list"  id="member_' + row.id + '" class="selectmember form-control form-select-sm " value="' + row.executiveMember + '" >'
                        drop += '<option value = "" disabled selected>Select Member</option>'
                        $.each(memberslist, function (i, v) {
                            if (v.id == row.executiveMember) {
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
                        if (row.attendee == null) {
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
                        return '<button class="button-31" onclick="update(' + row.id + ')">Save</button>';
                    }
                },

                {
                    data: "active",
                    render: function (data, type, row) {
                        if (type === 'display') {
                            return '<input type="checkbox" class="editor-active save_value" id="chkddl_' + row.id + '" onclick="Enableddl_(' + row.id + ')" value="' + row.id + '">';
                        }
                        return data;
                    },
                    className: "dt-body-center"
                },

            ],

        });

    selectmember();
})
function selectmember() {
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


function Enableddl_(id) {
    var chkddl_ = document.getElementById("chkddl_" + id);
    var ddl = document.getElementById("DDL");
    ddl.disabled = chkddl_.checked ? false : true;
    if (!ddl.disabled) {
        ddl.focus();
    }

}



var update = function (id) {


    var cno = $("#member_" + id).val();

    var data = {
        Id: id,
        ExecutiveMember: cno
    }
    $.ajax({
        type: 'post',
        url: "/Eventcouponassignmentmappings/Edit",
        data: data,
        success: function (result) {
            alert(result);

        },
        error: function (xhr) {
            alert('Request Status: ' + xhr.status + ' Status Text: ' + xhr.statusText + ' ' + xhr.responseText);
        }

    });
}



function dropdownval() {
    var ExeVal = $("#DDL").val();

    var val = '';
    $(':checkbox:checked').each(function (i) {
        val = (val == "" ? "" : val + "," ) + $(this).val();
    });

    
    var data = {
        ExecutiveMember: ExeVal,
        strids: val

    }
    $.ajax({
        type: 'post',
        url: "/Eventcouponassignmentmappings/Edit2",
        data: data,
        success: function (result) {
            alert(result);
            location.reload(); 

        },
        error: function (xhr) {
            alert('Request Status: ' + xhr.status + ' Status Text: ' + xhr.statusText + ' ' + xhr.responseText);
        }

    });

}

var update = function (id) {


    var cno = $("#selectmember" + id).val();

    var data = {
        Id: id,
        ExecutiveMember: cno
    }
    $.ajax({
        type: 'post',
        url: "/Eventcouponassignmentmappings/Edit",
        data: data,
        success: function (result) {
            $("#mySelect").trigger("change");
        },
        error: function (xhr) {
            alert('Request Status: ' + xhr.status + ' Status Text: ' + xhr.statusText + ' ' + xhr.responseText);
        }

    });
}



