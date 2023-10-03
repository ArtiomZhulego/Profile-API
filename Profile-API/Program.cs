using Domain.Repositories;
using Microsoft.OpenApi.Models;
using Persistance;
using Profile_API.Middleware;
using Services;
using Services.Abstraction;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IDoctorRepository, DoctorContext>();
builder.Services.AddSingleton<IPatientRepository, PatientContext>();
builder.Services.AddSingleton<IReceptionistRepository, ReceptionistContext>();

builder.Services.AddSingleton<IDoctorService, DoctorService>();
builder.Services.AddSingleton<IPatientService, PatientService>();
builder.Services.AddSingleton<IReceptionistService, ReceptionistService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
