using Microsoft.AspNetCore.Razor.TagHelpers;

namespace testingGithub.CutomValidationInputs
{


    [HtmlTargetElement("big")]
    [HtmlTargetElement(Attributes = "big")]
    public class BigTagHtmlTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //Html Big Tag replace by h3 Tag in Index file
            output.TagName = "h3";
            output.Attributes.RemoveAll("big");
            output.Attributes.SetAttribute("class", "h3");

        }



    }
}
