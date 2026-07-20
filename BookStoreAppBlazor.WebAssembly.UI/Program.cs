using Blazored.LocalStorage;
using BookStoreAppBlazor.WebAssembly.UI;
using BookStoreAppBlazor.WebAssembly.UI.Configration;
using BookStoreAppBlazor.WebAssembly.UI.Providers;
using BookStoreAppBlazor.WebAssembly.UI.Services;
using BookStoreAppBlazor.WebAssembly.UI.Services.Authentication;
using BookStoreAppBlazor.WebAssembly.UI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7207") });

//builder.Services.AddRazorComponents()
//    .AddInteractiveServerComponents();

builder.Services.AddBlazoredLocalStorage();

//builder.Services.AddHttpClient<Client>(cl =>
//    cl.BaseAddress = new Uri("https://localhost:7207"));

builder.Services.AddAuthorizationCore();

builder.Services.AddCascadingAuthenticationState();


// Authentication
builder.Services.AddScoped<Client, Client>();
builder.Services.AddScoped<ApiAuthenticationStateProvider>();

builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
    provider.GetRequiredService<ApiAuthenticationStateProvider>());


// Services
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();


builder.Services.AddAutoMapper(typeof(MapperConfig));

await builder.Build().RunAsync();
