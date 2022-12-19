using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MyFirstAspNetCoreApp.TagHelpers
{
    [HtmlTargetElement("h1")]
    [HtmlTargetElement("h2")]
    [HtmlTargetElement("h3")]
    [HtmlTargetElement("h4")]
    public class GreetingHeaderTagHelper:TagHelper
    {
        public string GreetingString { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("name", "Niki");
            output.Content.SetContent(GreetingString);
            output.PreContent.SetHtmlContent("<h1>hello</h1>");
            base.Process(context, output);
        }
    }
}
