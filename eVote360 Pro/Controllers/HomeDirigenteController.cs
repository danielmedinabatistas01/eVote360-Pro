using eVote360Pro.Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eVote360_Pro.Controllers
{
    public class HomeDirigenteController : Controller
    {
        private readonly IUserSession _userSession;
        private readonly IHomeDirigenteService _homeDirigenteService;

        public HomeDirigenteController(
            IUserSession userSession,
            IHomeDirigenteService homeDirigenteService)
        {
            _userSession = userSession;
            _homeDirigenteService = homeDirigenteService;
        }

        public async Task<IActionResult> Index()
        {
            if (!_userSession.HasUser())
                return RedirectToAction(
                    "Index",
                    "Login");

            if (!_userSession.IsDirigente())
                return RedirectToAction(
                    "AccessDenied",
                    "Login");

            var dashboard =
                await _homeDirigenteService
                    .GetDashboardAsync();

            return View(dashboard);
        }
    }
}
