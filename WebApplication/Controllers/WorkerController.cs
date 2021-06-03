using System.Threading.Tasks;
using BLL.CompanyManagement;
using BLL.WorkerManagement;
using Microsoft.AspNetCore.Mvc;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class WorkerController : Controller
    {
        private readonly IWorkerService _workerService;
        private readonly ICompanyService _companyService;

        public WorkerController(IWorkerService employeeService, ICompanyService companyService) =>
            (_workerService, _companyService) = (employeeService, companyService);

        [HttpGet]
        public async Task<IActionResult> Index() =>
            View(await _workerService.GetAllAsync());

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewData["title"] = "Добавление нового сотрудника";

            var vm = WorkerViewModel.Empty;
            vm.CompaniesList = await _companyService.GetAllAsync();
            return View("Edit", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([FromForm] WorkerViewModel vm)
        {
            if (!ModelState.IsValid)
                return await Add();

            var dto = WorkerViewModel.ToModel(vm);
            var result = await _workerService.AddAsync(dto);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["title"] = "Редактирование сотрудника";
            var vm = WorkerViewModel.FromModel(await _workerService.GetAsync(id));
            vm.CompaniesList = await _companyService.GetAllAsync();
            return View("Edit", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(WorkerViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.CompaniesList = await _companyService.GetAllAsync();
                return View("Edit", vm);
            }
            var model = WorkerViewModel.ToModel(vm);
            var result = await _workerService.EditAsync(model.Id, model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _workerService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}