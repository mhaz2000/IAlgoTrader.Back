using IAlgoTrader.Back;
using IAlgoTrader.Back.Repository.Implementation;
using IAlgoTrader.Back.Repository;
using IAlgorTrader.Back.Service;
using IAlgoTrader.Back.Service.Implementation;
using IAlgoTrader.Back.Service.SeedWorks.Helpers;
using IAlgoTrader.Back.Service.SeedWorks.Interfaces;
using IAlgorTrader.Back.Service.SeedWorks.Factories;
using Microsoft.EntityFrameworkCore;
using IAlgoTrader.Back.Domain.Entities;
using IAlgoTrader.Back.Service.SeedWorks.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IAlgoTrader.Back.SeedWorks.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using IAlgorTrader.Back.Service.SeedWorks.Interfaces;
using IAlgorTrader.Back.Service.SeedWorks.Helpers;
using Hangfire;
using Hangfire.SqlServer;
using IAlgoTrader.Back.Jobs;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .Build();

AppSettingsModel appSettingsModel = config.Get<AppSettingsModel>();
var jwtIssuerOptions = appSettingsModel.JwtIssuerOptions;


builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Main")));

SymmetricSecurityKey signingKey =
new SymmetricSecurityKey(
                   Encoding.ASCII.GetBytes(jwtIssuerOptions.SecretKey));

// Configure JwtIssuerOptions
builder.Services.Configure<JwtIssuerOptions>(options =>
{
    options.Issuer = jwtIssuerOptions.Issuer;
    options.Audience = jwtIssuerOptions.Audience;
    options.SecretKey = jwtIssuerOptions.SecretKey;
    options.ExpireTimeTokenInMinute = jwtIssuerOptions.ExpireTimeTokenInMinute;
    options.ValidTimeInMinute = jwtIssuerOptions.ValidTimeInMinute;
    options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
});

builder.Services.AddHangfireServer();

builder.Services.AddHangfire(configuration => configuration
             .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
             .UseSimpleAssemblyNameTypeSerializer()
             .UseRecommendedSerializerSettings()
             .UseSqlServerStorage(config.GetConnectionString("Main"), new SqlServerStorageOptions
             {
                 CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                 SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                 QueuePollInterval = TimeSpan.Zero,
                 UseRecommendedIsolationLevel = true,
                 DisableGlobalLocks = true
             }));

// add identity
var identityBuilder = builder.Services.AddIdentity<User, IdentityRole>(o =>
{
    // configure identity options
    o.Password.RequireDigit = false;
    o.Password.RequireLowercase = false;
    o.Password.RequireUppercase = false;
    o.Password.RequireNonAlphanumeric = false;
    o.Password.RequiredLength = 6;
    o.Tokens.ChangePhoneNumberTokenProvider = "Phone";
});
identityBuilder = new IdentityBuilder(identityBuilder.UserType, typeof(IdentityRole), builder.Services);
identityBuilder.AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidIssuer = jwtIssuerOptions.Issuer,

    ValidateAudience = true,
    ValidAudience = jwtIssuerOptions.Audience,

    ValidateIssuerSigningKey = true,
    IssuerSigningKey = signingKey,

    RequireExpirationTime = false,
    ValidateLifetime = true,
    ClockSkew = TimeSpan.Zero
};

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, configureOptions =>
{
    configureOptions.ClaimsIssuer = jwtIssuerOptions.Issuer;
    configureOptions.TokenValidationParameters = tokenValidationParameters;
    configureOptions.SaveToken = true;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin",
        authBuilder =>
        {
            authBuilder.RequireRole("Admin");
        });

    options.AddPolicy("User",
        authBuilder =>
        {
            authBuilder.RequireRole("User");
        });
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IServiceHolder, ServiceHolder>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
builder.Services.AddScoped<IJwtFactory, JwtFactory>();
builder.Services.AddScoped<ITokenFactory, TokenFactory>();
builder.Services.AddScoped<IOrderScheduler, OrderScheduler>();
builder.Services.AddScoped<ITransactionScheduler, TransactionScheduler>();

var app = builder.Build();

builder.WebHost.UseUrls(appSettingsModel.HostAddress);
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseCors(appBuilder => appBuilder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHangfireDashboard("/IAlgoTraderDashboard");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

RecurringJob.AddOrUpdate<IOrderScheduler>("OrderScheduler", x => x.ApplyOrders(), Cron.Hourly, TimeZoneInfo.Local);
//RecurringJob.AddOrUpdate<IOrderScheduler>("OrderScheduler", x => x.ApplyOrders(), "0 10 * * *", TimeZoneInfo.Local);
RecurringJob.AddOrUpdate<ITransactionScheduler>("TransactionScheduler", x => x.GenerateTransaction(), "30 12 * * *", TimeZoneInfo.Local);

app.Seed().Run();