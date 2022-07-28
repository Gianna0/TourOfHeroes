using hero_csharp.Database;
using hero_csharp.Exceptions;
using hero_csharp.Repositories;
using hero_csharp.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(cors =>
{
    cors.AddPolicy(name: myPolicy, policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSingleton<IHeroRepository, InMemoryHeroRepository>();
builder.Services.AddScoped<IHeroRepository, HeroRepository>();
builder.Services.AddScoped<IHeroService, HeroService>();
builder.Services.AddScoped<ErrorHandleMiddleware>();
var connection = new SqliteConnection(builder.Configuration.GetConnectionString("InMemory"));
connection.Open();
connection.EnableExtensions(true);
builder.Services.AddDbContext<Context>(context => context.UseSqlite(connection));
builder.Services.AddHostedService<DbInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(myPolicy);
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseMiddleware<ErrorHandleMiddleware>();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/api", () => "Welocome to Heroes API!");
app.Run();

public partial class Program 
{
    public const string myPolicy = "policy";
}