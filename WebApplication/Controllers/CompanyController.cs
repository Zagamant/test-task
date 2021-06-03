using System;
using System.Threading.Tasks;
using BLL.CompanyManagement;
using BLL.Models.CompanyManagement;
using Microsoft.AspNetCore.Mvc;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
        }

        /// <summary>
        /// Get page of all Companies
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index() =>
            View(await _companyService.GetAllAsync());


        /// <summary>
        /// Return page with form to add new <see cref="CompanyModel"/>
        /// </summary>
        [HttpGet]
        public IActionResult Add()
        {
            ViewData["title"] = "Добавление компании";

            return View("Edit", CompanyViewModel.Empty);
        }

        /// <summary>
        /// Provide action to add new Company
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(CompanyViewModel vm)
        {
            if (!ModelState.IsValid)
                return View("Edit", vm);

            var dto = CompanyViewModel.ToModel(vm);
            var result = await _companyService.AddAsync(dto);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Return page with form to edit existed <see cref="CompanyModel"/>
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["title"] = "Редактирование компании";

            var entity = await _companyService.GetAsync(id);
            var vm = CompanyViewModel.FromModel(entity);
            return View("Edit", vm);
        }

        /// <summary>
        /// Provide action to update existed Company with <see cref="CompanyViewModel"/>
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(CompanyViewModel vm)
        {
            if (!ModelState.IsValid)
                return View("Edit", vm);
            var model = CompanyViewModel.ToModel(vm);
            var result = await _companyService.EditAsync(model.Id, model);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Remove <see cref="CompanyModel"/>
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _companyService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}