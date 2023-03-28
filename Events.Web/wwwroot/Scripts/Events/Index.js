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
    $("#formAddEvent").validate({
        rules: {
            addEventName: {
                required: true,
            },
            addEventDate: {
                required: true
            },
            addEventVenue: {
                required: true
            },
            addStartTime: {
                required: true
            },
            addEndTime: {
                required: true
            },
        },
        messages: {
            addEventName: {
                required: "Event Name is a required field!!!"
            },
            addEventDate: {
                required: "Event Date is a required field!!!"
            },
            addEventVenue: {
                required: "Event Venue is a required field!!!"
            },
            addStartTime: {
                required: "Start Time is a required field!!!"
            },
            addEndTime: {
                required: "End Time is a required field!!!"
            },
        }
    });

    
    if ($('#formAddEvent').valid()) {
                    
        CKEDITOR.instances.FoodMenu.updateElement();
        var descProduct = document.getElementById('FoodMenu').value;

        var data = {
            Id: $("#EventID").val(),
            EventName: $("#addEventName").val(),
            EventDate: $("#addEventDate").val(),
            EventVenue: $("#addEventVenue").val(),
            EventStartTime: $("#addStartTime").val(),
            EventEndTime: $("#addEndTime").val(),
            FoodMenu:descProduct
        }
        $.ajax({
            type: "post",
            url: '/Events/CreateEvents',
            data: data,
            success: function (resonce) {
                alert(resonce);
                window.location.reload();
            }
        })
    }
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
            "order": [[2, 'asc']],
            "language": {
                "emptyTable": "No record found.",
                "processing":
                    '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> '
            },
            "columns": [
               
                {
<<<<<<< HEAD
=======
                    "data": "id"
                },
                {
>>>>>>> origin/rujal
                    "data": "eventName",
                },
                {
                    "data": "eventDate",
                    "render": function (data) {
                        var date = new Date(data);
                        var month = date.getMonth() + 1;
                        return (month.toString().length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                    }
                },
                {
                    "render": function (data, type, row, meta) {
                        var Time = new Date(row.eventStartTime);
                        return (Time.getHours() < 10 ? '0' : '') + Time.getHours() + ":" + (Time.getMinutes() < 10 ? '0' : '') + Time.getMinutes();
                    }
                },
                {
                    "render": function (data, type, row, meta) {
                        var Time = new Date(row.eventEndTime)
                        return (Time.getHours() < 10 ? '0' : '') + Time.getHours() +":"+(Time.getMinutes() < 10 ? '0' : '') + Time.getMinutes();
                    }
                },
                {
<<<<<<< HEAD
                    "data": "eventVenue",
=======

                    "render": function (data, type, row, meta) {
                        var date = new Date(row.eventDate);
                        var month = date.getMonth();
                        return date.getFullYear();
                    }
                },
                {
                    "data": "foodMenu",
                    //render: function (data, type, row, meta) {

                    //    return row.eventEndTime
                    //}
>>>>>>> origin/rujal
                },
                {
                    render: function (data, type, row, meta) {
                        return ' <table><tr><td> <a class="btn btn-primary" onclick="details_event(' + row.id + ')" >Details</a></td></tr>  <tr><th> <a class="btn btn-info" onclick="edit_event(' + row.id + ')" >Edit</a></th></tr>  <tr><th> <a class="btn btn-danger" onclick="delete_event(' + row.id + ')" >Delete</a></th></tr></table>';
                    }
                },
                {
                    render: function (data, type, row, meta) {
<<<<<<< HEAD
                        return '<table><tr><td> <a class="btn btn-primary"  href="/Eventsponsors/Index/' + row.id + '"  >Sponsors</a> </td><td> <a class="btn btn-primary"   href="/Eventsponsorsimages/Index/' + row.id + '"   >Sponsors Images</a> </td></tr><tr><th> <a class="btn btn-primary"  href="/Eventcouponassignments/Index/' + row.id + '"  >Coupon</a> </th><td> <a class="btn btn-primary"  href="/Eventcoupontypes/Index/' + row.id + '" >Coupon Type</a> </td></tr><tr><th> <a class="btn btn-primary"   href="/Eventattendees/Index/' + row.id + '" >Attendees</a> </th><td> <a class="btn btn-primary" href="/Eventexpenses/Index/' + row.id + '" >Expenses</a> </td></tr></table>';
                    }
                },
=======
                        return '<table><tr><th> <a class="btn btn-primary"   href="/Eventattendees/Index/' + row.id + '" >Attendees</a> </th> <th> <a class="btn btn-primary"  href="/Eventcouponassignments/Index/' + row.id + '"  >Coupon</a> </th></tr>  <tr><td> <a class="btn btn-primary"  href="/Eventcoupontypes/Index/' + row.id + '" >Coupon Type</a> </td><td> <a class="btn btn-primary"   href="/Eventsponsorsimages/Index/' + row.id + '"   >Sponsors Images</a> </td></tr>  <tr><td> <a class="btn btn-primary"  href="/Eventsponsors/Index/' + row.id + '"  >Sponsors</a> </td>  <td> <a class="btn btn-primary" href="/Eventexpenses/Index/' + row.id + '" >Expenses</a> </td></tr></table>';
                    }
                }
