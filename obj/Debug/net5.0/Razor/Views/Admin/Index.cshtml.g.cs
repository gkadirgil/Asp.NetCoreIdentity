#pragma checksum "C:\Users\gkadi\source\repos\ASP.NetCoreIdentity\ASP.NetCoreIdentity\Views\Admin\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ca890975f7800505a497af03d8417ca2201c5943"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(ASP.NetCoreIdentity.Pages.Admin.Views_Admin_Index), @"mvc.1.0.view", @"/Views/Admin/Index.cshtml")]
namespace ASP.NetCoreIdentity.Pages.Admin
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\gkadi\source\repos\ASP.NetCoreIdentity\ASP.NetCoreIdentity\Views\_ViewImports.cshtml"
using ASP.NetCoreIdentity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\gkadi\source\repos\ASP.NetCoreIdentity\ASP.NetCoreIdentity\Views\_ViewImports.cshtml"
using ASP.NetCoreIdentity.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ca890975f7800505a497af03d8417ca2201c5943", @"/Views/Admin/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"99bb4e27bb71bc6659058059703ff82d95087728", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<AppUser>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\gkadi\source\repos\ASP.NetCoreIdentity\ASP.NetCoreIdentity\Views\Admin\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_AdminLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<table class=\"table table-bordered table-striped\">\r\n    <tr>\r\n        <td>User Id</td>\r\n        <td>User Name</td>\r\n        <td>Email</td>\r\n    </tr>\r\n\r\n");
#nullable restore
#line 14 "C:\Users\gkadi\source\repos\ASP.NetCoreIdentity\ASP.NetCoreIdentity\Views\Admin\Index.cshtml"
     if (Model.Count == 0)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td colspan=\"3\">Hiç bir üye yoktur.</td>\r\n        </tr>\r\n");
#nullable restore
#line 19 "C:\Users\gkadi\source\repos\ASP.NetCoreIdentity\ASP.NetCoreIdentity\Views\Admin\Index.cshtml"
    }
    else
    {
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 22 "C:\Users\gkadi\source\repos\ASP.NetCoreIdentity\ASP.NetCoreIdentity\Views\Admin\Index.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>");
#nullable restore
#line 25 "C:\Users\gkadi\source\repos\ASP.NetCoreIdentity\ASP.NetCoreIdentity\Views\Admin\Index.cshtml"
               Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 26 "C:\Users\gkadi\source\repos\ASP.NetCoreIdentity\ASP.NetCoreIdentity\Views\Admin\Index.cshtml"
               Write(item.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 27 "C:\Users\gkadi\source\repos\ASP.NetCoreIdentity\ASP.NetCoreIdentity\Views\Admin\Index.cshtml"
               Write(item.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n");
#nullable restore
#line 29 "C:\Users\gkadi\source\repos\ASP.NetCoreIdentity\ASP.NetCoreIdentity\Views\Admin\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 29 "C:\Users\gkadi\source\repos\ASP.NetCoreIdentity\ASP.NetCoreIdentity\Views\Admin\Index.cshtml"
         
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</table>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<AppUser>> Html { get; private set; }
    }
}
#pragma warning restore 1591