using AplicatieDisertatie.DAL;
using AplicatieDisertatie.Models;
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
					File = item.File,
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

			return View(viewFile);
		}
    }
}