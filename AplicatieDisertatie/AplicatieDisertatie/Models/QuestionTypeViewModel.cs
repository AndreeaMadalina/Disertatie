using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicatieDisertatie.Models
{
	public class QuestionTypeViewModel
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public QuestionType()
		{
			this.Questions = new HashSet<Question>();
		}

		public int TypeId { get; set; }
		public string Type { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<Question> Questions { get; set; }
	}
}