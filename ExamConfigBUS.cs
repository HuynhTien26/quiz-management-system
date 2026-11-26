using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using QL_Thi_Trac_Nghiem.DAO;

namespace QL_Thi_Trac_Nghiem.BUS
{
    public class ExamConfigBUS
    {
        private readonly ExamConfigDAO _dao = new ExamConfigDAO();

        // ==================== 1. NGHIỆP VỤ KỲ THI ====================
        public DataTable LayDanhSachKyThi() => _dao.GetAllKyThi();

        public (bool ok, string msg) LuuKyThi(string maKT, string ten, DateTime ngayThi, bool isEdit, string? namHoc = null)
        {
            if (string.IsNullOrWhiteSpace(maKT) || string.IsNullOrWhiteSpace(ten))
                return (false, "Vui lòng nhập đầy đủ mã và tên kỳ thi.");

            if (_dao.IsDuplicateNgayThi(ngayThi, isEdit ? maKT : null))
                return (false, "Đã có kỳ thi diễn ra vào ngày này.");

            bool saved = isEdit
                ? _dao.UpdateKyThi(maKT, ten, ngayThi, namHoc)
                : _dao.InsertKyThi(maKT, ten, ngayThi, namHoc);

            return (saved, saved ? "Lưu kỳ thi thành công." : "Không thể lưu kỳ thi.");
        }

        public (bool ok, string msg) XoaKyThiAnToan(string maKT)
        {
            if (_dao.HasStudentsInKyThi(maKT))
                return (false, "Không thể xóa kỳ thi vì đã có thí sinh tham gia/làm bài.");
            bool ok = _dao.DeleteKyThi(maKT);
            return (ok, ok ? "Đã xóa kỳ thi." : "Xóa kỳ thi thất bại.");
        }

        // ==================== 2. NGHIỆP VỤ MA TRẬN ĐỀ ====================
        public DataTable LayDanhSachMon() => _dao.GetAllMon();
        public DataTable LayDanhSachCauHoi() => _dao.GetAllCauHoi();
        public DataTable LayDanhSachMaTran() => _dao.GetAllMaTran();

        public (bool ok, string msg) LuuMaTran(string maMT, string maMon, double coBan, double vanDung, double vdNangCao)
        {
            if (string.IsNullOrWhiteSpace(maMT) || string.IsNullOrWhiteSpace(maMon))
                return (false, "Vui lòng nhập mã ma trận và chọn môn học.");

            double tongTyLe = coBan + vanDung + vdNangCao;
            if (Math.Abs(tongTyLe - 100.0) > 0.001)
                return (false, $"Tổng tỷ lệ độ khó phải đúng 100% (hiện tại: {tongTyLe}%).");

            bool ok = _dao.SaveMaTran(maMT, maMon, coBan, vanDung, vdNangCao);
            return (ok, ok ? "Lưu ma trận đề thành công." : "Lưu ma trận đề thất bại.");
        }

        public bool XoaMaTran(string maMT) => _dao.DeleteMaTran(maMT);

        // ==================== 3. NGHIỆP VỤ SINH ĐỀ & QUẢN LÝ ĐỀ THI ====================
        public (bool ok, string msg) SinhVaLuuDeThi(
            string maDe, string maKT, string maMT, string maMon, string maGV,
            int soLuongCH, int thoiGianLamBai,
            double tyLeCoBan, double tyLeVanDung, double tyLeVDNangCao,
            List<string>? danhSachChonTay = null)
        {
            if (string.IsNullOrWhiteSpace(maDe))
                return (false, "Mã đề không được để trống.");

            if (soLuongCH <= 0 || thoiGianLamBai <= 0)
                return (false, "Số lượng câu hỏi và thời gian làm bài phải lớn hơn 0.");

            if (thoiGianLamBai < soLuongCH * 0.5)
                return (false, "Thời gian làm bài quá ngắn so với số lượng câu hỏi.");

            List<string> dsCauHoi;
            if (danhSachChonTay != null && danhSachChonTay.Count > 0)
            {
                dsCauHoi = danhSachChonTay.Distinct().ToList();
                if (dsCauHoi.Count != soLuongCH)
                    return (false, $"Số câu hỏi đã chọn ({dsCauHoi.Count}) không khớp số lượng đề ({soLuongCH}).");
            }
            else
            {
                int slCB = (int)Math.Round(soLuongCH * tyLeCoBan / 100.0);
                int slVD = (int)Math.Round(soLuongCH * tyLeVanDung / 100.0);
                int slVDNC = Math.Max(0, soLuongCH - slCB - slVD);

                var cbIds = _dao.GetRandomQuestionIds(maMon, "Cơ bản", slCB);
                var vdIds = _dao.GetRandomQuestionIds(maMon, "Vận dụng", slVD);
                var vdncIds = _dao.GetRandomQuestionIds(maMon, "Nâng cao", slVDNC);

                if (cbIds.Count < slCB || vdIds.Count < slVD || vdncIds.Count < slVDNC)
                    return (false, "Ngân hàng câu hỏi không đủ số lượng theo tỷ lệ ma trận đã chọn.");

                dsCauHoi = cbIds.Union(vdIds).Union(vdncIds).ToList();
            }

            bool ok = _dao.InsertDeThiWithDetails(maDe.Trim(), maKT, maMT, maMon, maGV, soLuongCH, thoiGianLamBai, dsCauHoi);
            return (ok, ok ? "Sinh và lưu đề thi thành công." : "Lưu đề thi thất bại.");
        }

        public DataTable LayDanhSachDeThi() => _dao.GetAllDeThi();

        public DataTable LayChiTietDeThi(string maDe) => _dao.GetChiTietDeThi(maDe);

        public string XuatNoiDungInDeThi(string maDe, string outputPath)
        {
            DataTable dt = _dao.GetChiTietDeThi(maDe);
            var sb = new StringBuilder();
            sb.AppendLine($"=== ĐỀ THI: {maDe} ===");
            int stt = 1;
            foreach (DataRow r in dt.Rows)
                sb.AppendLine($"Câu {stt++} [{r["Muc_Do"]}]: {r["Noi_Dung"]}");
            string content = sb.ToString();
            File.WriteAllText(outputPath, content, Encoding.UTF8);
            return content;
        }
    }
}
