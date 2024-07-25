using Microsoft.EntityFrameworkCore; // Imports Entity Framework Core namespace.
using TeamWebAPI.Data; // Imports namespace where data context is defined.
using TeamWebAPI.Models; // Imports namespace where models are defined.
using NSwag.AspNetCore; // Imports ASP.NET Core namespace.

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. Configures Entity Framework to use SQL Server with the connection string from configuration.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Adds services to the container for controllers, API endpoints, and Swagger.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApiDocument();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enables middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();
    // Enables middleware to serve Swagger UI.
    app.UseSwaggerUi();
    // Enables middleware to serve OpenAPI documentation.
    app.UseOpenApi();
}

// Enables HTTPS redirection middleware.
app.UseHttpsRedirection();
// Enables authorization middleware.
app.UseAuthorization();
// Maps controllers to endpoints.
app.MapControllers();
// Runs the application.
app.Run();
