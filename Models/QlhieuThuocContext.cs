using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BTL.Models;

public partial class QlhieuThuocContext : DbContext
{
    public QlhieuThuocContext()
    {
    }

    public QlhieuThuocContext(DbContextOptions<QlhieuThuocContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietHdb> ChiTietHdbs { get; set; }

    public virtual DbSet<ChiTietHdn> ChiTietHdns { get; set; }

    public virtual DbSet<HoaDonBan> HoaDonBans { get; set; }

    public virtual DbSet<HoaDonNhap> HoaDonNhaps { get; set; }

    public virtual DbSet<LoaiThuoc> LoaiThuocs { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("QlhieuThuocContext"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietHdb>(entity =>
        {
            entity.HasKey(e => new { e.MaHdb, e.MaSp });

            entity.ToTable("ChiTietHDB");

            entity.Property(e => e.MaHdb)
                .ValueGeneratedOnAdd()
                .HasColumnName("MaHDB");
            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.KhuyenMai).HasMaxLength(100);
            entity.Property(e => e.Slban).HasColumnName("SLBan");

            entity.HasOne(d => d.MaHdbNavigation).WithMany(p => p.ChiTietHdbs)
                .HasForeignKey(d => d.MaHdb)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietHDB_HoaDonBan");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.ChiTietHdbs)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietHDB_SanPham");
        });

        modelBuilder.Entity<ChiTietHdn>(entity =>
        {
            entity.HasKey(e => new { e.MaHdn, e.MaSp });

            entity.ToTable("ChiTietHDN");

            entity.Property(e => e.MaHdn)
                .ValueGeneratedOnAdd()
                .HasColumnName("MaHDN");
            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.KhuyenMai).HasMaxLength(100);
            entity.Property(e => e.Slnhap).HasColumnName("SLNhap");

            entity.HasOne(d => d.MaHdnNavigation).WithMany(p => p.ChiTietHdns)
                .HasForeignKey(d => d.MaHdn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietHDN_HoaDonNhap");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.ChiTietHdns)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietHDN_SanPham");
        });

        modelBuilder.Entity<HoaDonBan>(entity =>
        {
            entity.HasKey(e => e.MaHdb);

            entity.ToTable("HoaDonBan");

            entity.Property(e => e.MaHdb).HasColumnName("MaHDB");
            entity.Property(e => e.MaTk).HasColumnName("MaTK");
            entity.Property(e => e.NgayBan).HasColumnType("datetime");

            entity.HasOne(d => d.MaTkNavigation).WithMany(p => p.HoaDonBans)
                .HasForeignKey(d => d.MaTk)
                .HasConstraintName("FK_HoaDonBan_TaiKhoan");
        });

        modelBuilder.Entity<HoaDonNhap>(entity =>
        {
            entity.HasKey(e => e.MaHdn);

            entity.ToTable("HoaDonNhap");

            entity.Property(e => e.MaHdn).HasColumnName("MaHDN");
            entity.Property(e => e.MaNcc).HasColumnName("MaNCC");
            entity.Property(e => e.NgayNhap).HasColumnType("datetime");

            entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.HoaDonNhaps)
                .HasForeignKey(d => d.MaNcc)
                .HasConstraintName("FK_HoaDonNhap_NhaCungCap");
        });

        modelBuilder.Entity<LoaiThuoc>(entity =>
        {
            entity.HasKey(e => e.MaLt);

            entity.ToTable("LoaiThuoc");

            entity.Property(e => e.MaLt).HasColumnName("MaLT");
            entity.Property(e => e.TenLt)
                .HasMaxLength(200)
                .HasColumnName("TenLT");
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.MaNcc);

            entity.ToTable("NhaCungCap");

            entity.Property(e => e.MaNcc).HasColumnName("MaNCC");
            entity.Property(e => e.TenNcc)
                .HasMaxLength(200)
                .HasColumnName("TenNCC");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.MaSp);

            entity.ToTable("SanPham");

            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.Anh).HasColumnType("image");
            entity.Property(e => e.DonGiaBan).HasColumnType("money");
            entity.Property(e => e.DonGiaNhap).HasColumnType("money");
            entity.Property(e => e.Hsd)
                .HasColumnType("datetime")
                .HasColumnName("HSD");
            entity.Property(e => e.MaLt).HasColumnName("MaLT");
            entity.Property(e => e.MoTa).HasMaxLength(2000);
            entity.Property(e => e.TenSp)
                .HasMaxLength(200)
                .HasColumnName("TenSP");

            entity.HasOne(d => d.MaLtNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaLt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SanPham_LoaiThuoc");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.MaTk);

            entity.ToTable("TaiKhoan");

            entity.Property(e => e.MaTk).HasColumnName("MaTK");
            entity.Property(e => e.DienThoai).HasMaxLength(15);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.MatKhau).HasMaxLength(20);
            entity.Property(e => e.TenDangNhap).HasMaxLength(30);
            entity.Property(e => e.TrangThai).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
