#pragma checksum "C:\Users\gkadi\source\repos\ASP.NetCoreIdentity\ASP.NetCoreIdentity\Views\Admin\Claims.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b253d3938247a88bf26031fa044c39b6cc7950ee"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(ASP.NetCoreIdentity.Pages.Admin.Views_Admin_Claims), @"mvc.1.0.view", @"/Views/Admin/Claims.cshtml")]
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
#line 5 "C:\Users\gkadi\source\repos\ASP.NetCoreIdentity\ASP.NetCoreIdentity\Views\_ViewImports.cshtml"
using ASP.NetCoreIdentity.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\gkadi\source\repos\ASP.NetCoreIdentity\ASP.NetCoreIdentity\Views\_ViewImports.cshtml"
using ASP.NetCoreIdentity.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b253d3938247a88bf26031fa044c39b6cc7950ee", @"/Views/Admin/Claims.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e4cd93f44bbe3a547a5a8a8422c5c03803e1c79b", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_Claims : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<System.Security.Claims.Claim>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\gkadi\source\repos\ASP.NetCoreIdentity\ASP.NetCoreIdentity\Views\Admin\Claims.cshtml"
  
    ViewData["Title"] = "Claims";
    Layout = "~/Views/Shared/_AdminLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h2>Claims</h2>\r\n<hr />\r\n\r\n<table class=\"table table-bordered table-striped\">\r\n    <tr>\r\n        <th>Kim</th>\r\n        <th>Dağıtıcı</th>\r\n        <th>Ad</th>\r\n        <th>Değer</th>\r\n    </tr>\r\n\r\n");
#nullable restore
#line 18 "C:\Users\gkadi\source\repos\ASP.NetCoreIdentity\ASP.NetCoreIdentity\Views\Admin\Claims.cshtml"
     foreach (System.Security.Claims.Claim item in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>");
#nullable restore
#line 21 "C:\Users\gkadi\source\repos\ASP.NetCoreIdentity\ASP.NetCoreIdentity\Views\Admin\Claims.cshtml"
           Write(item.Subject.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 22 "C:\Users\gkadi\source\repos\ASP.NetCoreIdentity\ASP.NetCoreIdentity\Views\Admin\Claims.cshtml"
           Write(item.Issuer);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 23 "C:\Users\gkadi\source\repos\ASP.NetCoreIdentity\ASP.NetCoreIdentity\Views\Admin\Claims.cshtml"
           Write(item.Type);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 24 "C:\Users\gkadi\source\repos\ASP.NetCoreIdentity\ASP.NetCoreIdentity\Views\Admin\Claims.cshtml"
           Write(item.Value);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n");
#nullable restore
#line 26 "C:\Users\gkadi\source\repos\ASP.NetCoreIdentity\ASP.NetCoreIdentity\Views\Admin\Claims.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<System.Security.Claims.Claim>> Html { get; private set; }
    }
}
#pragma warning restore 1591
