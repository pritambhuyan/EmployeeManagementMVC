using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Models;

public partial class EmployeeDbContext : DbContext
{
    public EmployeeDbContext()
    {

    }
    public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
    {

    }

    public  DbSet<EmployeeMaster> EmployeeMasters { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("server=DESKTOP-11ETKM3\\MSSQLSERVER01; Database=EmployeeDB; Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeMaster>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F11044FD0B1");

            entity.ToTable("EmployeeMaster");

            entity.HasIndex(e => e.EmployeeCode, "UQ__Employee__1F6425485717CD25").IsUnique();

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Department)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Designation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EmployeeCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
