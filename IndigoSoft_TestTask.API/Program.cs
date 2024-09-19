using IndigoSoft_TestTask_EFCore.DataAccess.PostgresSQL;
using IndigoSoft_TestTask_EFCore.DataAccess.PostgresSQL.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IndigoSoftTestTaskDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(nameof(IndigoSoftTestTaskDbContext)));
});

builder.Services.AddTransient<IDataReposity, DataReposity>();

var app = builder.Build();

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
