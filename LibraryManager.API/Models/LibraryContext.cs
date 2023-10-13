using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LibraryManager.API.Models
{
    public partial class LibraryContext : DbContext
    {
        public LibraryContext()
        {
        }

        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<TransactionStatus> TransactionStatuses { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=DESKTOP-QC0CUU6;Initial Catalog=Library;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");

                entity.Property(e => e.Author).HasMaxLength(50);

                entity.Property(e => e.Category).HasMaxLength(50);

                entity.Property(e => e.Isbn).HasColumnName("ISBN");

                entity.Property(e => e.Price).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Publisher).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.TxnId);

                entity.ToTable("Transaction");

                entity.Property(e => e.CheckoutDate).HasColumnType("datetime");

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.LastRenewedDate).HasColumnType("datetime");

               // entity.Property(e => e.RenewalCount).HasColumnName("RenewalCount");

                entity.Property(e => e.ReturnedDate).HasColumnType("datetime");

                entity.Property(e => e.StatusDate).HasColumnType("datetime");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Transaction_Book");

                entity.HasOne(d => d.TxnStatus)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.TxnStatusId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Transaction_TransactionStatus");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Transaction_User");
            });

            modelBuilder.Entity<TransactionStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("TransactionStatus");

                entity.Property(e => e.StatusDesc).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.PhoneNo).HasMaxLength(12);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
