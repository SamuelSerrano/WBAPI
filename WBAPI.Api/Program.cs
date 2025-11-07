using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WBAPI.Api.Repositories;
using WBAPI.Api.Services;

var builder = WebApplication.CreateBuilder(args);


var jwtConfig = builder.Configuration.GetSection("Jwt");
var key = jwtConfig["Key"] ?? throw new InvalidOperationException("Missing JWT key configuration.");


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<ITokenService>(
    new TokenService(
        key,
        jwtConfig["Issuer"]!,
        jwtConfig["Audience"]!,
        int.Parse(jwtConfig["ExpireMinutes"]!)
    )
);


var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtConfig["Issuer"],
            ValidateAudience = true,
            ValidAudience = jwtConfig["Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero 
        };
    });


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
