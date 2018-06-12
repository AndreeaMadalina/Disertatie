using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicatieDisertatie.Models
{
	public class QuestionViewModel
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Question()
		{
			this.QuestionOptions = new HashSet<QuestionOption>();
		}

		public int QuestionId { get; set; }
		public int FileId { get; set; }
		public int TypeId { get; set; }
		public string Question1 { get; set; }

		public virtual File File { get; set; }
		public virtual QuestionType QuestionType { get; set; }
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<QuestionOption> QuestionOptions { get; set; }
	}
}