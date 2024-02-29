using GameOfChanceCqrs.Api;
using GameOfChanceCqrs.Api.Filters;
using GameOfChanceCqrs.Application.Bets.PlaceBet;
using GameOfChanceCqrs.Infrastructure.Data;
using GameOfChanceCqrs.Infrastructure.Repositories;
using GameOfChangeCqrs.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(PlaceBetCommand)));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("GameOfChance"));
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();

builder.Services.AddScoped<ApiExceptionFilterAttribute>();

var app = builder.Build();

SeedDb(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void SeedDb(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            DbInitializer.Initialize(context);
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex, "An error occurred while seeding the database.");
        }
    }
}