#pragma checksum "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\ShoppingCart\ViewCart.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1374e34413dad6cadb394b83b6797e2bd59b6287"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ShoppingCart_ViewCart), @"mvc.1.0.view", @"/Views/ShoppingCart/ViewCart.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/ShoppingCart/ViewCart.cshtml", typeof(AspNetCore.Views_ShoppingCart_ViewCart))]
namespace AspNetCore
{
    #line hidden
    using System;
#line 1 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\_ViewImports.cshtml"
using System.Collections.Generic;

#line default
#line hidden
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 2 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\_ViewImports.cshtml"
using GrandeGifts;

#line default
#line hidden
#line 3 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\_ViewImports.cshtml"
using GrandeGifts.ViewModels;

#line default
#line hidden
#line 4 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\_ViewImports.cshtml"
using GrandeGifts.ViewModels.Home;

#line default
#line hidden
#line 5 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\_ViewImports.cshtml"
using GrandeGifts.ViewModels.Account;

#line default
#line hidden
#line 6 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\_ViewImports.cshtml"
using GrandeGifts.ViewModels.Address;

#line default
#line hidden
#line 7 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\_ViewImports.cshtml"
using GrandeGifts.ViewModels.Admin;

#line default
#line hidden
#line 8 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\_ViewImports.cshtml"
using GrandeGifts.ViewModels.Category;

#line default
#line hidden
#line 9 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\_ViewImports.cshtml"
using GrandeGifts.ViewModels.Hamper;

#line default
#line hidden
#line 10 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\_ViewImports.cshtml"
using GrandeGifts.ViewModels.ShoppingCart;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1374e34413dad6cadb394b83b6797e2bd59b6287", @"/Views/ShoppingCart/ViewCart.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8ba8ef21c76af2c77c8887fdbf2a2da158a7942a", @"/Views/_ViewImports.cshtml")]
    public class Views_ShoppingCart_ViewCart : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<ShoppingCartViewViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(40, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\ShoppingCart\ViewCart.cshtml"
  
    ViewBag.Title = "View Shopping Cart";

#line default
#line hidden
            BeginContext(92, 42, true);
            WriteLiteral("\r\n<div class=\"headingOneBanner\">\r\n    <h1>");
            EndContext();
            BeginContext(135, 13, false);
#line 8 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\ShoppingCart\ViewCart.cshtml"
   Write(ViewBag.Title);

#line default
#line hidden
            EndContext();
            BeginContext(148, 42, true);
            WriteLiteral("</h1>\r\n</div>\r\n\r\n<div class=\"container\">\r\n");
            EndContext();
#line 12 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\ShoppingCart\ViewCart.cshtml"
     if (!ViewBag.CartHasItems)
    {

#line default
#line hidden
            BeginContext(230, 79, true);
            WriteLiteral("        <h5>Your shopping cart appears to be empty :(</h5>\r\n        <p>Why not ");
            EndContext();
            BeginContext(310, 77, false);
#line 15 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\ShoppingCart\ViewCart.cshtml"
              Write(Html.ActionLink("Browse through some of our hampers?", "ViewAll", "Category"));

#line default
#line hidden
            EndContext();
            BeginContext(387, 6, true);
            WriteLiteral("</p>\r\n");
            EndContext();
#line 16 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\ShoppingCart\ViewCart.cshtml"
    }
    else
    {

#line default
#line hidden
            BeginContext(417, 27, true);
            WriteLiteral("        <div class=\"row\">\r\n");
            EndContext();
#line 20 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\ShoppingCart\ViewCart.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
            BeginContext(501, 146, true);
            WriteLiteral("            <div class=\"col-lg-3 col-md-6 HamperPolaroid\">\r\n                <div class=\"container\" id=\"PolaroidHeading\">\r\n                    <h5>");
            EndContext();
            BeginContext(648, 92, false);
#line 24 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\ShoppingCart\ViewCart.cshtml"
                   Write(Html.ActionLink(item.HamperName, "ViewDetails", "Hamper", new { HamperId = @item.HamperId }));

#line default
#line hidden
            EndContext();
            BeginContext(740, 38, true);
            WriteLiteral("</h5>\r\n                    <p><i>Qty: ");
            EndContext();
            BeginContext(779, 13, false);
#line 25 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\ShoppingCart\ViewCart.cshtml"
                          Write(item.Quantity);

#line default
#line hidden
            EndContext();
            BeginContext(792, 41, true);
            WriteLiteral("</i></p>\r\n                    <p><strong>");
            EndContext();
            BeginContext(834, 10, false);
#line 26 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\ShoppingCart\ViewCart.cshtml"
                          Write(item.Price);

#line default
#line hidden
            EndContext();
            BeginContext(844, 59, true);
            WriteLiteral("</strong></p>\r\n                </div>\r\n                <img");
            EndContext();
            BeginWriteAttribute("src", " src=", 903, "", 922, 1);
#line 28 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\ShoppingCart\ViewCart.cshtml"
WriteAttributeValue("", 908, item.ImageUrl, 908, 14, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(922, 48, true);
            WriteLiteral(" id=\"shoppingCartImage\">\r\n                <p><i>");
            EndContext();
            BeginContext(971, 100, false);
#line 29 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\ShoppingCart\ViewCart.cshtml"
                 Write(Html.ActionLink("Remove From Cart", "DeleteItem", "ShoppingCart", new { HamperId = @item.HamperId }));

#line default
#line hidden
            EndContext();
            BeginContext(1071, 30, true);
            WriteLiteral("</i></p>\r\n            </div>\r\n");
            EndContext();
#line 31 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\ShoppingCart\ViewCart.cshtml"
            }

#line default
#line hidden
            BeginContext(1116, 300, true);
            WriteLiteral(@"        </div>
        <hr />
        <!--I'm using the address class here because it's a good match:-->
        <div class=""row"" id=""addressDisplay"">
            <div class=""col-lg-6 col-md-6 col-sm-12 col-xs-12 cartGrandTotalBlock"">
                <br />
                <h4><strong>Total: $");
            EndContext();
            BeginContext(1417, 13, false);
#line 38 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\ShoppingCart\ViewCart.cshtml"
                               Write(ViewBag.Total);

#line default
#line hidden
            EndContext();
            BeginContext(1430, 40, true);
            WriteLiteral("</strong></h4>\r\n                <p><i>($");
            EndContext();
            BeginContext(1471, 16, false);
#line 39 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\ShoppingCart\ViewCart.cshtml"
                   Write(ViewBag.Subtotal);

#line default
#line hidden
            EndContext();
            BeginContext(1487, 132, true);
            WriteLiteral(" + $7.50 postage and handling)</i></p>\r\n                <hr />\r\n                <button class=\"form-button\" id=\"shoppingCartButton\">");
            EndContext();
            BeginContext(1620, 66, false);
#line 41 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\ShoppingCart\ViewCart.cshtml"
                                                               Write(Html.ActionLink("Proceed To Checkout", "Checkout", "ShoppingCart"));

#line default
#line hidden
            EndContext();
            BeginContext(1686, 103, true);
            WriteLiteral("</button>\r\n                <br />\r\n                <button class=\"form-button\" id=\"shoppingCartButton\">");
            EndContext();
            BeginContext(1790, 59, false);
#line 43 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\ShoppingCart\ViewCart.cshtml"
                                                               Write(Html.ActionLink("Continue Shopping", "ViewAll", "Category"));

#line default
#line hidden
            EndContext();
            BeginContext(1849, 47, true);
            WriteLiteral("</button>\r\n            </div>\r\n        </div>\r\n");
            EndContext();
#line 46 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\ShoppingCart\ViewCart.cshtml"
    }

#line default
#line hidden
            BeginContext(1903, 6, true);
            WriteLiteral("</div>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<ShoppingCartViewViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591