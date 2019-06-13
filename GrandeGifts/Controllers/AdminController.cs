using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
// Added namespaces:
using Microsoft.AspNetCore.Identity;
using GrandeGifts.Models;
using GrandeGifts.Services;
using GrandeGifts.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using GrandeGifts.Helpers;

namespace GrandeGifts.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // Manage users:
        private readonly UserManager<ApplicationUser> _userManager;
        // Used to manage sign-in and sign-out:
        private readonly SignInManager<ApplicationUser> _signInManager;
        // Role service:
        private readonly RoleManager<IdentityRole> _roleManagerService;
        private readonly IDataService<Address> _addressService;
        private TextFormatter _textFormatter { get; set; }

        public AdminController(UserManager<ApplicationUser> U,
                                 SignInManager<ApplicationUser> S,
                                 RoleManager<IdentityRole> R,
                                 IDataService<Address> A)
        {
            _userManager = U;
            _signInManager = S;
            _roleManagerService = R;
            _addressService = A;
            _textFormatter = new TextFormatter();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "")
        {
            AdminLoginViewModel VM = new AdminLoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(VM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLoginViewModel VM)
        {
            // Check model binding worked correctly:
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(VM.Email, VM.Password, VM.RememberMe, false);

                // Check that the Sign In Manager was able to sign user in successfully:
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(VM.ReturnUrl))
                    {
                        return RedirectToAction("LoggedInSuccessfully", "Admin");
                    }
                    else
                    {
                        return Redirect(VM.ReturnUrl);
                    }
                }
            }
            ModelState.AddModelError("", "Incorrect Username or Password");
            return View(VM);
        }

        public IActionResult LoggedInSuccessfully()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult RegisterAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAdmin(AdminRegisterAdminViewModel VM, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                // Create an object that will become a new user and add the newly created address list:
                ApplicationUser newUser = new ApplicationUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = VM.Email,
                    Email = VM.Email,
                    GivenNames = VM.GivenNames,
                    Surname = VM.Surname
                };

                // Use the 'Create' method from the userManager to create a new user.
                IdentityResult result = await _userManager.CreateAsync(newUser, VM.Password);

                // If the return IdentityResult succeeded (if boolean prop of 'result' == true), proceed:
                if (result.Succeeded)
                {
                    // Assign newUser to role:
                    await _userManager.AddToRoleAsync(newUser, "Admin");

                    // Add user to address, save address in the DB:
                    Address newAddress = new Address
                    {
                        AddressId = new Guid(),

                        StreetAddress = VM.StreetAddress == null ? "" :
                    _textFormatter.RemoveDoubleSpaces
                    (_textFormatter.CapitaliseFirstLetters(VM.StreetAddress, false)),

                        Suburb = VM.Suburb == null ? "" :
                    _textFormatter.RemoveDoubleSpaces
                    (_textFormatter.CapitaliseFirstLetters(VM.Suburb, false)),

                        State = VM.State == null ? "" :
                    _textFormatter.RemoveDoubleSpaces
                    (_textFormatter.CapitaliseFirstLetters(VM.State, false)),

                        Postcode = VM.Postcode,

                        AddressType = "Home",

                        PreferredShippingAddress = true,

                        ApplicationUserId = newUser.Id
                    };

                    // Add address to the database:
                    _addressService.Create(newAddress);

                    return RedirectToAction("RegisteredSuccessfully", "Admin");
                }
                else
                {
                    // Record which errors have occured:
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(VM);
        }

        [HttpGet]
        public IActionResult RegisteredSuccessfully()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ManageProfile()
        {
            if (User.IsInRole("Admin") || User.IsInRole("SuperUser"))
            {
                string UserName = User.Identity.Name;
                bool userHasAddress = false;
                ApplicationUser user = _userManager.Users.Where(x => x.Email == UserName).FirstOrDefault();

                AdminManageProfileViewModel VM = new AdminManageProfileViewModel()
                {
                    Email = user.Email,
                    GivenNames = user.GivenNames,
                    Surname = user.Surname,
                    PhoneNo = user.PhoneNumber
                };

                Address userAddress = new Address();

                // Check if user has an address in the system. If not, deal with the thrown exception.
                // I've simply added 'Exception' to the catch block as I'm not too fussed about which specific exception I get.
                try
                {
                    userAddress = _addressService.Query(x => x.ApplicationUserId == user.Id).FirstOrDefault();
                    VM.StreetAddress = userAddress.StreetAddress;
                    VM.Suburb = userAddress.Suburb;
                    VM.State = userAddress.State;
                    VM.Postcode = userAddress.Postcode;
                    userHasAddress = true;
                }
                catch (Exception)
                {
                    VM.StreetAddress = "";
                    VM.Suburb = "";
                    VM.State = "";
                    VM.Postcode = 1000;
                }

                ViewBag.UserHasAddress = userHasAddress;
                return View(VM);
            }
            else
            {
                return View();
            }
        }

        // ---- Manage Profile ---- //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageProfile(AdminManageProfileViewModel VM)
        {
            string UserName = User.Identity.Name;

            ApplicationUser user = _userManager.Users.FirstOrDefault(x => x.UserName == UserName);

            user.UserName = VM.Email;
            user.Email = VM.Email;
            user.GivenNames = VM.GivenNames;
            user.Surname = VM.Surname;
            user.PhoneNumber = VM.PhoneNo;

            IdentityResult Updateduser = await _userManager.UpdateAsync(user);

            if (Updateduser.Succeeded)
            {
                try
                {
                    Address userAddress = _addressService.Query(x => x.ApplicationUserId == user.Id).FirstOrDefault();
                    userAddress.AddressType = "Home";
                    userAddress.StreetAddress = VM.StreetAddress;
                    userAddress.Suburb = VM.Suburb;
                    userAddress.State = VM.State;
                    userAddress.Postcode = VM.Postcode;
                    userAddress.PreferredShippingAddress = true;
                    userAddress.ApplicationUserId = user.Id;
                    _addressService.Update(userAddress);
                }
                catch (Exception)
                {
                    Address userAddress = new Address();
                    userAddress.AddressId = new Guid();
                    userAddress.AddressType = "Home";
                    userAddress.StreetAddress = VM.StreetAddress;
                    userAddress.Suburb = VM.Suburb;
                    userAddress.State = VM.State;
                    userAddress.Postcode = VM.Postcode;
                    userAddress.PreferredShippingAddress = true;
                    userAddress.ApplicationUserId = user.Id;
                    _addressService.Create(userAddress);
                }
                return RedirectToAction("Index", "Home");
            }
            // To do: try catch block around return in case user cant be found or there's an error...
            return View(VM);
        }
    }
}