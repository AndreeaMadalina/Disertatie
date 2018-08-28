using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicatieDisertatie.Models
{
	public class FileViewModel
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public FileViewModel()
		{
			this.ResponseFiles = new HashSet<ResponseFileViewModel>();
			this.Questions = new HashSet<QuestionViewModel>();
			this.UserAnswers = new HashSet<UserAnswerViewModel>();
		}

		public int FileId { get; set; }
		public string AuthorId { get; set; }
		public string FileName { get; set; }
		public string TemplateFile { get; set; }
		public System.DateTime UpdatedOn { get; set; }

		public virtual AspNetUser AspNetUser { get; set; }
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<ResponseFileViewModel> ResponseFiles { get; set; }
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<QuestionViewModel> Questions { get; set; }
		public virtual ICollection<UserAnswerViewModel> UserAnswers { get; set; }
	}
}