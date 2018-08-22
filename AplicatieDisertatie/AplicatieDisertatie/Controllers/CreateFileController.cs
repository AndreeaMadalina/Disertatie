using AplicatieDisertatie.DAL;
using AplicatieDisertatie.Models;
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
			CommonViewModel model = new CommonViewModel();
			model.FileVM = new FileViewModel();
			model.QuestionVM = new QuestionViewModel();
			model.QuestionTypeVM = new QuestionTypeViewModel();
			model.QuestionOptionVM = new QuestionOptionViewModel();
			model.QuestionList = new List<QuestionViewModel>();

			var questionTypes = _unitOfWork.QuestionTypeRepository.Get().ToList();

			if (questionTypes != null)
			{
				ViewBag.QuestionTypes = new SelectList(questionTypes.Select(x => x.Type));
			}
			
			return View(new CommonViewModel());
		}

		// POST: Reviews/Create
		[HttpPost]
		public ActionResult Create(int fileID, File newFile)
		{
			try
			{
				var questionTypes = _unitOfWork.QuestionTypeRepository.Get().ToList();
				ViewBag.QuestionTypes = questionTypes;

				var files = _unitOfWork.FileRepository.Get().ToList();
				var file = _unitOfWork.FileRepository.GetByID(fileID);
				files.Add(newFile);
				_unitOfWork.Save();

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		[HttpPost]
		public ActionResult AddNewQuestion(CommonViewModel cm)
		{
			//CommonViewModel cvm = new CommonViewModel { QuestionList = new List<QuestionViewModel>() };
			//cvm.QuestionList.Add(new QuestionViewModel { QuestionId = 1, QuestionText = "Test" });



			return View("~/Views/CreateFile/Partial/AddNewQuestion.cshtml", cm);
		}

	}
}