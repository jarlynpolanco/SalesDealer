using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SalesDealer.UI.Contracts;
using SalesDealer.UI.Services;

namespace SalesDealer.UI
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
            builder.Services.AddTransient<ISalesRepository, SalesRepository>();
            builder.Services.AddTransient<IFileRepository, FileRepository>();

            await builder.Build().RunAsync();
        }
    }
}
