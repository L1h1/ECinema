using ECinema.Components;
using System.Reflection;
using ECinema.Data;
using ECinema.Data.Repository;
using Microsoft.AspNetCore.Connections;
using MudBlazor.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();




builder.Services.AddData(builder.Configuration);


var baseAddress = builder.Configuration.GetValue<string>("ApiSettings:BaseAddress");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(baseAddress)
});
builder.Services.AddMudServices();

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

//�������� ������
//await DbInitializer.ExecuteTablesCreation(app.Services);
//���������� ��������� �������
//await DbInitializer.SeedData(app.Services);

app.UseStaticFiles();


app.Run();

