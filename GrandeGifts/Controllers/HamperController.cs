using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
// Added namespaces:
using GrandeGifts.Services;
using GrandeGifts.Models;
using GrandeGifts.Helpers;
using GrandeGifts.ViewModels.Hamper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace GrandeGifts.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HamperController : Controller
    {
        private readonly IDataService<Hamper> _hamperService;
        private readonly IDataService<Category> _catService;
        private readonly IDataService<ShoppingCartItem> _shoppingCartService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        private TextFormatter _textFormatter { get; set; }

        public HamperController(IDataService<Hamper> H,
                                IDataService<Category> C,
                                IDataService<ShoppingCartItem> S,
                                IHttpContextAccessor httpContextAccessor)
        {
            _hamperService = H;
            _catService = C;
            _shoppingCartService = S;
            _textFormatter = new TextFormatter();
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddFirstCategory()
        {
            return View();
        }

        [HttpGet]
        [Route("edit-hamper/category/{CategoryId}")]
        public IActionResult Add(int CategoryId)
        {
            if (_catService.GetAll().Count() > 0)
            {
                HamperAddViewModel VM = new HamperAddViewModel
                {
                    CategoryId = CategoryId
                };

                IEnumerable<Category> categories = _catService.GetAll();
                ViewBag.CategoriesExist = true;
                ViewBag.Categories = categories;
                return View(VM);
            }
            else
            {
                ViewBag.CategoriesExist = false;
                return View();
            }
            //TempData["categoryId"] = CategoryId.ToString();
            //_httpContextAccessor.HttpContext.Session.SetString("categoryId", CategoryId.ToString());
        }

        [Route("edit-hamper/category/{CategoryId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(HamperAddViewModel VM)
        {
            if (ModelState.IsValid)
            {
                string hamperName = _textFormatter.
                    RemoveDoubleSpaces(_textFormatter.
                    CapitaliseFirstLetters(VM.HamperName, false));

                string description = VM.Description != null && VM.Description != "" ? _textFormatter.
                    RemoveDoubleSpaces(VM.Description) : "";

                Hamper newHamper = new Hamper()
                {
                    HamperName = hamperName,
                    Description = description,
                    Price = VM.Price,
                    Products = VM.Products,
                    ImageUrl = VM.ImageUrl,
                    Supplier = VM.Supplier,
                    InUse = true,
                    CategoryId = VM.CategoryId
                };

                _hamperService.Create(newHamper);

                return RedirectToAction("Edit", "Category", new { CategoryId = newHamper.CategoryId });
            }
            else
            {
                return View(VM);
            }
        }

        [Route("edit-hamper/{HamperId}")]
        [HttpGet]
        public IActionResult Edit(int HamperId)
        {
            Hamper hamper = _hamperService.Query(x => x.HamperId == HamperId).FirstOrDefault();

            //TempData["categoryId"] = CategoryId.ToString();
            //_httpContextAccessor.HttpContext.Session.SetString("categoryId", CategoryId.ToString());

            HamperEditViewModel VM = new HamperEditViewModel
            {
                HamperId = HamperId,
                HamperName = hamper.HamperName,
                Description = hamper.Description,
                Products = hamper.Products,
                Supplier = hamper.Supplier,
                Price = hamper.Price,
                ImageUrl = hamper.ImageUrl,
                CategoryId = hamper.CategoryId
            };
            return View(VM);
        }

        [Route("edit-hamper/{HamperId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HamperEditViewModel VM)
        {
            if (ModelState.IsValid)
            {
                Hamper updatedHamper = new Hamper
                {
                    HamperId = VM.HamperId,
                    HamperName = VM.HamperName,
                    Description = VM.Description,
                    Products = VM.Products,
                    Supplier = VM.Supplier,
                    Price = VM.Price,
                    ImageUrl = VM.ImageUrl,
                    CategoryId = VM.CategoryId,
                    InUse = true
                };

                _hamperService.Update(updatedHamper);

                return RedirectToAction("Edit", "Category", new { CategoryId = VM.CategoryId });
            }
            else
            {
                return View(VM);
            }
        }

        [Route("delete-hamper/{HamperId}")]
        [HttpGet]
        public IActionResult Delete(int HamperId)
        {
            Hamper hamperToDelete = _hamperService.GetSingle(x => x.HamperId == HamperId);

            HamperDeleteViewModel VM = new HamperDeleteViewModel
            {
                HamperId = HamperId,
                HamperName = hamperToDelete.HamperName
            };

            return View(VM);
        }

        [Route("delete-hamper/{HamperId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(HamperDeleteViewModel VM)
        {
            Hamper hamperToDelete = _hamperService.GetSingle(x => x.HamperId == VM.HamperId);
            _hamperService.Delete(hamperToDelete);

            return RedirectToAction("Edit", "Category", new { CategoryId = hamperToDelete.CategoryId });
        }

        [Route("view-hampers-by-category/{CategoryId}")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ViewByCategory(int CategoryId)
        {          
            if (_hamperService.GetAll().Count() > 0 && _hamperService.Query(x => x.CategoryId == CategoryId) != null)
            {
                string categoryName = _catService.GetSingle(x => x.CategoryId == CategoryId).CategoryName;
                IEnumerable<Hamper> hampers = _hamperService.Query(x => x.CategoryId == CategoryId).OrderBy(y => y.HamperName);
                List<HamperViewByCategoryViewModel> VM = new List<HamperViewByCategoryViewModel>();

                foreach(Hamper hamper in hampers)
                {
                    if (hamper.InUse)
                    {
                        HamperViewByCategoryViewModel viewModel = new HamperViewByCategoryViewModel
                        {
                            HamperId = hamper.HamperId,
                            HamperName = hamper.HamperName,
                            Description = hamper.Description,
                            ImageUrl = hamper.ImageUrl,
                            Products = hamper.Products,
                            Supplier = hamper.Supplier,
                            Price = hamper.Price
                        };
                        VM.Add(viewModel);
                    } 
                }
                ViewBag.HampersExist = true;
                ViewBag.CategoryName = categoryName;
                return View(VM);
            }
            else
            {
                ViewBag.HampersExist = false;
                return View();
            }
        }

        [Route("view-details/{HamperId}")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ViewDetails(int HamperId)
        {
            ViewBag.HamperFound = true;
            if (_hamperService.GetSingle(x => x.HamperId == HamperId) != null)
            {
                Hamper hamper = _hamperService.GetSingle(x => x.HamperId == HamperId);
                string categoryName = _catService.GetSingle(x => x.CategoryId == hamper.CategoryId).CategoryName;
                string price = "$" + hamper.Price.ToString();

                HamperViewDetailsViewModel VM = new HamperViewDetailsViewModel
                {
                    HamperName = hamper.HamperName,
                    HamperId = hamper.HamperId,
                    CategoryName = categoryName,
                    Description = hamper.Description,
                    Price = price,
                    Products = hamper.Products,
                    Supplier = hamper.Supplier,
                    ImageUrl = hamper.ImageUrl
                };
                return View(VM);
            }
            else
            {
                ViewBag.HamperFound = false;
                return View();
            }
        }
    }
}