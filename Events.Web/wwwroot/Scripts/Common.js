var common = {

    pagingText: function (iStart, iEnd, iTotal, entity, pageSize) {
        iStart = iTotal == 0 ? 0 : iStart;
        var iEnd2 = (parseInt(iStart) + parseInt(pageSize) - 1);
        if (iEnd2 > iEnd) {
            iEnd2 = iEnd
        }
        if (iTotal == 0) {
            return "";
        }
        return "Showing " + iStart + " to " + iEnd2 + " of " + iTotal + " " + entity;
    },

    alertPOS: function (message, fun) {
        bootbox.alert(message, fun);
    },

    RemoveAmountformat: function (val) {
        return val.replace(/[XOF,]+/g, "");
    },
    RemoveAmountformatWithSpace: function (val) {
        return val.replace(/[XOF, ]+/g, "");
    },
    ReadURL: function (input, element) {

        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                element.attr('src', e.target.result);
                element.show();
            }

            reader.readAsDataURL(input.files[0]);
        }
    },
    blockUI: function (options, loadervalue) {
        var options = $.extend(true, {}, options);
        var html = '';

        if (loadervalue != undefined && loadervalue == 1) {
            //html = '<div class="payment-loader-container">';
            //html += '<div class="payment-loader">';
            //html += '<div class="payment-circle">';
            //html += '<div class="payment-inner-circle">';
            //html += '</div>';
            //html += '<h1>';
            //html += '$';
            //html += '</h1>';
            //html += '</div>';
            //html += '</div>';
            //html += '</div>';

            //html = '<div class="loading spin-1"><div class="loading spin-2"><div class="loading spin-3"><div class="loading spin-4"><div class="loading spin-5"><div class="loading spin-6"></div></div></div></div></div></div>';

            html = '<div class="cssload-loader"><div class="cssload-inner cssload-one"></div><div class="cssload-inner cssload-two"></div><div class="cssload-inner cssload-three"></div></div>';

        }
        else {
            if (options.iconOnly) {
                html = '<div class="loading-message ' + (options.boxed ? 'loading-message-boxed' : '') + '"><img src="../../images/loading-spinner-grey.gif" align=""></div>';
            } else if (options.textOnly) {
                html = '<div class="loading-message ' + (options.boxed ? 'loading-message-boxed' : '') + '"><span>&nbsp;&nbsp;' + (options.message ? options.message : 'LOADING...') + '</span></div>';
            } else {
                html = '<div class="loading-message ' + (options.boxed ? 'loading-message-boxed' : '') + '"><img src="../../images/loading-spinner-grey.gif" align=""><span>&nbsp;&nbsp;' + (options.message ? options.message : 'LOADING...') + '</span></div>';
            }
        }

        if (options.target) {
            var el = jQuery(options.target);
            if (el.height() <= ($(window).height())) {
                options.cenrerY = true;
            }
            el.block({
                message: html,
                baseZ: options.zIndex ? options.zIndex : 1000,
                centerY: options.cenrerY != undefined ? options.cenrerY : false,
                css: {
                    top: '10%',
                    border: '0',
                    padding: '0',
                    backgroundColor: 'none'
                },
                overlayCSS: {
                    backgroundColor: options.overlayColor ? options.overlayColor : '#000',
                    opacity: options.boxed ? 0.05 : 0.1,
                    cursor: 'wait'
                }
            });
        } else {
            $.blockUI({
                message: html,
                baseZ: options.zIndex ? options.zIndex : 1000,
                css: {
                    border: '0',
                    padding: '0',
                    backgroundColor: 'none'
                },
                overlayCSS: {
                    backgroundColor: options.overlayColor ? options.overlayColor : '#000',
                    opacity: options.boxed ? 0.05 : 0.1,
                    cursor: 'wait'
                }
            });
        }
    },
    unblockUI: function (target) {
        if (target) {
            jQuery(target).unblock({
                onUnblock: function () {
                    jQuery(target).css('position', '');
                    jQuery(target).css('zoom', '');
                }
            });
        } else {
            $.unblockUI();
        }
    },
    ParseUrlParams: function (obj) {
        var str = "";
        for (var key in obj) {
            if (str != "") {
                str += "&";
            }
            str += key + "=" + encodeURIComponent(obj[key]);
        }
        return str;
    },
    formatDate: function (dateStr) {
        if ($('#isFrenchCulture').length > 0) {
            return dateStr;
        }
        else {
            var arr = dateStr.split('/');
            return arr[1] + '/' + arr[0] + '/' + arr[2];
        }
    },

    formatDateTime: function (dateStr) {
        if ($('#isFrenchCulture').length > 0) {
            return dateStr;
        }
        else {
            var arr = dateStr.split(' ');
            var date = arr[0].split('/');
            return date[1] + '/' + date[0] + '/' + date[2] + ' ' + arr[1];
        }
    },

    formatDecimal: function (decimalStr) {
        if ($('#isFrenchCulture').length > 0) {
            return decimalStr.replace('.', ',');
        }
        else {
            return decimalStr
        }
    },

    HandleEnterButton: function () {

        $(document).on("keypress", function (e) {
            if (e.keyCode == 13) {
                // Cancel the default action on keypress event
                e.preventDefault();
            }
        });
    },

    CreateUniqueId: function () {
        function S4() {
            return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
        }
        return (S4() + S4() + "-" + S4() + "-4" + S4().substr(0, 3) + "-" + S4() + "-" + S4() + S4() + S4()).toLowerCase();
    }
}

