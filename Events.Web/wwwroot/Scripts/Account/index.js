
function reload() {
    URL = "/Account/login";
    if (document.URL.indexOf("#") == -1) {
        // Set the URL to whatever it was plus "#".
        url = document.URL + "#";
        location = "#";

        //Reload the page
        location.reload(true);
    }
}

$("document").ready(function () {

    $(".form-password-toggle i").click(function () {
        $(this).toggleClass("bx-hide bx-show");
        var input = $($(this).closest(".input-group").find(".form-control"));
        if (input.attr("type") == "password") {
            input.attr("type", "text");
        } else {
            input.attr("type", "password");
        }
    });
});
$('#create_member').click(function () {
    var data = {
        Id: $("#password").val(),
        FullName: $("#email").val(),
    }
    $.ajax({
        type: "post",
        data: data,
        url: '/Account/Login',
        success: function (resonce) {
            alert(resonce);
        }
    })
});

function myFunction() {
    var x = document.getElementById("password");
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}