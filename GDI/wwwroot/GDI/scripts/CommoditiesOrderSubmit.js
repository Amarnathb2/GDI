document.querySelector('#drop-button').addEventListener('click', function(event) {
		event.preventDefault();
});
$(document).ready(function () {
	$('.submitButton').click(function () {
		let selectedInterests = '';
		$('#checkBoxSection :checkbox').each(function () {
			if (this.checked) {
				let currID = this.id;
				selectedInterests += $("label[for=" + currID + "]")[0].textContent + ';';
			}
		});
		$(".description").val(selectedInterests);
	});
});
function OnSuccessCommoditiesOrderForm() {
	Success();
}

function OnCompleteCommoditiesOrderForm() {
	Success();
}

function Success() {
	$('.commodityForm ').addClass('hide');
	$('.ThankyouPage ').removeClass('hide');
}

function OnFailureCommoditiesOrderForm() {
	$('#frmCommoditiesOrder').trigger("reset");
	$('.commodityForm ').removeClass('hide');
	$('.ThankyouPage ').addClass('hide');
}
