# quiz-management-system
Ứng dụng Quản lý thi trắc nghiệm viết bằng C# – Hỗ trợ quản lý ngân hàng câu hỏi, tạo đề ngẫu nhiên, làm bài thi có đếm ngược, chấm điểm tự động và thống kê báo cáo kết quả. 
---

## 1. Phân công Nhiệm vụ & Cụm Module

| Thành viên | Phân hệ phụ trách | Nhiệm vụ chính | Nhánh Git phụ trách |
| :--- | :--- | :--- | :--- |
| **Thành viên 1** | CSDL, Xác thực & Tài khoản | Khởi tạo CSDL 9 bảng, Đăng nhập/Phân quyền, Quản lý Giảng viên & Sinh viên | `tv1-auth-user` |
| **Thành viên 2** | Môn học & Ngân hàng câu hỏi | CRUD Môn học, Ngân hàng câu hỏi 3 mức độ, Import/Export Excel đề thi | `tv2-mon-cauhoi` |
| **Thành viên 3** | Kỳ thi & Sinh đề tự động | Quản lý Kỳ thi, Ma trận đề (% tỷ lệ câu hỏi), Thuật toán sinh đề thi & In đề | `tv3-kythi-dethi` |
| **Thành viên 4** | Phòng thi, Chấm điểm & Báo cáo | Form làm bài (Timer đếm ngược), Logic tự động chấm điểm, Báo cáo & Thống kê | `tv4-phongthi-chamdiem` |

---

## 2. Cấu trúc Thư mục Dự án

Các thành viên đặt file code và tài nguyên vào đúng thư mục phân quyền:

```text
quiz-management-system/
├── SQLQuery1.sql                       # Script CSDL toàn hệ thống (Thành viên 1 phụ trách)
├── QL_Thi_Trac_Nghiem.slnx             # Solution Visual Studio
│
├── QL_Thi_Trac_Nghiem/                 # Core logic & WinForms
│   ├── DAO/                            # Tầng truy xuất CSDL (Mỗi TV viết DAO tương ứng)
│   ├── BUS/                            # Tầng xử lý nghiệp vụ (Mỗi TV viết BUS tương ứng)
│   ├── DTO/                            # Tầng truyền nhận dữ liệu (Models/Entities)
│   └── GUI/                            # Giao diện Form
│       ├── Auth/                       # Form Đăng nhập (Thành viên 1)
│       ├── User/                       # Quản lý GV, Quản lý SV (Thành viên 1)
│       ├── Subject/                    # Quản lý Môn học (Thành viên 2)
│       ├── Question/                   # Ngân hàng Câu hỏi, Import/Export (Thành viên 2)
│       ├── ExamConfig/                 # Quản lý Kỳ thi, Ma trận, Đề thi (Thành viên 3)
│       ├── Examination/                # Giao diện Làm bài thi, Xem kết quả (Thành viên 4)
│       └── Reports/                    # Báo cáo, Thống kê điểm (Thành viên 4)
│
├── QL_Thi_Trac_Nghiem.Web/             # Phân hệ Web
├── ERD/                                # Sơ đồ CSDL và thiết kế thực thể
└── Tài liệu/                           # Tài liệu đặc tả, hướng dẫn biểu mẫu
```

## 3. Quy tắc Đặt tên File & Commit Code

### Quy tắc đặt tên file
* **Form giao diện:** `frm[TênChứcNăng].cs` (Ví dụ: `frmLogin.cs`, `frmMonHoc.cs`, `frmPhongThi.cs`)
* **Class xử lý:** `[TênĐốiTượng]BUS.cs`, `[TênĐốiTượng]DAO.cs`, `[TênĐốiTượng]DTO.cs` (Ví dụ: `CauHoiBUS.cs`, `CauHoiDAO.cs`, `CauHoiDTO.cs`)

### Quy tắc commit
Viết commit rõ ràng theo cú pháp: `[loại]: [nội dung tóm tắt]`

* **`feat`:** Thêm tính năng mới
  ```bash
  git commit -m "feat(mon-hoc): them form quan ly va logic CRUD"
  ```
* **`fix`:** Sửa lỗi giao diện hoặc logic
  ```bash
  git commit -m "fix(cham-diem): sua loi tinh sai tong diem"
  ```
* **`docs`:** Cập nhật tài liệu, ERD hoặc file SQL
  ```bash
  git commit -m "docs: cap nhat rang buoc bang de thi"
  ```

---

## 4. Quy trình Làm việc với Git (Dành cho Thành viên)

> **LƯU Ý:** Không commit hoặc push trực tiếp lên nhánh `main` và nhánh `Phuong`(nhánh `tv1-auth-user` của Thành viên 1). Mỗi thành viên chỉ làm việc trên nhánh riêng của mình.

### Bước 1: Tải dự án về máy (Chỉ thực hiện lần đầu)
Mở Command Prompt (CMD) hoặc Git Bash tại thư mục bạn muốn lưu bài (ví dụ `D:\SGU\C#`) và chạy:

```bash
git clone [https://github.com/HuynhTien26/quiz-management-system.git](https://github.com/HuynhTien26/quiz-management-system.git)
cd quiz-management-system
```

### Bước 2: Chuyển sang nhánh phụ trách của bạn
Ai phụ trách phần nào thì chạy đúng lệnh của nhánh đó:

* **Thành viên 1:**
  ```bash
  git checkout tv1-auth-user
  ```
* **Thành viên 2:**
  ```bash
  git checkout tv2-mon-cauhoi
  ```
* **Thành viên 3:**
  ```bash
  git checkout tv3-kythi-dethi
  ```
* **Thành viên 4:**
  ```bash
  git checkout tv4-phongthi-chamdiem
  ```

*(Kiểm tra nhánh hiện tại bằng lệnh `git status` trước khi bắt đầu code).*

### Bước 3: Mở Visual Studio và thực hiện công việc
1. Mở file solution `QL_Thi_Trac_Nghiem.slnx`.
2. Tạo các Form, class `BUS`, `DAO` tương ứng đúng thư mục quy định tại Mục 2.
3. Build và kiểm tra chạy thử (Debug) trên máy cá nhân để chắc chắn không có lỗi compile.

### Bước 4: Lưu thay đổi và đẩy lên GitHub
Sau khi hoàn thành công việc hoặc kết thúc buổi làm việc, chạy lần lượt các lệnh sau:

```bash
git add .
git commit -m "feat(phan-he): mo ta noi dung vua hoan thanh"
git push origin <ten-nhanh-cua-ban>
```

**Ví dụ cụ thể:**
* **Nếu bạn là Thành viên 2:**
  ```bash
  git push origin tv2-mon-cauhoi
  ```
* **Nếu bạn là Thành viên 3:**
  ```bash
  git push origin tv3-kythi-dethi
  ```
* **Nếu bạn là Thành viên 4:**
  ```bash
  git push origin tv4-phongthi-chamdiem
  ```

### Bước 5: Báo cáo gộp code (Merge)
Sau khi push thành công lên GitHub, thông báo vào nhóm để kiểm tra code (**Pull Request**) trước khi gộp vào nhánh chính.
