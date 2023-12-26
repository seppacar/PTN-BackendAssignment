using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PTN_BackendAssignment.Data;
using PTN_BackendAssignment.Helpers;
using PTN_BackendAssignment.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//
var jwtSecret = builder.Configuration["Authentication:JWT:Secret"];
var jwtAudience = builder.Configuration["Authentication:JWT:Audience"];
var jwtIssuer = builder.Configuration["Authentication:JWT:Issuer"];
var connectionString = builder.Configuration.GetConnectionString("MSSQL");

if (string.IsNullOrEmpty(jwtSecret) ||
    string.IsNullOrEmpty(jwtAudience) ||
    string.IsNullOrEmpty(jwtIssuer) ||
    string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("One or more required configuration values are missing or empty.");
}

// Setup db context
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

// Initalize JWT helper
JwtHelper.Initialize(builder.Configuration.GetSection("Authentication:JWT"));

// JSON Web Token Authentication
if (string.IsNullOrEmpty(jwtSecret))
{
    // Handle the case where the secret is null or empty. You might throw an exception, log an error, or use a default key.
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
            ValidAudience = builder.Configuration["Authentication:JWT:Audience"],
            ValidIssuer = builder.Configuration["Authentication:JWT:Issuer"]
        };
    }
);

// Add services to the container.
builder.Services.AddScoped<AuthService, AuthService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Enter the JWT string as following: `Bearer Generated-JWT`",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        }
        );
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
