using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class HoaDonBan
{
    public int MaHdb { get; set; }

    public DateTime? NgayBan { get; set; }

    public int? MaTk { get; set; }

    public virtual ICollection<ChiTietHdb> ChiTietHdbs { get; set; } = new List<ChiTietHdb>();

    public virtual TaiKhoan? MaTkNavigation { get; set; }
}
