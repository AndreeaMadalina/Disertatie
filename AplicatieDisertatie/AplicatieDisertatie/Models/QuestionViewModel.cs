using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicatieDisertatie.Models
{
	public class QuestionViewModel
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public QuestionViewModel()
		{
			this.QuestionOptions = new HashSet<QuestionOptionViewModel>();
		}

		public int QuestionId { get; set; }
		public int FileId { get; set; }
		public int TypeId { get; set; }
		public string QuestionText { get; set; }

		public virtual File File { get; set; }
		public virtual QuestionType QuestionType { get; set; }
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<QuestionOptionViewModel> QuestionOptions { get; set; }
	}
}