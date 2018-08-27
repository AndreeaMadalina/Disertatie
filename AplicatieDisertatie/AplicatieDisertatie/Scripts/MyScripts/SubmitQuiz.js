var _ansArray = [];
$('.rdomcqans').on('change', function () {
	var McqAnswerListArray = [];
	var count = 0;
	//McqAnswerListArray += "";
	$('.rdomcqans:checked').each(function () {

		//if (count == 0) {
		//	McqAnswerListArray += $(this).attr('name') + ":" + $(this).val();
		//	//_ansList.push($(this).attr('name') + ":" + $(this).val());
		//} else {
		//	McqAnswerListArray += ",";
		//	McqAnswerListArray += $(this).attr('name') + ":" + $(this).val();
		//	//_ansList.push($(this).attr('name') + ":" + $(this).val());
		//}

		count++;
		var a = $(this).attr('name');
		var index = McqAnswerListArray.findIndex(x => x === a);
		if (index === -1) {
			McqAnswerListArray.push(a);
		}
	});
	_ansArray = McqAnswerListArray;
});

//$(document).ready(function () {

	//$('#SubmitQuiz').on('click', function () {
	function SubmitQuiz(fileId) {
		var resultQuiz = [], countQuestion = _ansArray.length, question = {}, j = 1;

		for (var i = 0; i < countQuestion; i++) {

			var splitString = _ansArray[i].split("_ansId_")
			var qId = splitString[0].replace(/[^0-9]/g, '');
			var aId = splitString[1].replace(/[^0-9]/g, '');

			question = {
				QuestionId: qId,
				OptionId: aId,
				FileId: fileId
			}

			resultQuiz.push(question);
		}

		$.ajax({
			type: "POST",
			url: "/File/QuizTest",
			data: { fileId, resultQuiz },
			dataType: "json",
			//contentType: "application/json; charset=utf-8",
			success: function (result) {
				window.location.href = '/File/OpenFile';
			}
		});
	}
//});