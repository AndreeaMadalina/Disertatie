using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicatieDisertatie.Models
{
	public class ResponseFileViewModel
	{
		public int ResponseFileId { get; set; }
		public string UserId { get; set; }
		public int FileId { get; set; }
		public string File { get; set; }
		public System.DateTime UpdatedOn { get; set; }

		public virtual AspNetUser AspNetUser { get; set; }
		public virtual File File1 { get; set; }
	}
}