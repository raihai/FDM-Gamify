#pragma checksum "C:\Users\Admin\RiderProjects\fdm-gamify2\Pages\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c37814ace7ef1f4865344998c8dec5d54bc5ba92"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(fdm_gamify2.Pages.Pages_Index), @"mvc.1.0.razor-page", @"/Pages/Index.cshtml")]
namespace fdm_gamify2.Pages
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
#line 4 "C:\Users\Admin\RiderProjects\fdm-gamify2\Pages\_ViewImports.cshtml"
using fdm_gamify2;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Admin\RiderProjects\fdm-gamify2\Pages\_ViewImports.cshtml"
using fdm_gamify2.Pages.Shared;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c37814ace7ef1f4865344998c8dec5d54bc5ba92", @"/Pages/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dd93543ad35f7578eed05585515e1949ade5b555", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Index : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n\r\n");
#nullable restore
#line 6 "C:\Users\Admin\RiderProjects\fdm-gamify2\Pages\Index.cshtml"
  
    ViewData["Title"] = "Home page";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<section class=""fdm-header-banner reduced-height"" >
	<div class=""container h-100 "">
		<div class=""row align-items-center h-100"">
			<div  class = ""text-center"">
				<h1 itemprop=""name"" class=""banner-heading"">Play games to test your suitability with our <span class=""font-weight-bold"">Technical Graduate Programme</span></h1><br>
				<p> &nbsp;&nbsp; </p>
				<div class=""header-buttons center "">
						<a href=""./Branch-Selection"" class=""button mt-3 btn-grad"" >Play</a><br>
				</div>
			</div>
		</div>
	</div>
</section>

<hr class=""gradient"">

<section class=""secondSelection"" >  
  <div >
    <img src=""welcomePage.png"" width= ""100%"" alt=""gaming""> 
    
 </div>
</section>



<script type=""text/javascript"">
    document.getElementById(""FileReader"").onclick = function () {
        location.href = ""http://localhost:5000/Index"";
    };
</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IndexModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IndexModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IndexModel>)PageContext?.ViewData;
        public IndexModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
