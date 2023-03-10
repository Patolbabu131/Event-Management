var Events = {

    variables: {
        oTable: null,
        srcGetEventsList: '/Events/GetEventList'
    },
    controls: {
        tblevents: '#tblevents',
        txteventsearchbox: "#txteventsearchbox",
        tblevents_Rows: '#tblevents tbody tr',
    },

    IntializeEventExpenseTable: function () {

        Events.variables.oTable = $(Events.controls.tblevents).dataTableEvents({
            "sAjaxSource": Events.variables.srcGetEventsList,
            "aaSorting": [[1, "desc"]],// default sorting
            "sDom": "frtlip",
            "autoWidth": false,
            "bLengthChange": true,
            "fixedHeader": true,
            "aoColumnDefs": [
                {
                    "aTargets": [0],
                    "className": "text-center",
                    "bSortable": false,
                    //"mRender": function (data, type, row, meta) {
                    //    return '<span data-ExpenseId=' + row[3] + '>' + parseInt(meta.row + meta.settings._iDisplayStart + 1) + '</span';
                    //},
                },
                {
                    "aTargets": [1],
                    "className": "text-center",
                },
                {
                    "aTargets": [2],
                    "className": "text-center",
                    //"mRender": function (data, type, full) {
                    //    return '<div>' + full[2] + '</div>';
                    //},
                },
                {
                    "aTargets": [3],
                    "className": "text-center",
                },
                {
                    "aTargets": [4],
                    "className": "text-center",
                },
                {
                    "aTargets": [5],
                    "className": "text-center",
                },
                {
                    "aTargets": [6],
                    "className": "text-center",
                },
                {
                    "aTargets": [7],
                    "className": "text-center",
                    //"mRender": function (data, type, full) {
                    //    var row = '';
                    //    row += '<a href= "javascript:void(0);" title="Remove Expense Details" onclick=Expenses.DeleteExpenseDetails(' + full[3] + ')> <i class="fa fa-times red"></i></a >';

                    //    return row;
                    //},
                    "bSortable": false,

                }
            ],

            "oLanguage": {
                "sEmptyTable": $('#hdnNodataavailable').val(),
                "sLengthMenu": "Page Size: _MENU_",
                "oPaginate": {
                    "sNext": $('#hdnNext').val(),
                    "sPrevious": $('#hdnPrevious').val()
                }
            },
            "fnServerParams": function (aoData) {

                $("div").data("srchParams",
                    [
                        { name: 'iDisplayLength', value: $("#hdnGeneralPageSize").val() },
                        { name: 'srchTxt', value: encodeURIComponent($(Events.controls.txteventsearchbox).val().trim() == '' ? '' : $(Events.controls.txteventsearchbox).val().trim()) },
                        { name: 'srchBy', value: 'ALL' },
                    ]);

                var srchParams = $("div").data("srchParams");
                if (srchParams) {
                    for (var i = 0; i < srchParams.length; i++) {
                        aoData.push({ "name": "" + srchParams[i].name + "", "value": "" + srchParams[i].value + "" });
                    }
                }
            },
            "fnInfoCallback": function (oSettings, iStart, iEnd, iMax, iTotal, sPre) {
                //smallTable();
                return common.pagingText(iStart, iEnd, iTotal, $('#hdnRecordsText').val(), oSettings._iDisplayLength);
            },
        });
    },



}

function save_event() {
    var data = {
        Id: $("#EventID").val(),
        EventName: $("#addEventName").val(),
        EventDate: $("#addEventDate").val(),
        EventVenue: $("#addEventVenue").val(),
        EventStartTime: $("#addStartTime").val(),
        EventEndTime: $("#addEndTime").val(),
        FoodMenu: $("").val(),
    }
    $.ajax({
        type: "post",
        url: '/Events/CreateEvents',
        data: data,
        success: function (resonce) {
            alert(resonce);
        }
    })
}

