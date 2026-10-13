using Basedatos;
using BaseDatos;
using BaseDatos.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(cors =>
{
    cors.AddPolicy("todos", policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
});

builder.Services.Configure<JsonOptions>(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDb"));
builder.Services.AddSingleton<IMongoClient>(sp =>
{
     var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
     return new MongoClient(settings.ConnectionString);   
});

// Configurar repositorios
builder.Services.AddTransient<AlumnosRepository>(); 
builder.Services.AddTransient<DocentesRepository>();
// Fin repositorios 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("todos");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
