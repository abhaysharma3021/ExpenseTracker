using ExpenseTracker.Infrastructure;
using ExpenseTracker.Application;
using ExpenseTracker.API.Configuration;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddRepositories();

builder.Services.AddAutoMapping();
builder.Services.AddServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
