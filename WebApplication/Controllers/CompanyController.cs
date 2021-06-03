using System.Linq;
using System.Threading.Tasks;
using BLL.CompanyManagement;
using BLL.Models.CompanyManagement;
using BLL.WorkerManagement;
using Microsoft.AspNetCore.Mvc;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IWorkerService _workerService;

        public CompanyController(ICompanyService companyService, IWorkerService workerService)
        {
            _companyService = companyService;
            _workerService = workerService;
        }

        public async Task<IActionResult> Index() =>
            View(await _companyService.GetAllAsync());

        [HttpGet]
        public IActionResult Add()
        {
            ViewData["title"] = "Добавление компании";

            return View("Edit", CompanyViewModel.Empty);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CompanyViewModel vm)
        {
            if (!ModelState.IsValid)
                return View("Edit", vm);
            
            var dto = CompanyViewModel.ToModel(vm);
            var result = await _companyService.AddAsync(dto);

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["title"] = "Редактирование компании";

            var entity = await _companyService.GetAsync(id);
            var vm = CompanyViewModel.FromModel(entity);
            return View("Edit", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CompanyViewModel vm)
        {
            if (!ModelState.IsValid) 
                return View("Edit", vm);
            var model = CompanyViewModel.ToModel(vm);
            var result = await _companyService.EditAsync(model.Id, model);
            return RedirectToAction("Index");

        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _companyService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}