$(document).on("click", "button[data-dismiss='modal']", function () {
    if ($(".modal-backdrop.in").length > 0) {
        $(".modal-backdrop.in").remove();
    }
    
});

function validateIPaddress(ipaddress) {

    if (/^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/.test(ipaddress)) {
        return (true)
    }
    return (false)
}

function validateEmail(email) {
    var regExp = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return regExp.test(email);


}

window.StaxZax = (function () {
    return {
        //parameter is required
        ValidateForm: function ($this) {

            var isFormValid = true;
            //validate ip address field
            $($this).find("input.ip-field").each(function (ind, $ctrl) {
                if ($($ctrl).val().trim().length > 0 && !validateIPaddress($($ctrl).val())) {
                    $($ctrl).siblings("span.ip").show();
                    isFormValid = false;
                } else {
                    $($ctrl).siblings("span.ip").hide();
                }
            });
            //validate required fields
            $($this).find("input.required-field").each(function (ind, $ctrl) {
                if ($($ctrl).val().trim().length == 0) {
                    $($ctrl).siblings("span.required").show();
                    isFormValid = false;
                } else {
                    $($ctrl).siblings("span.required").hide();
                }
            });
            //validate textarea required fields 
            $($this).find("textarea.required-field").each(function (ind, $ctrl) {
                if ($($ctrl).val().trim().length == 0) {
                    $($ctrl).siblings("span.required").show();
                    isFormValid = false;
                } else {
                    $($ctrl).siblings("span.required").hide();
                }
            });
            //validate email fields
            $($this).find("input.email-field").each(function (ind, $ctrl) {
                if ($($ctrl).val().trim().length > 0 && !validateEmail($($ctrl).val())) {
                    $($ctrl).siblings("span.email").show();
                    isFormValid = false;
                } else {
                    $($ctrl).siblings("span.email").hide();
                }
            });
            //validate password fields
            $($this).find("input.password-field").each(function (ind, $ctrl) {
                //if ($($ctrl).val().trim().length > 0 && ($($ctrl).val().trim().length < 8 || $($ctrl).val().trim().length > 100)) {
                //    $($ctrl).siblings("span.password").show();
                //    isFormValid = false;
                //} else {
                //    $($ctrl).siblings("span.password").hide();

                //    if ($($ctrl).val().trim().length >= 8 && $($ctrl).val().trim().length < 100) {
                //        var regex = /(?=.*\d)\w+/g;
                //        var result = regex.test($($ctrl).val())
                //        if (!result) {
                //            $($ctrl).siblings("span.password").show();
                //            isFormValid = false;
                //        }
                //    }
                //    //validate new password and confirm password fields
                if ($($ctrl).hasClass("new-password-field")) {
                    if ($($ctrl).val() != $($this).find("input.confirm-password-field").val()) {
                        $($this).find("input.confirm-password-field").siblings(".confirm-password").show();
                        isFormValid = false;
                    }
                    else {
                        $($this).find("input.confirm-password-field").siblings(".confirm-password").hide();
                    }
                }
                //}
            });
            //validate phone fields
            $($this).find("input.phone-field").each(function (ind, $ctrl) {
                var regexpnumeric = new RegExp("[^0-9]");
                var value = ($ctrl).value
                if (regexpnumeric.test(value)) {
                    $($ctrl).siblings("span.phone").show();
                    isFormValid = false;
                }
                else {
                    $($ctrl).siblings("span.phone").hide();
                }
            });
            //validate dropdown required fields
            $($this).find("select.required-field").each(function (ind, $ctrl) {
                if ($($ctrl).val().trim().length == 0 || $($ctrl).val().trim() == '0') {
                    $($ctrl).siblings("span.required").show();
                    isFormValid = false;
                } else {
                    $($ctrl).siblings("span.required").hide();
                }
            });
            //validate percenatge fields
            $($this).find("input.percentage-field").each(function (ind, $ctrl) {
                //var regex = new RegExp("^[0-9]{1,18}(?:\.[0-9]{0,2})?$");
                var pctForReqdFld = new RegExp("^[0-9]{1,18}(?:\.[0-9]{0,2})?$");
                value = ($ctrl).value
                if (isNaN($($ctrl).val().trim()) || Number($($ctrl).val().trim()) > 100) {
                    $($ctrl).siblings("span.percentage").show();
                    isFormValid = false;
                }
                else if (!pctForReqdFld.test(value)) {
                    $($ctrl).siblings("span.percentagepositive").show();
                    isFormValid = false;
                }
                else {
                    $($ctrl).siblings("span.percentage").hide();
                }
            });
            return isFormValid;
        },
        //Not accept space in password field if password field has a class "password-field"
        StopInputSpaceInPassword: function () {
            $("input.password-field").on("keypress", function (e) {
                if (e.which === 32) {
                    return false;
                }
            });
        },
        StopNeagtiveValueInNumberField: function () {
            $("input.number-field").on("keypress", function (e) {
                if (e.which === 45 || e.which === 69 || e.which === 101) {
                    return false;
                }
            });
        },
        GetUrlVars: function () {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        },

    }
}());

