using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using WebApi_LS1.Dtos;

namespace WebApi_LS1.Formatters
{
    public class TextCsvOutputFormatter : TextOutputFormatter
    {
        public TextCsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var sb = new StringBuilder();
            if (context.Object is IEnumerable<StudentDto> list)
            {
                foreach (var student in list)
                {
                    FormatCSV(sb, student);
                }
            }
            else if (context.Object is StudentDto item)
            {
                FormatCSV(sb, item);
            }
            await response.WriteAsync(sb.ToString());
        }

        private void FormatCSV(StringBuilder sb, StudentDto student)
        {
            sb.AppendLine("Id  -  Fullname  -  SeriaNo  -  Age  -  Score");
            sb.AppendLine($"{student.Id}  -  {student.Fullname}  -  {student.SeriaNo}  -  {student.Age}  -  {student.Score}");
        }
    }
}
