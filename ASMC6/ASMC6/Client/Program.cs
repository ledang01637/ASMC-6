using ASMC6.Client.Session;
using ASMC6.Server.Service;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ASMC6.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<CartService>();
            builder.Services.AddAuthorizationCore(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("1"));
            });
            await builder.Build().RunAsync();
        }
    }
}
