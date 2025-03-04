using DotNetEnv;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using myproject.Model;
using myproject.Repositories.Implementations;
using myproject.Repositories.Interfaces;
using myproject.Services.Implementations;
using myproject.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var dbConnection = Environment.GetEnvironmentVariable("DB_CONNECTION");

builder.Configuration.AddEnvironmentVariables();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseMySql(
        dbConnection,
        ServerVersion.AutoDetect(dbConnection) 
    ));
Console.WriteLine(builder.Configuration.GetConnectionString("DefaultConnection"));
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
