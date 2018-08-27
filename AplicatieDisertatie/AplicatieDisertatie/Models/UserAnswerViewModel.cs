using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicatieDisertatie.Models
{
	public class UserAnswerViewModel
	{
		public string UserId { get; set; }
		public int OptionId { get; set; }
		public string Text { get; set; }
		public int FileId { get; set; }
		public int QuestionId { get; set; }

		public virtual AspNetUser AspNetUser { get; set; }
		public virtual QuestionOptionViewModel QuestionOption { get; set; }
		public virtual File File { get; set; }
		public virtual Question Question { get; set; }
	}
}