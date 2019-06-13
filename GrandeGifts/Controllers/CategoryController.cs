using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
// Added namespaces:
using GrandeGifts.Services;
using GrandeGifts.Models;
using GrandeGifts.Helpers;
using GrandeGifts.ViewModels.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace GrandeGifts.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IDataService<Category> _catService;
        private readonly IDataService<Hamper> _hamperService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private TextFormatter _textFormatter;

        public CategoryController(IDataService<Category> C,
                                  IDataService<Hamper> H,
                                  IHttpContextAccessor httpContextAccessor)
        {
            _catService = C;
            _hamperService = H;
            _textFormatter = new TextFormatter();
            _httpContextAccessor = httpContextAccessor;
        }

        [NonAction]
        public void UpdateHampersInUse(Category category, bool inUse)
        {
            IEnumerable<Hamper> hampers = _hamperService.Query(x => x.CategoryId == category.CategoryId);

            if(hampers.Count() > 0)
            {
                foreach (var h in hampers)
                {
                    h.InUse = inUse;
                }
                _hamperService.UpdateMultiple(hampers);
            }
            return;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CategoryAddViewModel VM, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                string categoryName = _textFormatter.
                    RemoveDoubleSpaces(_textFormatter.
                    CapitaliseFirstLetters(VM.CategoryName, false));

                string description = VM.Description != null && VM.Description != "" ? _textFormatter.
                    RemoveDoubleSpaces(VM.Description) : "";

                Category newCategory = new Category()
                {
                    CategoryName = categoryName,
                    Description = description,
                    ImageUrl = VM.ImageUrl,
                    InUse = true
                };

                _catService.Create(newCategory);

                return RedirectToAction("SuccessfullyAdded", "Category");
            }
            else
            {
                return View(VM);
            }
        }

        public IActionResult SuccessfullyAdded()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Manage()
        {
            if (_catService.GetAll() != null)
            {
                IEnumerable<Category> allCategories = _catService.GetAll().OrderByDescending(x => x.InUse).ThenBy(y => y.CategoryName);
                int total = allCategories.Count();
                int inUse = allCategories.Where(x => x.InUse).Count();
                int notInUse = total - inUse;

                CategoryManageViewModel VM = new CategoryManageViewModel
                {
                    Total = total,
                    InUse = inUse,
                    NotInUse = notInUse,
                    Categories = allCategories
                };

                return View(VM);
            }
            else
            {
                return RedirectToAction("NoCategoriesExist", "Category");
            }
        }

        [HttpGet]
        public IActionResult NoCategoriesExist()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ViewAll()
        {
            if (_catService.GetAll() != null)
            {
                IEnumerable<Category> allCategories = _catService.GetAll().Where(x => x.InUse).OrderBy(y => y.CategoryName);
                int total = allCategories.Count();

                CategoryViewAllViewModel VM = new CategoryViewAllViewModel
                {
                    Total = total,
                    Categories = allCategories
                };

                return View(VM);
            }
            else
            {
                return RedirectToAction("NoCategoriesExist", "Category");
            }
        }

        [Route("edit-category/{CategoryId}")]
        [HttpGet]
        public IActionResult Edit(int CategoryId)
        {
            Category cat = _catService.Query(x => x.CategoryId == CategoryId).FirstOrDefault();

            TempData["categoryId"] = CategoryId.ToString();
            //_httpContextAccessor.HttpContext.Session.SetString("categoryId", CategoryId.ToString());

            CategoryEditViewModel VM = new CategoryEditViewModel
            {
                CategoryId = cat.CategoryId,
                CategoryName = cat.CategoryName,
                Description = cat.Description,
                ImageUrl = cat.ImageUrl
            };

            // Creating a list of hampers attached to this category:
            bool HampersExist = false;

            if (_hamperService.Query(x => x.CategoryId == cat.CategoryId).Count() != 0)
            {
                HampersExist = true;
                ViewBag.Hampers = _hamperService.Query(x => x.CategoryId == cat.CategoryId);
            }
            ViewBag.HampersExist = HampersExist;
            return View(VM);
        }

        [Route("edit-category/{CategoryId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryEditViewModel VM)
        {
            if (ModelState.IsValid)
            {
                Category cat = _catService.Query(x => x.CategoryId == VM.CategoryId).FirstOrDefault();
                cat.CategoryName = VM.CategoryName;
                cat.Description = VM.Description;
                cat.ImageUrl = VM.ImageUrl;
                cat.InUse = true;

                _catService.Update(cat);

                return RedirectToAction("SuccessfullyUpdated", "Category");
            }
            else
            {
                return View();
            }
        }

        public IActionResult SuccessfullyUpdated()
        {
            return View();
        }

        [Route("delete-category/{CategoryId}")]
        [HttpGet]
        public IActionResult Delete(int CategoryId)
        {
            Category categoryToDelete = _catService.GetSingle(x => x.CategoryId == CategoryId);

            CategoryDeleteViewModel VM = new CategoryDeleteViewModel
            {
                CategoryId = CategoryId,
                CategoryName = categoryToDelete.CategoryName
            };

            return View(VM);
        }

        [Route("delete-category/{CategoryId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(CategoryDeleteViewModel VM)
        {
            Category categoryToDelete = _catService.GetSingle(x => x.CategoryId == VM.CategoryId);

            UpdateHampersInUse(categoryToDelete, false);

            categoryToDelete.InUse = false;
            _catService.Update(categoryToDelete);

            return RedirectToAction("Manage", "Category");
        }

        [Route("reactivate-category/{CategoryId}")]
        [HttpGet]
        public IActionResult Reactivate(int CategoryId)
        {
            Category categoryToReactivate = _catService.GetSingle(x => x.CategoryId == CategoryId);

            CategoryReactivateViewModel VM = new CategoryReactivateViewModel
            {
                CategoryId = CategoryId,
                CategoryName = categoryToReactivate.CategoryName
            };

            return View(VM);
        }

        [Route("reactivate-category/{CategoryId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reactivate(CategoryReactivateViewModel VM)
        {
            Category categoryToReactivate = _catService.GetSingle(x => x.CategoryId == VM.CategoryId);

            UpdateHampersInUse(categoryToReactivate, true);

            categoryToReactivate.InUse = true;
            _catService.Update(categoryToReactivate);

            return RedirectToAction("Manage", "Category");
        }
    }
}