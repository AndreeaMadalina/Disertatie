function UpdateQuestionList(Elem) {
	var id = Elem.id;
	var text = $("#" + id).val();
	var isQuestionUpdated;
	var isChecked = false;

	if (text === 'on') {
		text = true;
	}

	qVM = {}; qOptionVm = {};

	var splitString = id.split("_qId_")
	var arrayLength = splitString.length;
	if (arrayLength === 2) {
		var aId = splitString[0].replace(/[^0-9]/g, '');
		var qId = splitString[1].replace(/[^0-9]/g, '');

		if (typeof (text) === 'boolean' || text === 'false' || text === 'true') {

			isChecked = true;
			qVM = {
				QuestionId: qId,
				QuestionText: text
			};

			qOptionVm = {
				QuestionId: qId,
				OptionId: aId,
				IsValid: text
			};

			commonvm = {
				QuestionVM: qVM,
				QuestionOptionVM: qOptionVm
			};
		}
		else {
			isChecked = false;

			qVM = {
				QuestionId: qId,
				QuestionText: text
			};

			qOptionVm = {
				QuestionId: qId,
				OptionId: aId,
				Answer: text
			};

			commonvm = {
				QuestionVM: qVM,
				QuestionOptionVM: qOptionVm
			};
		}

		isQuestionUpdated = false;
	}
	else {
		var qId = splitString[0].replace(/[^0-9]/g, '');

		qVM = {
			QuestionId: qId,
			QuestionText: text
		};

		commonvm = {
			QuestionVM: qVM
		};

		isQuestionUpdated = true;
	}

	$.ajax({
		type: "POST",
		url: "/CreateFile/UpdateQuestionList",
		data: { isQuestionUpdated, commonvm, isChecked },
		dataType: "json",
		success: function () {
		}
	});
}