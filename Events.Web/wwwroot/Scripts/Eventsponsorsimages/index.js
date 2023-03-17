function imgtable(id) { 
    datatable = $('#Eventsponsorsimagestable')
        .DataTable
        ({

            "sAjaxSource": "/Eventsponsorsimages/GetEventsponsorsimages/"+id,
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
                    "data": "id",
                   
                },
                {
                    "data": "eventId",  
                    
                },
                {
                    "data": 'sponsorImage',
                    "render": function (data, type, row, meta) {
                        return '<img src="'+row.sponsorImage+'" width="300px">';
                    }
                },
                {
                    render: function (data, type, row, meta) {
                        return '<a class="btn btn-info" onclick="Edit_i(' + row.id + ')" >Edit</a> | <a class="btn btn-danger" onclick="Delete(' + row.id + ')" >Delete</a>';
                    }
                }
                //{
                //    "data": "duties",
                //    render: function (data, type, row, meta) {
                //        return row.duties
                //    }
                //},
                //{
                //    render: function (data, type, row, meta) {
                //        return ' <a class="btn btn-primary" onclick="details_member(' + row.id + ')" >Details</a> |  <a class="btn btn-info"  onclick="edit_member(' + row.id + ')" >Edit</a> |  <a class="btn btn-danger" onclick="delete_member(' + row.id + ')" >Delete</a>';
                //    }
                //}
            ]
        });


}

function Delete(id) {
    var confirmation = confirm("Are you sure to delete this Member...");
    if (confirmation) {
        $.ajax({
            type: "get",
            url: '/Eventsponsorsimages/Delete/' + id,
            success: function (responce) {
                alert(responce);
                window.location.reload();
            }
        })
    }
}

function create_i(id) {
    $.ajax({
        url: '/Eventsponsorsimages/Create/' + id,
        success: function (resonce) {
            $('#image').html(resonce);
            $("#addimage").modal('show');

        }
    })
}


function Edit_i(id) {
    $.ajax({
        url: '/Eventsponsorsimages/Edit/' + id,
        success: function (resonce) {
            $('#image').html(resonce);
            $("#Editimage").modal('show');

        }
    })
}
