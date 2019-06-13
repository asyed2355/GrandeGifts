using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
// Added namespaces:
using Microsoft.AspNetCore.Identity;
using GrandeGifts.Models;
using GrandeGifts.Services;
using GrandeGifts.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;

namespace GrandeGifts.Controllers
{
    public class AccountController : Controller
    {
        // Manage users:
        private readonly UserManager<ApplicationUser> _userManager;
        // Used to manage sign-in and sign-out:
        private readonly SignInManager<ApplicationUser> _signInManager;
        // Role service:
        private RoleManager<IdentityRole> _roleManagerService;
        private IDataService<Address> _addressService;

        public AccountController(UserManager<ApplicationUser> U,
                                 SignInManager<ApplicationUser> S,
                                 RoleManager<IdentityRole> R,
                                 IDataService<Address> A)
        {
            _userManager = U;
            _signInManager = S;
            _roleManagerService = R;
            _addressService = A;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            AccountLoginViewModel VM = new AccountLoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(VM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccountLoginViewModel VM)
        {
            // Check model binding worked correctly:
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(VM.UserName, VM.Password, VM.RememberMe, false);

                // Check that the Sign In Manager was able to sign user in successfully:
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(VM.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
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

        // ---- Logout ----//
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("LoggedOutSuccessfully", "Account");
        }

        [HttpGet]
        public IActionResult LoggedOutSuccessfully()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AccountRegisterViewModel VM, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                string userId = Guid.NewGuid().ToString();

                ApplicationUser newUser = new ApplicationUser()
                {
                    Id = userId,
                    UserName = VM.UserName,
                    Email = VM.Email,
                    GivenNames = VM.GivenNames,
                    Surname = VM.Surname,
                };

                // Use the 'Create' method from the userManager to create a new user.
                IdentityResult result = await _userManager.CreateAsync(newUser, VM.Password);

                // If the return IdentityResult succeeded (if boolean prop of 'result' == true), proceed:
                if (result.Succeeded)
                {
                    // Assign newUser to role:
                    await _userManager.AddToRoleAsync(newUser, "Customer");

                    // Sign them in for the first time:
                    await _signInManager.SignInAsync(newUser, isPersistent: false);

                    // Add user to address, save address in the DB:
                    Address newAddress = new Address
                    {
                        AddressId = new Guid(),
                        StreetAddress = VM.StreetAddress == null ? "" : VM.StreetAddress,
                        Suburb = VM.Suburb == null ? "" : VM.Suburb,
                        State = VM.State == null ? "" : VM.State,
                        Postcode = VM.Postcode,
                        AddressType = VM.AddressType,
                        PreferredShippingAddress = true,
                        ApplicationUserId = userId
                    };

                    // Add address to the database:
                    _addressService.Create(newAddress);

                    return RedirectToAction("Index", "Home");
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
        public IActionResult ManageProfile()
        {
            if (User.IsInRole("Customer"))
            {
                string UserName = User.Identity.Name;

                ApplicationUser user = _userManager.Users.FirstOrDefault(x => x.UserName == UserName);
                IEnumerable<Address> userAddresses = _addressService.Query(x => x.ApplicationUserId == user.Id).OrderByDescending(y => y.PreferredShippingAddress).ThenBy(z => z.StreetAddress);

                AccountManageProfileViewModel VM = new AccountManageProfileViewModel()
                {
                    UserName = user.UserName,
                    GivenNames = user.GivenNames,
                    Surname = user.Surname,
                    Email = user.Email,
                    PhoneNo = user.PhoneNumber,
                    Addresses = userAddresses.ToList()
                };
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
        public async Task<IActionResult> ManageProfile(AccountManageProfileViewModel VM)
        {
            string UserName = User.Identity.Name;
            var user = _userManager.Users.FirstOrDefault(x => x.UserName == UserName);

            user.GivenNames = VM.GivenNames;
            user.Surname = VM.Surname;
            user.Email = VM.Email;
            user.PhoneNumber = VM.PhoneNo;

            IdentityResult Updateduser = await _userManager.UpdateAsync(user);

            if (Updateduser.Succeeded)
            {
                // Not sure if I'm duplicating code here...
                _userManager.UpdateAsync(user);
                return RedirectToAction("Index", "Home");
            }
            // To do: try catch block around return in case user cant be found or there's an error...
            return View(VM);
        }
    }
}