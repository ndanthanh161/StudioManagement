using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudioManagement.Application;
using StudioManagement.Contract.Validates;
using StudioManagement.Infrastructure;
using StudioManagement.Infrastructure.DataSeeding;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// =========================
// 1. CORS cho Frontend
// =========================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:5173", "http://localhost:3000") // React/Vite
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials());
});

// =========================
// 2. DbContext
// =========================
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// =========================
// 3. Controllers + Swagger
// =========================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
// Nếu có Authentication / Authorization
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options => { ... });
// builder.Services.AddAuthorization();

builder.Logging.AddConsole(); // nếu chưa có

// quét validators ở assembly bạn đặt validator


var jwt = builder.Configuration.GetSection("Jwt");
var keyBytes = Encoding.UTF8.GetBytes(jwt["Key"]!);
var signingKey = new SymmetricSecurityKey(keyBytes);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt["Issuer"],
            ValidAudience = jwt["Audience"],
            IssuerSigningKey = signingKey,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddHttpLogging(o =>
{
    // Log khi có request/response, còn lúc khởi động thì im
    o.LoggingFields =
        HttpLoggingFields.RequestMethod |
        HttpLoggingFields.RequestPath |
        HttpLoggingFields.ResponseStatusCode |
        HttpLoggingFields.Duration;
    // có thể thêm headers/body khi cần debug sâu (cẩn thận lộ data)
    // o.LoggingFields |= HttpLoggingFields.RequestHeaders | HttpLoggingFields.ResponseHeaders;
    // o.LoggingFields |= HttpLoggingFields.RequestBody | HttpLoggingFields.ResponseBody;
});

// (tuỳ chọn) ép mức log mặc định về Warning ngay trong code:
builder.Logging.AddFilter("", LogLevel.Warning);
builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging.HttpLoggingMiddleware", LogLevel.Information);
builder.Logging.AddFilter("StudioManagement", LogLevel.Information);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<LoginValidator>();
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<ApiBehaviorOptions>(o =>
{
    o.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationLoggingFilter>();
});
var app = builder.Build();

// =========================
// 4. Seed dữ liệu
// =========================
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await db.Database.MigrateAsync();  // tự động apply migrations
    await DbInitializer.SeedAsync(db); // seed role + admin
}


// =========================
// 5. Middleware pipeline
// =========================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpLogging();
app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
