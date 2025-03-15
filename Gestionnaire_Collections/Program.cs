using Gestionnaire_Collections.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Gestionnaire_Collections.Models;
using System;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Identity.UI.Services;
using VotreProjet.Services;
using Microsoft.Extensions.Options;
using Gestionnaire_Collections.Services;

var builder = WebApplication.CreateBuilder(args);

// Ajouter les services de localisation
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Configuration de la culture
var supportedCultures = new[] { new CultureInfo("fr-FR") };

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("fr-FR");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

// Ajout des services Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;  
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// Ajout des services pour les mails et les rappels des notes
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddHostedService<ReminderService>();

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbContext>();

// Ajout du service pour les catégories de la barre de navigation
builder.Services.AddScoped<ICategoryService,CategoryService>();

// Add services to the container.
builder.Services.AddRazorPages();

//Ajout du Service SignalR
builder.Services.AddSignalR();

var app = builder.Build();

// Creation des roles en db
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    
    await CreateRolesAsync(roleManager);
}
async Task CreateRolesAsync(RoleManager<IdentityRole> roleManager)
{
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }
    if (!await roleManager.RoleExistsAsync("User"))
    {
        await roleManager.CreateAsync(new IdentityRole("User"));
    }
}

var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapControllers();
//Gestion erreurs 404 et rédirection)

//app.UseStatusCodePagesWithRedirects("/Pages/Index");

app.UseStatusCodePagesWithReExecute("/Pages/Error/{0}");

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseRequestLocalization();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
