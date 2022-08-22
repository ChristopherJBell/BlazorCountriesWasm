global using BlazorCountriesWasm.Client.Services.CountryService;
global using BlazorCountriesWasm.Shared;
using BlazorCountriesWasm.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<ICountryService, CountryService>();

builder.Services.AddSyncfusionBlazor();

var SyncfusionLicenceKey = builder.Configuration["SyncfusionLicenceKey"];
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(SyncfusionLicenceKey);

await builder.Build().RunAsync();
