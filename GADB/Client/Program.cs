using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GADB.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // register the Telerik services
            builder.Services.AddTelerikBlazor();

            // Set JsonSerializationOption PropertyNameCaseInsensitive to true
            ((JsonSerializerOptions)typeof(JsonSerializerOptions)
                    .GetField("s_defaultOptions",
                        BindingFlags.Static |
                        BindingFlags.NonPublic).GetValue(null))
                .PropertyNameCaseInsensitive = true;

            await builder.Build().RunAsync();
        }
    }
}