>>>>>>> origin/rujal
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
                $("#addEventDate").datepicker();
                $('#addStartTime').timepicker({
                    timeFormat: 'HH:mm',
                    dynamic: true,
                    dropdown: true,
                    scrollbar: true
                });
                $('#addEndTime').timepicker({
                    timeFormat: 'HH:mm',
                    dynamic: true,
                    dropdown: true,
                    scrollbar: true
                });
                CKEDITOR.replace('FoodMenu', {
                    toolbar: [
                     
                        { name: 'basicstyles', groups: ['basicstyles', 'cleanup'], items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
                        { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'], items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl', 'Language'] },
                        { name: 'links', items: ['Link', 'Anchor'] },
                        { name: 'insert', items: ['Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe'] },
                        { name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
                        { name: 'colors', items: ['TextColor', 'BGColor'] },
                        { name: 'tools', items: ['Maximize', 'ShowBlocks'] },
                        { name: 'others', items: ['-'] }
                    ]
                });
            }
<<<<<<< HEAD

        })


    });
    bindDatatable();

    function ok() {
        $('#addStartTime').timepicker({
            timeFormat: 'h:mm p',
            interval: 60,
            minTime: '10',
            maxTime: '6:00pm',
            defaultTime: '11',
            startTime: '10:00',
            dynamic: false,
            dropdown: true,
            scrollbar: true
        });

    }

=======
        })
    });
    bindDatatable();
 
    setInterval(function () {
        $(".ui-timepicker-container").css("z-index", "33442");
    }, 100);

>>>>>>> origin/rujal
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
            $('.modal-title').text('Edit Event Details');
            $("#addEventDate").datepicker();
            $('#addStartTime').timepicker({
                timeFormat: 'HH:mm',
                dynamic: true,
                dropdown: true,
                scrollbar: true
            });
            $('#addEndTime').timepicker({
                timeFormat: 'HH:mm',
                dynamic: true,
                dropdown: true,
                scrollbar: true
            });
            CKEDITOR.replace('FoodMenu', {
                toolbar: [

                    { name: 'basicstyles', groups: ['basicstyles', 'cleanup'], items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
                    { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'], items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl', 'Language'] },
                    { name: 'links', items: ['Link', 'Anchor'] },
                    { name: 'insert', items: ['Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe'] },

                    { name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
                    { name: 'colors', items: ['TextColor', 'BGColor'] },
                    { name: 'tools', items: ['Maximize', 'ShowBlocks'] },
                    { name: 'others', items: ['-'] }
                ]
            });
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
            var today = now.getFullYear() + "/" + (month) + "/" + (day);

            var start = new Date(resonce.eventStartTime);
            var end = new Date(resonce.eventEndTime);
<<<<<<< HEAD
            //var strTime = start.getHours() + ':' + caches.getMinutes();
            var strTime = (start.getHours || '00') + ':' + (start.getMinutess || '00');
            var ampm = "am";
            if (start.getHours > 12) {
                start.getHours -= 12;
                ampm = "pm";
            }
            // = end.getHours() + ':' + end.getMinutes() + ampm;
=======
            var strTime = start.getHours() + ':' + start.getMinutes();
>>>>>>> origin/rujal
            var endtime = (end.getHours() || '00') + ':' + (end.getMinutes() || '00');

            $('#EventID').val(resonce.id);
            $('#addEventName').val(resonce.eventName);
            $('#addEventDate').val(today);
            $('#addEventVenue').val(resonce.eventVenue);
            $('#addStartTime').val(strTime);
            $('#addEndTime').val(endtime);
<<<<<<< HEAD



=======
            $('#FoodMenu').val(resonce.foodMenu);
>>>>>>> origin/rujal
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
                alert(resonce);
                window.location.reload();
            }
        })
    }
}


<<<<<<< HEAD
=======
//function eventattendeestable() {
//      $.ajax({

//        url: '/Eventattendees/Index',

//    });


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
>>>>>>> origin/rujal

