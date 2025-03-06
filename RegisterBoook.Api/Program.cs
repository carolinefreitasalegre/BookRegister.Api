using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RegisterBoook.Api.DataAccess.AppDbContext;
using RegisterBoook.Api.Dto.Requests;
using RegisterBoook.Api.Exceptions;
using RegisterBoook.Api.Service.Services;
using RegisterBoook.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContextApi>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IRegisterBook, RegisterBookService>();
builder.Services.AddTransient<IValidator<RegisterBookRequest>, RegisterBookValidator>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
