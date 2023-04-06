﻿$("#mySelect").change(function () {
   
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
      
                            var drop = '<select name = "list" class="selectmember form-control">'
                            drop += '<option value = "" disabled selected>Select Member</option>'
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
                        return '<a class="btn btn-info">Save</a>';
                    }
                },
            ]
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