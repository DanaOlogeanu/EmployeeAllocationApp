using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWasm;
using BlazorWasm.Auth;
using Domain.Auth;
using HttpClients.ClientInterfaces;
using HttpClients.Implementations;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


 builder.Services.AddScoped( sp => new HttpClient {  BaseAddress = new Uri("http://localhost:5127") });  
//create user - register as service
builder.Services.AddScoped<IUserService, UserHttpClient>();
builder.Services.AddScoped<IUserSkillService, UserSkillHttpClient>();
builder.Services.AddScoped<ISkillService, SkillHttpClient>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
AuthorizationPolicies.AddPolicies(builder.Services);  //same policies as in WebApi
await builder.Build().RunAsync();