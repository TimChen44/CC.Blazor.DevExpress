global using CC.Blazor.Dx;
global using Microsoft.AspNetCore.Components;
global using System.Text;
using BlazorDownloadFile;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDevExpressBlazor();
builder.Services.AddBlazorDownloadFile();

builder.Services.AddCCPBlazor();

builder.Services.Configure<DevExpress.Blazor.Configuration.GlobalOptions>(options =>
{
    options.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v5;
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
