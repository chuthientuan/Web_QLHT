using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTL.Models;

public partial class ChiTietHdb
{
    [Key]
    public int MaHdb { get; set; }
    public int MaSp { get; set; }
    public int? Slban { get; set; }

    public virtual HoaDonBan? MaHdbNavigation { get; set; } = null!;

    public virtual SanPham? MaSpNavigation { get; set; } = null!;
}
