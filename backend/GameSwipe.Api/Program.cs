
using GameSwipe.Api.Extensions;
using GameSwipe.Api.Services;
using GameSwipe.Application.Interfaces.Services;
using GameSwipe.Application.Interfaces.Services.Games;
using GameSwipe.Application.Interfaces.Services.Others;
using GameSwipe.Application.Interfaces.Services.Users;
using GameSwipe.Application.Services;
using GameSwipe.DataAccess.DbContexts;

using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder();

// Add services to the container.

builder.Services.AddDbContext<GameSwipeDbContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GameSwipeDb;Integrated Security=true;"));
builder.Services.AddScoped<ILanguageService, LanguageService>();
builder.Services.AddScoped<IPlatformService, PlatformService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IMatchService, MatchService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddJwtAuthentication(builder.Configuration.GetSection("Jwt"));
builder.Services.AddSwaggerAuthentication();

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
