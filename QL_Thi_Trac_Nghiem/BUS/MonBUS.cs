using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using QL_Thi_Trac_Nghiem.DAO;
using QL_Thi_Trac_Nghiem.DTO;

namespace QL_Thi_Trac_Nghiem.BUS
{
    public class MonBUS
    {
        private const int MaMonToiDa = 20;
        private const int TenMonToiDa = 100;
        private const int MaKhoaToiDa = 20;

        private readonly MonDAO monDAO = new MonDAO();

        // Bộ nhớ đệm ArrayList phục vụ yêu cầu tìm kiếm trên ArrayList trong tầng BUS
        public static ArrayList arrMon = new ArrayList();

        // ================= ĐỌC DỮ LIỆU =================
        public List<MonDTO> DocDSMon()
        {
            try
            {
                List<MonDTO> list = monDAO.GetAllMon();
                arrMon = new ArrayList(list); // Đồng bộ vào bộ nhớ đệm
                return list;
            }
            catch (Exception)
            {
                return new List<MonDTO>(); // Mất kết nối khi đọc thì trả danh sách rỗng
            }
        }

        // ================= KIỂM TRA TRÙNG MÃ =================
        public bool KiemTraTrungMaMon(string maMon)
        {
            if (string.IsNullOrWhiteSpace(maMon)) return false;

            try
            {
                return monDAO.KiemTraTrungMaMon(maMon.Trim());
            }
            catch (Exception)
            {
                return false;
            }
        }

        // ================= NGHIỆP VỤ THÊM =================
        public bool ThemMon(MonDTO m, out string thongBao)
        {
            if (!ChuanHoaVaKiemTra(m, out thongBao)) return false;

            try
            {
                if (monDAO.KiemTraTrungMaMon(m.MaMon))
                {
                    thongBao = $"Mã môn '{m.MaMon}' đã tồn tại trong hệ thống!";
                    return false;
                }

                bool kq = monDAO.InsertMon(m);
                thongBao = kq ? "Thêm môn học thành công!" : "Lỗi hệ thống khi thêm môn học!";
                if (kq) arrMon.Clear();
                return kq;
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                thongBao = "Mã khoa không tồn tại. Vui lòng chọn lại khoa!";
                return false;
            }
            catch (SqlException ex) when (ex.Number == 8152 || ex.Number == 2628)
            {
                thongBao = "Dữ liệu nhập vào vượt quá độ dài cho phép của CSDL!";
                return false;
            }
            catch (Exception ex)
            {
                thongBao = "Lỗi kết nối hoặc thực thi CSDL: " + ex.Message;
                return false;
            }
        }

        // ================= NGHIỆP VỤ CẬP NHẬT =================
        public bool CapNhatMon(MonDTO m, out string thongBao)
        {
            if (!ChuanHoaVaKiemTra(m, out thongBao)) return false;

            try
            {
                bool kq = monDAO.UpdateMon(m);
                thongBao = kq ? "Cập nhật môn học thành công!" : "Không tìm thấy mã môn để cập nhật!";
                if (kq) arrMon.Clear();
                return kq;
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                thongBao = "Mã khoa không tồn tại. Vui lòng chọn lại khoa!";
                return false;
            }
            catch (SqlException ex) when (ex.Number == 8152 || ex.Number == 2628)
            {
                thongBao = "Dữ liệu nhập vào vượt quá độ dài cho phép của CSDL!";
                return false;
            }
            catch (Exception ex)
            {
                thongBao = "Lỗi kết nối hoặc thực thi CSDL: " + ex.Message;
                return false;
            }
        }

        // ================= NGHIỆP VỤ XÓA =================
        public bool XoaMon(string maMon, out string thongBao)
        {
            if (string.IsNullOrWhiteSpace(maMon))
            {
                thongBao = "Vui lòng chọn môn học cần xóa!";
                return false;
            }

            maMon = maMon.Trim();

            try
            {
                if (monDAO.DemThamChieuMon(maMon) > 0)
                {
                    thongBao = "Môn học đang được sử dụng, không thể xóa!";
                    return false;
                }

                bool kq = monDAO.DeleteMon(maMon);
                thongBao = kq ? "Xóa môn học thành công!" : "Không tìm thấy mã môn để xóa!";
                if (kq) arrMon.Clear();
                return kq;
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                // Lưới an toàn nếu CSDL có FK chặn xóa
                thongBao = "Môn học đang được sử dụng, không thể xóa!";
                return false;
            }
            catch (Exception ex)
            {
                thongBao = "Lỗi kết nối hoặc thực thi CSDL: " + ex.Message;
                return false;
            }
        }

        // ================= TÌM KIẾM TRÊN ARRAYLIST =================
        public List<MonDTO> TimKiemMon(string tuKhoa)
        {
            if (arrMon == null || arrMon.Count == 0) DocDSMon();

            List<MonDTO> ketQua = new List<MonDTO>();
            tuKhoa = (tuKhoa ?? "").Trim().ToLower();

            foreach (object item in arrMon!)
            {
                if (item is MonDTO m)
                {
                    if (m.MaMon.ToLower().Contains(tuKhoa) || m.TenMon.ToLower().Contains(tuKhoa))
                    {
                        ketQua.Add(m);
                    }
                }
            }
            return ketQua;
        }

        // ================= KIỂM TRA DỮ LIỆU ĐẦU VÀO =================
        private bool ChuanHoaVaKiemTra(MonDTO m, out string thongBao)
        {
            if (m == null)
            {
                thongBao = "Dữ liệu môn học không hợp lệ!";
                return false;
            }

            m.MaMon = (m.MaMon ?? "").Trim();
            m.TenMon = (m.TenMon ?? "").Trim();
            m.MaKhoa = (m.MaKhoa ?? "").Trim();

            if (string.IsNullOrWhiteSpace(m.MaMon) || string.IsNullOrWhiteSpace(m.TenMon))
            {
                thongBao = "Mã môn và Tên môn không được để trống!";
                return false;
            }

            if (m.MaMon.Length > MaMonToiDa)
            {
                thongBao = $"Mã môn không được vượt quá {MaMonToiDa} ký tự!";
                return false;
            }

            if (m.MaMon.Contains(" "))
            {
                thongBao = "Mã môn không được chứa khoảng trắng!";
                return false;
            }

            if (m.TenMon.Length > TenMonToiDa)
            {
                thongBao = $"Tên môn không được vượt quá {TenMonToiDa} ký tự!";
                return false;
            }

            if (m.TinChi <= 0 || m.TinChi > 15)
            {
                thongBao = "Số tín chỉ phải lớn hơn 0 và không vượt quá 15!";
                return false;
            }

            if (m.MaKhoa.Length > MaKhoaToiDa)
            {
                thongBao = $"Mã khoa không được vượt quá {MaKhoaToiDa} ký tự!";
                return false;
            }

            if (!string.IsNullOrEmpty(m.MaKhoa))
            {
                try
                {
                    if (!monDAO.KiemTraTonTaiKhoa(m.MaKhoa))
                    {
                        thongBao = $"Mã khoa '{m.MaKhoa}' không tồn tại trong hệ thống!";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    thongBao = "Lỗi kết nối hoặc thực thi CSDL: " + ex.Message;
                    return false;
                }
            }

            thongBao = "";
            return true;
        }
    }
}
