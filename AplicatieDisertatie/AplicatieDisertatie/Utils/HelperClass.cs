using AplicatieDisertatie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicatieDisertatie.Utils
{
	public static class HelperClass
	{
		public static List<QuestionViewModel> QuestionList { get; set; } = new List<QuestionViewModel>();
	}

	public enum QuestionTypes
	{
		SingleChoice = 1,
		MultipleChoice = 2,
		Text = 3
	}
}