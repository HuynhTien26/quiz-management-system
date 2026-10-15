# quiz-management-system
Ứng dụng Quản lý thi trắc nghiệm viết bằng C# – Hỗ trợ quản lý ngân hàng câu hỏi, tạo đề ngẫu nhiên, làm bài thi có đếm ngược, chấm điểm tự động và thống kê báo cáo kết quả. 

# Hệ Thống Quản Lý Thi Trắc Nghiệm (QL_Thi_Trac_Nghiem)

Ứng dụng Windows Forms (C# .NET) kết hợp cơ sở dữ liệu SQL Server hỗ trợ quản lý ngân hàng câu hỏi, tạo đề thi tự động theo ma trận và tổ chức thi trắc nghiệm phân quyền theo 3 vai trò: **Quản trị viên (Admin)**, **Giảng viên** và **Sinh viên**.

---

## 1. Kiến trúc hệ thống
Dự án được xây dựng theo **Mô hình 3 lớp (3-Tier Architecture)** kết hợp **DTO** và mẫu thiết kế **Singleton**:
* **GUI (Graphical User Interface):** Tầng giao diện người dùng (Windows Forms), tiếp nhận thao tác và hiển thị dữ liệu.
* **BUS (Business Logic Layer):** Tầng xử lý nghiệp vụ, kiểm tra tính hợp lệ của dữ liệu trước khi truyền xuống tầng DAO.
* **DAO (Data Access Object):** Tầng giao tiếp trực tiếp với cơ sở dữ liệu SQL Server thông qua các câu lệnh truy vấn.
* **DTO (Data Transfer Object):** Các lớp đối tượng đóng gói dữ liệu để truyền tải giữa các tầng.

---

## 2. Cấu trúc thư mục và Chức năng từng tệp

```text
QL_Thi_Trac_Nghiem/
├── SQLQuery1.sql                # Script khởi tạo CSDL, bảng, dữ liệu mẫu và Trigger
└── QL_Thi_Trac_Nghiem/
    ├── Program.cs               # Điểm khởi chạy ứng dụng (mở FormDangNhap đầu tiên)
    ├── DTO/
    │   └── TaiKhoanDTO.cs       # Lớp đối tượng lưu trữ thông tin tài khoản người dùng
    ├── DAO/
    │   ├── DataProvider.cs      # Lớp Singleton quản lý kết nối SQL và thực thi truy vấn chung
    │   ├── TaiKhoanDAO.cs       # Truy vấn kiểm tra đăng nhập và lấy quyền người dùng
    │   └── AdminDAO.cs          # Truy vấn Thêm, Sửa, Xóa, Tìm kiếm Giảng viên & Sinh viên
    ├── BUS/
    │   ├── TaiKhoanBUS.cs       # Nghiệp vụ kiểm tra thông tin đăng nhập
    │   └── AdminBUS.cs          # Nghiệp vụ quản lý nhân sự, kiểm tra trùng mã GV/SV
    └── GUI/
        ├── FormDangNhap.cs      # Màn hình đăng nhập và điều hướng theo phân quyền
        ├── FormAdmin.cs         # Màn hình Quản trị viên quản lý Giảng viên & Sinh viên
        ├── FormGiaoVien.cs      # Màn hình Giảng viên quản lý môn, câu hỏi, ma trận, đề thi
        └── FormSinhVien.cs      # Màn hình Sinh viên làm bài thi và xem kết quả
