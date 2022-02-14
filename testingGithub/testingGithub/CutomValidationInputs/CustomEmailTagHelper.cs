using Microsoft.AspNetCore.Razor.TagHelpers;

namespace testingGithub.CutomValidationInputs
{
    public class CustomEmailTagHelper : TagHelper
    {
        public string Email { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //Added in _HomeView layout file **********************************
            output.TagName = "a";
            output.Attributes.SetAttribute("href", $"mailto:{Email}");
            output.Attributes.Add("id", "my-email-id");
            output.Content.SetContent("contact");


        }


    }
}
