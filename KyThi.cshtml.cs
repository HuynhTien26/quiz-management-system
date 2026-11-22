using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QL_Thi_Trac_Nghiem.BUS;

namespace QL_Thi_Trac_Nghiem.Pages.ExamConfig
{
    public class KyThiModel : PageModel
    {
        private readonly ExamConfigBUS _bus = new ExamConfigBUS();

        [BindProperty] public string MaKT { get; set; } = string.Empty;
        [BindProperty] public string Ten { get; set; } = string.Empty;
        [BindProperty] public DateTime NgayThi { get; set; } = DateTime.Today;
        [BindProperty] public bool IsEdit { get; set; } = false;

        public DataTable DanhSachKyThi { get; set; } = new DataTable();
        public string? StatusMessage { get; set; }
        public bool IsSuccess { get; set; }

        public void OnGet(string? editMaKT = null)
        {
            LoadData();

            if (!string.IsNullOrWhiteSpace(editMaKT))
            {
                foreach (DataRow row in DanhSachKyThi.Rows)
                {
                    if (string.Equals(row["Ma_KT"]?.ToString()?.Trim(), editMaKT.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        MaKT = row["Ma_KT"]?.ToString()?.Trim() ?? string.Empty;
                        Ten = row["Ten"]?.ToString()?.Trim() ?? string.Empty;
                        if (DateTime.TryParse(row["Ngay_thi"]?.ToString(), out DateTime d))
                        {
                            NgayThi = d;
                        }
                        IsEdit = true;
                        break;
                    }
                }
            }
        }

        public IActionResult OnPostSave()
        {
            var res = _bus.LuuKyThi(MaKT.Trim(), Ten.Trim(), NgayThi.Date, IsEdit);
            IsSuccess = res.ok;
            StatusMessage = res.msg;

            if (res.ok)
            {
                MaKT = string.Empty;
                Ten = string.Empty;
                NgayThi = DateTime.Today;
                IsEdit = false;
            }

            LoadData();
            return Page();
        }

        public IActionResult OnPostDelete(string maKT)
        {
            if (!string.IsNullOrWhiteSpace(maKT))
            {
                var res = _bus.XoaKyThiAnToan(maKT.Trim());
                IsSuccess = res.ok;
                StatusMessage = res.msg;
            }

            LoadData();
            return Page();
        }

        private void LoadData()
        {
            DanhSachKyThi = _bus.LayDanhSachKyThi();
        }
    }
}
