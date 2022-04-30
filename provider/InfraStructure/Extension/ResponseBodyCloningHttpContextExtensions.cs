using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace provider.InfraStructure.Extension
{
    public static class ResponseBodyCloningHttpContextExtensions
    {
        public static async Task<Stream> CloneBodyAsync(this HttpContext context, Func<Task> writeBody)
        {
            var readableStream = new MemoryStream();
            var originalBody = context.Response.Body;
            context.Response.Body = readableStream;
            try
            {
                await writeBody();
                readableStream.Position = 0;
                await readableStream.CopyToAsync(originalBody);
                readableStream.Position = 0;
            }
            finally
            {
                context.Response.Body = originalBody;
            }
            return readableStream;
        }
    }
}
