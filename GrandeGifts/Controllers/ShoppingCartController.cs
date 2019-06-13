using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
// Added namespaces:
using GrandeGifts.Services;
using GrandeGifts.Models;
using Microsoft.AspNetCore.Http;
using GrandeGifts.Helpers;
using Newtonsoft.Json;
using GrandeGifts.ViewModels.ShoppingCart;
using Microsoft.AspNetCore.Identity;
using System.Web.Http;
using NonActionAttribute = Microsoft.AspNetCore.Mvc.NonActionAttribute;

namespace GrandeGifts.Controllers
{
    public class ShoppingCartController : Controller
    {
        private const string cartKey = "_ShoppingCart";
        private const string items = "_NumberOfItemsInCart";
        private const string totalPrice = "_TotalPrice";

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDataService<Address> _addressService;
        private readonly IDataService<Hamper> _hamperService;
        private readonly IDataService<ShoppingCartItem> _shoppingCartService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        private TextFormatter _textFormatter { get; set; }

        public ShoppingCartController(UserManager<ApplicationUser> U,
                                      IDataService<Address> A,
                                      IDataService<Hamper> H,
                                      IDataService<ShoppingCartItem> S,
                                      IHttpContextAccessor httpContextAccessor)
        {
            _userManager = U;
            _addressService = A;
            _hamperService = H;
            _shoppingCartService = S;
            _textFormatter = new TextFormatter();
            _httpContextAccessor = httpContextAccessor;
        }

