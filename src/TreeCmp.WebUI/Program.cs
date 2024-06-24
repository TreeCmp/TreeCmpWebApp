using Infrastructure;
using Infrastructure.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    //.AddCore()
    //.AddApplication()
    .AddInfrastructure(builder.Configuration);
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseRouting();
app.UseInfrastructure();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();

app.Run();
