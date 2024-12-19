using Microsoft.EntityFrameworkCore;
using ProjectPersonal;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using ProjectPersonal.Application;
using ProjectPersonal.Infrastructure;
using System.Reflection;
using Microsoft.OpenApi.Models;
using ProjectPersonal.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog.
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(
    theme: SystemConsoleTheme.Colored,
    outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}")
    .CreateLogger();
Log.Information("Start!!!");
// Add services Database to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<QlcoffeeContext>(options =>
    options.UseSqlServer(connectionString));

//Add All Service to the container.
#region
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureService();
#endregion
//-----------------------------------------
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
     {
         options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
         options.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore;
     }); 
builder.Services.AddEndpointsApiExplorer();
#region ConfigureSwagger
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "QLCoffee", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
#endregion

//Add CORS
#region
builder.Services.AddCors(options =>
{
    options.AddPolicy("", policy =>
    {
        policy.WithOrigins("http://localhost:5173")  
              .AllowAnyMethod()  
              .AllowAnyHeader(); 
    });
});
#endregion
builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<AddTokenToHeaderMiddleware>();
app.MapControllers();

app.Run();
