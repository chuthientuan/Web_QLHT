using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTL.Models;

public partial class SanPham
{
    [Key]
    public int MaSp { get; set; }

    public int MaLt { get; set; }

    public string? TenSp { get; set; }

    public decimal DonGiaNhap { get; set; }

    public string? MoTa { get; set; }

    public decimal DonGiaBan { get; set; }

    public int? SoLuong { get; set; }

    public byte[]? Anh { get; set; }

    public DateTime? Hsd { get; set; }

    public virtual ICollection<ChiTietHdb>? ChiTietHdbs { get; set; } = new List<ChiTietHdb>();

    public virtual ICollection<ChiTietHdn>? ChiTietHdns { get; set; } = new List<ChiTietHdn>();

    public virtual LoaiThuoc? MaLtNavigation { get; set; } = null!;
}
