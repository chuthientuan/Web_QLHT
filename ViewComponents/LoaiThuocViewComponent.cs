using BTL.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTL.ViewComponents
{
    public class LoaiThuocViewComponent : ViewComponent
    {
        QlhieuThuocContext db;
        List<LoaiThuoc> loaiThuocs;
        public LoaiThuocViewComponent(QlhieuThuocContext db)
        {
            this.db = db;
            loaiThuocs = db.LoaiThuocs.ToList();
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("RenderLoaiThuoc", loaiThuocs);
        }
    }
}
