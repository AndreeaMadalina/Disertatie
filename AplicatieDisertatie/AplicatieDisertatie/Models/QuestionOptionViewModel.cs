using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicatieDisertatie.Models
{
	public class QuestionOptionViewModel
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public QuestionOptionViewModel()
		{
			this.UserAnswers = new HashSet<UserAnswerViewModel>();
		}

		public int OptionId { get; set; }
		public int QuestionId { get; set; }
		public string Answer { get; set; }
		public Nullable<bool> IsValid { get; set; }

		public virtual Question Question { get; set; }
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<UserAnswerViewModel> UserAnswers { get; set; }
	}
}