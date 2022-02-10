using CourseWebApi.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWebApi.Utilitys
{
    public static class SSEHttpContextExtensions
    {
        public static async Task SSEInitAsync(this HttpContext context)
        {
            //context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.Response.Headers.Add("Cache-control","no-cache");
            context.Response.Headers.Add("Content-Type","text/event-stream");
            await context.Response.Body.FlushAsync();
        }

        public static async Task SSESendDataAsync(this HttpContext context,string data)
        {
            foreach (var eachData in data.Split('\n'))
            {
                await context.Response.WriteAsync("data: " + eachData + "\n");
            }

            await context.Response.WriteAsync("\n");
            await context.Response.Body.FlushAsync();
        }

        public static async Task SSESendEventAsync(this HttpContext context,SSEEvent sSEEvent)
        {
            if (!String.IsNullOrWhiteSpace(sSEEvent.Id))
            {
                await context.Response.WriteAsync("id: " + sSEEvent.Id + "\n");
            }

            if (sSEEvent.Retry is not null)
            {
                await context.Response.WriteAsync("retry: " + sSEEvent.Retry + "\n");
            }

            await context.Response.WriteAsync("event: " + sSEEvent.Name + "\n");

            var lines = sSEEvent.Data switch
            {
                null => new[] { String.Empty },
                string s => s.Split('\n').ToArray(),
                _ => new[] { JsonConvert.SerializeObject(sSEEvent.Data) }
            };

            foreach (var line in lines)
            {
                await context.Response.WriteAsync("data: " + line + "\n");
            }

            await context.Response.WriteAsync("\n");
            await context.Response.Body.FlushAsync();
        }
    }
}
