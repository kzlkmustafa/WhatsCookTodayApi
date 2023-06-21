using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenAI.GPT3.Extensions;
using System.Text;
using WhatsCookTodayApi.Controllers;
using WhatsCookTodayApi.Data;
using WhatsCookTodayApi.MyModels;
using WhatsCookTodayApi.Repository;
using WhatsCookTodayApi.Services;
using WhatsCookTodayApi.Services.Abstracts;
using WhatsCookTodayApi.Services.AIService;
using WhatsCookTodayApi.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

string secretStr = builder.Configuration["Application:Secret"];
byte[] secret = Encoding.UTF8.GetBytes(secretStr);
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<AIAnswerController>();
builder.Services.AddScoped<OpenAIPromptService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IMyPromptService, MyPromptManager>();
builder.Services.AddScoped<IAlPromptService, AIPromptManager>();
builder.Services.AddScoped<IMealOfDayService, MealOfDayManager>();
builder.Services.AddIdentity<MyUsers, IdentityRole>(o =>
{
    o.Password.RequireDigit = false;
    o.Password.RequireLowercase = false;
    o.Password.RequireUppercase = false;
    o.Password.RequireNonAlphanumeric = false;
    o.User.RequireUniqueEmail = false;
})
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Audience = builder.Configuration["Application:Audience"];
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        IssuerSigningKey = new SymmetricSecurityKey(secret),
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = true,
        ClockSkew = TimeSpan.Zero
    };
});
//builder.Services.ConfigureIdentity();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WhatCookTodayApi", Version = "v1" });
});

builder.Services.AddOpenAIService(settings => settings.ApiKey = builder.Configuration["AIKeys:OpenAIKeys"]);
builder.Services.AddDbContext<DatabaseContext>(
    opt => opt.UseSqlServer(
            builder.Configuration.GetConnectionString("DataBaseContext")
        )
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });
builder.Services.AddControllers().AddNewtonsoftJson(options =>
                                                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
