using Microsoft.EntityFrameworkCore;
using PizzaSalesAPI.Data;
using PizzaSalesAPI.Extensions;
using PizzaSalesAPI.Repository;
using PizzaSalesAPI.Repository.Interfaces;
using PizzaSalesAPI.Services;
using PizzaSalesAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Register services and repositories
builder.Services.AddApplicationServices();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

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
