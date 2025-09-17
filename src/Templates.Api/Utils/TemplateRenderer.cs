using Scriban;
using Scriban.Runtime;
using Scriban.Syntax;

namespace Templates.Api.Utils
{
    public static class TemplateRenderer
    {
        public static string RenderTemplate(string templateText, object data)
        {
            var template = Template.Parse(templateText);

            if (template.HasErrors)
                throw new InvalidOperationException(
                    $"Template syntax error: {template.Messages.FirstOrDefault()?.Message}"
                );

            var scriptObject = new ScriptObject();
            scriptObject.Import(data);

            var context = new TemplateContext(scriptObject)
            {
                StrictVariables = true
            };

            try
            {
                return template.Render(context);
            }
            catch (ScriptRuntimeException ex)
            {
                throw new InvalidOperationException("Template rendering error: " + ex.Message);
            }
        }
    }
}
