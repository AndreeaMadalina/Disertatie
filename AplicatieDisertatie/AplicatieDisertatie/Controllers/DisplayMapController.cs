using System.Web.Mvc;

namespace AplicatieDisertatie.Controllers
{
    public class DisplayMapController : Controller
    {
        // GET: DisplayMap
        public ActionResult DisplayMap(int actionId)
        {
            ViewBag.ActionId = actionId;
            if(actionId == 1)
            {
                ViewBag.PageTitle = "Adresa pentru institutia de eliberari permise auto: ";
            }
            else if (actionId == 2)
            {
                ViewBag.PageTitle = "Adresa pentru institutia de eliberari carti de identitate si pasapoarte: ";
            }
            else if(actionId == 3)
            {
                ViewBag.PageTitle = "Adresa pentru institutia de inmatriculari autovehicule: ";
            }
            else if(actionId == 4)
            {
                ViewBag.PageTitle = "Adresa pentru institutia de radieri autovehicule: ";
            }
            return View();
        }
    }
}