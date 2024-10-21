using Cobo.Api.Configuration.Auth;
using Cobo.Api.Swagger;
using Cobo.Domain.Interfaces;
using Cobo.Domain.Repos.Accounts;
using Cobo.Domain.Repos.Transacction;
using Cobo.Domain.Repos.Users;
using Cobo.Infraestructure.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;


builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = config["Jwt:Issuer"],
        ValidAudience = config["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(config["Jwt:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddDbContext<BancoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserInterface, UserRepository>();
builder.Services.AddScoped<IAccountInterface, AccountRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ITransactionInterface, TransactionRepository>();

WebApplication app = builder.Build();

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
