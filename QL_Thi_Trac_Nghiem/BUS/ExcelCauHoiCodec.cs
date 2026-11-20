using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using ClosedXML.Excel;
using QL_Thi_Trac_Nghiem.DTO;

namespace QL_Thi_Trac_Nghiem.BUS
{
    /// <summary>
    /// Đọc file Excel danh sách câu hỏi. Không truy cập CSDL, không dùng UI.
    /// </summary>
    public static class ExcelCauHoiCodec
    {
        private const string CotMaCH = "Mã CH";
        private const string CotMaMon = "Mã môn";
        private const string CotNoiDung = "Nội dung";
        private const string CotDapAnA = "Đáp án A";
        private const string CotDapAnB = "Đáp án B";
        private const string CotDapAnC = "Đáp án C";
        private const string CotDapAnD = "Đáp án D";
        private const string CotDapAnDung = "Đáp án đúng";
        private const string CotMucDo = "Mức độ";

        private static readonly string[] TieuDeCot =
        {
            CotMaCH, CotMaMon, CotNoiDung, CotDapAnA, CotDapAnB, CotDapAnC, CotDapAnD, CotDapAnDung, CotMucDo
        };

        // Tên cột chấp nhận cho mỗi cột, so sau khi đã chuẩn hóa (bỏ dấu, bỏ khoảng trắng, viết hoa).
        // Ví dụ cột môn có thể ghi "Mã môn", "Môn", "Tên môn", "Môn học".
        private static readonly Dictionary<string, string[]> BieuThucCot = new Dictionary<string, string[]>
        {
            { CotMaCH, new[] { "Mã CH", "Mã câu hỏi", "MaCH" } },
            { CotMaMon, new[] { "Mã môn", "Môn", "Tên môn", "Môn học", "Mã môn học" } },
            { CotNoiDung, new[] { "Nội dung", "Nội dung câu hỏi", "Câu hỏi" } },
            { CotDapAnA, new[] { "Đáp án A", "Đáp án a" } },
            { CotDapAnB, new[] { "Đáp án B", "Đáp án b" } },
            { CotDapAnC, new[] { "Đáp án C", "Đáp án c" } },
            { CotDapAnD, new[] { "Đáp án D", "Đáp án d" } },
            { CotDapAnDung, new[] { "Đáp án đúng", "Đáp án đúng" } },
            { CotMucDo, new[] { "Mức độ", "Độ khó", "Mức độ khó" } }
        };

        // ================= ĐỌC FILE =================
        // Trả về cặp (Dong: số dòng trong Excel, CauHoi) để báo cáo lỗi khớp với file người dùng thấy
        public static List<(int Dong, CauHoiDTO CauHoi)> DocFile(string duongDan, out string thongBao)
        {
            List<(int Dong, CauHoiDTO CauHoi)> ketQua = new List<(int Dong, CauHoiDTO CauHoi)>();

            if (string.IsNullOrWhiteSpace(duongDan))
            {
                thongBao = "Chưa chọn file cần nhập!";
                return ketQua;
            }

            if (!File.Exists(duongDan))
            {
                thongBao = "Không tìm thấy file đã chọn!";
                return ketQua;
            }

            try
            {
                using (XLWorkbook workbook = new XLWorkbook(duongDan))
                {
                    IXLWorksheet? worksheet = workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        thongBao = "File Excel không có sheet dữ liệu nào!";
                        return ketQua;
                    }

                    int dongTieuDe = TimDongTieuDe(worksheet);
                    if (dongTieuDe <= 0)
                    {
                        thongBao = "Không nhận diện được dòng tiêu đề cột. Cần có các cột: "
                                   + string.Join(", ", TieuDeCot) + "!";
                        return ketQua;
                    }

                    Dictionary<string, int> viTriCot = DocViTriCot(worksheet, dongTieuDe);
                    List<string> cotThieu = new List<string>();
                    foreach (string ten in TieuDeCot.Take(8))
                    {
                        if (!viTriCot.ContainsKey(ten)) cotThieu.Add(ten);
                    }

                    if (cotThieu.Count > 0)
                    {
                        thongBao = "File thiếu cột: " + string.Join(", ", cotThieu) + "!";
                        return ketQua;
                    }

                    int dongCuoi = worksheet.LastRowUsed()?.RowNumber() ?? dongTieuDe;
                    for (int i = dongTieuDe + 1; i <= dongCuoi; i++)
                    {
                        CauHoiDTO? cauHoi = DocDong(worksheet, i, viTriCot);
                        if (cauHoi == null) continue;
                        ketQua.Add((i, cauHoi));
                    }
                }

                if (ketQua.Count == 0)
                {
                    thongBao = "File không có dòng dữ liệu câu hỏi nào!";
                    return ketQua;
                }

                thongBao = "";
                return ketQua;
            }
            catch (Exception ex)
            {
                thongBao = "Không đọc được file Excel: " + ex.Message;
                return new List<(int Dong, CauHoiDTO CauHoi)>();
            }
        }

