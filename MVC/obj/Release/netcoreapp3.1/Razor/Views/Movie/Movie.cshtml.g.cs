#pragma checksum "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Movie\Movie.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8534660624d3148b413734178583f64d13b1ba68"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Movie_Movie), @"mvc.1.0.view", @"/Views/Movie/Movie.cshtml")]
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
#line 1 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Movie\Movie.cshtml"
using System.IdentityModel.Tokens.Jwt;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Movie\Movie.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8534660624d3148b413734178583f64d13b1ba68", @"/Views/Movie/Movie.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7d7a8f56340c239c091cff637a00cc2fdf252300", @"/Views/_ViewImports.cshtml")]
    public class Views_Movie_Movie : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<RetrieveMovieViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Movie", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Rank_Movie", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("display: inline-block ; margin-left: 25px"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Who_Added_It", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("add-new-button"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Who_Ranked_It", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete_Movie", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 6 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Movie\Movie.cshtml"
  
    var access_token = ViewData["access_token"].ToString();
    // ------- Extract userId (sub) from access token
    string accessTokenString = new JwtSecurityTokenHandler().ReadJwtToken(access_token).ToString();
    string toBeSearched = "\"sub\":\"";
    var userId = accessTokenString.Substring(accessTokenString.IndexOf(toBeSearched) + toBeSearched.Length);
    userId = userId.Substring(0, userId.IndexOf("\""));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container\">\r\n    <div class=\"row movie-container d-flex justify-content-start \" style=\"margin-bottom: 85px;background-color: #333;color: #fafafa\">\r\n        <div class=\"col-md-3\" style=\"padding: 0;margin-right: 40px\">\r\n            <img");
            BeginWriteAttribute("src", " src=\"", 789, "\"", 811, 1);
#nullable restore
#line 18 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Movie\Movie.cshtml"
WriteAttributeValue("", 795, Model.ImagePath, 795, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" alt=\"Movie Photo\" style=\"height: 345px;width: 280px\">\r\n        </div>\r\n        <div class=\"col-md-8\" style=\"padding: 0;margin: 20px 5px;\">\r\n            <p style=\"font-weight: 700;font-size: 22px\">\r\n                ");
#nullable restore
#line 22 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Movie\Movie.cshtml"
           Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("<span style=\"margin-left: 15px;font-weight: 400;font-size: 18px;\">(");
#nullable restore
#line 22 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Movie\Movie.cshtml"
                                                                                         Write(Model.ReleaseDate);

#line default
#line hidden
#nullable disable
            WriteLiteral(")</span>\r\n            </p>\r\n            <span class=\"fa fa-star checked\" style=\"color: orange\"></span>\r\n            <span style=\"font-weight: 700;font-size: 18px\">");
#nullable restore
#line 25 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Movie\Movie.cshtml"
                                                      Write(Model.OverallRank);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n            <span style=\"font-weight: 400\r\n            ;font-size: 12px;\">/ ");
#nullable restore
#line 27 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Movie\Movie.cshtml"
                            Write(Model.RankCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n\r\n\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8534660624d3148b413734178583f64d13b1ba689285", async() => {
                WriteLiteral("\r\n                <div class=\"form-group\" style=\"display: inline-block\">\r\n");
                WriteLiteral("                    <input type=\"hidden\" name=\"movieName\"");
                BeginWriteAttribute("value", " value=\"", 1747, "\"", 1766, 1);
#nullable restore
#line 33 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Movie\Movie.cshtml"
WriteAttributeValue("", 1755, Model.Name, 1755, 11, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral("/>\r\n                    <input type=\"range\" name=\"RankInputId\" id=\"RankInputId\"");
                BeginWriteAttribute("value", " value=\"", 1846, "\"", 1869, 1);
#nullable restore
#line 34 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Movie\Movie.cshtml"
WriteAttributeValue("", 1854, Model.UserRank, 1854, 15, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" min=\"0\" max=\"10\" step=\"1\" oninput=\"RankOutputId.value = RankInputId.value\" class=\"form-control-range\" style=\"color: #fafafa;width: 150px;display: inline-block\">\r\n");
                WriteLiteral("                    <output name=\"RankOutputId\" id=\"RankOutputId\" style=\"color: #fafafa; display: inline-block;color: #1b6ec2; margin-left: 2rem\">");
#nullable restore
#line 36 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Movie\Movie.cshtml"
                                                                                                                                             Write(Model.UserRank);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</output>
                </div>
                <div class=""form-group"" style=""text-align: center;display: inline-block; margin-left: 2rem;"">
                    <input type=""submit"" value=""Rank"" class=""add-new-button"" style=""border: none;padding: 5px 10px;border-radius: 5px; background-color: orange"">
                </div>
            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n            <p style=\"font-weight: 600;font-size: 18px\">");
#nullable restore
#line 43 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Movie\Movie.cshtml"
                                                   Write(Model.Director);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n            <p style=\"height: 195px;overflow: hidden;font-size: 16px;padding-top: 5px\">");
#nullable restore
#line 44 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Movie\Movie.cshtml"
                                                                                  Write(Model.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row d-flex justify-content-center\" style=\"margin-bottom: 40px\">\r\n        <div class=\"col-md-4 d-flex justify-content-center\">\r\n");
#nullable restore
#line 50 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Movie\Movie.cshtml"
             if (new Guid(userId) == Model.UserId)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <a href=\"?\" class=\"add-new-button\">you added this movie </a>\r\n");
#nullable restore
#line 53 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Movie\Movie.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8534660624d3148b413734178583f64d13b1ba6814864", async() => {
                WriteLiteral("who added it ? &#8594;");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-movieName", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 56 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Movie\Movie.cshtml"
                                                                             WriteLiteral(Model.Name);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["movieName"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-movieName", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["movieName"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 57 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Movie\Movie.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n        <div class=\"col-md-4 d-flex justify-content-center\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8534660624d3148b413734178583f64d13b1ba6817759", async() => {
                WriteLiteral("who has been ranked? &#8594;");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-movieName", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 60 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Movie\Movie.cshtml"
                                                                          WriteLiteral(Model.Name);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["movieName"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-movieName", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["movieName"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n        </div>\r\n    </div>\r\n\r\n\r\n");
#nullable restore
#line 66 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Movie\Movie.cshtml"
     if (new Guid(userId) == Model.UserId)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"row d-flex justify-content-center\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8534660624d3148b413734178583f64d13b1ba6820714", async() => {
                WriteLiteral("\r\n                <div class=\"form-group\" style=\"text-align: center;display: inline-block; margin-left: 2rem;\">\r\n                    <input type=\"hidden\" name=\"movieName\"");
                BeginWriteAttribute("value", " value=\"", 4050, "\"", 4069, 1);
#nullable restore
#line 71 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Movie\Movie.cshtml"
WriteAttributeValue("", 4058, Model.Name, 4058, 11, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@">
                </div>
                <div class=""form-group"" style=""text-align: center;display: inline-block; margin-left: 2rem;"">
                    <input type=""submit"" value=""Delete!"" class=""add-new-button"" style=""border: none;padding: 5px 10px;border-radius: 5px; background-color: red; font-weight: 500;font-family: monospace;"">
                </div>
            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n");
#nullable restore
#line 78 "C:\Users\Alekhandr0\Desktop\dot-net-core\PracticalApps\MovieRanker_v1\MovieRanker_v1\MVC\Views\Movie\Movie.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<RetrieveMovieViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
