var errorEmptyEmailAddress = 'Please enter your Email Address.',
    errorInvalidEmailAddress = 'Please enter your valid Email Address.',
    errorEmptyZipAddress = 'Please enter your Zip Code.',
    errorInvalidZipAddress = 'Please enter your valid Zip Code.',
    errorEmptyPhoneNumber = 'Please enter your Phone Number.',
    errorInvalidPhoneNumber = 'Please enter your valid Phone Number.';

$(document).ready(function () {
    //kEYPRESS EVENT IS HAVING AN ISSUE IN ANDROID CHROME devices.
    // $("input.firstName,.lastName,.phone,.zip").on('keydown',function(e){
    //I fwe need to use key up then we have to change the keycodes. Below are the some key codes.
    //backspace: 8, tab: 9, shift tab: shift && 9, openingbracket (:57, closingbracket ):48, hyphen -:189, period .:190, space:32, F1 to F12: 112 to 123
    $("input.firstName,.lastName,.phone,.zip").keypress(function (e) {
        try {
            if (window.event) {
                var charCode = window.event.keyCode;
            }
            else if (e) {
                var charCode = e.which;
            }
            else { return true; }
            // phone no. only contain digits, space and '-', '(' and ')'
            if (($(this).hasClass('phone') && (this.value.trim().length < 14 || (charCode == 8) || (charCode == 9) || (charCode == 11) || (charCode == 0)))) {
                if ((charCode > 47 && charCode < 58) || (charCode == 8) || (charCode == 0) || (charCode == 9) || (charCode == 11) || (charCode == 40) || (charCode == 41) || (charCode == 45) || (charCode == 46) || (charCode == 32))
                    return true;
                else
                    return false;
            } // if zip only contain digits and alphabets and space then (charCode > 47 && charCode < 58) || (charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || (charCode == 8) ||  (charCode == 0) || (charCode == 9) ||  (charCode == 11) || (charCode == 32)
            // Zip contains only digits.
            else if (($(this).hasClass('zip') && (this.value.trim().length < 5 || (charCode == 8) || (charCode == 9) || (charCode == 11) || (charCode == 0)))) {
                if ((charCode > 47 && charCode < 58) || (charCode == 8) || (charCode == 0) || (charCode == 9) || (charCode == 11))
                    return true;
                else
                    return false;
            } //First Name contains alphabets only.
            else if ($(this).hasClass('firstName')) {
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || (charCode == 32) || (charCode == 8) || (charCode == 46) || (charCode == 0))
                    return true;
                else
                    return false;
            } //Last Name contains alphabets and '-' hypen sign only. 
            else if ($(this).hasClass('lastName')) {
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || (charCode == 32) || (charCode == 8) || (charCode == 46) || (charCode == 45) || (charCode == 0))
                    return true;
                else
                    return false;
            } else {
                return false;
            }
        }
        catch (err) {
            console.log(err.Description);
        }
    });
    $("input.firstName,.lastName").bind("paste", function (e) {
        e.preventDefault();
    });
    $('article input[type=text]').on('focusout', function () { validateField(this) });
    $('article .product-request').on('focusout', function () { validateField(this) });
    $('article select').on('focusout', function () { selectState() });
    $('#checkBoxSection').click(function () { validateForm() });
    /*$('.submitButton').click(function(){submitForm()});*/
    disableSubmitButton(true);
})

