using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Yacaba.Shared.Services;
using Yacaba.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the Yacaba.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

await builder.Build().RunAsync();
