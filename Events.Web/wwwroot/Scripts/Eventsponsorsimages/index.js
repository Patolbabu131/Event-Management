function imgtable(id) {
    datatable = $('#Eventsponsorsimagestable')
        .DataTable
        ({

            "sAjaxSource": "/Eventsponsorsimages/GetEventsponsorsimages/" + id,
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
                        return '<img src="/Files/'+row.sponsorImage+'" width="300px">';
                    }
                },
                {
                    render: function (data, type, row, meta) {
                        return ' <a class="btn btn-danger" onclick="Delete(' + row.id + ')" >Delete</a>';
                    }
                }
            ]
        });


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

function save_image() {

    $("#imageform").validate({
        rules: {
            File: "required",
        },
        messages: {
            File: " Please Select An Image",
        },
    });
    if ($('#imageform').valid()) {

    var formData = new FormData();
    var data = {
        EventId: $("#EventId").val(),
        File: $("#File")[0].files[0]
    }
    formData.append("EventId", $("#EventId").val());
    formData.append("File", $("#File")[0].files[0]);

    $.ajax({
        type: "POST",
        url: '/Eventsponsorsimages/Create',
        data: formData,
        processData: false,
        contentType: false,
        success: function ConfirmDialog(message) {
            $("#Editimage").modal('hide');
            $('#image').appendTo('body')
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



function Edit_i(id) {
    $.ajax({
        url: '/Eventsponsorsimages/Edit1',
        success: function (resonce) {
            $('#image').html(resonce);
            $("#Editimage").modal('show');

            $.ajax({
                type:"get",
                url: '/Eventsponsorsimages/Edit/' + id,
                success: function (resonce) {
                    $("#Id").val(resonce.id);
                    $("#EventId").val(resonce.eventId);
                    $("#simage").attr('src', resonce.sponsorImage);
                }
            })
        }
    })
}



function Delete(id) {
    $('#image').appendTo('body')
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
                        url: '/Eventsponsorsimages/Delete/' + id,
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