using eVote360Pro.Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eVote360_Pro.Controllers
{
    public class HomeDirigenteController : Controller
    {
        private readonly IUserSession _userSession;

        public HomeDirigenteController(IUserSession userSession)
        {
            _userSession = userSession;
        }

        public IActionResult Index()
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsDirigente())
                return RedirectToAction("AccessDenied", "Login");

            return View();
        }
    }
}