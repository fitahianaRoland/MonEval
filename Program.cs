using dotnetEval;
using dotnetEval.Data;
using dotnetEval.service;
using dotnetEval.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Ajoutez le middleware d'authentification
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        // Configurez les options du cookie d'authentification
        options.LoginPath = "/Login/LoginPage"; // Spécifiez le chemin de connexion
        options.AccessDeniedPath = "/Import"; // Spécifiez le chemin pour l'accès refusé
        options.AccessDeniedPath = "/Annonce"; // Spécifiez le chemin pour l'accès refusé
        options.AccessDeniedPath = "/Champs"; // Spécifiez le chemin pour l'accès refusé
    });

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddNewtonsoftJson();
//register
builder.Services.AddDbContext<AppDbContext>(options =>{
    options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase"));
});

//services et repository:
builder.Services.AddScoped<ExeptionMessage>();
// builder.Services.AddScoped<ExcelExporterMembres>();
builder.Services.AddScoped<TraitementImage>();
builder.Services.AddScoped<PdfService>();
// builder.Services.AddScoped<ImportService>();
builder.Services.AddScoped<CustomAuthorizationFilter>();
builder.Services.AddScoped<GetPaginationModelService>();

// eval add scoop 
builder.Services.AddScoped<VEtapeCoureurEquipeRepository>();
builder.Services.AddScoped<AdministrateurRepository>();
builder.Services.AddScoped<ClassementGeneralViewRepository>();
builder.Services.AddScoped<GenreRepository>();
builder.Services.AddScoped<EquipeRepository>();
builder.Services.AddScoped<CategorieRepository>();
builder.Services.AddScoped<CategorieCoureurRepository>();
builder.Services.AddScoped<EtapeRepository>();
builder.Services.AddScoped<EtapeCoureurRepository>();
builder.Services.AddScoped<TempsCoureurRepository>();
builder.Services.AddScoped<CoureurRepository>();
builder.Services.AddScoped<PointsRepository>();
builder.Services.AddScoped<PointsCoureurRepository>();
builder.Services.AddScoped<Import>();
builder.Services.AddScoped<InsertionTableRationnel>();
builder.Services.AddScoped<Helper>();
builder.Services.AddScoped<ClassementGeneralCategorieViewRepository>();
builder.Services.AddScoped<EtapeEquipePenaliteRepository>();
builder.Services.AddScoped<VEtapeCoureurEquipeDureeRepository>();
builder.Services.AddScoped<VEtapeCoureurEquipeRepository>();
builder.Services.AddScoped<CoefficientEtapeRepository>();
builder.Services.AddScoped<ClassementGeneralCoefficientEtapeRepository>();
builder.Services.AddScoped<ClassementGeneralCoefficientEtapeRangRepository>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=LoginPageEquipe}/{id?}");
app.UseSession();
app.Run();
