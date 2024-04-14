using AspNetCore.Identity.MongoDbCore.Models;
using AspNetCore.Identity.MongoDbCore;

using AspNetCore.Identity.Mongo;
using AspNetCore.Identity.Mongo.Model;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);


// Dodaj MongoDB settings
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

// Rejestracja serwisu użytkownika
builder.Services.AddSingleton<UserService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Konfiguracja pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();
app.Run();
