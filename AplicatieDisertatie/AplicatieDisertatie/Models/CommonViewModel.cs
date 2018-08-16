using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicatieDisertatie.Models
{
	public class CommonViewModel
	{
		public int CommonVMId { get; set; }
		public FileViewModel FileVM { get; set; }
		public QuestionViewModel QuestionVM { get; set; }
		public QuestionOptionViewModel QuestionOptionVM { get; set; }
		public QuestionTypeViewModel QuestionTypeVM { get; set; } = new QuestionTypeViewModel();
		public List<QuestionViewModel> QuestionList { get; set; }
	}
}