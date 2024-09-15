using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using WebApi_LS1.Dtos;

namespace WebApi_LS1.Formatters
{
    public class VCardInputFormatter : TextInputFormatter
    {
        public VCardInputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        //public override IReadOnlyList<string>? GetSupportedContentTypes(string contentType, Type objectType)
        //{
        //    return base.GetSupportedContentTypes(contentType, objectType);
        //}


        public override async Task<InputFormatterResult> ReadRequestBodyAsync(
            InputFormatterContext context, Encoding effectiveEncoding)
        {
            var httpContext = context.HttpContext;

            var serviceProvider = httpContext.RequestServices;

            var logger = serviceProvider.GetRequiredService<ILogger<VCardInputFormatter>>();


            using var reader = new StreamReader(httpContext.Request.Body, effectiveEncoding);
            //string? nameLine = null;


            /*


                BEGIN:VCARD        BEGIN:VCARD
                VERSION:2.1        VERSION:2.1
                N:Davolio;Nancy    FN:Tom Thomas
                FN:Nancy Davolio   SNO:VV0101
                END:VCARD          AGE:26
                                   SCORE:100
                                   UID:1
                                   END:VCARD                                                             */
            string? line = null;
            try
            {
                line = await ReadLineAsync("BEGIN:VCARD", reader, context, logger);
                line = await ReadLineAsync("VERSION:2.1", reader, context, logger);

                var fullname = await ReadLineAsync("FN:", reader, context, logger);

                var seriaNo = await ReadLineAsync("SNO:", reader, context, logger);

                var ageLine = await ReadLineAsync("AGE:", reader, context,logger);
                var result_age = int.TryParse(ageLine.Substring(4), out var age);

                var scoreLine = await ReadLineAsync("SCORE:", reader, context, logger);
                var result_score = double.TryParse(scoreLine.Substring(6), out var score);

                var uid = await ReadLineAsync("UID:", reader, context, logger);

                line = await ReadLineAsync("END:VCARD", reader, context,logger);

                var studentAddDto = new StudentAddDto
                {
                    Fullname = fullname.Substring(3), 
                    SeriaNo = seriaNo.Substring(4),   
                    Age = age,
                    Score = score
                };


                logger.LogInformation("Line = {Line}", line);
                return await InputFormatterResult.SuccessAsync(studentAddDto);
            }
            catch
            {
                logger.LogError("Read failed: Line = {Line}", line);
                return await InputFormatterResult.FailureAsync();
            }
        }

        //private static async Task<string> ReadLineAsync(string expectedText, StreamReader reader, InputFormatterContext context)
        //{
        //    var line = await reader.ReadLineAsync();
        //    if (line is null || !line.StartsWith(expectedText))
        //    {
        //        var errorMessage = $"Looked for '{expectedText}' and got '{line}'";
        //        context.ModelState.TryAddModelError(context.ModelName, errorMessage);
        //        throw new Exception(errorMessage);
        //    }
        //    return line;
        //}
        private static async Task<string> ReadLineAsync(
     string expectedText, StreamReader reader, InputFormatterContext context,
     ILogger logger)
        {
            var line = await reader.ReadLineAsync();

            if (line is null || !line.StartsWith(expectedText))
            {
                var errorMessage = $"Looked for '{expectedText}' and got '{line}'";

                context.ModelState.TryAddModelError(context.ModelName, errorMessage);
                logger.LogError(errorMessage);

                throw new Exception(errorMessage);
            }

            return line;
        }





    }





    //public class VCardInputFormatter : TextInputFormatter
    //{
    //    public VCardInputFormatter()
    //    {
    //        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));

    //        SupportedEncodings.Add(Encoding.UTF8);
    //        SupportedEncodings.Add(Encoding.Unicode);
    //    }



    //    public override async Task<InputFormatterResult> ReadRequestBodyAsync(
    //        InputFormatterContext context, Encoding effectiveEncoding)
    //    {
    //        var httpContext = context.HttpContext;
    //        var serviceProvider = httpContext.RequestServices;

    //        var logger = serviceProvider.GetRequiredService<ILogger<VCardInputFormatter>>();

    //        using var reader = new StreamReader(httpContext.Request.Body, effectiveEncoding);
    //        string? nameLine = null;

    //        try
    //        {
    //            await ReadLineAsync("BEGIN:VCARD", reader, context, logger);
    //            await ReadLineAsync("VERSION:", reader, context, logger);

    //            nameLine = await ReadLineAsync("N:", reader, context, logger);

    //            var split = nameLine.Split(";".ToCharArray());
    //            //var contact = new Contact(FirstName: split[1], LastName: split[0].Substring(2));
    //            var contact = new StudentAddDto();
    //            contact.Fullname = split[0]+" - "+split[1];
    //            await ReadLineAsync("FN:", reader, context, logger);
    //            await ReadLineAsync("END:VCARD", reader, context, logger);

    //            logger.LogInformation("nameLine = {nameLine}", nameLine);

    //            return await InputFormatterResult.SuccessAsync(contact);
    //        }
    //        catch
    //        {
    //            logger.LogError("Read failed: nameLine = {nameLine}", nameLine);
    //            return await InputFormatterResult.FailureAsync();
    //        }
    //    }

    //    private static async Task<string> ReadLineAsync(
    //        string expectedText, StreamReader reader, InputFormatterContext context,
    //        ILogger logger)
    //    {
    //        var line = await reader.ReadLineAsync();

    //        if (line is null || !line.StartsWith(expectedText))
    //        {
    //            var errorMessage = $"Looked for '{expectedText}' and got '{line}'";

    //            context.ModelState.TryAddModelError(context.ModelName, errorMessage);
    //            logger.LogError(errorMessage);

    //            throw new Exception(errorMessage);
    //        }

    //        return line;
    //    }
    //}


}
