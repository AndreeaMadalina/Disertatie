// Send the values to the server side
$("#btnAddNewQuestion").click(function () {
	var itemIndex = $("#newQuestion").length;
	//var data = $("#submitForm").serialize();

	question = {}; qTypeVm = {}; qVM = {};
	var qTypeId = $("#questionTypeId option:selected").index();	
	var qText = $("#qText").val();
	var ansNo = $("#ansNo").val();

	qTypeVm = {
		TypeId: qTypeId
	};
	qVM = {
		AnswersNo: ansNo,
		QuestionText: qText
	};

	questionDetails = {
		QuestionTypeVM: qTypeVm,
		QuestionVM: qVM
	};

	$.ajax({
		type: "POST",
		url: "/CreateFile/AddNewQuestion",
		data: { itemIndex, questionDetails },
		success: function (partialView) {
			$("#addNewQuestionPopup").modal("hide");
			$('#newQuestions').append(partialView);
			$.clearFormFields('#addNewQuestionPopup');
			hideOnLoad();
		}
	})
});