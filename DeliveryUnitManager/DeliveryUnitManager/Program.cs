using DeliveryUnitManager.Middleware;
using DeliveryUnitManager.Reponsitory.Models.Users;
using DeliveryUnitManager.Reponsitory.Services;
using DeliveryUnitManager.Reponsitory.Services.Services;
using DeliveryUnitManager.Repository.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DeliveryUnitDataContext>();
builder.Services.AddScoped<IService<Users>, UserSevice>();
builder.Services.AddScoped<IService<Positions>, PositionService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder => builder.SetIsOriginAllowed(origin => true)
.AllowAnyHeader()
.AllowAnyMethod()
.AllowCredentials()
);
app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
