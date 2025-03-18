using Authentication.Api.Controllers;
using Authentication.Infrastructure.Data;
using Authentication.Infrastructure.DI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureService(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var appDbContext = services.GetRequiredService<AuthenticationDbContext>();
        appDbContext.Database.Migrate();
        Console.WriteLine("Migrations applied successfully.");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while applying migrations.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    Console.WriteLine("Running in Development Mode");
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.AccountInfrastructurePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
