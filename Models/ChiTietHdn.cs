using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class ChiTietHdn
{
    public int MaHdn { get; set; }

    public int MaSp { get; set; }

    public int? Slnhap { get; set; }

    public string? KhuyenMai { get; set; }

    public virtual HoaDonNhap MaHdnNavigation { get; set; } = null!;

    public virtual SanPham MaSpNavigation { get; set; } = null!;
}
