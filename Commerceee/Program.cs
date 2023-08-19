using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Reposotory;
using Microsoft.AspNetCore.Cors.Infrastructure;
using StackExchange.Redis;
using Infrastructure.Identity;
using Core.Idenrity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infrastructure.Servises;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CommerceContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<AppUserContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));
builder.Services.AddIdentityCore<AppUser>(opt =>
{

})
    .AddEntityFrameworkStores<AppUserContext>()
    .AddSignInManager<SignInManager<AppUser>>();
var jwt = builder.Configuration.GetSection("Token");
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options=>options.TokenValidationParameters=new Microsoft.IdentityModel.Tokens.TokenValidationParameters)
//    {
//    validateIssureSigningKey =true,
//};
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(jwt["Key"])),
                        ValidateIssuer =true,
                        ValidIssuer= jwt["Issuer"],
                        ValidateAudience = false
                    };
                });
builder.Services.AddAuthorization();
builder.Services.AddScoped<IProductRepo,ProductRepo>();
builder.Services.AddScoped<IBasketRepo, CustomBaskertRepo>();
builder.Services.AddScoped<ITokenService, TokenServicecs>();
builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
{
    var option = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));
    return ConnectionMultiplexer.Connect(option);
});
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
        Description = "An ASP.NET Core Web API for managing ToDo items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
        new CorsPolicyBuilder()
           .WithOrigins("http://localhost:4200",
           "http://localhost:5200")
           .AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials()
           .Build());
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseRouting();
app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();
using var scope = app.Services.CreateScope();
var service = scope.ServiceProvider;
var identityContext = service.GetRequiredService<AppUserContext>();
var userManager = service.GetRequiredService<UserManager<AppUser>>();
try
{
    await identityContext.Database.MigrateAsync();
    await AppIdentityUserSeed.AddUserAdminAsync(userManager);
}
catch(Exception e)
{

}
app.Run();
