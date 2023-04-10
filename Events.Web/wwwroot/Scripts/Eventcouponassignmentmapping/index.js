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

                        var drop = '<select name = "list" id="member_' + row.id + '" class="selectmember form-control">'
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
                        return '<button class="button-31" onclick="update(' + row.id + ')">Save</button>';
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
                location.reload();
            },
            error: function (xhr) {
                alert('Request Status: ' + xhr.status + ' Status Text: ' + xhr.statusText + ' ' + xhr.responseText);
            }

        });
    }
