﻿using Beershop24.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Beershop24.Domains.Data;

public partial class BeerDbContext : DbContext
{
	public BeerDbContext()
	{
	}

	public BeerDbContext(DbContextOptions<BeerDbContext> options)
		: base(options)
	{
	}

	public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

	public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

	public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

	public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

	public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

	public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

	public virtual DbSet<Beer> Beers { get; set; }

	public virtual DbSet<Brewery> Breweries { get; set; }

	public virtual DbSet<Variety> Varieties { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			// install this packages:
			// - Microsoft.Extensions.Configuration.Json
			IConfigurationRoot configuration = new ConfigurationBuilder()
					  .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
					  .AddJsonFile("appsettings.json")
					  .Build();

			// add connectionstring to appsettings.json file (see appsettings.json)
			optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
		}
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.UseCollation("Latin1_General_CI_AS");

		modelBuilder.Entity<AspNetRole>(entity =>
		{
			entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
				.IsUnique()
				.HasFilter("([NormalizedName] IS NOT NULL)");

			entity.Property(e => e.Name).HasMaxLength(256);
			entity.Property(e => e.NormalizedName).HasMaxLength(256);
		});

		modelBuilder.Entity<AspNetRoleClaim>(entity =>
		{
			entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

			entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
		});

		modelBuilder.Entity<AspNetUser>(entity =>
		{
			entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

			entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
				.IsUnique()
				.HasFilter("([NormalizedUserName] IS NOT NULL)");

			entity.Property(e => e.Email).HasMaxLength(256);
			entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
			entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
			entity.Property(e => e.UserName).HasMaxLength(256);

			entity.HasMany(d => d.Roles).WithMany(p => p.Users)
				.UsingEntity<Dictionary<string, object>>(
					"AspNetUserRole",
					r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
					l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
					j =>
					{
						j.HasKey("UserId", "RoleId");
						j.ToTable("AspNetUserRoles");
						j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
					});
		});

		modelBuilder.Entity<AspNetUserClaim>(entity =>
		{
			entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

			entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
		});

		modelBuilder.Entity<AspNetUserLogin>(entity =>
		{
			entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

			entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

			entity.Property(e => e.LoginProvider).HasMaxLength(128);
			entity.Property(e => e.ProviderKey).HasMaxLength(128);

			entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
		});

		modelBuilder.Entity<AspNetUserToken>(entity =>
		{
			entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

			entity.Property(e => e.LoginProvider).HasMaxLength(128);
			entity.Property(e => e.Name).HasMaxLength(128);

			entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
		});

		modelBuilder.Entity<Beer>(entity =>
		{
			entity.HasKey(e => e.Biernr).HasName("PK_Bieren");

			entity.Property(e => e.Alcohol).HasColumnType("decimal(3, 2)");
			entity.Property(e => e.Image)
				.HasMaxLength(50)
				.IsFixedLength();
			entity.Property(e => e.Naam)
				.HasMaxLength(50)
				.IsFixedLength();

			entity.HasOne(d => d.BrouwernrNavigation).WithMany(p => p.Beers)
				.HasForeignKey(d => d.Brouwernr)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_Bieren_Brouwerij");

			entity.HasOne(d => d.SoortnrNavigation).WithMany(p => p.Beers)
				.HasForeignKey(d => d.Soortnr)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_Bieren_Soorten");
		});

		modelBuilder.Entity<Brewery>(entity =>
		{
			entity.HasKey(e => e.Brouwernr).HasName("PK_Brouwerij");

			entity.Property(e => e.Brouwernr).ValueGeneratedNever();
			entity.Property(e => e.Adres)
				.HasMaxLength(60)
				.IsFixedLength();
			entity.Property(e => e.Gemeente)
				.HasMaxLength(40)
				.IsFixedLength();
			entity.Property(e => e.Naam)
				.HasMaxLength(50)
				.IsFixedLength();
			entity.Property(e => e.Omzet).HasColumnType("decimal(12, 2)");
			entity.Property(e => e.Postcode)
				.HasMaxLength(10)
				.IsFixedLength();
		});

		modelBuilder.Entity<Variety>(entity =>
		{
			entity.HasKey(e => e.Soortnr).HasName("PK_Soorten");

			entity.Property(e => e.Soortnr).ValueGeneratedNever();
			entity.Property(e => e.Soortnaam)
				.HasMaxLength(30)
				.IsUnicode(false)
				.IsFixedLength();
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
