$(document).ready(function () {

    datatable = $('#Eventsponsorsimagestable')
        .DataTable
        ({

            "sAjaxSource": "/Eventsponsorsimages/GetEventsponsorsimages",
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
                        return '<img src="'+row.sponsorImage+'" width="40px">';
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


});
