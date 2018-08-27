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
    public class FileController : Controller
    {
		private UnitOfWork _unitOfWork = new UnitOfWork();

		// GET: File
		public ActionResult Index()
        {
			var files = _unitOfWork.FileRepository.Get().ToList();

			List<FileViewModel> viewFiles = new List<FileViewModel>();
			foreach (var item in files)
			{
				viewFiles.Add(new FileViewModel
				{
					FileId = item.FileId,
					AuthorId = item.AuthorId,
					FileName = item.FileName,
					TemplateFile = item.TemplateFile,
					UpdatedOn = item.UpdatedOn,
					AspNetUser = item.AspNetUser
				});
			}

            return View(viewFiles);
        }

		public ActionResult OpenFile(int id)
		{
			var file = _unitOfWork.FileRepository.GetByID(id);

			var questionList = new List<QuestionViewModel>();

			foreach (var item in file.Questions)
			{
				var optionList = new List<QuestionOptionViewModel>();

				foreach (var o in item.QuestionOptions)
				{
					optionList.Add(new QuestionOptionViewModel
					{
						Answer = o.Answer,
						IsValid = o.IsValid,
						OptionId = o.OptionId
					});
				}

				questionList.Add(new QuestionViewModel
				{
					File = item.File_Question,
					QuestionText = item.QuestionText,
					FileId = item.FileId,
					QuestionId = item.QuestionId,
					QuestionType = item.QuestionType,
					TypeId = item.TypeId,
					QuestionOptions = optionList
				});
			}

			var viewFile = new FileViewModel
			{
				FileId = file.FileId,
				AuthorId = file.AuthorId,
				FileName = file.FileName,
				TemplateFile = file.TemplateFile,
				UpdatedOn = file.UpdatedOn,
				AspNetUser = file.AspNetUser,
				Questions = questionList
			};

			List<UserAnswer> userAns = _unitOfWork.UserAnswerRepository.Get(u => u.FileId == id).ToList();
			List<QuestionOptionViewModel> answers = new List<QuestionOptionViewModel>();
			foreach (var u_ans in userAns)
			{
				QuestionOptionViewModel ans = new QuestionOptionViewModel
				{
					OptionId = u_ans.OptionId,
					QuestionId = u_ans.QuestionId
				};
				answers.Add(ans);
			}

			ViewBag.UserNoOfResponses = GetQuizResult(answers);
			return View(viewFile);
		}

		[HttpPost]
		public ActionResult QuizTest(int fileId, List<QuestionOptionViewModel> resultQuiz)
		{
			//List<QuestionOptionViewModel> finalResultQuiz = new List<QuestionOptionViewModel>();
			List<UserAnswer> userAnswers = new List<UserAnswer>();
			var options = _unitOfWork.QuestionOptionRepository.Get().ToList();
			int correctAns = 0;
			int result;
			
			foreach (QuestionOptionViewModel answser in resultQuiz)
			{
				//QuestionOptionViewModel ans = options.Where(a => a.OptionId == answser.OptionId).Select(a => new QuestionOptionViewModel
				//{
				//	QuestionId = a.QuestionId,
				//	OptionId = a.OptionId,
				//	Answer = a.Answer,
				//	IsValid= a.IsValid

				//}).FirstOrDefault();

				UserAnswer userAnswer = options.Where(a => a.OptionId == answser.OptionId).Select(a => new UserAnswer
				{
					UserId = AppSettings.User.Id,					
					FileId = fileId,
					QuestionId = answser.QuestionId,
					OptionId = a.OptionId,

				}).FirstOrDefault();

				userAnswers.Add(userAnswer);
				_unitOfWork.UserAnswerRepository.Add(userAnswer);
				//finalResultQuiz.Add(ans);
			}

			correctAns = GetQuizResult(resultQuiz);
			result = correctAns;
			_unitOfWork.Save();

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		public JsonResult Delete(int? id)
		{
			bool result = false;

			File file = _unitOfWork.FileRepository.GetByID(id);
			if (file != null)
			{
				_unitOfWork.FileRepository.Delete(id);
				_unitOfWork.Save();
				result = true;
			}

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		#region Private Methods

		private int GetQuizResult(List<QuestionOptionViewModel> resultQuiz)
		{
			int result = 0;
			var options = _unitOfWork.QuestionOptionRepository.Get().ToList();
			var questions = _unitOfWork.QuestionRepository;
			Dictionary<Question, bool> qResult = new Dictionary<Question, bool>();

			List<Question> questionList = new List<Question>();

			foreach (var r in resultQuiz)
			{
				Question qu = questions.GetByID(r.QuestionId);

				if (!questionList.Exists(q => q.QuestionId == r.QuestionId))
				{
					questionList.Add(qu);
				}
			}

			foreach (var q in questionList)
			{
				if (q.TypeId == (int)QuestionTypes.SingleChoice)
				{
					var r = resultQuiz.FirstOrDefault(x => x.QuestionId == q.QuestionId);
					var correct_ans = q.QuestionOptions.FirstOrDefault(a => a.OptionId == r?.OptionId);
					if (r != null && correct_ans != null && correct_ans.IsValid.Value)
					{
						result++;
					}
				}

				if (q.TypeId == (int)QuestionTypes.MultipleChoice)
				{
					bool isCorrect = true;
					var r = resultQuiz.Where(x => x.QuestionId == q.QuestionId);
					var correct_ans = q.QuestionOptions.Where(a => (bool)a.IsValid);
					if (r != null)
					{
						foreach (var ca in correct_ans)
						{
							if (r.FirstOrDefault(x => x.OptionId == ca.OptionId) == null)
							{
								isCorrect = false;
							}
						}
					}
					if (isCorrect)
					{
						result++;
					}
				}
			}
			return result;
		}

		#endregion
	}
}