function bindDatatable() {
    datatable = $('#tblevents')
        .DataTable
        ({
            "sAjaxSource": "/Events/GetEventList",
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
                    "data": "eventName",
                },
                {
                    "render": function (data, type, row, meta) {
                        var date = new Date(row.eventDate);
                        var month = date.getMonth();
                        return date.getDay() + "/" + date.getMonth() + "/" + date.getFullYear();
                    }
                },
                {
                    "data": "eventVenue",
                },
                {
                    "render": function (data, type, row, meta) {
                        var Time = new Date(row.eventStartTime);
                        return Time.getHours() + ":" + Time.getMinutes();
                    }
                },
                {   
                    "render": function (data, type, row, meta) {
                        var Time = new Date(row.eventEndTime);
                        return Time.getHours() + ":" + Time.getMinutes();
                    }
                },
                {
                    "render": function (data, type, row, meta) {
                        var date = new Date(row.eventDate);
                        var month = date.getMonth();
                        return  date.getFullYear();
                    }
                },
                {
                    "data": "foodMenu",
                    //render: function (data, type, row, meta) {

                    //    return row.eventEndTime
                    //}
                },
                {
                    render: function (data, type, row, meta) {
                        return ' <table><tr><td> <a class="btn btn-primary" onclick="details_event(' + row.id + ')" >Details</a></td></tr>  <tr><th> <a class="btn btn-info" onclick="edit_event(' + row.id + ')" >Edit</a></th></tr>  <tr><th> <a class="btn btn-danger" onclick="delete_event(' + row.id + ')" >Delete</a></th></tr></table>';
                    }
                },
                {
                    render: function (data, type, row, meta)
                    {
                        return '<table><tr><th> <a class="btn btn-primary"   href="/eventattendees/Index/' + row.id + '" >Attendees</a> </th> <th> <a class="btn btn-primary" onclick="Index_Couponassignments(' + row.id + ')" >Coupon</a> </th></tr>  <tr><td> <a class="btn btn-primary" onclick="Index_CType(' + row.id + ')" >Coupon Type</a> </td><td> <a class="btn btn-primary" onclick="Index_SImage(' + row.id + ')" >Sponsors Images</a> </td></tr>  <tr><td> <a class="btn btn-primary" onclick="Index_Sponsors(' + row.id + ')" >Sponsors</a> </td>  <td> <a class="btn btn-primary" onclick="Index_Expenses(' + row.id + ')" >Expenses</a> </td></tr></table>';
                    }   
                }
            ]
        });

   

}

$(document).ready(function () {

    $('#create_event').click(function () {
        $.ajax({
            type: "get",
            url: '/Events/CreateEvent',
            success: function (resonce) {
                $('#CreateContainer').html(resonce);
                $("#addEventModal").modal('show');
            }
        })
    });
    bindDatatable();

    $('#selectEl').change(function () {
        // set the window's location property to the value of the option the user has selected
        window.location = $(this).val();

    });


   
});

function details_event(id) {
    $.ajax({
        type: "post",
        data: id,
        url: '/Events/EventsDetails/' + id,
        success: function (resonce) {
            $('#CreateContainer').html(resonce);
            $("#DetailsEventsModal").modal('show');
        }
    })
}

function edit_event(id) {
    $.ajax({
        type: "get",
        url: '/Events/CreateEvent',
        success: function (resonce) {
            $('#CreateContainer').html(resonce);
            $("#addEventModal").modal('show');
        }
    })

    $.ajax({
        type: "post",
        data: id,
        url: '/Events/Edit/' + id,
        success: function (resonce) {
            var now = new Date(resonce.eventDate);
            var day = ("0" + now.getDate()).slice(-2);
            var month = ("0" + (now.getMonth() + 1)).slice(-2);
            var today = now.getFullYear() + "-" + (month) + "-" + (day);

            var start = new Date(resonce.eventStartTime);
            var end = new Date(resonce.eventEndTime);
            var strTime = start.getHours() + ':' + start.getMinutes();

            var ampm = "am";
            if (start.getHours > 12) {
                start.getHours -= 12;
                ampm = "pm";
            }
           // = end.getHours() + ':' + end.getMinutes() + ampm;
            var endtime = (end.getHours() || '00') + ':' + (end.getMinutes() || '00');

            $('#EventID').val(resonce.id);
            $('#addEventName').val(resonce.eventName);
            $('#addEventDate').val(today);
            $('#addEventVenue').val(resonce.eventVenue);
            $('#addStartTime').val(strTime);
            $('#addEndTime').val(endtime);
    


        }
    })
}

function delete_event(id) {
    var confirmation = confirm("Are you sure to delete this Member...");
    if (confirmation) {
        $.ajax({
            type: "post",
            url: '/Events/DeteleEvent/' + id,
            success: function (resonce) {
                alert("Record Deleted Successfuly..");
                window.location.reload();
            }
        })
    }
}


function eventattendeestable() {
    $.ajax({

        url: '/Eventattendees/Index',
     
    });


    //datatable = $('#Eventattendeestable')
    //    .DataTable
    //    ({
    //        "sAjaxSource": "/Events/GetEventattendees/" + EID,
    //        "bServerSide": true,
    //        "bProcessing": true,
    //        "bSearchable": true,
    //        "filter": true,
    //        "language": {
    //            "emptyTable": "No record found.",
    //            "processing":
    //                '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> '
    //        },
    //        "columns": [
    //            {
    //                "data": "id",

    //            },
    //            {
    //                "data": "eventId",

    //            },
    //            {
    //                "data": 'sponsorImage',
    //                "render": function (data, type, row, meta) {
    //                    return '<img src="' + row.sponsorImage + '" width="40px">';
    //                }
    //            }
    //            //{
    //            //    "data": "duties",
    //            //    render: function (data, type, row, meta) {
    //            //        return row.duties
    //            //    }
    //            //},
    //            //{
    //            //    render: function (data, type, row, meta) {
    //            //        return ' <a class="btn btn-primary" onclick="details_member(' + row.id + ')" >Details</a> |  <a class="btn btn-info"  onclick="edit_member(' + row.id + ')" >Edit</a> |  <a class="btn btn-danger" onclick="delete_member(' + row.id + ')" >Delete</a>';
    //            //    }
    //            //}
    //        ]
    //    });

}