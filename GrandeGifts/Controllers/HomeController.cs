using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
// Added namespaces:
using GrandeGifts.Helpers;
using GrandeGifts.Services;
using GrandeGifts.Models;
using GrandeGifts.ViewModels.Home;

namespace GrandeGifts.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataService<Category> _catService;
        private readonly IDataService<Hamper> _hamperService;
        private TextFormatter _textFormatter;

        public HomeController(IDataService<Category> C,
                              IDataService<Hamper> H)
        {
            _catService = C;
            _hamperService = H;
            _textFormatter = new TextFormatter();
        }

        public IActionResult Index()
        {
            IEnumerable<Category> Categories = _catService.Query(x => x.InUse).Take(3);

            HomeIndexViewModel VM = new HomeIndexViewModel
            {
                Categories = Categories
            };
            return View(VM);
        }

        [HttpPost]
        [Route("search-by-category/{Keyword}")]
        [Route("/Search/")]
        [ValidateAntiForgeryToken]
        public IActionResult Search(string Keyword)
        {
            string scrubbedKeyword = _textFormatter.CapitaliseFirstLetters(
                                     _textFormatter.RemoveDoubleSpaces(Keyword), false);

            Category foundCat = _catService.GetSingle(x => x.CategoryName == scrubbedKeyword);

            if(foundCat != null)
            {
                IEnumerable<Hamper> foundHampers = _hamperService.Query(x => x.CategoryId == foundCat.CategoryId);
                HomeSearchViewModel VM = new HomeSearchViewModel
                {
                    Keyword = scrubbedKeyword,
                    Hampers = foundHampers
                };

                ViewBag.CategoryExist = true;
                return View(VM);
            }
            else
            {
                ViewBag.CategoryExist = false;
                return View();
            }   
        }

        [HttpPost]
        [Route("filter-by-price/{Keyword}")]
        [Route("/filter-by-price/")]
        [ValidateAntiForgeryToken]
        public IActionResult FilterByPrice(HomeSearchViewModel VM)
        {
            string scrubbedKeyword = _textFormatter.CapitaliseFirstLetters(
                                     _textFormatter.RemoveDoubleSpaces(VM.Keyword), false);

            Category foundCat = _catService.GetSingle(x => x.CategoryName == scrubbedKeyword);

            if (foundCat != null)
            {
                IEnumerable<Hamper> foundHampers = _hamperService.Query(x => x.CategoryId == foundCat.CategoryId);
                IEnumerable<Hamper> filteredHampers = foundHampers.Where(y => y.Price >= VM.Min && y.Price <= VM.Max).ToList();

                if (filteredHampers.Count() != 0)
                {
                    HomeSearchViewModel newVM = new HomeSearchViewModel
                    {
                        Keyword = scrubbedKeyword,
                        Hampers = filteredHampers,
                        Min = VM.Min,
                        Max = VM.Max
                    };

                    ViewBag.ResultsFound = true;
                    return View(newVM);
                }
                else
                {
                    HomeSearchViewModel newVM = new HomeSearchViewModel
                    {
                        Keyword = scrubbedKeyword,
                        Hampers = foundHampers,
                        Min = VM.Min,
                        Max = VM.Max
                    };

                    ViewBag.ResultsFound = false;
                    return View(VM);
                }  
            }
            else
            {
                ViewBag.ResultsFound = false;
                return View();
            }
        }
    }
}