function onlynumber() {
    $('.numberonly').keypress(function (e) {

        var charCode = (e.which) ? e.which : event.keyCode

        if (String.fromCharCode(charCode).match(/[^0-9]/g))
            return false;
    });
}

$(document).ready(function () {

    $('#fileFormatValidation').hide();
    var m_names = ['January', 'February', 'March',
        'April', 'May', 'June', 'July',
        'August', 'September', 'October', 'November', 'December'];

    d = new Date();
    var n = m_names[d.getMonth()];

    var time = d.getHours() + ":" + d.getMinutes()

    $('.date-inner').html(n + " " + d.getDate());


    setInterval(function () {
        // Create a newDate() object and extract the minutes of the current time on the visitor's
        var hours = new Date().getHours();
        var minutes = new Date().getMinutes();
        // Add a leading zero to the minutes value
        $(".time-inner").html(((hours < 10 ? "0" : "") + hours) + ':' + (minutes < 10 ? "0" : "") + minutes);
    }, 1000);




})

function checkfile(sender) {
    $('#fileFormatValidation').hide();
    var validExts = new Array(".xlsx", ".xls");
    var fileExt = sender.value;
    fileExt = fileExt.substring(fileExt.lastIndexOf('.'));
    if (validExts.indexOf(fileExt) < 0) {
        $('#fileFormatValidation').show();
        $('#Excelfile').val("");
        return false;
    }
    else return true;
}

