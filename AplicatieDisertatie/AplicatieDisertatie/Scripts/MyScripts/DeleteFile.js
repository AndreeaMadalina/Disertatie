var tempId = 0;
function DeleteFile(FileId) {
	tempId = FileId;
	$("#DeleteConfirmation").modal("show");
};

function ConfirmDelete() {
	var id = tempId;
	$.ajax({
		type: "POST",
		url: "/File/Delete",
		data: { id: id },
		dataType: "json",
		success: function (result) {
			$("#DeleteConfirmation").modal("hide");
			window.location.href ='/File/Index';
		}
	})
}