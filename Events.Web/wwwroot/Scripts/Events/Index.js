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
    //var img = new FormData;
    var files = $("#addFileMultiple").get(0).files;
    
    //if (files.length > 0) {
    //    data.append("MyImages", files[0]);
    //}


    var data = {
        EventName: $("#addEventName").val(),
        EventDate: $("#addEventDate").val(),
        EventVenue: $("#addEventVenue").val(),
        EventStartTime: $("#addStartTime").val(),
        EventEndTime: $("#addEndTime").val(),
        FoodMenu: $("").val(),
        //Eventsponsorsimages:files    
    }

    var formData = new FormData();

    formData.append("data",data);
   
    formData.append("base64image", files);
    
    $.ajax({
        type: "post",
        url: '/Events/CreateEvents',
        data: formData,
        success: function (resonce) {
            alert("ok");

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
            "language": {
                "emptyTable": "No record found.",
                "processing":
                    '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> '
            },
            "columns": [
                {
                    "data": "eventeame",
                    render: function (data, type, row, meta) {
                        return row.eventName
                    }
                },
                {
                    "data": "eventdate",
                    render: function (data, type, row, meta) {
                        return row.eventDate
                    }
                },
                {
                    "data": "eventdate",
                    render: function (data, type, row, meta) {
                        return row.eventDate
                    }
                },
                {
                    "data": "eventvenue",
                    render: function (data, type, row, meta) {
                        return row.eventVenue
                    }
                },
                {
                    "data": "eventstarttime",
                    render: function (data, type, row, meta) {
                        return row.eventStartTime
                    }
                },
                {
                    render: function (data, type, row, meta) {

                        return row.eventEndTime
                    }
                },
                {
                    render: function (data, type, row, meta) {
                        var dropdown = '';
                        if (row != null) {
                            dropdown += '<select id="select">';
                            dropdown += '<option value="0">&vellip;<option>';
                            dropdown += '<option onclick="select()"value="Completed" >Completed</option>';
                            dropdown += '<option value="Cancelled">Cancelled</option>';
                            dropdown += '<option value="InProgress">In Progress</option>';
                            dropdown += '<option value="OnHold">On Hold</option>';
                            dropdown += '<option value="WaitingToStart">Waiting To Start</option>';
                            dropdown += '</select>';
                        }
                        else {
                            dropdown = '<select class="form-control"><option value="0">Select Status</option></select>';
                        }
                        return dropdown;
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
