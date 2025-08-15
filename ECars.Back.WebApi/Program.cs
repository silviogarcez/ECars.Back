using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ECars.Back.Data;
using ECars.Back.Hubs;

var builder = WebApplication.CreateBuilder(args);

// DB Context
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity com roles
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// SSO
builder.Services.AddAuthentication()
    .AddGoogle(options => { options.ClientId = builder.Configuration["Authentication:Google:ClientId"]; options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]; })
    .AddFacebook(options => { options.AppId = builder.Configuration["Authentication:Facebook:AppId"]; options.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"]; });

// CORS
builder.Services.AddCors(options => options.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

// SignalR para chat
builder.Services.AddSignalR();

// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();
app.MapControllers();
app.MapHub<ChatHub>("/chat");
app.Run();