using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PTN_BackendAssignment.Data;
using PTN_BackendAssignment.Helpers;
using PTN_BackendAssignment.Services;
using System.Text;
using System.Text.Json.Serialization;

// Create the web application builder
var builder = WebApplication.CreateBuilder(args);

// Retrieve configuration values
var jwtSecret = builder.Configuration["Authentication:JWT:Secret"];
var jwtAudience = builder.Configuration["Authentication:JWT:Audience"];
var jwtIssuer = builder.Configuration["Authentication:JWT:Issuer"];
var connectionString = builder.Configuration.GetConnectionString("MSSQL");

// Check for missing or empty configuration values
if (string.IsNullOrEmpty(jwtSecret) ||
    string.IsNullOrEmpty(jwtAudience) ||
    string.IsNullOrEmpty(jwtIssuer) ||
    string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("One or more required configuration values are missing or empty.");
}

// Setup database context
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

// Initialize JWT helper
JwtHelper.Initialize(builder.Configuration.GetSection("Authentication:JWT"));

// Configure JSON Web Token Authentication
if (string.IsNullOrEmpty(jwtSecret))
{
    throw new InvalidOperationException("JWT secret is not configured.");
}

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret)),
        ValidAudience = jwtAudience,
        ValidIssuer = jwtIssuer
    };
});

// Add services to the container
builder.Services.AddScoped<AuthService, AuthService>();
builder.Services.AddScoped<TaskItemService, TaskItemService>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// Configure controllers with JSON serialization settings
builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Configure Bearer token security
    options.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Enter the JWT string as following: `Bearer Generated-JWT`",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        }
    );

    // Add security requirement for Bearer token
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] { }
        }
    });
});

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable authorization
app.UseAuthorization();

// Map controllers
app.MapControllers();

// Run the application
app.Run();
