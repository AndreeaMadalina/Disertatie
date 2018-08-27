using AplicatieDisertatie.DAL;
using AplicatieDisertatie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicatieDisertatie.Utils
{
	public sealed class AppSettings
	{
		private static UnitOfWork _unitOfWork = new UnitOfWork();
		private static readonly AppSettings instance = new AppSettings();
		private static AspNetUser _user;

		private AppSettings()
		{
			_user = new AspNetUser();
		}

		public static AppSettings Instance
		{
			get
			{
				return instance;
			}
		}

		public static AspNetUser User
		{
			get { return _user; }
			set { _user = value; }
		}

		public static void SetUserId()
		{
			var users = _unitOfWork.UserRepository.Get(u => u.Email == AppSettings.User.Email).ToList();

			if (users != null)
			{
				User.Id = users[0].Id;
			}
		}
	}
}