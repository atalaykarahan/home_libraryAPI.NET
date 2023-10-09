using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace home_libraryAPI.Models;

public partial class HomeLibraryContext : DbContext
{
    public HomeLibraryContext()
    {
    }

    public HomeLibraryContext(DbContextOptions<HomeLibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Authority> Authorities { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Reading> Readings { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Translator> Translators { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=home_library;Username=postgres;Password=951753");
        //=> optionsBuilder.UseNpgsql("Host=localhost;Database=home_library;Username=postgres;Password=951753");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AUTHOR_pkey");

            entity.ToTable("AUTHOR");

            entity.HasIndex(e => new { e.AuthorName, e.AuthorSurname }, "AUTHOR_author_name_author_surname_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AuthorName)
                .HasMaxLength(50)
                .HasColumnName("author_name");
            entity.Property(e => e.AuthorSurname)
                .HasMaxLength(50)
                .HasColumnName("author_surname");
        });

        modelBuilder.Entity<Authority>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AUTHORITY_pkey");

            entity.ToTable("AUTHORITY");

            entity.HasIndex(e => e.Role, "AUTHORITY_role_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Role)
                .HasMaxLength(16)
                .HasColumnName("role");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BOOK_pkey");

            entity.ToTable("BOOK");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.BookTitle).HasColumnName("book_title");
            entity.Property(e => e.ImagePath).HasColumnName("image_path");
            entity.Property(e => e.PublisherId).HasColumnName("publisher_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BOOK_author_id");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublisherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BOOK_publisher_id");

            entity.HasOne(d => d.Status).WithMany(p => p.Books)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BOOK_status_id");

            entity.HasMany(d => d.Categories).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookCategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BOOK_CATEGORY_category_id"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BOOK_CATEGORY_book_id"),
                    j =>
                    {
                        j.HasKey("BookId", "CategoryId").HasName("BOOK_CATEGORY_pkey");
                        j.ToTable("BOOK_CATEGORY");
                        j.IndexerProperty<int>("BookId").HasColumnName("book_id");
                        j.IndexerProperty<int>("CategoryId").HasColumnName("category_id");
                    });

            entity.HasMany(d => d.Translators).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookTranslator",
                    r => r.HasOne<Translator>().WithMany()
                        .HasForeignKey("TranslatorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BOOK_TRANSLATOR_translator_id"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BOOK_TRANSLATOR_book_id"),
                    j =>
                    {
                        j.HasKey("BookId", "TranslatorId").HasName("BOOK_TRANSLATOR_pkey");
                        j.ToTable("BOOK_TRANSLATOR");
                        j.IndexerProperty<int>("BookId").HasColumnName("book_id");
                        j.IndexerProperty<int>("TranslatorId").HasColumnName("translator_id");
                    });
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CATEGORY_pkey");

            entity.ToTable("CATEGORY");

            entity.HasIndex(e => e.CategoryName, "CATEGORY_category_name_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EVENT_TYPE_pkey");

            entity.ToTable("EVENT_TYPE");

            entity.HasIndex(e => e.EventName, "EVENT_TYPE_event_name_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.EventName)
                .HasMaxLength(50)
                .HasColumnName("event_name");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LOG_pkey");

            entity.ToTable("LOG");

            entity.HasIndex(e => e.AuthorId, "fki_FK_LOG.author_id");

            entity.HasIndex(e => e.BookId, "fki_FK_LOG.book_id");

            entity.HasIndex(e => e.CategoryId, "fki_FK_LOG.category_id");

            entity.HasIndex(e => e.EventTypeId, "fki_FK_LOG.event_type_id");

            entity.HasIndex(e => e.PublisherId, "fki_FK_LOG.publisher_id");

            entity.HasIndex(e => e.ReadingId, "fki_FK_LOG.reading_id");

            entity.HasIndex(e => e.TranslatorId, "fki_FK_LOG.translator_id");

            entity.HasIndex(e => e.UserId, "fki_FK_LOG.user_id");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EventDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("event_date");
            entity.Property(e => e.EventTypeId).HasColumnName("event_type_id");
            entity.Property(e => e.PublisherId).HasColumnName("publisher_id");
            entity.Property(e => e.ReadingId).HasColumnName("reading_id");
            entity.Property(e => e.TranslatorId).HasColumnName("translator_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Author).WithMany(p => p.Logs)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_LOG_author_id");

            entity.HasOne(d => d.Book).WithMany(p => p.Logs)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK_LOG_book_id");

            entity.HasOne(d => d.Category).WithMany(p => p.Logs)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_LOG_category_id");

            entity.HasOne(d => d.EventType).WithMany(p => p.Logs)
                .HasForeignKey(d => d.EventTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LOG_event_type_id");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Logs)
                .HasForeignKey(d => d.PublisherId)
                .HasConstraintName("FK_LOG_publisher_id");

            entity.HasOne(d => d.Reading).WithMany(p => p.Logs)
                .HasForeignKey(d => d.ReadingId)
                .HasConstraintName("FK_LOG_reading_id");

            entity.HasOne(d => d.Translator).WithMany(p => p.Logs)
                .HasForeignKey(d => d.TranslatorId)
                .HasConstraintName("FK_LOG_translator_id");

            entity.HasOne(d => d.User).WithMany(p => p.Logs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_LOG_user_id");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PUBLISHER_pkey");

            entity.ToTable("PUBLISHER");

            entity.HasIndex(e => e.PublisherName, "PUBLISHER_publisher_name_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.PublisherName)
                .HasMaxLength(50)
                .HasColumnName("publisher_name");
        });

        modelBuilder.Entity<Reading>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("READING_pkey");

            entity.ToTable("READING");

            entity.HasIndex(e => e.BookId, "fki_FK_READING.book_id");

            entity.HasIndex(e => e.UserId, "fki_FK_READING.user_id");

            entity.HasIndex(e => e.UserId, "fki_sdfsdfsdfs");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Book).WithMany(p => p.Readings)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_READING_book_id");

            entity.HasOne(d => d.Status).WithMany(p => p.Readings)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_READING_status_id");

            entity.HasOne(d => d.User).WithMany(p => p.Readings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_READING_user_id");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("STATUS_pkey");

            entity.ToTable("STATUS");

            entity.HasIndex(e => e.StatusName, "STATUS_status_name_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<Translator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TRANSLATOR_pkey");

            entity.ToTable("TRANSLATOR");

            entity.HasIndex(e => new { e.TranslatorName, e.TranslatorSurname }, "TRANSLATOR_translator_name_translator_surname_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.TranslatorName)
                .HasMaxLength(30)
                .HasColumnName("translator_name");
            entity.Property(e => e.TranslatorSurname)
                .HasMaxLength(30)
                .HasColumnName("translator_surname");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("USER_pkey");

            entity.ToTable("USER");

            entity.HasIndex(e => e.Email, "USER_email_key").IsUnique();

            entity.HasIndex(e => e.UserName, "USER_user_name_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AuthorityId).HasColumnName("authority_id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(120)
                .HasColumnName("password");
            entity.Property(e => e.UserName)
                .HasMaxLength(16)
                .HasColumnName("user_name");

            entity.HasOne(d => d.Authority).WithMany(p => p.Users)
                .HasForeignKey(d => d.AuthorityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_authority_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
