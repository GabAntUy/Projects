using ApplicationLogic.Interfaces;
using ApplicationLogic.UseCases.Amenazas;
using ApplicationLogic.UseCases.Ecosistemas;
using ApplicationLogic.UseCases.Especies;
using ApplicationLogic.UseCases.Paises;
using ApplicationLogic.UseCases.Usuarios;
using ApplicationLogic.UseCases.EstadosDeConservacion;
using BusinessLogic.Configuration;
using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces;
using DataAccessLogic.EF;
using Microsoft.EntityFrameworkCore;
using ApplicationLogic.UseCases.Logs;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession();

//Se agrega context de base de datos
builder.Services.AddDbContext<EcosistemasMarinosContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("EcosistemasMarinos"))
    );

//Inyección de repositorios
builder.Services
    .AddScoped<IRepositorioAmenaza, RepositorioAmenaza>()
    .AddScoped<IRepositorioEcosistema, RepositorioEcosistema>()
    .AddScoped<IRepositorioEspecie, RepositorioEspecie>()
    .AddScoped<IRepositorioUsuario, RepositorioUsuario>()
    .AddScoped<IRepositorioPais, RepositorioPais>()
    .AddScoped<IRepositorioLog, RepositorioLog>()
    .AddScoped<IRepositorioEstadoDeConservacion, RepositorioEstadoDeConservacion>();

//Inyección casos de uso Log
builder.Services
    .AddScoped<ICreate<Log>, AltaLog>();

//Inyección casos de uso Amenaza
builder.Services
    .AddScoped<IGetAll<Amenaza>, ListarAmenza>()
    .AddScoped<IGetSelected<Amenaza>, ListarAmenza>();
    //.AddScoped<IGetAllById<Amenaza>, ListarAmenza>();

//Inyección casos de uso Estado de Conservacion
builder.Services
    .AddScoped<IGetAll<EstadoDeConservacion>, ListarEstadoDeConservacion>()
    .AddScoped<IGet<EstadoDeConservacion>, ListarEstadoDeConservacion>();

//Inyección casos de uso Ecosistema
builder.Services
    .AddScoped<IGetAll<Ecosistema>, ListarEcosistema>()
    .AddScoped<ICreate<Ecosistema>, AltaEcosistema>()
    .AddScoped<IDelete<Ecosistema>, BorrarEcosistema>()
    .AddScoped<IUpdate<Ecosistema>, EditarEcosistema>()
    .AddScoped<IGetSelected<Ecosistema>, ListarEcosistema>()
    .AddScoped<IGet<Ecosistema>, ListarEcosistema>();

//Inyección casos de uso Especie
builder.Services
    .AddScoped<IGetAll<Especie>, ListarEspecie>()
    .AddScoped<IGet<Especie>, ListarEspecie>()
    .AddScoped<IUpdate<Especie>, EditarEspecie>()
    .AddScoped<IAddEcosistema<Especie>, EditarEspecie>()
    .AddScoped<ICreate<Especie>, AltaEspecie>()
    .AddScoped<IGetAllByString<Especie>, ListarEspecie>()
    .AddScoped<IGetEspeciesPeligro<Especie>, ListarEspecie>()
    .AddScoped<IGetEspeciePorPeso<Especie>, ListarEspecie>()
    .AddScoped<IGetAllByEcosistema<Especie>, ListarEspecie>()
    .AddScoped<IListarEcosistema<Ecosistema>, ListarEcosistema>();

//Inyección casos de uso Usuario
builder.Services
    .AddScoped<ICreate<Usuario>, AltaUsuario>()
    .AddScoped<ILogin<Usuario>, LoginUsuario>();

//Inyecciónn casos de uso Pais
builder.Services
    .AddScoped<IGetSelected<Pais>, ListarPais>()
    .AddScoped<IGetAll<Pais>, ListarPais>();


//Configuracion
var config = new ConfigurationBuilder()
    .AddJsonFile("parametros.json", optional: true, reloadOnChange: true)
    .Build();

Config.NombreMinimo = config.GetValue<int>("config:Nombre:Minimo");
Config.NombreMaximo= config.GetValue<int>("config:Nombre:Maximo");
Config.DescripcionMaximo = config.GetValue<int>("config:Descripcion:Maximo");
Config.DescripcionMinimo = config.GetValue<int>("config:Descripcion:Minimo");


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Login}/{id?}");

app.Run();
