using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
// Added namespaces:
using GrandeGifts.Services;
using GrandeGifts.Models;
using System.Net;
using Microsoft.AspNetCore.Cors;
using GrandeGifts.ViewModels.API;

namespace GrandeGifts.Controllers.API
{
    //[Route("api/[controller]")]
    //[ApiController]
    [EnableCors]
    public class HamperApiController : Controller
    {
        private IDataService<Category> _catService;
        private IDataService<Hamper> _hamperService;

        public HamperApiController(IDataService<Category> catService, IDataService<Hamper> hamperService)
        {
            _catService = catService;
            _hamperService = hamperService;
        }

        //Get all hampers
        [HttpGet("api/hampers")]
        [EnableCors]
        public JsonResult GetAll()
        {
            try
            {
                IEnumerable<Hamper> hamperList = _hamperService.GetAll();
                List<HamperApiViewModel> apiReturnList = new List<HamperApiViewModel>();

                foreach(Hamper h in hamperList)
                {
                    string categoryName = _catService.GetSingle(x => x.CategoryId == h.CategoryId).CategoryName;
                    HamperApiViewModel VM = new HamperApiViewModel
                    {
                        HamperId = h.HamperId,
                        HamperName = h.HamperName,
                        ImageUrl = h.ImageUrl,
                        Price = h.Price,
                        Products = h.Products,
                        CategoryName = categoryName
                    };

                    apiReturnList.Add(VM);
                }
                return Json(apiReturnList);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = ex.Message });
            }
        }

        // Get all hampers by category name
        [HttpGet("api/hampersbycategory")]
        public JsonResult GetHampersByCategory(string category)
        {
            /*localhost:44319/api/hampersbycategory?category={categoryName}*/
            try
            {
                Category C = _catService.GetSingle(c => c.CategoryName.ToUpper() == category.ToUpper());

                if (C != null)
                {
                    IEnumerable<Hamper> hamperList = _hamperService.Query(x => x.CategoryId == C.CategoryId);
                    return Json(hamperList);
                }
                else
                {
                    return Json(new { message = "This category cannot be found, unfortunately." });
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = ex.Message });
            }
        }
    }
}