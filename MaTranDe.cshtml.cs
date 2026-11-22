using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using QL_Thi_Trac_Nghiem.BUS;
using QL_Thi_Trac_Nghiem.DAO;

namespace QL_Thi_Trac_Nghiem.Pages.ExamConfig
{
    public class MaTranDeModel : PageModel
    {
        private readonly ExamConfigBUS _bus = new ExamConfigBUS();

        [BindProperty]
        public string MaMT { get; set; } = string.Empty;

        [BindProperty]
        public string MaMon { get; set; } = string.Empty;

        // Để nullable (null) để khi mở mới hoặc bấm "Tạo mới", 3 ô % sẽ trắng hoàn toàn
        [BindProperty]
        public double? TyLeCoBan { get; set; } = null;

        [BindProperty]
        public double? TyLeVanDung { get; set; } = null;

        [BindProperty]
        public double? TyLeVDNangCao { get; set; } = null;

        public List<SelectListItem> MonHocOptions { get; set; } = new List<SelectListItem>();
        public DataTable DanhSachMaTran { get; set; } = new DataTable();
        public string? StatusMessage { get; set; }
        public bool IsSuccess { get; set; }

        public void OnGet(string? editMaMT = null)
        {
            LoadDropdownAndGrid();

            if (!string.IsNullOrWhiteSpace(editMaMT))
            {
                foreach (DataRow row in DanhSachMaTran.Rows)
                {
                    if (string.Equals(row["Ma_MT"]?.ToString()?.Trim(), editMaMT.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        MaMT = row["Ma_MT"]?.ToString()?.Trim() ?? string.Empty;
                        MaMon = row["Ma_Mon"]?.ToString()?.Trim() ?? string.Empty;
                        TyLeCoBan = Convert.ToDouble(row["Ty_le_co_ban"] ?? 0);
                        TyLeVanDung = Convert.ToDouble(row["Ty_le_van_dung"] ?? 0);
                        TyLeVDNangCao = Convert.ToDouble(row["Ty_le_vd_nang_cao"] ?? 0);
                        break;
                    }
                }
            }
            else
            {
                // Khi mở mới hoặc bấm nút "Tạo mới" (/ExamConfig/MaTranDe), xóa trắng toàn bộ ô nhập
                MaMT = string.Empty;
                MaMon = string.Empty;
                TyLeCoBan = null;
                TyLeVanDung = null;
                TyLeVDNangCao = null;
            }
        }

        public IActionResult OnPostSave()
        {
            if (!TyLeCoBan.HasValue || !TyLeVanDung.HasValue || !TyLeVDNangCao.HasValue)
            {
                IsSuccess = false;
                StatusMessage = "Vui lòng nhập đầy đủ cả 3 tỷ lệ phần trăm độ khó.";
                LoadDropdownAndGrid();
                return Page();
            }

            var res = _bus.LuuMaTran(
                MaMT.Trim(),
                MaMon.Trim(),
                TyLeCoBan.Value,
                TyLeVanDung.Value,
                TyLeVDNangCao.Value);

            IsSuccess = res.ok;
            StatusMessage = res.msg;

            LoadDropdownAndGrid();
            return Page();
        }

        public IActionResult OnPostDelete(string maMT)
        {
            if (!string.IsNullOrWhiteSpace(maMT))
            {
                bool ok = _bus.XoaMaTran(maMT.Trim());
                IsSuccess = ok;
                StatusMessage = ok ? "Đã xóa ma trận." : "Xóa ma trận thất bại.";
            }

            LoadDropdownAndGrid();
            return Page();
        }

        private void LoadDropdownAndGrid()
        {
            MonHocOptions.Clear();
            DataTable dtMon = DataProvider.Instance.ExecuteQuery("SELECT Ma_Mon, Ten_mon FROM Mon");
            foreach (DataRow r in dtMon.Rows)
            {
                string id = r["Ma_Mon"]?.ToString()?.Trim() ?? "";
                string name = r["Ten_mon"]?.ToString()?.Trim() ?? id;
                MonHocOptions.Add(new SelectListItem { Value = id, Text = $"{id} - {name}" });
            }

            DanhSachMaTran = _bus.LayDanhSachMaTran();
        }
    }
}