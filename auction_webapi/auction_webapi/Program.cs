global using Microsoft.EntityFrameworkCore;
using auction_webapi.BL;
using auction_webapi.DAL;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using webapi.Middleware;
using Microsoft.OpenApi.Models;
using webapi;
using auction_webapi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDonarDAL, DonarDAL>();
builder.Services.AddScoped<IDonarService, DonarService>();
builder.Services.AddScoped<IPresentDAL, PresentDAL>();
builder.Services.AddScoped<IPresentService, PresentService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDAL, OrderDAL>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IOrderItemDAL, OrderItemDAL>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserDAL, UserDAL>();
builder.Services.AddScoped<IRaffleService, RaffleService>();
builder.Services.AddScoped<IRaffleDAL, RaffleDAL>();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", 
                  builder =>
                  {
                      builder.WithOrigins("http://localhost:4200",
                                           "development web site")
                                          .AllowAnyHeader()
                                          .AllowAnyMethod()
                                          ;
                  });

});

builder.Services.AddDbContext<AuctionContext>(option=>option.UseSqlServer(builder.Configuration.GetConnectionString("AuctionContextHome")));

builder.Services.AddAuthentication(options =>
     {
         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

     }).AddJwtBearer(options =>
     {
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = true,
             ValidateAudience = true,
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,
             ValidIssuer = "http://localhost:44342/",
             ValidAudience = "http://localhost:4200/",
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
         };
     });

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailService, EmailService>();

var app = builder.Build();

app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();


app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/raffle"), orderApp =>
{
    orderApp.Use(async (context, next) =>
    {
        var a = context.User;

        if (context.Request.Headers.ContainsKey("Authorization"))
        {
            var authorizationHeader = context.Request.Headers["Authorization"].ToString();
            if (authorizationHeader.StartsWith("Bearer "))
            {
                context.Request.Headers["Authorization"] = authorizationHeader.Substring("Bearer ".Length); 
            }
        }

        await next();
    });
    orderApp.UseMiddleware<AuthenticationMiddleware>();
});
IConfiguration configuration = app.Configuration;
IWebHostEnvironment environment = app.Environment;

app.MapControllers();



app.Run();