        [AllowAnonymous]
        [NonAction]
        public List<ShoppingCartItem> retrieveCartFromSession()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(cartKey)))
            {
                HttpContext.Session.SetString(items, "0");
                HttpContext.Session.SetString(totalPrice, "$0.00");
                return new List<ShoppingCartItem>();
            }
            else
            {
                var sessionItems = HttpContext.Session.GetString(cartKey);
                return JsonConvert.DeserializeObject<List<ShoppingCartItem>>(sessionItems);
            }
        }

        [AllowAnonymous]
        [NonAction]
        public void updateShoppingCart(List<ShoppingCartItem> shoppingCartItemList)
        {
            double cartBalance = calculateTotalPrice(shoppingCartItemList, 1, false);

            var serialisedShoppingCart = JsonConvert.SerializeObject(shoppingCartItemList);

            HttpContext.Session.SetString(cartKey, serialisedShoppingCart);
            HttpContext.Session.SetString(items, shoppingCartItemList.Count().ToString());
            HttpContext.Session.SetString(totalPrice, "$" + cartBalance.ToString());
        }

        [AllowAnonymous]
        [NonAction]
        public double calculateTotalPrice(List<ShoppingCartItem> shoppingCartItemList, double discount, bool includeDelivery)
        {
            if (shoppingCartItemList == null)
            {
                return 0;
            }
            else
            {
                double totalPrice = 0;
                if (discount > 1)
                {
                    discount = 1;
                }

                foreach (ShoppingCartItem item in shoppingCartItemList)
                {
                    totalPrice += (item.Hamper.Price * item.Quantity * discount);
                }

                if (includeDelivery)
                {
                    totalPrice += 7.5;
                }
                return totalPrice;
            }
        }

        [AllowAnonymous]
        [NonAction]
        public void clearShoppingCart()
        {
            HttpContext.Session.Remove(cartKey);
            HttpContext.Session.SetString(items, "0");
            HttpContext.Session.SetString(totalPrice, "$0.00");
        }

        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult AddToCart(int HamperId)
        {
            ViewBag.CartHasItems = true;
            Hamper chosenHamper = _hamperService.GetSingle(x => x.HamperId == HamperId);
            List<ShoppingCartItem> shoppingCart = retrieveCartFromSession();

            ShoppingCartItem newShoppingCartItem;

            // Check whether or not there's anything in session:
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(cartKey)))
            {
                newShoppingCartItem = new ShoppingCartItem
                {
                    Hamper = chosenHamper,
                    Quantity = 1
                };

                shoppingCart.Add(newShoppingCartItem);
            }
            else
            {
                // If this hamper has already been added to the shopping cart,
                // this will extract it, increase the qty by 1, and put it back into session:
                if (shoppingCart.Where(x => x.Hamper.HamperId == HamperId).FirstOrDefault() != null)
                {
                    newShoppingCartItem = shoppingCart.Where(x => x.Hamper.HamperId == HamperId).FirstOrDefault();

                    // Removing newShoppingCartItem before re-adding it
                    // (I wasn't able to find a LINQ equivalent to 'update
                    shoppingCart.Remove(newShoppingCartItem);
                    newShoppingCartItem.Quantity++;
                    shoppingCart.Add(newShoppingCartItem);
                }
                else
                {
                    newShoppingCartItem = new ShoppingCartItem
                    {
                        Hamper = chosenHamper,
                        Quantity = 1
                    };

                    shoppingCart.Add(newShoppingCartItem);
                }
            }

            updateShoppingCart(shoppingCart);

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult ViewCart()
        {
            ViewBag.CartHasItems = true;

            // Check to see if the cart is null:
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(cartKey)))
            {
                ViewBag.CartHasItems = false;
            }
            else
            {
                List<ShoppingCartItem> shoppingCart = retrieveCartFromSession();
                List<ShoppingCartViewViewModel> VM = new List<ShoppingCartViewViewModel>();

                double totalPriceLessDelivery = calculateTotalPrice(shoppingCart, 1, false);

                foreach (ShoppingCartItem item in shoppingCart)
                {
                    string itemPricePrinted = "$" + (item.Quantity * item.Hamper.Price).ToString();

                    ShoppingCartViewViewModel VM_Item = new ShoppingCartViewViewModel
                    {
                        HamperId = item.Hamper.HamperId,
                        HamperName = item.Hamper.HamperName,
                        Quantity = item.Quantity,
                        Price = itemPricePrinted,
                        ImageUrl = _hamperService.GetSingle(x => x.HamperId == item.Hamper.HamperId).ImageUrl
                    };

                    VM.Add(VM_Item);
                }
                ViewBag.TotalPrice = totalPriceLessDelivery;
                return View(VM);
            }
            return View();
        }

        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult DeleteItem(int HamperId)
        {
            Hamper chosenHamper = _hamperService.GetSingle(x => x.HamperId == HamperId);
            List<ShoppingCartItem> shoppingCart = retrieveCartFromSession();
            ViewBag.CartHasItems = true;

            if (shoppingCart.Count() <= 1)
            {
                // If this item was the last item remaining (and the cart is now empty)
                // I'll save myself the trouble and just clear the whole cart.
                clearShoppingCart();
                ViewBag.CartHasItems = false;
            }
            else
            {
                ShoppingCartItem deletedItem = shoppingCart.Where(x => x.Hamper.HamperId == HamperId).FirstOrDefault();
                shoppingCart.Remove(deletedItem);
                updateShoppingCart(shoppingCart);
            }
            return RedirectToAction("ViewCart", "ShoppingCart");
        }

        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        public IActionResult Checkout()
        {
            List<ShoppingCartItem> shoppingCart = retrieveCartFromSession();
            ViewBag.CartHasItems = shoppingCart.Count() > 0 ? true : false;
            ViewBag.LoggedIn = false;

            ShoppingCartCheckoutViewModel VM = new ShoppingCartCheckoutViewModel
            {
                PriceLessDelivery = calculateTotalPrice(shoppingCart, 1, true),
                shoppingCartItems = shoppingCart
            };

            if (User.Identity.IsAuthenticated)
            {
                string UserName = User.Identity.Name;
                ApplicationUser user = _userManager.Users.FirstOrDefault(x => x.UserName == UserName);
                ViewBag.LoggedIn = true;
                ViewBag.UserHasAddress = true;

                IEnumerable<Address> userAddresses = _addressService.Query(x => x.ApplicationUserId == user.Id);

                if (userAddresses.Count() < 1)
                {
                    ViewBag.UserHasAddress = false;
                }
                else if (User.IsInRole("Admin"))
                {
                    VM.PreferredAddress = userAddresses.FirstOrDefault();
                }
                else
                {
                    VM.PreferredAddress = userAddresses.Where(y => y.PreferredShippingAddress).FirstOrDefault();
                }
            }
            return View(VM);
        }
    }
}