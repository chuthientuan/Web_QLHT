using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTL.Models;

public partial class HoaDonBan
{
    [Key]
    public int MaHdb { get; set; }

    public DateTime? NgayBan { get; set; }
    public string TrangThai { get; set; } = null!;

    public int MaTk { get; set; }

    public virtual ICollection<ChiTietHdb> ChiTietHdbs { get; set; } = new List<ChiTietHdb>();

    public virtual TaiKhoan? MaTkNavigation { get; set; }
}
