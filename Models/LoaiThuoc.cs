using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTL.Models;

public partial class LoaiThuoc
{
    [Key]
    public int MaLt { get; set; }

    public string? TenLt { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
