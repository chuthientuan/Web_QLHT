using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTL.Models;

public partial class NhaCungCap
{
    [Key]
    public int MaNcc { get; set; }

    public string? TenNcc { get; set; }

    public virtual ICollection<HoaDonNhap> HoaDonNhaps { get; set; } = new List<HoaDonNhap>();
}
