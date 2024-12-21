using Hotlis.Application;
using Hotlis.BlazorUI.Components;
using Hotlis.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

    builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
    
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await app.InitializeDatabaseAsync();//Code to seed the database
app.Run();
