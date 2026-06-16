using eVote360_Pro.Middlewares;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.Mappings;
using eVote360Pro.Core.Application.Services;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Core.Domain.Settings;
using eVote360Pro.Infrastructure.Persistence.Contexts;
using eVote360Pro.Infrastructure.Persistence.Repositories;
using eVote360Pro.Infrastructure.Shared.Services;
using Microsoft.EntityFrameworkCore;
using eVote360_Pro.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<MailSettings>(
    builder.Configuration.GetSection("MailSettings"));

// Repositorios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEleccionRepository, EleccionRepository>();
builder.Services.AddScoped<IVotoRepository, VotoRepository>();
builder.Services.AddScoped<IEleccionPuestoElectivoRepository, EleccionPuestoElectivoRepository>();
builder.Services.AddScoped<IVotoDetalleRepository, VotoDetalleRepository>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserSession, UserSession>();

// Servicios
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IEleccionService, EleccionService>();
builder.Services.AddScoped<IVotoService, VotoService>();
builder.Services.AddScoped<IEleccionPuestoElectivoService, EleccionPuestoElectivoService>();
builder.Services.AddScoped<IVotoDetalleService, VotoDetalleService>();
builder.Services.AddScoped<IResultadoElectoralService, ResultadoElectoralService>();
builder.Services.AddScoped<IHomeAdministradorService, HomeAdministradorService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<GeneralProfile>();
});


builder.Services.AddApplicationLayerIoc();
builder.Services.AddPersistenceLayerIoc();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseSession();


app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();