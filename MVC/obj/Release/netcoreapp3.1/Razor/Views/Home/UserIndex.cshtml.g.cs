#pragma checksum "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Home\UserIndex.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b19492792e6b040ba0939276a34206fc56454e14"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_UserIndex), @"mvc.1.0.view", @"/Views/Home/UserIndex.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b19492792e6b040ba0939276a34206fc56454e14", @"/Views/Home/UserIndex.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7d7a8f56340c239c091cff637a00cc2fdf252300", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_UserIndex : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IList<GetMoviesViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("margin-bottom: 25px; margin-top: 86px"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("add-new-button"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Movie", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Add_New_Movie", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "User", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n");
            WriteLiteral("\r\n\r\n");
            WriteLiteral("<div class=\"container\">\r\n    <div class=\"row\">\r\n        <div class=\"col-md-4 d-flex flex-column align-items-baseline\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b19492792e6b040ba0939276a34206fc56454e145354", async() => {
                WriteLiteral("+ Add New Movie");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b19492792e6b040ba0939276a34206fc56454e146906", async() => {
                WriteLiteral("Your Account");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n   \r\n\r\n\r\n        </div>\r\n        <div class=\"col-md-8\">\r\n            <h4 class=\"text-center\" style=\"margin-bottom: 50px;font-size: 30px;font-weight:300;line-height:1.2\">List of all recent movie</h4>\r\n            \r\n");
#nullable restore
#line 25 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Home\UserIndex.cshtml"
                             foreach (GetMoviesViewModel movie in @Model)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <a class=\" movies-container-link\"");
            BeginWriteAttribute("href", " href=\"", 969, "\"", 994, 2);
            WriteAttributeValue("", 976, "/Movie/", 976, 7, true);
#nullable restore
#line 27 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Home\UserIndex.cshtml"
WriteAttributeValue("", 983, movie.Name, 983, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                    <div class=\"row movies-container\">\r\n                                        <div class=\"col-md-4\" style=\"padding: 0\">\r\n                                            <img");
            BeginWriteAttribute("src", " src=\"", 1201, "\"", 1223, 1);
#nullable restore
#line 30 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Home\UserIndex.cshtml"
WriteAttributeValue("", 1207, movie.ImagePath, 1207, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" alt=\"Movie Photo\" style=\"width: 200px;height: 152px;\">\r\n                                        </div>\r\n                                        <div class=\"col-md-8\">\r\n                                            <p style=\"font-weight: 700\">");
#nullable restore
#line 33 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Home\UserIndex.cshtml"
                                                                   Write(movie.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                            <span class=\"fa fa-star checked\" style=\"color: orange\"></span>\r\n                                            <span style=\"font-weight: 700;font-size: 14px;\">");
#nullable restore
#line 35 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Home\UserIndex.cshtml"
                                                                                       Write(movie.OverallRank);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                            <span style=\"font-weight: 400;font-size: 12px;\">/ ");
#nullable restore
#line 36 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Home\UserIndex.cshtml"
                                                                                         Write(movie.RankCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                            <p style=\"line-height: 16px; height: 103px;overflow: hidden;font-size: 14px;\">");
#nullable restore
#line 37 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Home\UserIndex.cshtml"
                                                                                                                     Write(movie.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                        </div>\r\n                                    </div>\r\n                            \r\n                                </a>\r\n");
#nullable restore
#line 42 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Home\UserIndex.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n        </div>\r\n    </div>\r\n</div>\r\n    \r\n\r\n");
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
