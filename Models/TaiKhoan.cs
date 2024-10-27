using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class TaiKhoan
{
    public int MaTk { get; set; }

    public string HoTen { get; set; } = null!;

    public int Role { get; set; }

    public string TenDangNhap { get; set; } = null!;

    public string TrangThai { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string? Email { get; set; }

    public string? DienThoai { get; set; }

    public virtual ICollection<HoaDonBan> HoaDonBans { get; set; } = new List<HoaDonBan>();
}
