using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Q2ConsoleAppSetup.Models;

public partial class BooksDBContext : DbContext
{
    public BooksDBContext()
    {
    }

    public BooksDBContext(DbContextOptions<BooksDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Title> Titles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C:\\USERS\\ASUSZ\\ONEDRIVE\\DESKTOP\\DB\\BOOKS.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasMany(d => d.Isbns).WithMany(p => p.Authors)
                .UsingEntity<Dictionary<string, object>>(
                    "AuthorIsbn",
                    r => r.HasOne<Title>().WithMany()
                        .HasForeignKey("Isbn")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AuthorISBN_Titles"),
                    l => l.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AuthorISBN_Authors"),
                    j =>
                    {
                        j.HasKey("AuthorId", "Isbn");
                        j.ToTable("AuthorISBN");
                        j.IndexerProperty<int>("AuthorId").HasColumnName("AuthorID");
                        j.IndexerProperty<string>("Isbn")
                            .HasMaxLength(20)
                            .IsUnicode(false)
                            .HasColumnName("ISBN");
                    });
        });

        modelBuilder.Entity<Title>(entity =>
        {
            entity.HasKey(e => e.Isbn);

            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ISBN");
            entity.Property(e => e.Copyright)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.Title1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
