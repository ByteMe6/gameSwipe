using GameSwipe.DataAccess.Entities.Games;
using GameSwipe.DataAccess.Entities.Other;
using GameSwipe.DataAccess.Entities.Users;

using Microsoft.EntityFrameworkCore;

namespace GameSwipe.DataAccess.DbContexts;

public class GameSwipeDbContext : DbContext
{
	public virtual DbSet<Game> Games { get; set; } = null!;
	public virtual DbSet<GameRecord> GameRecords { get; set; } = null!;
	public virtual DbSet<Genre> Genres { get; set; } = null!;

	public virtual DbSet<Contact> Contacts { get; set; } = null!;
	public virtual DbSet<Language> Languages { get; set; } = null!;
	public virtual DbSet<Schedule> Schedules { get; set; } = null!;
	public virtual DbSet<Platform> Platforms { get; set; } = null!;

	public virtual DbSet<User> Users { get; set; } = null!;
	public virtual DbSet<Match> Matches { get; set; } = null!;

	public GameSwipeDbContext(DbContextOptions<GameSwipeDbContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<Match>().HasOne(m => m.User).WithMany(u => u.OwnMatches).HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.NoAction);
		modelBuilder.Entity<Match>().HasOne(m => m.TargetUser).WithMany(u => u.ReceivedMatches).HasForeignKey(m => m.TargetUserId).OnDelete(DeleteBehavior.NoAction);
	}
}
