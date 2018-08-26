using AplicatieDisertatie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicatieDisertatie.Utils
{
	public sealed class AppSettings
	{
		private static readonly AppSettings instance = new AppSettings();

		private AppSettings()
		{
			User = new AspNetUser();
		}

		public static AppSettings Instance
		{
			get
			{
				return instance;
			}
		}

		public static AspNetUser User { get; set; }
	}
}