using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OpenAI.GPT3.Extensions;
using WhatsCookTodayApi.Data;
using WhatsCookTodayApi.Repository;
using WhatsCookTodayApi.Services.AIService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<OpenAIPromptService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(EfRepository<>));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WhatCookTodayApi", Version = "v1" });
});
builder.Services.AddOpenAIService(settings => settings.ApiKey = "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");
builder.Services.AddDbContext<DatabaseContext>(
    opt => opt.UseSqlServer(
            builder.Configuration.GetConnectionString("DataBaseContext")
        )
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
