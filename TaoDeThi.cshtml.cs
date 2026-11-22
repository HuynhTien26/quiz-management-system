using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using QL_Thi_Trac_Nghiem.BUS;
using QL_Thi_Trac_Nghiem.DAO;

namespace QL_Thi_Trac_Nghiem.Pages.ExamConfig
{
    public class TaoDeThiModel : PageModel
    {
        private readonly ExamConfigBUS _bus = new ExamConfigBUS();

        [BindProperty] public string MaDe { get; set; } = string.Empty;
        [BindProperty] public string MaKT { get; set; } = string.Empty;
        [BindProperty] public string MaMT { get; set; } = string.Empty;
        [BindProperty] public string MaMon { get; set; } = string.Empty;
        [BindProperty] public string MaGV { get; set; } = string.Empty;
        [BindProperty] public int SoLuongCH { get; set; } = 20;
        [BindProperty] public int ThoiGianLamBai { get; set; } = 30;
        [BindProperty] public double TyLeCoBan { get; set; } = 50;
        [BindProperty] public double TyLeVanDung { get; set; } = 30;
        [BindProperty] public double TyLeVDNangCao { get; set; } = 20;
        [BindProperty] public List<string> DanhSachChonTay { get; set; } = new List<string>();

        public List<SelectListItem> KyThiOptions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> MaTranOptions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> MonHocOptions { get; set; } = new List<SelectListItem>();
        public DataTable DanhSachCauHoi { get; set; } = new DataTable();
        public string? StatusMessage { get; set; }
        public bool IsSuccess { get; set; }

        public void OnGet()
        {
            LoadOptionsAndQuestions();
        }

        public IActionResult OnPostSinhDe()
        {
            var res = _bus.SinhVaLuuDeThi(
                MaDe.Trim(),
                MaKT.Trim(),
                MaMT.Trim(),
                MaMon.Trim(),
                MaGV.Trim(),
                SoLuongCH,
                ThoiGianLamBai,
                TyLeCoBan,
                TyLeVanDung,
                TyLeVDNangCao,
                DanhSachChonTay.Count > 0 ? DanhSachChonTay : null);

            IsSuccess = res.ok;
            StatusMessage = res.msg;

            LoadOptionsAndQuestions();
            return Page();
        }

        private void LoadOptionsAndQuestions()
        {
            KyThiOptions.Clear();
            foreach (DataRow r in _bus.LayDanhSachKyThi().Rows)
            {
                string id = r["Ma_KT"]?.ToString()?.Trim() ?? "";
                string ten = r["Ten"]?.ToString()?.Trim() ?? id;
                KyThiOptions.Add(new SelectListItem { Value = id, Text = $"{id} - {ten}" });
            }

            MaTranOptions.Clear();
            foreach (DataRow r in _bus.LayDanhSachMaTran().Rows)
            {
                string id = r["Ma_MT"]?.ToString()?.Trim() ?? "";
                MaTranOptions.Add(new SelectListItem { Value = id, Text = id });
            }

            MonHocOptions.Clear();
            foreach (DataRow r in _bus.LayDanhSachMon().Rows)
            {
                string id = r["Ma_Mon"]?.ToString()?.Trim() ?? "";
                string ten = r["Ten_mon"]?.ToString()?.Trim() ?? id;
                MonHocOptions.Add(new SelectListItem { Value = id, Text = $"{id} - {ten}" });
            }

            DanhSachCauHoi = DataProvider.Instance.ExecuteQuery(
                "SELECT Ma_CH, Ma_Mon, Muc_do, Noi_dung FROM CAU_HOI ORDER BY Ma_CH");
        }
    }
}