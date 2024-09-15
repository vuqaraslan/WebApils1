using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi_LS1.Formatters
{
    public class AddVcardContentTypeOperationFilter : IOperationFilter
    {
        //public void Apply(OpenApiOperation operation, OperationFilterContext context)
        //{
        //    var textVcardMediaType = new OpenApiMediaType();
        //    textVcardMediaType.Schema = new OpenApiSchema
        //    {
        //        Type = "string"
        //    };

        //    // POST ve PUT istekleri için text/vcard formatını ekle
        //    if (operation.RequestBody != null)
        //    {
        //        operation.RequestBody.Content.Add("text/vcard", textVcardMediaType);
        //    }
        //}


        //public void Apply(OpenApiOperation operation, OperationFilterContext context)
        //{
        //    if (operation.RequestBody != null)
        //    {
        //        var content = operation.RequestBody.Content;
        //        if (content.ContainsKey("application/vnd.vcard"))
        //        {
        //            content.Remove("application/json");
        //            content.Add("application/vnd.vcard", new OpenApiMediaType
        //            {
        //                Schema = new OpenApiSchema
        //                {
        //                    Type = "string",
        //                    Format = "vcard"
        //                }
        //            });
        //        }
        //    }
        //}



        //public void Apply(OpenApiOperation operation, OperationFilterContext context)
        //{
        //    if (operation.RequestBody != null)
        //    {
        //        // `text/vcard` formatını ekleyin
        //        var content = operation.RequestBody.Content;
        //        if (!content.ContainsKey("text/vcard"))
        //        {
        //            content.Add("text/vcard", new OpenApiMediaType
        //            {
        //                Schema = new OpenApiSchema
        //                {
        //                    Type = "string",
        //                    Format = "vcard"
        //                }
        //            });
        //        }
        //    }
        //}

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.RequestBody != null)
            {
                var content = operation.RequestBody.Content;
                if (!content.ContainsKey("text/vcard"))
                {
                    content.Add("text/vcard", new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "string",
                            Format = "vcard"
                        }
                    });
                }
            }
        }








    }





}
