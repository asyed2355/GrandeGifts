#pragma checksum "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\Admin\LoggedInSuccessfully.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "38a01e94c1373a72f0d08f6a24c180a65af5be5a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_LoggedInSuccessfully), @"mvc.1.0.view", @"/Views/Admin/LoggedInSuccessfully.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Admin/LoggedInSuccessfully.cshtml", typeof(AspNetCore.Views_Admin_LoggedInSuccessfully))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"38a01e94c1373a72f0d08f6a24c180a65af5be5a", @"/Views/Admin/LoggedInSuccessfully.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8ba8ef21c76af2c77c8887fdbf2a2da158a7942a", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_LoggedInSuccessfully : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\Admin\LoggedInSuccessfully.cshtml"
  
    ViewBag.Title = "You've Successfully Logged In";

#line default
#line hidden
            BeginContext(61, 42, true);
            WriteLiteral("\r\n<div class=\"headingOneBanner\">\r\n    <h1>");
            EndContext();
            BeginContext(104, 13, false);
#line 6 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\Admin\LoggedInSuccessfully.cshtml"
   Write(ViewBag.Title);

#line default
#line hidden
            EndContext();
            BeginContext(117, 142, true);
            WriteLiteral("</h1>\r\n</div>\r\n\r\n<div class=\"container\">\r\n    <p>Now go and do some admin stuff ;)</p>\r\n    <hr />\r\n    <div class=\"LinkReturnHome\">\r\n        ");
            EndContext();
            BeginContext(260, 47, false);
#line 13 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\Admin\LoggedInSuccessfully.cshtml"
   Write(Html.ActionLink("Return Home", "Index", "Home"));

#line default
#line hidden
            EndContext();
            BeginContext(307, 20, true);
            WriteLiteral("\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591