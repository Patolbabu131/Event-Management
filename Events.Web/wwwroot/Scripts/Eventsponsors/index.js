﻿function Loadsponsor(id) {
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

function save_Sponsors() {
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
        success: function (resonce) {
            alert(resonce);
            window.location.reload();
        }
    })
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


function Delete(id) {
    var confirmation = confirm("Are you sure to delete this Member...");
    if (confirmation) {
        $.ajax({
            url: '/Eventsponsors/Delete/    ' + id,
            success: function (resonce) {
                alert("Record Deleted Successfuly..");
                window.location.reload();
            }
        })
    }
}