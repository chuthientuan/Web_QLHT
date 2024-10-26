using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class ChiTietHdb
{
    public int MaHdb { get; set; }

    public int MaSp { get; set; }

    public int? Slban { get; set; }

    public string? KhuyenMai { get; set; }

    public virtual HoaDonBan MaHdbNavigation { get; set; } = null!;

    public virtual SanPham MaSpNavigation { get; set; } = null!;
}
