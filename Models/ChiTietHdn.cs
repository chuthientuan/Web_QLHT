using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTL.Models;

public partial class ChiTietHdn
{
    [Key]
    public int MaHdn { get; set; }
    public int MaSp { get; set; }

    public int? Slnhap { get; set; }

    public virtual HoaDonNhap? MaHdnNavigation { get; set; } = null!;

    public virtual SanPham? MaSpNavigation { get; set; } = null!;
}
