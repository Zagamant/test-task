using System;
using System.Threading.Tasks;
using BLL.CompanyManagement;
using BLL.Models.WorkerManagement;
using BLL.WorkerManagement;
using Microsoft.AspNetCore.Mvc;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class WorkerController : Controller
    {
        private readonly IWorkerService _workerService;
        private readonly ICompanyService _companyService;

        public WorkerController(IWorkerService workerService, ICompanyService companyService)
        {
            _workerService = workerService ?? throw new ArgumentNullException(nameof(workerService));
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
        }

        /// <summary>
        /// Get page of all Workers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index() =>
            View(await _workerService.GetAllAsync());

        /// <summary>
        /// Return page with form to add new <see cref="WorkerModel"/>
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewData["title"] = "Добавление нового сотрудника";

            var vm = WorkerViewModel.Empty;
            vm.CompaniesList = await _companyService.GetAllAsync();
            return View("Edit", vm);
        }

        /// <summary>
        /// Provide action to add new <see cref="WorkerModel"/>
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Return page with form to edit existed <see cref="WorkerModel"/>
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["title"] = "Редактирование сотрудника";
            var vm = WorkerViewModel.FromModel(await _workerService.GetAsync(id));
            vm.CompaniesList = await _companyService.GetAllAsync();
            return View("Edit", vm);
        }

        /// <summary>
        /// Provide action to update existed Worker with <see cref="WorkerViewModel"/>
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Remove <see cref="WorkerModel"/>
        /// </summary>
        public async Task<IActionResult> Delete(int id)
        {
            await _workerService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}