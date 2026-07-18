using Blazored.LocalStorage;
using BookStoreAppBlazor.Server.UI.Components;
using BookStoreAppBlazor.Server.UI.Services.Base;
using BookStoreAppBlazor.Server.UI.Services.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using BookStoreAppBlazor.Server.UI.Providers;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddHttpClient<Client>(cl =>
    cl.BaseAddress = new Uri("https://localhost:7207"));

builder.Services.AddAuthorizationCore();

builder.Services.AddCascadingAuthenticationState();


// Authentication
builder.Services.AddScoped<ApiAuthenticationStateProvider>();

builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
    provider.GetRequiredService<ApiAuthenticationStateProvider>());


// Services
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAntiforgery();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.Run();