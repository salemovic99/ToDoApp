using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TODOAPP.Models;

namespace TODOAPP.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<TaskTable> TaskTables { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server = DESKTOP-S9CO9EB\\SQLEXPRESS; Database = TODODB; Trusted_Connection = True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StatusName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("StatusNAME");
            });

            modelBuilder.Entity<TaskTable>(entity =>
            {
                entity.HasKey(e => e.TaskId)
                    .HasName("PK__Task_tab__DD5D5A42F9BAAC2F");

                entity.ToTable("Task_table");

                entity.Property(e => e.TaskId).HasColumnName("taskId");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.CreatedAtdate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAtdate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Priority).HasColumnName("priority");

                entity.Property(e => e.TaskName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("taskName");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TaskTables)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Task_tabl__categ__66603565");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TaskTables)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Task_tabl__Statu__6754599E");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
