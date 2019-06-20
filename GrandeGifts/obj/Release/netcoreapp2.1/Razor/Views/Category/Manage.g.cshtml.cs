#pragma checksum "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\Category\Manage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f3dd20496c2f5d6fee22d69c317cc2fcfcf7e72b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Category_Manage), @"mvc.1.0.view", @"/Views/Category/Manage.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Category/Manage.cshtml", typeof(AspNetCore.Views_Category_Manage))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f3dd20496c2f5d6fee22d69c317cc2fcfcf7e72b", @"/Views/Category/Manage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8ba8ef21c76af2c77c8887fdbf2a2da158a7942a", @"/Views/_ViewImports.cshtml")]
    public class Views_Category_Manage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CategoryManageViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(32, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\Category\Manage.cshtml"
  
    ViewBag.Title = "Manage Categories";

#line default
#line hidden
            BeginContext(83, 42, true);
            WriteLiteral("\r\n<div class=\"headingOneBanner\">\r\n    <h1>");
            EndContext();
            BeginContext(126, 13, false);
#line 8 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\Category\Manage.cshtml"
   Write(ViewBag.Title);

#line default
#line hidden
            EndContext();
            BeginContext(139, 106, true);
            WriteLiteral("</h1>\r\n</div>\r\n\r\n<div class=\"container\">\r\n    <h2>View, update and delete categories.</h2>\r\n    <hr />\r\n\r\n");
            EndContext();
#line 15 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\Category\Manage.cshtml"
     if (Model.Categories.Count() == 0)
    {

#line default
#line hidden
            BeginContext(293, 46, true);
            WriteLiteral("        <p>No categories exist yet :( Why not ");
            EndContext();
            BeginContext(340, 46, false);
#line 17 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\Category\Manage.cshtml"
                                         Write(Html.ActionLink("Add one?", "Add", "Category"));

#line default
#line hidden
            EndContext();
            BeginContext(386, 6, true);
            WriteLiteral("</p>\r\n");
            EndContext();
#line 18 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\Category\Manage.cshtml"
    }
    else
    {
        

#line default
#line hidden
#line 21 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\Category\Manage.cshtml"
         foreach (var a in Model.Categories)
        {
            if (a.InUse)
            {

#line default
#line hidden
            BeginContext(514, 137, true);
            WriteLiteral("                <div class=\"row rowItem InUse\">\r\n                    <div class=\"col-4 col-sm-8 float-left\">\r\n                        <p>");
            EndContext();
            BeginContext(652, 14, false);
#line 27 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\Category\Manage.cshtml"
                      Write(a.CategoryName);

#line default
#line hidden
            EndContext();
            BeginContext(666, 123, true);
            WriteLiteral("</p>\r\n                    </div>\r\n                    <div class=\"col-4 col-sm-2 float-right\">\r\n                        <p>");
            EndContext();
            BeginContext(790, 78, false);
#line 30 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\Category\Manage.cshtml"
                      Write(Html.ActionLink("Edit", "Edit", "Category", new { CategoryId = a.CategoryId }));

#line default
#line hidden
            EndContext();
            BeginContext(868, 123, true);
            WriteLiteral("</p>\r\n                    </div>\r\n                    <div class=\"col-4 col-sm-2 float-right\">\r\n                        <p>");
            EndContext();
            BeginContext(992, 82, false);
#line 33 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\Category\Manage.cshtml"
                      Write(Html.ActionLink("Delete", "Delete", "Category", new { CategoryId = a.CategoryId }));

#line default
#line hidden
            EndContext();
            BeginContext(1074, 58, true);
            WriteLiteral("</p>\r\n                    </div>\r\n                </div>\r\n");
            EndContext();
#line 36 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\Category\Manage.cshtml"
            }
        }

#line default
#line hidden
#line 39 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\Category\Manage.cshtml"
         if (@Model.NotInUse > 0)
        {


#line default
#line hidden
            BeginContext(1208, 33, true);
            WriteLiteral("            <h4>Not In Use</h4>\r\n");
            EndContext();
#line 44 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\Category\Manage.cshtml"
             foreach (var a in Model.Categories)
            {
                if (!a.InUse)
                {

#line default
#line hidden
            BeginContext(1358, 152, true);
            WriteLiteral("                    <div class=\"row rowItem NotInUse\">\r\n                        <div class=\"col-4 col-sm-8 float-left\">\r\n                            <p>");
            EndContext();
            BeginContext(1511, 14, false);
#line 50 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\Category\Manage.cshtml"
                          Write(a.CategoryName);

#line default
#line hidden
            EndContext();
            BeginContext(1525, 124, true);
            WriteLiteral("</p>\r\n                        </div>\r\n                        <div class=\"col float-right\">\r\n                            <p>");
            EndContext();
            BeginContext(1650, 91, false);
#line 53 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\Category\Manage.cshtml"
                          Write(Html.ActionLink("Re-activate", "Reactivate", "Category", new { CategoryId = a.CategoryId }));

#line default
#line hidden
            EndContext();
            BeginContext(1741, 66, true);
            WriteLiteral("</p>\r\n                        </div>\r\n                    </div>\r\n");
            EndContext();
#line 56 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\Category\Manage.cshtml"
                }
            }

#line default
#line hidden
#line 57 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\Category\Manage.cshtml"
             
        }

#line default
#line hidden
            BeginContext(1852, 52, true);
            WriteLiteral("        <hr />\r\n        <button class=\"form-button\">");
            EndContext();
            BeginContext(1905, 50, false);
#line 60 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\Category\Manage.cshtml"
                               Write(Html.ActionLink("Add Category", "Add", "Category"));

#line default
#line hidden
            EndContext();
            BeginContext(1955, 11, true);
            WriteLiteral("</button>\r\n");
            EndContext();
#line 61 "C:\Users\student\Desktop\GrandeGifts\GrandeGifts\Views\Category\Manage.cshtml"
    }

#line default
#line hidden
            BeginContext(1973, 6, true);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CategoryManageViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591