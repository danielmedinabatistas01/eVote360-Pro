using eVote360_Pro.Middlewares;
using eVote360Pro.Core.Application;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.Mappings;
using eVote360Pro.Core.Application.Services;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Core.Domain.Settings;
using eVote360Pro.Infrastructure.Persistence;
using eVote360Pro.Infrastructure.Persistence.Contexts;
using eVote360Pro.Infrastructure.Persistence.Repositories;
using eVote360Pro.Infrastructure.Shared.Services;
using InvestmentApp.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using eVote360Pro.Core.Application.Mappings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUserSession, UserSession>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<MailSettings>(
    builder.Configuration.GetSection("MailSettings"));

builder.Services.AddPersistenceLayerIoc();
builder.Services.AddApplicationLayerIoc();
builder.Services.AddSharedLayerIoc(builder.Configuration);


/* Repositorios
//builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
//builder.Services.AddScoped<IEleccionRepository, EleccionRepository>();
builder.Services.AddScoped<IEleccionPuestoElectivoRepository, EleccionPuestoElectivoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEleccionRepository, EleccionRepository>();
builder.Services.AddScoped<IVotoRepository, VotoRepository>();
builder.Services.AddScoped<IEleccionPuestoElectivoRepository, EleccionPuestoElectivoRepository>();
builder.Services.AddScoped<IVotoDetalleRepository, VotoDetalleRepository>();
builder.Services.AddScoped<IAsignacionCandidatoRepository, AsignacionCandidatoRepository>();
builder.Services.AddScoped<ICandidatoRepository,CandidatoRepository>();
builder.Services.AddScoped<ICodigoVerificacionRepository,CodigoVerificacionRepository>();
builder.Services.AddScoped<IPuestoElectivoRepository,PuestoElectivoRepository>();
builder.Services.AddScoped<IAlianzaPoliticaRepository,AlianzaPoliticaRepository>();
builder.Services.AddScoped<ICiudadanoRepository, CiudadanoRepository>();
builder.Services.AddScoped<IPartidoPoliticoRepository, PartidoPoliticoRepository>();
builder.Services.AddScoped<ICodigoVerificacionRepository, CodigoVerificacionRepository>();
builder.Services.AddScoped<IAsignacionDirigenteRepository, AsignacionDirigenteRepository>();*/

/* Servicios
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IEleccionService, EleccionService>();
builder.Services.AddScoped<IVotoService, VotoService>();
builder.Services.AddScoped<IEleccionPuestoElectivoService, EleccionPuestoElectivoService>();
builder.Services.AddScoped<IVotoDetalleService, VotoDetalleService>();
builder.Services.AddScoped<IResultadoElectoralService, ResultadoElectoralService>();
builder.Services.AddScoped<IHomeAdministradorService, HomeAdministradorService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ICodigoVerificacionService,CodigoVerificacionService>();
builder.Services.AddScoped<IProcesoVotacionService,ProcesoVotacionService>();
builder.Services.AddScoped<ICandidatoService,CandidatoService>();
builder.Services.AddScoped<IAsignacionCandidatoService,AsignacionCandidatoService>();
builder.Services.AddScoped<IPuestoElectivoService,PuestoElectivoService>();
builder.Services.AddScoped<IAlianzaPoliticaService,AlianzaPoliticaService>();
builder.Services.AddScoped<ICiudadanoService, CiudadanoService>();
builder.Services.AddScoped<IPartidoPoliticoService, PartidoPoliticoService>();
builder.Services.AddScoped<IPuestoElectivoService, PuestoElectivoService>();
builder.Services.AddScoped<ICodigoVerificacionService, CodigoVerificacionService>();
builder.Services.AddScoped<IAlianzaPoliticaService, AlianzaPoliticaService>();
builder.Services.AddScoped<IAsignacionDirigenteService, AsignacionDirigenteService>();
builder.Services.AddScoped<IProcesoVotacionService, ProcesoVotacionService>();
builder.Services.AddScoped<ICandidatoService, CandidatoService>();
builder.Services.AddScoped<IAsignacionCandidatoService, AsignacionCandidatoService>();
builder.Services.AddScoped<IOcrService, OcrService>();*/

var loggerFactory = LoggerFactory.Create(builder => { });

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddMaps(typeof(GeneralProfile).Assembly);
}, loggerFactory);

builder.Services.AddSingleton<IMapper>(
    mapperConfig.CreateMapper());

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
   pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

await app.RunAsync();