using AutoMapper;
using Business_Logic_Layer.Services.AdminActionsService;
using Business_Logic_Layer.Services.AuctionsService;
using Business_Logic_Layer.Services.BGService;
using Business_Logic_Layer.Services.UsersService;
using Data_Access_Layer.Data;
using Data_Access_Layer.Repositories.AdminActionsRepository;
using Data_Access_Layer.Repositories.AuctionsRepository;
using Data_Access_Layer.Repositories.UserRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Online_Auction.API.Helpers;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
        });

IMapper mapper = MappingConfigure.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<AuctionsDbContextClass>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AuctionsConnection")));

builder.Services.AddHostedService<AuctionCompleteTask>();

builder.Services.AddScoped<IUserAuthenticationRepository, UserAuthenticationRepository>();
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();

builder.Services.AddScoped<IAuctionsService, AuctionsService>();
builder.Services.AddScoped<IAuctionsRepository, AuctionsRepository>();

builder.Services.AddScoped<IAdminActionsRepository, AdminActionsRepository>();
builder.Services.AddScoped<IAdminActionsService, AdminActionsService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("IamCreatingtheJwtTokenAuction123"))
    };
});

builder.Services.AddCors(options => options.AddPolicy(name : "AuctionsPolicy", policy =>
{
    policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AuctionsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
