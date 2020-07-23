using Microsoft.AspNetCore.Mvc;

namespace OnlineJudgeWeb.Controllers
{
    public class CreateProblemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}