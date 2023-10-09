using BRL.Infrastructure.Data.Services;
using BRL.Infrastructure.Models.Base.Settings;
using BRP.Domain.Application.Implementation.Interfaces;
using BRP.Domain.Application.Implementation.Services;
using BRP.Services.API.Person.Extensions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

// load and check configs
var connectionString = builder.Configuration["CONNECTION_STRING"];
if (string.IsNullOrEmpty(connectionString))
    throw new ArgumentException($"Falha ao iniciar {assemblyName}. Parâmetro connection_string não encontrado.");

var seqUri = builder.Configuration["SEQ_URI"];
if (string.IsNullOrEmpty(seqUri))
    throw new ArgumentException($"Falha ao iniciar {assemblyName}. Parâmetro seq_uri não encontrado.");

var rabbitMqUri = builder.Configuration["RABBITMQ_URI"];
if (string.IsNullOrEmpty(rabbitMqUri))
    throw new ArgumentException($"Falha ao iniciar {assemblyName}. Parâmetro rabbitmq_uri não encontrado.");

var corsOrigins = builder.Configuration["CORS"];
if (string.IsNullOrEmpty(corsOrigins))
    throw new ArgumentException($"Falha ao iniciar {assemblyName}. Parâmetro cors não encontrado.");

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(corsOrigins!.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});

// Database
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("BRPDatabase"));
builder.Services.AddSingleton<IDomainPersonDeleteService, DomainPersonDeleteService>();
builder.Services.AddSingleton<IDomainPersonGetService, DomainPersonGetService>();
builder.Services.AddSingleton<IDomainPersonPostService, DomainPersonPostService>();
builder.Services.AddSingleton<IDomainPersonPutService, DomainPersonPutService>();
builder.Services.AddSingleton<PersonService>();

// Add Log SEQ
builder.ConfigureLogs(seqUri);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddResponseCaching();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();

    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API - Person", Version = "v1" });

    c.TagActionsBy(api =>
    {
        if (api.GroupName != null)
        {
            return new[] { api.GroupName };
        }

        if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
        {
            return new[] { controllerActionDescriptor.ControllerName };
        }

        throw new InvalidOperationException("Unable to determine tag for endpoint.");
    });
    c.DocInclusionPredicate((name, api) => true);
});

var app = builder.Build();

app.UseExceptionHandler("/error");

app.UseSwagger();

app.UseSwaggerUI();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

Log.Information("Iniciando serviço {AssemblyName}", assemblyName);

app.Run();

Log.Information("Finalizando serviço {AssemblyName}", assemblyName);