using ECinema.Components;
using System.Reflection;
using ECinema.Data;
using ECinema.Data.Repository;
using Microsoft.AspNetCore.Connections;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();




builder.Services.AddData(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();



app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

//Создание таблиц
//await DbInitializer.ExecuteTablesCreation(app.Services);
//Наполнение тестовыми данными
//await DbInitializer.SeedData(app.Services);




app.Run();