function validateField(me) {
    var control = $(me).closest('.cell').find('span.errorLabel');
    if ($(me).hasClass('noRequired')) return false;

    if (me.value.trim() != '' && $(me).hasClass('email')) {
        validateEmail(me.value, control);
    }
    else if (me.value.trim() != '' && $(me).hasClass('zip')) {
        validateZip(me.value, control)
    }
    else if (me.value.trim() != '' && $(me).hasClass('phone')) {
        validatePhone(me.value, control)
    }
    else if (me.value.trim() == '') {
        if ($(me).hasClass('email'))
            control.html(errorEmptyEmailAddress);
        if ($(me).hasClass('zip'))
            control.html(errorEmptyZipAddress);
        if ($(me).hasClass('phone'))
            control.html(errorEmptyPhoneNumber);


        control.css("display", "block");
    }
    else if (me.value.trim().length < 3) {
        control.css("display", "block");
    }
    else {
        control.css("display", "none");
    }
    validateForm();
}
function validateForm() {



    var inputFields = $('article input[type=text]'),
        result = false;
    $.each(inputFields, function (index, val) {
        var spanControl = $(this).closest('.cell').find('span.errorLabel');
        if (!$(this).hasClass('noRequired')) {
            if ((this.value.trim() != '' && $(this).hasClass('email')) && !validateEmail(this.value.trim(), spanControl))
                result = true;
            else if ((this.value.trim() != '' && $(this).hasClass('zip')) && !validateZip(this.value.trim(), spanControl))
                result = true;
            else if ((this.value.trim() != '' && $(this).hasClass('phone')) && !validatePhone(this.value.trim(), spanControl))
                result = true;
            else if (this.value.trim().length < 3)
                result = true;
        }
    })

    var checkBoxFields = $('#checkBoxSection input[type = checkbox]');
    if (checkBoxFields.length > 0) {
        $.each(checkBoxFields, function (index, val) {
            var spanControl = $('#checkBoxSection').next('span.errorLabel');
            checked = $("#checkBoxSection input[type = checkbox]:checked").length;
            if (!checked) {
                result = true;
                spanControl.css("display", "block");
            } else {
                spanControl.css("display", "none");
            }
        })
    }
    var inputFields = $('article .product-request');
    $.each(inputFields, function (index, val) {
        var spanControl = $(this).closest('.cell').find('span.errorLabel');
        if (this.value.trim() != '' && $(this).hasClass('.product-request')) {
            result = true;
            spanControl.css("display", "block");
        } else if (this.value.trim().length < 3) {
            result = true;
            spanControl.css("display", "block");
        } else {
            spanControl.css("display", "none");
        }
    })
    result ? disableSubmitButton(true) : disableSubmitButton(false);
}
//It validates email in the format of bogus@example.com
function validateEmail(email, control) {
    var emailRegex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    emailRegex.test(email) ? control.css("display", "none").html(errorEmptyEmailAddress) : control.css("display", "block").html(errorInvalidEmailAddress);
    return emailRegex.test(email);
}

//It validates three formats of US zip code i.e. 44240 | 44240-5555 | G3H 6A3
function validateZip(zip, control) {
    var zipRegex = /(^\d{5}$)|(^\d{5}-\d{4}$)|(^[A-Za-z]\d[A-Za-z]( )?\d[A-Za-z]\d$)/;
    //var zipRegex = /^\d{5}$/;
    zipRegex.test(zip) ? control.css("display", "none").html(errorEmptyZipAddress) : control.css("display", "block").html(errorInvalidZipAddress);
    return zipRegex.test(zip);
}
//It validates multiple formats of US phone number i.e. 9988066490, 998 806 6490, 998-806-6490, 998.806.6490, 1 998 806 6490, 1 998-806-6490,1 998.806.6490
function validatePhone(phone, control) {
    var phoneRegex = /^(1\s?)?((\([0-9]{3}\))|[0-9]{3})[\s\-\.]?[\0-9]{3}[\s\-\.]?[0-9]{4}$/;
    //var phoneRegex = /^(1?)[\s\-]?((\([0-9]{3}\))|[0-9]{3})[\s\-]?[\0-9]{3}[\s\-]?[0-9]{4}$/;
    phoneRegex.test(phone) ? control.css("display", "none").html(errorEmptyPhoneNumber) : control.css("display", "block").html(errorInvalidPhoneNumber);
    return phoneRegex.test(phone);
}

function disableSubmitButton(value) {
    $(".btn-primary").length > 0 ? $(".btn-primary").prop("disabled", value) : $(".submitButton").prop("disabled", value);
}

function submitForm() {
    $('.contact-address').length > 0 ? $('.contact-address').css("display", "none") : '';
    $('.common-form').length > 0 ? $('.common-form').css("display", "none") : '';
    $('.ThankyouPage').css("display", "block");
}
function selectState() {
    var selectFields = $('article select');
    if (selectFields.length > 0) {
        $.each(selectFields, function (index, val) {
            var spanControl = $(this).closest('.cell').find('span.errorLabel');
            if (this.value.trim() == '' && !$(this).hasClass('noRequired')) {
                result = true;
                spanControl.css("display", "block");
            } else {
                spanControl.css("display", "none");
            }
        })
    }
}