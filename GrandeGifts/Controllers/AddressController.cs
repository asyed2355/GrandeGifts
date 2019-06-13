using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
// Added namespaces:
using GrandeGifts.Models;
using GrandeGifts.Services;
using GrandeGifts.ViewModels.Address;
using Microsoft.AspNetCore.Identity;
using GrandeGifts.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace GrandeGifts.Controllers
{
    public class AddressController : Controller
    {
        private IDataService<Address> _addressService;
        private UserManager<ApplicationUser> _userManager;
        private TextFormatter _textFormatter;

        public AddressController(IDataService<Address> A, UserManager<ApplicationUser> U)
        {
            _addressService = A;
            _userManager = U;
            _textFormatter = new TextFormatter();
        }

        public IActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCustomer(AddressCustomerAddViewModel VM)
        {
            if (ModelState.IsValid)
            {
                string UserName = User.Identity.Name;
                ApplicationUser user = _userManager.Users.FirstOrDefault(x => x.UserName == UserName);

                // If this address has been selected as the preferred, set all others from this user to false.
                if (VM.PreferredShippingAddress)
                {
                    if(_addressService.Query(x => x.ApplicationUserId == user.Id) != null)
                    {
                        IEnumerable<Address> userAddresses = _addressService.Query(x => x.ApplicationUserId == user.Id).ToList();
                        foreach (var address in userAddresses)
                        {
                            address.PreferredShippingAddress = false;
                            _addressService.Update(address);
                        }
                    }
                }

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

                    AddressType = VM.AddressType,

                    PreferredShippingAddress = VM.PreferredShippingAddress,

                    ApplicationUserId = user.Id
                };

                // Add address to the database:
                _addressService.Create(newAddress);

                return RedirectToAction("AddressAddedSuccessfully", "Address");
            }
            return View(VM);
        }

        [HttpGet]
        public IActionResult AddressAddedSuccessfully()
        {
            return View();
        }

        [Route("edit-address/{AddressId}")]
        [HttpGet]
        public IActionResult CustomerEdit(string AddressId)
        {
            Address userAddress = _addressService.Query(x => x.AddressId.ToString() == AddressId).FirstOrDefault();

            AddressCustomerEditViewModel VM = new AddressCustomerEditViewModel
            {
                StreetAddress = userAddress.StreetAddress == null ? "" :
                    _textFormatter.RemoveDoubleSpaces
                    (_textFormatter.CapitaliseFirstLetters(userAddress.StreetAddress, false)),

                Suburb = userAddress.Suburb == null ? "" :
                    _textFormatter.RemoveDoubleSpaces
                    (_textFormatter.CapitaliseFirstLetters(userAddress.Suburb, false)),

                State = userAddress.State == null ? "" :
                    _textFormatter.RemoveDoubleSpaces
                    (_textFormatter.CapitaliseFirstLetters(userAddress.State, false)),

                Postcode = userAddress.Postcode,

                AddressType = userAddress.AddressType,

                PreferredShippingAddress = userAddress.PreferredShippingAddress
            };

            TempData["AddressId"] = AddressId.ToString();

            return View(VM);
        }

        [Route("edit-address/{AddressId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CustomerEdit(AddressCustomerEditViewModel VM)
        {
            if (ModelState.IsValid)
            {
                string AddressIdTemp;

                if (TempData["AddressId"] != null)
                {
                    AddressIdTemp = TempData["AddressId"].ToString();
                }
                else
                {
                    return RedirectToAction("ManageProfile", "Account");
                }

                // If this address has been selected as the preferred, set all others from this user to false.
                if (VM.PreferredShippingAddress)
                {
                    string UserName = User.Identity.Name;
                    ApplicationUser user = _userManager.Users.FirstOrDefault(x => x.UserName == UserName);

                    if (_addressService.Query(x => x.ApplicationUserId == user.Id) != null)
                    {
                        IEnumerable<Address> userAddresses = _addressService.Query(x => x.ApplicationUserId == user.Id).ToList();

                        foreach (var address in userAddresses)
                        {
                            address.PreferredShippingAddress = false;
                            _addressService.Update(address);
                        }
                    }
                }

                Address userAddress = _addressService.Query(x => x.AddressId.ToString() == AddressIdTemp).FirstOrDefault();

                // Converting VM to Model:
                userAddress.StreetAddress = _textFormatter.RemoveDoubleSpaces
                (_textFormatter.CapitaliseFirstLetters(VM.StreetAddress, false));

                userAddress.Suburb = _textFormatter.RemoveDoubleSpaces
                (_textFormatter.CapitaliseFirstLetters(VM.Suburb, false));

                userAddress.State = _textFormatter.RemoveDoubleSpaces
                (_textFormatter.CapitaliseFirstLetters(VM.State, false));

                userAddress.Postcode = VM.Postcode;

                userAddress.AddressType = VM.AddressType;

                userAddress.PreferredShippingAddress = VM.PreferredShippingAddress;

                // Updating address:
                _addressService.Update(userAddress);

                return RedirectToAction("ManageProfile", "Account");
            }
            return View(VM);
        }

        [HttpGet]
        public IActionResult ChangeShippingAddress()
        {
            string UserName = User.Identity.Name;
            ApplicationUser user = _userManager.Users.FirstOrDefault(x => x.UserName == UserName);
            IEnumerable<Address> userAddresses = _addressService.Query(x => x.ApplicationUserId == user.Id)
                                                 .OrderByDescending(y => y.PreferredShippingAddress)
                                                 .ThenBy(z => z.StreetAddress);

            List<AddressChangeShippingAddressViewModel> VM = new List<AddressChangeShippingAddressViewModel>();

            foreach (Address a in userAddresses)
            {
                AddressChangeShippingAddressViewModel viewItem = new AddressChangeShippingAddressViewModel
                {
                    AddressId = a.AddressId,
                    AddressType = a.AddressType,
                    ApplicationUserId = a.ApplicationUserId,
                    StreetAddress = a.StreetAddress,
                    Suburb = a.Suburb,
                    State = a.State,
                    Postcode = a.Postcode,
                    PreferredShippingAddress = a.PreferredShippingAddress
                };
                VM.Add(viewItem);
            }
            return View(VM);
        }

        [Route("update-delivery-address/{AddressId}")]
        [System.Web.Http.HttpPost]
        public IActionResult ChangeShippingAddressSuccessful(string AddressId)
        {
            Guid Id = Guid.Parse(AddressId);
            Address newPreferredAddress = _addressService.GetSingle(x => x.AddressId == Id);
            string UserName = User.Identity.Name;
            ApplicationUser user = _userManager.Users.FirstOrDefault(x => x.UserName == UserName);

            IEnumerable<Address> userAddresses = _addressService.Query(x => x.ApplicationUserId == user.Id);

            foreach (Address a in userAddresses)
            {
                if(a.AddressId == Id)
                {
                    a.PreferredShippingAddress = true;
                }
                else
                {
                    a.PreferredShippingAddress = false;
                }
                          
            }

            _addressService.UpdateMultiple(userAddresses);

            return RedirectToAction("Checkout", "ShoppingCart");
        }
    }
}