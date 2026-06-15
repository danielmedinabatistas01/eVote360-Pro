var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
//Franklin
//repo
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEleccionRepository, EleccionRepository>();
builder.Services.AddScoped<IVotoRepository, VotoRepository>();
builder.Services.AddScoped<IEleccionPuestoElectivoRepository, EleccionPuestoElectivoRepository>();
builder.Services.AddScoped<IVotoDetalleRepository, VotoDetalleRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
//servicios
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IEleccionService, EleccionService>();
builder.Services.AddScoped<IVotoService, VotoService>();
builder.Services.AddScoped<IResultadoElectoralService, ResultadoElectoralService>();
builder.Services.AddScoped<IHomeAdministradorService, HomeAdministradorService>();




builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEleccionRepository, EleccionRepository>();
builder.Services.AddScoped<IEleccionPuestoElectivoRepository, EleccionPuestoElectivoRepository>();
builder.Services.AddScoped<IVotoRepository, VotoRepository>();
builder.Services.AddScoped<IVotoDetalleRepository, VotoDetalleRepository>();
//agregar despues de perla
//builder.Services.AddScoped<IPartidoPoliticoRepository, PartidoPoliticoRepository>();

// Services
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IEleccionService, EleccionService>();
builder.Services.AddScoped<IEleccionPuestoElectivoService, EleccionPuestoElectivoService>();
builder.Services.AddScoped<IVotoService, VotoService>();
builder.Services.AddScoped<IVotoDetalleService, VotoDetalleService>();
builder.Services.AddScoped<IResultadoElectoralService, ResultadoElectoralService>();
//Agregar despues de perla
builder.Services.AddScoped<IHomeAdministradorService, HomeAdministradorService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Login}/{id?}")
    .WithStaticAssets();

app.Run();