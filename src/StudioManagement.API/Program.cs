using Microsoft.EntityFrameworkCore;
using StudioManagement.Infrastructure;
using StudioManagement.Infrastructure.DataSeeding;

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

// Nếu có Authentication / Authorization
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options => { ... });
// builder.Services.AddAuthorization();

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

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

// app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
