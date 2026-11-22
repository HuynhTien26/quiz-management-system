using System.Data;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QL_Thi_Trac_Nghiem.BUS;

namespace QL_Thi_Trac_Nghiem.Pages.ExamConfig
{
    public class QuanLyDeThiModel : PageModel
    {
        private readonly ExamConfigBUS _bus = new ExamConfigBUS();

        public DataTable DanhSachDeThi { get; set; } = new DataTable();
        public DataTable ChiTietDeThi { get; set; } = new DataTable();
        public string? SelectedMaDe { get; set; }

        public void OnGet(string? maDe = null)
        {
            DanhSachDeThi = _bus.LayDanhSachDeThi();

            if (!string.IsNullOrWhiteSpace(maDe))
            {
                SelectedMaDe = maDe.Trim();
            }
            else if (DanhSachDeThi.Rows.Count > 0)
            {
                SelectedMaDe = DanhSachDeThi.Rows[0]["Ma_De"]?.ToString()?.Trim();
            }

            if (!string.IsNullOrWhiteSpace(SelectedMaDe))
            {
                ChiTietDeThi = _bus.LayChiTietDeThi(SelectedMaDe);
            }
        }

        public IActionResult OnGetInDe(string maDe)
        {
            if (string.IsNullOrWhiteSpace(maDe))
            {
                return RedirectToPage();
            }

            string cleanMaDe = maDe.Trim();
            string tempPath = Path.Combine(Path.GetTempPath(), $"{cleanMaDe}.txt");
            string content = _bus.XuatNoiDungInDeThi(cleanMaDe, tempPath);

            byte[] fileBytes = Encoding.UTF8.GetBytes(content);
            return File(fileBytes, "text/plain; charset=utf-8", $"{cleanMaDe}.txt");
        }
    }
}
