using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTL.Models;

public partial class HoaDonNhap
{
    [Key]
    public int MaHdn { get; set; }

    public DateTime? NgayNhap { get; set; }

    public int? MaNcc { get; set; }

    public virtual ICollection<ChiTietHdn> ChiTietHdns { get; set; } = new List<ChiTietHdn>();

    public virtual NhaCungCap? MaNccNavigation { get; set; }
}
