using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Data;

public partial class BookStoreDbContext : IdentityDbContext<ApiUser>
{
    public BookStoreDbContext()
    {
    }

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Author>(entity =>
        {

            entity.HasKey(e => e.Id).HasName("PK__Authors__3214EC0734CA1E98");

            entity.Property(e => e.Bio).HasMaxLength(250);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Books__3214EC07A707B70C");

            entity.HasIndex(e => e.Isbn, "UQ__Books__447D36EABA3444F8").IsUnique();

            entity.Property(e => e.Image).HasMaxLength(50);
            entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .HasColumnName("ISBN");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Summary).HasMaxLength(250);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_Books_ToTable");
        });

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER",
                Id = "9777cb41-7ddf-4ca2-968d-a3047a90efc2"
            },
            new IdentityRole
            {
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
                Id = "3877cfbf-fa1e-40c6-86b1-08e2039a82db"
            }   
        );

        var hasher=new PasswordHasher<ApiUser>();

        modelBuilder.Entity<ApiUser>().HasData(
            new ApiUser
            {
                Id = "26222e5b-7980-4e6b-970b-974b73c3a26a",
                Email="admin@bookstore.com",
                NormalizedEmail = "ADMIN@BOOKSTORE.COM",
                UserName = "admin@bookstore.com",
                NormalizedUserName = "ADMIN@BOOKSTORE.COM",
                FirstName = "System",
                LastName = "Admin",
                PasswordHash=hasher.HashPassword(null, "P@ssword1")
            },
            new ApiUser
            {
                Id = "1e7b9958-af07-4b1d-a04f-d4f52d5422c9",
                Email = "user@bookstore.com",
                NormalizedEmail = "USER@BOOKSTORE.COM",
                UserName = "user@bookstore.com",
                NormalizedUserName = "USER@BOOKSTORE.COM",
                FirstName = "System",
                LastName = "User",
                PasswordHash = hasher.HashPassword(null, "P@ssword1")
            }
        );

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId= "9777cb41-7ddf-4ca2-968d-a3047a90efc2",
                UserId = "1e7b9958-af07-4b1d-a04f-d4f52d5422c9"
            },
            new IdentityUserRole<string>
            {
                RoleId = "3877cfbf-fa1e-40c6-86b1-08e2039a82db",
                UserId = "26222e5b-7980-4e6b-970b-974b73c3a26a"
            }

        );

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
