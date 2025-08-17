
using GameSwipe.DataAccess.DbContexts;

using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder();

// Add services to the container.

builder.Services.AddDbContext<GameSwipeDbContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GameSwipeDb;Integrated Security=true;"));

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
