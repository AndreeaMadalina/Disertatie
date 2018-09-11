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
		//private List<QuestionViewModel> questionList = new List<QuestionViewModel>();

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
				var l = new List<SelectListItem>();
				foreach (var item in questionTypes)
				{
					l.Add(new SelectListItem
					{
						Text = item.Type,
						Value = item.TypeId.ToString(),
						// Put all sorts of business logic in here
						Selected = item.TypeId == (int)QuestionTypes.SingleChoice ? true : false
					});
				}

				ViewBag.QuestionTypes = l;// new SelectList(questionTypes.Select(x => x.Type));
				
			}
			
			return View(new CommonViewModel());
		}

		// POST: Reviews/Create
		[HttpPost]
		public ActionResult Create(CommonViewModel vm)
		{
			try
			{
				var users = _unitOfWork.UserRepository.Get(u => u.Email == AppSettings.User.Email).ToList();

				File file = new File();
				file.AuthorId = users[0].Id;
				file.FileName = vm.FileVM.FileName;
				file.UpdatedOn = DateTime.Now.Date;

				foreach (var q in HelperClass.QuestionList)
				{
					Question question = new Question
					{
						QuestionText = q.QuestionText,
						TypeId = q.TypeId
					};

					foreach (var ans in q.QuestionOptions)
					{
						QuestionOption option = new QuestionOption
						{
							Answer = ans.Answer,
							IsValid = ans.IsValid ?? false
						};

						question.QuestionOptions.Add(option);
					}

					file.Questions.Add(question);
				}

				_unitOfWork.FileRepository.Add(file);
				_unitOfWork.Save();

				HelperClass.QuestionList.Clear();

				return RedirectToAction("Index", "File");
			}
			catch(Exception ex)
			{
				return RedirectToAction("Index", "File");
			}
		}

		[HttpPost]
		public ActionResult AddNewQuestion(int itemIndex, CommonViewModel questionDetails)
		{
			questionDetails.QuestionVM.QuestionId = itemIndex + 1;
			questionDetails.QuestionVM.TypeId = questionDetails.QuestionTypeVM.TypeId;

			bool questionExists = HelperClass.QuestionList.Exists(q => q.QuestionId == questionDetails.QuestionVM.QuestionId);
			if (!questionExists)
			{
				HelperClass.QuestionList.Add(questionDetails.QuestionVM);
			}

			return View("~/Views/CreateFile/Partial/AddNewQuestion.cshtml", questionDetails);
		}

		[HttpPost]
		public void UpdateQuestionList(bool isQuestionUpdated, CommonViewModel commonvm, bool isChecked)
		{
			if (isQuestionUpdated)
			{
				QuestionViewModel questionTextHasChanged = HelperClass.QuestionList.FirstOrDefault(q => q.QuestionId == commonvm.QuestionVM.QuestionId);
				if (questionTextHasChanged != null)
				{
					questionTextHasChanged.QuestionText = commonvm.QuestionVM.QuestionText;
				}
			}
			else
			{
				QuestionViewModel question = HelperClass.QuestionList.FirstOrDefault(q => q.QuestionId == commonvm.QuestionVM.QuestionId);
				QuestionOptionViewModel ansExists = question.QuestionOptions.FirstOrDefault(a => a.OptionId == commonvm.QuestionOptionVM.OptionId);

				if (ansExists != null)
				{
					if (!isChecked)
					{
						ansExists.Answer = commonvm.QuestionOptionVM.Answer;
					}
					else
					{					
						if (commonvm.QuestionOptionVM.IsValid.HasValue && commonvm.QuestionOptionVM.IsValid.Value && question.TypeId == (int)QuestionTypes.SingleChoice)
						{
							foreach (var item in question.QuestionOptions)
							{
								item.IsValid = false;
							}
						}
						ansExists.IsValid = commonvm.QuestionOptionVM.IsValid;
					}
				}
				else
				{
					QuestionOptionViewModel newAns = new QuestionOptionViewModel();
					newAns.OptionId = commonvm.QuestionOptionVM.OptionId;
					newAns.QuestionId = commonvm.QuestionOptionVM.QuestionId;
					newAns.Answer = commonvm.QuestionOptionVM.Answer ?? string.Empty;

					if (commonvm.QuestionOptionVM.IsValid.HasValue && commonvm.QuestionOptionVM.IsValid.Value && question.TypeId == (int)QuestionTypes.SingleChoice)
					{
						foreach (var item in question.QuestionOptions)
						{
							item.IsValid = false;
						}
					}
					newAns.IsValid = commonvm.QuestionOptionVM.IsValid;

					question.QuestionOptions.Add(newAns);
				}
			}
		}

		public ActionResult NewInterestRow(int id)
		{
			var interest = new CommonViewModel { CommonVMId = id };
			return View("~/Views/CreateFile/Partial/AddNewQuestionPopUp.cshtml", interest);
		}
	}
}