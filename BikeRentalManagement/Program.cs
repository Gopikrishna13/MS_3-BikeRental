using System.Text;
using BikeRentalManagement.Database;
using BikeRentalManagement.IRepository;
using BikeRentalManagement.IService;
using BikeRentalManagement.Repository;
using BikeRentalManagement.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Register Swagger service
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BikeDbContext>(option => 
option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IUserRepository,UserRepository>();

builder.Services.AddScoped<IBikeService,BikeService>();
builder.Services.AddScoped<IBikeRepository,BikeRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();  // This enables the Swagger UI
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
