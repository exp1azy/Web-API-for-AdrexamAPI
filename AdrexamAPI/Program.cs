using AdrexamAPI;
using AdrexamAPI.Data;
using AdrexamAPI.Middleware;
using AdrexamAPI;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.AddSecurityDefinition("x-api-key", new OpenApiSecurityScheme
    {
        Description = "x-api-key должен быть передан в заголовке запроса.",
        Type = SecuritySchemeType.ApiKey,
        Name = "x-api-key",
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });
    var key = new OpenApiSecurityScheme()
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "x-api-key"
        },
        In = ParameterLocation.Header
    };
    var requirement = new OpenApiSecurityRequirement
    {
        { key, new List<string>() }
    };
    config.AddSecurityRequirement(requirement);

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    config.IncludeXmlComments(xmlPath);
    config.UseInlineDefinitionsForEnums();

});

builder.Services.AddTransient<Service>();
builder.Services.AddTransient<TokenService>();
builder.Services.AddMemoryCache();

var app = builder.Build();
ServiceProviderAccessor.ServiceProvider = app.Services;

//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<TokenMiddleware>();

app.Run();