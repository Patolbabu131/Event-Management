function Loadsponsor(id) {
    datatable = $('#sTable')
        .DataTable
        ({
            "sAjaxSource": "/Eventsponsors/Getsponsors/" + id,
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
                    "data": "id",
                },
                {
                    "data": "eventId",
                },
                {
                    "data": "sponsorName",
                },
                {
                    "data": "sponsorOrganization",
                },
                {
                    "data": "amountSponsored",
                },
                {
                 
                    "data": "createdOn",
                    "render": function (data) {
                        var date = new Date(data);
                        var month = date.getMonth() + 1;
                        return (month.toString().length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                    }
                },
                {
                    "data": "createdBy",
                },
                {
                    "data": "modifiedBy",
                },
                {
                    "data": "modifiedOn",
                    "render": function (data) {
                        var date = new Date(data);
                        var month = date.getMonth() + 1;
                        return (month.toString().length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                    }
                },
                {
                    render: function (data, type, row, meta) {
                        return ' <a class="btn btn-info" onclick="edit_sponsors(' + row.id + ')" >Edit</a> | <a class="btn btn-danger" onclick="Delete(' + row.id + ')" >Delete</a>';
                    }
                },
            ]
        });
}


function create_sponsors(id) {
    $.ajax({
        type: "get",
        url: '/Eventsponsors/CreateEdit/' + id,
        success: function (resonce) {
            $('#sponsors').html(resonce);
            $("#addsponsors").modal('show');
        }
    })
}

/*---Deepti---*/

function save_Sponsors() {

    $("#formAddEvent").validate({
        rules: {
            SponsorName: {
                required: true,
                maxlength: 200
            },
            SponsorOrganization: {
                required: true,
                maxlength: 200
            },
            AmountSponsored: {
                required: true,
                number: true
            },
            CreatedOn: {
                required: true
            },
        },
        messages: {
            SponsorName: {
                required: "Sponsor Name is a required field!!!"
            },
            SponsorOrganization: {
                required: "Sponsor Organization is a required field!!!"
            },
            AmountSponsored: {
                required: "Amount Sponsored is a required field!!!"
            },
            CreatedOn: {
                required: "Created On is a required field!!!"
            },
        }
    });

    if ($('#formAddEvent').valid()) {
        var data = {
            Id: $("#ID").val(),
            EventId: $("#EventId").val(),
            SponsorName: $("#SponsorName").val(),
            SponsorOrganization: $("#SponsorOrganization").val(),
            AmountSponsored: $("#AmountSponsored").val(),
            CreatedOn: $("#CreatedOn").val(),
            CreatedBy: $("#CreatedBy").val(),
        }
        $.ajax({
            type: "post",
            url: '/Eventsponsors/CreateEdit',
            data: data,
            success: function ConfirmDialog(message)
            {
                $("#addsponsors").modal('hide');
                $('#sponsors').appendTo('body')
                    .html('<div><h6>' + message + '</h6></div>')
                    .dialog({
                        modal: true,
                        title: 'Save Message',
                        zIndex: 10000,
                        autoOpen: true,
                        width: 'auto',
                        icon: 'fa fa- close',
                        click: function ()
                        {
                            $(this).dialog("close");
                        },
                        buttons: [
                            {
                                text: "Ok",
                                icon: "ui-icon-heart",
                                click: function ()
                                {
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


function edit_sponsors(id) {
    $.ajax({
        type: "get",
        url: '/Eventsponsors/CreateEdit',
        success: function (resonce) {
            $('#sponsors').html(resonce);
            $("#addsponsors").modal('show');


            $.ajax({
                type: "get",
                url: '/Eventsponsors/Edit/' + id,
                success: function (resonce) {
                    var now = new Date(resonce.createdOn);
                    var day = ("0" + now.getDate()).slice(-2);
                    var month = ("0" + (now.getMonth() + 1)).slice(-2);
                    var today = now.getFullYear() + "-" + month + "-" + day;

                    $("#ID").val(resonce.id);
                    $("#EventId").val(resonce.eventId);
                    $("#SponsorName").val(resonce.sponsorName);
                    $("#SponsorOrganization").val(resonce.sponsorOrganization);
                    $("#AmountSponsored").val(resonce.amountSponsored);
                    $("#CreatedOn").val(today);
                    $("#CreatedBy").val(resonce.createdBy);     
                }
            })
        }
    })

}

// original

//function Delete(id) {
//    var confirmation = confirm("Are you sure to delete this Member...");
//    if (confirmation) {
//        $.ajax({
//            url: '/Eventsponsors/Delete/    ' + id,
//            success: function (resonce) {
//                alert("Record Deleted Successfuly..");
//                window.location.reload();
//            }
//        })
//    }
//}


function Delete(id) {   
    $('#sponsors').appendTo('body')
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
                                url: '/Eventsponsors/Delete/' + id,
                                success: function ()
                                {
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