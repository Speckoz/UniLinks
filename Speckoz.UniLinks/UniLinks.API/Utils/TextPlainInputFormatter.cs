using Microsoft.AspNetCore.Mvc.Formatters;

using System.IO;
using System.Threading.Tasks;

namespace UniLinks.API.Utils
{
    public class TextPlainInputFormatter : InputFormatter
    {
        private const string ContentType = "text/plain";

        public TextPlainInputFormatter() => SupportedMediaTypes.Add(ContentType);

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            using (var reader = new StreamReader(context.HttpContext.Request.Body))
            {
                string content = await reader.ReadToEndAsync();
                return await InputFormatterResult.SuccessAsync(content);
            }
        }

        public override bool CanRead(InputFormatterContext context) =>
            context.HttpContext.Request.ContentType.StartsWith(ContentType);
    }
}