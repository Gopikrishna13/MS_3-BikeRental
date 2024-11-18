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
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});


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

builder.Services.AddScoped<IRentService,RentService>();
builder.Services.AddScoped<IRentRepository,RentRepository>();

builder.Services.AddScoped<IReportService,ReportService>();
builder.Services.AddScoped<IReportRepository,ReportRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
      name: "CORSOpenPolicy",
      builder => {
          builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
      });
});
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();  // This enables the Swagger UI
}
app.UseCors("CORSOpenPolicy");

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
