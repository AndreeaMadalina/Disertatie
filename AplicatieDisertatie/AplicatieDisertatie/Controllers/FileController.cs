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
					UpdatedOn = item.UpdatedOn
				});
			}

			//if(files == null || files.Count() == 0)
			//{
			//	return new EmptyResult();
			//}

            return View(viewFiles);
        }
    }
}