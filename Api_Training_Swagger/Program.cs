using Api_Training_Swagger;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DetailsDb>(options => options.UseInMemoryDatabase("InMemoryDb"));
builder.Services.AddControllers();
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
    config.ApiVersionReader = new HeaderApiVersionReader("api-version");
});


//builder.Services.AddApiVersioning(options => {options.ReportApiVersions = true;});
//builder.Services.AddApiVersioning(options => { options.IApiVersionDescriptionProvider = true; });

var app = builder.Build();
/*var versionSet = app.NewApiVersionSet()
                    .HasApiVersion(1)
                    .HasApiVersion(2)
                    .Build();
*/

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
