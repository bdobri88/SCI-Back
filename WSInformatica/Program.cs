using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WSInformatica.Models;
using WSInformatica.Models.Common;
using WSInformatica.Services;

var MyAllowSpecificOrigins = "__myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true) 
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true) 
    .AddEnvironmentVariables()
    .Build();

// Add services to the container.

/* Este codigo lo saque de la pagina de cors,es para interectuar con el Front-End*/
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200",
                                              "http://localhost:59202",
                                              "https://tudominiofrontend.com") // <--- ¡CAMBIAR ESTO PARA PRODUCCIÓN!
                                              .AllowAnyHeader()
                                              .AllowAnyMethod()
                                              .AllowCredentials();
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
        d.RequireHttpsMetadata = !builder.Environment.IsDevelopment(); // True en producción, false en desarrollo
        d.SaveToken = true;
        d.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(llave),
            ValidateIssuer = true,    // <--- ¡AHORA VALIDAMOS EL EMISOR!
            ValidIssuer = appSettings.Issuer, // <--- Aquí el emisor esperado
            ValidateAudience = true,  // <--- ¡AHORA VALIDAMOS LA AUDIENCIA!
            ValidAudience = appSettings.Audience, // <--- Aquí la audiencia esperada
            ValidateLifetime = true,  // <--- ¡CRÍTICO: VALIDAR LA VIDA ÚTIL DEL TOKEN!
            ClockSkew = TimeSpan.Zero // <--- Elimina el tiempo de gracia en la expiración
        };
    });


builder.Services.AddScoped<IUserService, UserService>();/* codigo injectado, no ncesito crearlo lo puedo recibir directamente por
                                                          cada solicitud por cada resquest gracias al scoped */
builder.Services.AddScoped<IConsultaService, ConsultaService>(); // codigo injectado 


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SCI-Informatica", Version = "v1" });

    // Configuración para JWT Bearer en Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Ingresa el token JWT de esta manera: Bearer {tu token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Aplicar migraciones al inicio
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<InfoContext>(); 
        dbContext.Database.Migrate();
        // Opcional: Si quieres seedear datos iniciales después de migrar
        // SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
        // Considera detener la aplicación si la migración es crítica
        // throw;
    }
}


// Configure the HTTP request pipeline.

// Configura el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SCI-Informatica v1");
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}



app.UseStaticFiles(); // Para servir archivos estáticos desde wwwroot
app.UseRouting();


app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);// Permite usar los CORS, lo saque de la Pag Oficial. va con lo de arriba addpolicy

app.UseAuthentication();// JWT llamada a la funcion

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("index.html"); // Redirige todas las solicitudes no manejadas a index.html en wwwroot


app.Run();
