// hide all div options
function hideOnLoad() {
	$('#questionText').hide();
	$('#answersNo').hide();
};

// clear the inputs
$.clearFormFields = function (area) {
	$(area).find('input[type="text"], input[type="number"],textarea,select').val('');
};

// Render the popup controls
$(document).ready(function () {
	hideOnLoad();
	$("#questionTypeId").on("change", function () {
		var value = $("#questionTypeId option:selected").index();
		if (value == 3) {
			$("#questionText").show();
			$("#answersNo").hide();
		}
		else if (value == 1 || value == 2) {
			$("#questionText").show();
			$("#answersNo").show();
		}
		else {
			hideOnLoad();
		}
	});
});

// Add answer options
//$(document).ready(function () {
//	var oldAnsNo = 0;
//	$("#ansNo").on("propertychange change keyup keydown keypress paste input", function () {
//		var newAnsNo = $("#ansNo").val();
//		if (newAnsNo > oldAnsNo) {
//			$("#answers".append('@Html.EditorFor(model => model.QuestionOptionVM.Answer, new { htmlAttributes = new { @class = "form - control" } })'));
//		}
//	})
//})