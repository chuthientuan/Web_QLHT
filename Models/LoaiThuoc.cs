using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class LoaiThuoc
{
    public int MaLt { get; set; }

    public string? TenLt { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
