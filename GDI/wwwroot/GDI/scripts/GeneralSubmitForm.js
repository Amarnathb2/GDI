$(document).ready(function () {
    $(".submitButton").attr("disabled", true);
});
$('#Country__c').on('change', function () {
    let _val = $(this).val();
    if (_val == 'USA') {
        $('#State__c').removeClass('noRequired').prop('disabled', false);
        $('#Postal_Zip_Code__c').removeClass('noRequired');
        $('#State__c').prev('label').find('span').show();
        $('#Postal_Zip_Code__c').prev('label').find('span').show();
    } else {
        $('#State__c').prop("selectedIndex", 0);
        $('#State__c').addClass('noRequired').prop('disabled', true);
        $('#State__c').prev('label').find('span').hide();
        $('#Postal_Zip_Code__c').addClass('noRequired').next('span').hide();
        $('#Postal_Zip_Code__c').prev('label').find('span').hide();
    }
});

function OnSuccessContactUsForm() {
    successContactUsForm();
}
function OnFailureContactUsForm(){
    $('.contactUsForm ').removeClass('hide');
    $('.ThankyouPage ').addClass('hide');
    $(".submitButton").attr("disabled", true);
}
function OnCompleteContactUsForm() {
    successContactUsForm();
}
function successContactUsForm() {
    $('.contactUsForm ').addClass('hide');
    $('.ThankyouPage ').removeClass('hide');
}