using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RegisterBoook.Api.DataAccess.AppDbContext;
using RegisterBoook.Api.Dto.Requests;
using RegisterBoook.Api.Exceptions;
using RegisterBoook.Api.Service.Interfaces;
using RegisterBoook.Api.Service.Services;
using RegisterBoook.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var secretToken = "bf070603 - edc3 - 4fbb-b153-5ea1c590cca3"; 
// Add services to the container.

builder.Services.AddDbContext<AppDbContextApi>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IRegisterBook, RegisterBookService>();
builder.Services.AddScoped<IRegisterAuthor, RegisterAuthorService>();
builder.Services.AddTransient<IValidator<RegisterBookRequest>, RegisterBookValidator>();
builder.Services.AddTransient<IValidator<RegisterAuthorRequest>, RegisterAuthorValidator>();

builder.Services.AddAuthentication(options => { 
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "register_book",
        ValidAudience = "app_book",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretToken))


    };
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Registro de Livros - Api", Version = "v1" });

    var securitySchema = new OpenApiSecurityScheme
    {
        Name = "Jwt Autentication",
        Description = "Entre com o JWT Bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme,
        }
    };
    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securitySchema);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securitySchema, new string[] {} }
    });
});








var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
