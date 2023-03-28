


function save_event() {
    $("#formAddEvent").validate({
        rules: {
            addEventName: {
                required: true,
                maxlength: 100
            },
            addEventDate: {
                required: true
            },
            addEventVenue: {
                required: true,
                maxlength: 500
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
            FoodMenu: descProduct
        }
        $.ajax({
            type: "post",
            url: '/Events/CreateEvents',
            data: data,
            success: function ConfirmDialog(message) {
                $("#addEventModal").modal('hide');
                $('#CreateContainer').appendTo('body')
                    .html('<div><h6>' + message + '</h6></div>')
                    .dialog({
                        modal: true,
                        title: 'Save Message',
                        zIndex: 10000,
                        autoOpen: true,
                        width: 'auto',
                        icon: 'fa fa- close',
                        click: function () {
                            $(this).dialog("close");
                        },
                        buttons: [
                            {
                                text: "Ok",
                                icon: "ui-icon-heart",
                                click: function () {
                                    $(this).dialog("close");
                                    window.location.reload();
                                }
                            }
                        ]
                    });
            }
        })
    }
}

function bindDatatable() {
    //if($.fn.dataTable.isDataTable('#tblevents')) {
    //    $('#tblevents').DataTable().destroy();
    //}
    $("#tblevents").empty();
    datatable = $('#tblevents')
        .DataTable
        ({
            "sAjaxSource": "/Events/GetEventList",
            "bServerSide": true,
            "bProcessing": true,
            "bSearchable": true,
            "filter": true,
            "autoWidth": true,
            "order": [[1, 'asc']],
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
                        const year = date.getFullYear();
                        const month = String(date.getMonth() + 1).padStart(2, '0');
                        const day = String(date.getDate()).padStart(2, '0');
                        const joined = [day, month, year].join('/');
                        return joined;

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
                        return (Time.getHours() < 10 ? '0' : '') + Time.getHours() + ":" + (Time.getMinutes() < 10 ? '0' : '') + Time.getMinutes();
                    }
                },
                {

                    "render": function (data, type, row) {
                       
                        //return "<span> 820, </br> Siddharth Complex, </br> R.C Datt road, Alkapuri, </br> Vadodara</span>";
                        return "<span> " + row.eventVenue.replaceAll("\n", "</br>") + "</span>";
                    }
                },
                {
                    render: function (data, type, row, meta) {
                        return ' <table><tr><td> <a class="btn btn-primary" onclick="details_event(' + row.id + ')" >Details</a></td></tr>  <tr><th> <a class="btn btn-info" onclick="edit_event(' + row.id + ')" >Edit</a></th></tr>  <tr><th> <a class="btn btn-danger" onclick="delete_event(' + row.id + ')" >Delete</a></th></tr></table>';
                    }
                },
                {
                    render: function (data, type, row, meta) {
                        return '<table><tr><td> <a class="btn btn-primary"  href="/Eventsponsors/Index/' + row.id + '"  >Sponsors</a> </td><td> <a class="btn btn-primary"   href="/Eventsponsorsimages/Index/' + row.id + '"   >Sponsors Images</a> </td></tr><tr><th> <a class="btn btn-primary"  href="/Eventcouponassignments/Index/' + row.id + '"  >Coupon</a> </th><td> <a class="btn btn-primary"  href="/Eventcoupontypes/Index/' + row.id + '" >Coupon Type</a> </td></tr><tr><th> <a class="btn btn-primary"   href="/Eventattendees/Index/' + row.id + '" >Attendees</a> </th><td> <a class="btn btn-primary" href="/Eventexpenses/Index/' + row.id + '" >Expenses</a> </td></tr></table>';
                    }
                },
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
                $("#addEventDate").datepicker({ dateFormat: 'dd/mm/yyyy' });
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
        })

    });

    setInterval(function () {
        $(".ui-timepicker-container").css("z-index", "33442");
    }, 100);

    $('#selectEl').change(function () {
        // set the window's location property to the value of the option the user has selected
        window.location = $(this).val();
    });

});
bindDatatable();

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
            $("#addEventDate").datepicker({ dateFormat: 'dd/mm/yy' });
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
            var today = (day) + "/" + (month) + "/" + now.getFullYear();

            var start = new Date(resonce.eventStartTime);
            var end = new Date(resonce.eventEndTime);
            var strTime = String(start.getHours()).padStart(2, '0') + ':' + String(start.getMinutes()).padStart(2, '0');

            var endtime = String(end.getHours()).padStart(2, '0') + ':' + String(end.getMinutes()).padStart(2, '0');


            $('#EventID').val(resonce.id);
            $('#addEventName').val(resonce.eventName);
            $('#addEventDate').val(today);
            $('#addEventVenue').val(resonce.eventVenue);
            $('#addStartTime').val(strTime);
            $('#addEndTime').val(endtime);
            $('#FoodMenu').val(resonce.foodMenu);
        }
    })
}



function delete_event(id) {
    $('#CreateContainer').appendTo('body')
        .html('<div id="dailog"><h6>' + "Are You Sure Want To Delete This Member ?... " + '</h6></div>')
        .dialog({
            modal: true,
            title: 'Delete Message',
            zIndex: 10000,
            autoOpen: true,
            width: 'auto',
            icon: 'fa fa- close',
            click: function () {
                $(this).dialog("close");
            },
            buttons: {
                Yes: function () {
                    $.ajax({
                        url: '/Events/DeteleEvent/' + id,
                        success: function () {
                            $('#dailog').appendTo('body')
                                .html('<div><h6>' + "Deleted Successfully ... " + '</h6></div>')
                                .dialog({
                                    modal: true,
                                    title: 'Delete Message',
                                    zIndex: 10000,
                                    autoOpen: true,
                                    width: 'auto',
                                    icon: 'fa fa- close',
                                    click: function () {
                                        $(this).dialog("close");
                                    },
                                    buttons: [
                                        {
                                            text: "Ok",
                                            icon: "ui-icon-heart",
                                            click: function () {
                                                $(this).dialog("close");
                                                window.location.reload();
                                            }
                                        }
                                    ]
                                });
                        }

                    })
                },
                No: function () {

                    $(this).dialog("close");
                }
            }
        });
}
