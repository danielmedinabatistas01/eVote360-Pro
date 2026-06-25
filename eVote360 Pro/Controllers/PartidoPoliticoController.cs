using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.ViewModels.PartidoPolitico;
using Microsoft.AspNetCore.Mvc;

namespace eVote360_Pro.Controllers
{
    public class PartidoPoliticoController : Controller
    {
        private readonly IPartidoPoliticoService _service;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserSession _userSession;

        public PartidoPoliticoController(
            IPartidoPoliticoService service,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            IUserSession userSession)
        {
            _service = service;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _userSession = userSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            var partidos = await _service.GetAllAsync();
            return View(_mapper.Map<List<PartidoPoliticoViewModel>>(partidos));
        }

        public IActionResult Create()
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            return View("Save", new SavePartidoPoliticoViewModel()
            {
                Nombre = string.Empty,
                Siglas = string.Empty,
                EsActivo = true
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePartidoPoliticoViewModel vm)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            if (!ModelState.IsValid) return View("Save", vm);

            if (vm.LogoFile == null)
            {
                ModelState.AddModelError("LogoFile", "El logo del partido es requerido.");
                return View("Save", vm);
            }

            try
            {
                vm.LogoUrl = await UploadFile(vm.LogoFile);
                var dto = _mapper.Map<PartidoPoliticoDto>(vm);
                await _service.AddAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Save", vm);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return RedirectToAction(nameof(Index));

            var vm = _mapper.Map<SavePartidoPoliticoViewModel>(dto);
            return View("Save", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePartidoPoliticoViewModel vm)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            if (!ModelState.IsValid) return View("Save", vm);

            try
            {
                if (vm.LogoFile != null)
                {
                    vm.LogoUrl = await UploadFile(vm.LogoFile);
                }
                var dto = _mapper.Map<PartidoPoliticoDto>(vm);
                await _service.UpdateAsync(vm.Id, dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Save", vm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            try
            {
                await _service.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        private async Task<string> UploadFile(IFormFile file)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "logos");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return "/images/logos/" + uniqueFileName;
        }
    }
}