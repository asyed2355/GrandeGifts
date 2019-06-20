using Microsoft.AspNetCore.Mvc;

namespace GrandeGifts.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult OrderProcessedSuccessfully()
        {
            return View(); 
        }
    }
}