using AplicatieDisertatie.DAL;
using AplicatieDisertatie.Models;
using AplicatieDisertatie.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicatieDisertatie.Controllers
{
    public class CreateFileController : Controller
    {
		private UnitOfWork _unitOfWork = new UnitOfWork();

		CommonViewModel ViewModel { get; set; }

		// GET: CreateFile
		public ActionResult Index()
        {
			// Initialization.  
			CommonViewModel model = new CommonViewModel();
			model.FileVM = new FileViewModel();
			model.QuestionVM = new QuestionViewModel();
			model.QuestionTypeVM = new QuestionTypeViewModel();
			model.QuestionOptionVM = new QuestionOptionViewModel();
			model.QuestionList = new List<QuestionViewModel>();

			// Get Result  
			//model.ResultSetVM.ResultSet = this.LoadData();

			return View(model);			
        }

		// GET: Reviews/Create
		public ActionResult Create()
		{
			// Initialization.  
			ViewModel = new CommonViewModel();
			ViewModel.FileVM = new FileViewModel();
			ViewModel.QuestionVM = new QuestionViewModel();
			ViewModel.QuestionTypeVM = new QuestionTypeViewModel();
			ViewModel.QuestionOptionVM = new QuestionOptionViewModel();
			ViewModel.QuestionList = new List<QuestionViewModel>();

			var questionTypes = _unitOfWork.QuestionTypeRepository.Get().ToList();

			if (questionTypes != null)
			{
				ViewBag.QuestionTypes = new SelectList(questionTypes.Select(x => x.Type));
			}
			
			return View(new CommonViewModel());
		}

		// POST: Reviews/Create
		[HttpPost]
		public ActionResult Create(CommonViewModel vm)
		{
			try
			{
				var files = _unitOfWork.FileRepository.Get().ToList();
				var users = _unitOfWork.UserRepository.Get(u => u.Email == AppSettings.User.Email).ToList();

				File file = new File();
				file.AuthorId = users[0].Id;
				file.FileName = vm.FileVM.FileName;
				foreach(var q in ViewModel.FileVM.Questions)
				{
					Question question = new Question
					{
						QuestionText = q.QuestionText,
						TypeId = q.TypeId
					};
					file.Questions.Add(question);
				}
				
				_unitOfWork.Save();

				return RedirectToAction("Index", "File");
			}
			catch
			{
				return View();
			}
		}

		[HttpPost]
		public ActionResult AddNewQuestion(CommonViewModel cm)
		{
			//cm.QuestionVM.QuestionType.TypeId = cm.QuestionTypeVM.TypeId;
			//ViewModel.FileVM.Questions.Add(cm.QuestionVM);

			return View("~/Views/CreateFile/Partial/AddNewQuestion.cshtml", cm);
		}

		public ActionResult NewInterestRow(int id)
		{
			var interest = new CommonViewModel { CommonVMId = id };
			return View("~/Views/CreateFile/Partial/AddNewQuestionPopUp.cshtml", interest);
		}

	}
}