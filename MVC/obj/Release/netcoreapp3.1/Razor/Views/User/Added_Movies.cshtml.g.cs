#pragma checksum "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\User\Added_Movies.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c36c31dadeea77acee1e5df725dea0f94e0b3e45"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Added_Movies), @"mvc.1.0.view", @"/Views/User/Added_Movies.cshtml")]
namespace AspNetCore
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
#line 1 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\_ViewImports.cshtml"
using MVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\_ViewImports.cshtml"
using MVC.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\User\Added_Movies.cshtml"
using System.Data;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c36c31dadeea77acee1e5df725dea0f94e0b3e45", @"/Views/User/Added_Movies.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7d7a8f56340c239c091cff637a00cc2fdf252300", @"/Views/_ViewImports.cshtml")]
    public class Views_User_Added_Movies : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IList<GetMoviesViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<div class=\"container\">\r\n    <div class=\"row\">\r\n        <div class=\"col-md-2 d-flex flex-column\">\r\n");
            WriteLiteral("\r\n        </div>\r\n        <div class=\"col-md-2\">\r\n\r\n        </div>\r\n        <div class=\"col-md-7 d-flex flex-column\">\r\n            <h5 style=\"margin-bottom: 75px;font-size: 20px;font-weight:400;line-height:1.2\">The Movies Added by You :</h5>\r\n\r\n");
#nullable restore
#line 18 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\User\Added_Movies.cshtml"
             foreach (var movie in @Model) 
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <a class=\"movies-container-link\"");
            BeginWriteAttribute("href", "  href=\"", 873, "\"", 899, 2);
            WriteAttributeValue("", 881, "/Movie/", 881, 7, true);
#nullable restore
#line 20 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\User\Added_Movies.cshtml"
WriteAttributeValue("", 888, movie.Name, 888, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                    <div class=\"row movies-container\">\r\n                        <div class=\"col-md-4\" style=\"padding: 0\">\r\n                            <img");
            BeginWriteAttribute("src", " src=\"", 1058, "\"", 1080, 1);
#nullable restore
#line 23 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\User\Added_Movies.cshtml"
WriteAttributeValue("", 1064, movie.ImagePath, 1064, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" alt=\"Movie Photo\" style=\"width: 200px;height: 152px;\">\r\n                        </div>\r\n                        <div class=\"col-md-8\">\r\n                            <p style=\"font-weight: 700\">");
#nullable restore
#line 26 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\User\Added_Movies.cshtml"
                                                   Write(movie.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                            <span class=\"fa fa-star checked\" style=\"color: orange\"></span>\r\n                            <span style=\"font-weight: 700;font-size: 14px;\">");
#nullable restore
#line 28 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\User\Added_Movies.cshtml"
                                                                       Write(movie.OverallRank);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                            <span style=\"font-weight: 400;font-size: 12px;\">/ ");
#nullable restore
#line 29 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\User\Added_Movies.cshtml"
                                                                         Write(movie.RankCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                            <p style=\"line-height: 16px; height: 103px;overflow: hidden;font-size: 14px;\">");
#nullable restore
#line 30 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\User\Added_Movies.cshtml"
                                                                                                     Write(movie.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        </div>\r\n                    </div>\r\n\r\n                </a>\r\n");
#nullable restore
#line 35 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\User\Added_Movies.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IList<GetMoviesViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
