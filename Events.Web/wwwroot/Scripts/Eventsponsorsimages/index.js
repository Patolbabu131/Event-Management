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

        formData.append("EventId", $("#EventId").val());

       
        $.each($(".multipleimg"), function (i, obj) {
            $.each(obj.files, function (j, File) {
                formData.append('File[' + i + ']', File); // is the var i against the var j, because the i is incremental the j is ever 0
            });
        });
/*    formData.append("File", $("#File")[0].files[0]);*/

    $.ajax({
        type: "POST",
        url: '/Eventsponsorsimages/Create',
        data: formData,
        processData: false,
        contentType: false,
        success: function ConfirmDialog(message) {
            $("#addimage").modal('hide');
            CallDailog(message);
        }
    })
    }
}


function CallDailog(message) {
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
                $(this).dialog('destroy');
            },
            buttons: [
                {
                    text: "Ok",
                    icon: "ui-icon-heart",
                    click: function () {
                        $(this).dialog('destroy');
                        window.location.reload();
                    }
                }
            ]
        });
}



function Delete(id) {
    $('#image').appendTo('body')
        .html('<div id="dailog"><h6>' + "Are You Sure Want To Delete This Image ?... " + '</h6></div>')
        .dialog({
            modal: true,
            title: 'Delete Message',
            zIndex: 10000,
            autoOpen: true,
            width: 'auto',
            icon: 'fa fa- close',
            click: function () {
                $(this).dialog('destroy');
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
                                        $(this).dialog('destroy');
                                    },
                                    buttons: [
                                        {
                                            text: "Ok",
                                            icon: "ui-icon-heart",
                                            click: function () {
                                                $(this).dialog('destroy');

                                                window.location.reload();
                                            }
                                        }
                                    ]
                                });
                        }

                    })
                },
                No: function () {

                    $(this).dialog('destroy');
                }
            }
        });
}