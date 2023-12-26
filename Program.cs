using Microsoft.EntityFrameworkCore;
using PTN_BackendAssignment.Data;
using PTN_BackendAssignment.Helpers;
using PTN_BackendAssignment.Services;

var builder = WebApplication.CreateBuilder(args);

// Setup db context
var connectionString = builder.Configuration.GetConnectionString("MSSQL");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

// Initalize Jwt helper
JwtHelper.Initialize(builder.Configuration.GetSection("Authentication:JWT"));

// Add services to the container.
builder.Services.AddScoped<AuthService, AuthService>();

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
