using CancunHotel.Api.Data;
using CancunHotel.Api.Domain.Interfaces;
using CancunHotel.Api.Services;
using CancunHotel.Api.Validators;
using FluentValidation;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Cancun Hotel API - V1",
            Version = "v1"
        }
     );

    x.MapType<DateOnly>(() => new OpenApiSchema { Type = "string" });
});

builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddSingleton<IReservationRepository, ReservationRepository>();

builder.Services.AddValidatorsFromAssemblyContaining<ReservationValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<GuestValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/ping", async context => await context.Response.WriteAsync("pong"));

app.Run();
