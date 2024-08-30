using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WSInformatica.Models;
using WSInformatica.Models.Common;
using WSInformatica.Services;

var MyAllowSpecificOrigins = "__myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration
 .SetBasePath(Directory.GetCurrentDirectory())
 .AddJsonFile($"appsettings.json", optional: false)
 .AddJsonFile($"appsettings.{builder}.json", optional: true)
 .AddEnvironmentVariables()
 .Build();//se lo agregue para la inyeccion de dependecias, lo saque mgp_Stock

// Add services to the container.

/* Este codigo lo saque de la pagina de cors,es para interectuar con el Front-End*/
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {                            // Si utilizo '*' permite todo, si  no debo especificar 
                          policy.WithHeaders("*");// Aqui se agregan las cabezeras que pueden acceder con el metodos POST
                          policy.WithOrigins("*");// este es de la  pag official, aqui se agregan las paginas permitidas
                          policy.WithMethods("*");//Este permite correr todos los metodos
                      });
});




builder.Services.AddControllers();

builder.Services.AddDbContext<InfoContext>(options => // inyeccion de dependencia para la DB
options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"), b => b.EnableRetryOnFailure()));


builder.Services.AddAutoMapper(typeof(Program));

//JWT FUNCION
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

var appSettings = appSettingsSection.Get<AppSettings>();
var llave = Encoding.ASCII.GetBytes(appSettings.Secreto);
builder.Services.AddAuthentication(d =>
{
    d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(d =>
    {
        d.RequireHttpsMetadata = false;
        d.SaveToken = true;
        d.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(llave),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });




builder.Services.AddScoped<IUserService, UserService>();/* codigo injectado, no ncesito crearlo lo puedo recibir directamente por
                                                          cada solicitud por cada resquest gracias al scoped */
builder.Services.AddScoped<IConsultaService, ConsultaService>(); // codigo injectado 


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);// Permite usar los CORS, lo saque de la Pag Oficial. va con lo de arriba addpolicy

app.UseAuthentication();// JWT llamada a la funcion

app.UseAuthorization();

app.MapControllers();

app.Run();
