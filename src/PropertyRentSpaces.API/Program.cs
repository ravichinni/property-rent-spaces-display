using PropertyRentSpaces.Infrastructure.Services;
using DotNetEnv;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Load .env file from the API project directory
Env.Load();

// Add environment variables configuration
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger/OpenAPI
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Property Rent Spaces API",
        Version = "v1",
        Description = "API for retrieving property and space rental information from Azure Blob Storage",
        Contact = new OpenApiContact
        {
            Name = "API Support",
            Email = "support@example.com"
        }
    });

    // Set the comments path for the Swagger JSON and UI
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

// Add memory cache
builder.Services.AddMemoryCache();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder => builder
            .WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Register services
builder.Services.AddScoped<IAzureBlobStorageService, AzureBlobStorageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.SerializeAsV2 = false;
    });
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Property Rent Spaces API V1");
        c.RoutePrefix = "swagger";
        c.DocumentTitle = "Property Rent Spaces API Documentation";
        c.DefaultModelsExpandDepth(-1); // Hide models section by default
    });
}

app.UseCors("AllowReactApp");
app.UseAuthorization();
app.MapControllers();

app.Run(); 