        // Chọn dòng có nhiều cột nhận diện được nhất trong 5 dòng đầu (không đòi đủ cột,
        // để khi thiếu cột vẫn báo được chính xác cột nào thiếu)
        private static int TimDongTieuDe(IXLWorksheet worksheet)
        {
            int dongCuoi = Math.Min(5, worksheet.LastRowUsed()?.RowNumber() ?? 0);
            int dongTotNhat = 0;
            int soCotTotNhat = 0;

            for (int i = 1; i <= dongCuoi; i++)
            {
                Dictionary<string, int> viTri = DocViTriCot(worksheet, i);
                int soCotNhanDien = viTri.Keys.Count(ten => TieuDeCot.Contains(ten));
                if (soCotNhanDien > soCotTotNhat)
                {
                    soCotTotNhat = soCotNhanDien;
                    dongTotNhat = i;
                }
            }

            return dongTotNhat;
        }

        private static Dictionary<string, int> DocViTriCot(IXLWorksheet worksheet, int dongTieuDe)
        {
            Dictionary<string, int> viTri = new Dictionary<string, int>();
            int cotCuoi = worksheet.LastColumnUsed()?.ColumnNumber() ?? 0;

            for (int i = 1; i <= cotCuoi; i++)
            {
                string tieuDe = ChuanHoaTieuDe(worksheet.Cell(dongTieuDe, i).GetString());
                if (string.IsNullOrEmpty(tieuDe)) continue;

                foreach (KeyValuePair<string, string[]> cot in BieuThucCot)
                {
                    if (viTri.ContainsKey(cot.Key)) continue;

                    if (cot.Value.Any(ten => ChuanHoaTieuDe(ten) == tieuDe))
                    {
                        viTri[cot.Key] = i;
                        break;
                    }
                }
            }
            return viTri;
        }

        private static CauHoiDTO? DocDong(IXLWorksheet worksheet, int dong, Dictionary<string, int> viTriCot)
        {
            Dictionary<string, string> giaTri = new Dictionary<string, string>();
            foreach (KeyValuePair<string, int> kv in viTriCot)
            {
                giaTri[kv.Key] = worksheet.Cell(dong, kv.Value).GetString().Trim();
            }

            bool rongHoanToan = giaTri.Values.All(g => string.IsNullOrWhiteSpace(g));
            if (rongHoanToan) return null;

            return new CauHoiDTO
            {
                MaCH = giaTri.TryGetValue(CotMaCH, out string? maCH) ? maCH : "",
                MaMon = giaTri.TryGetValue(CotMaMon, out string? maMon) ? maMon : "",
                NoiDung = giaTri.TryGetValue(CotNoiDung, out string? noiDung) ? noiDung : "",
                DapAnA = giaTri.TryGetValue(CotDapAnA, out string? a) ? a : "",
                DapAnB = giaTri.TryGetValue(CotDapAnB, out string? b) ? b : "",
                DapAnC = giaTri.TryGetValue(CotDapAnC, out string? c) ? c : "",
                DapAnD = giaTri.TryGetValue(CotDapAnD, out string? d) ? d : "",
                DapAnDung = giaTri.TryGetValue(CotDapAnDung, out string? dung) ? dung : "",
                MucDo = giaTri.TryGetValue(CotMucDo, out string? mucDo) ? mucDo : ""
            };
        }

        // Bỏ dấu tiếng Việt, bỏ khoảng trắng, viết hoa: "Đáp án A" -> "DAPANA"
        private static string ChuanHoaTieuDe(string tieuDe)
        {
            if (string.IsNullOrWhiteSpace(tieuDe)) return "";

            string khongDau = tieuDe.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder(khongDau.Length);
            foreach (char ch in khongDau)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(ch);
                }
            }

            string s = sb.ToString().Normalize(NormalizationForm.FormC);
            s = s.Replace("đ", "d").Replace("Đ", "D");
            return s.Where(c => !char.IsWhiteSpace(c)).Aggregate(new StringBuilder(), (acc, c) => acc.Append(c))
                    .ToString().ToUpperInvariant();
        }
    }
}
