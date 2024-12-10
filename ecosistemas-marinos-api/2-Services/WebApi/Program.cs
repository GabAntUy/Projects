using ApplicationLogic.Interfaces;
using ApplicationLogic.UseCases.Amenazas;
using ApplicationLogic.UseCases.Ecosistemas;
using ApplicationLogic.UseCases.Especies;
using ApplicationLogic.UseCases.EstadosDeConservacion;
using ApplicationLogic.UseCases.Logs;
using ApplicationLogic.UseCases.Paises;
using ApplicationLogic.UseCases.Usuarios;
using BusinessLogic.Configuration;
using BusinessLogic.Entities;
using BusinessLogic.RepoInterfaces;
using DataAccessLogic.EF;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using WebApi.Mapper;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Utils.Interfaces;
using WebApi.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc(
        "v1", new OpenApiInfo 
        { 
            Title = "Ecosistemas Marinos", 
            Version = "v1",
            Description = "Sistema para gestionar Ecosistemas Marinos",
        });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opt.IncludeXmlComments(xmlPath);

    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddControllers().AddJsonOptions(
    option =>
    option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
    );

var claveSecreta = "ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE=";
builder.Services.AddAuthentication(aut =>
{
    aut.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    aut.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(aut =>
{
    aut.RequireHttpsMetadata = false;
    aut.SaveToken = true;
    aut.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(claveSecreta)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddHttpContextAccessor();
//permite obtener usuario que hizo request para guardar ene el log
builder.Services.AddScoped<IUserNameService,UserNameService>();
builder.Services.AddScoped<ILogService,LogService>();

//configuracion AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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


///////////////////////////////////////////////////////////////////////////

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

////////////////////////////////////////////////////////////////////////////



//Configuracion
var config = new ConfigurationBuilder()
    .AddJsonFile("parametros.json", optional: true, reloadOnChange: true)
    .Build();

Config.NombreMinimo = config.GetValue<int>("config:Nombre:Minimo");
Config.NombreMaximo = config.GetValue<int>("config:Nombre:Maximo");
Config.DescripcionMaximo = config.GetValue<int>("config:Descripcion:Maximo");
Config.DescripcionMinimo = config.GetValue<int>("config:Descripcion:Minimo");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

///////////////////////////////////////////////////////////////

app.UseCors("AllowAnyOrigin");


app.Run();

