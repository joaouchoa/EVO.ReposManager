using EVO.ReposManager.Infrastructure.Config;
using EVO.ReposManager.Infrastructure.Context;
using EVO.ReposManager.WebApi.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApiConfig(builder.Configuration);

//builder.Services.Configure<GitHubSettings>(builder.Configuration.GetSection("GitHub"));

//builder.Services.AddDbContext<ReposManagerContext>(options =>
//{
//    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
//});

builder.Services.AddHttpClient();
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
