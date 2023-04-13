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
                       
                        var drop = '<select name = "list" id="selectmember'+row.id+'" class="selectmember form-control form-select-sm " value="' + row.executiveMember +'" >'
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
                        return '<button class="btn btn-primary" onclick="update(' + row.id + ')">Save</button>';
                    }
                },
            ]

        });
    

})

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



