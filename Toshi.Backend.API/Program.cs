using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.MSSqlServer;

//using NLog.Web;
using Toshi.Backend.API.Middleware;
using Toshi.Backend.Application;
using Toshi.Backend.Domain;
using Toshi.Backend.Infraestructure;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();

#region Logs...
//
// NLog
//
//builder.Logging.ClearProviders();
//builder.Host.UseNLog();

ColumnOptions GetColumnOptions()
{
    var columnOptions = new ColumnOptions();

    columnOptions.Store.Remove(StandardColumn.Properties);
    columnOptions.Store.Add(StandardColumn.LogEvent);

    return columnOptions;
}

var sinkOptions = new MSSqlServerSinkOptions
{
    TableName = "REQUEST_LOGS",
    AutoCreateSqlTable = true
};

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.MSSqlServer(
        connectionString: builder.Configuration.GetConnectionString("ToshiDBContext"),
        sinkOptions: sinkOptions,
        columnOptions: GetColumnOptions()
    )
    .CreateLogger();

builder.Host.UseSerilog();

#endregion

builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add(new ProducesAttribute("application/json"));
    })
    .AddNewtonsoftJson()
    //.AddJsonOptions(options =>
    //{
    //    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    //})
    ;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Granja Toshi", Version = "v1" });
    //options.CustomSchemaIds(type => type.FullName?.Replace(".", "_"));

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor ingrese un token válido",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{ }
        }
    });
});


//builder.Services.AddScoped<ISessionRepository, SessionRepository>();
//builder.Services.AddScoped<UserContainerDTO>();

// 🔹 Registrar `SessionStorage` como Singleton
builder.Services.AddSingleton<SessionStorage>();

// 🔹 Registrar `ISessionRepository` como Scoped
// builder.Services.AddScoped<ISessionRepository, SessionRepository>();

#region Redis...
// Configurar Redis
//builder.Services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect("localhost:6379")); // Reemplaza con la IP/host de tu servidor Redis


//builder.Services.AddSingleton<TokenBlacklistService>();
#endregion

builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.ConfigureIdentityServices(builder.Configuration);

var app = builder.Build();

// SE AGREGA LA SIGUIENTE LINEA, SOLO SI VA HA TRABAJAR ARCHIVOS ESTATICOS
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Sample");
    });
}

app.UseDeveloperExceptionPage();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());//put

app.UseMiddleware<ExceptionMiddleware>();

//#if !DEBUG
//app.UseHttpsRedirection();
//#endif

app.UseHttpsRedirection();
app.UseAuthentication();//put
app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapControllers();
app.Run();