--A Kiểm tra dữ liệu hiện có trong các bảng
USE QL_ThiTracNghiem;
GO

SELECT * FROM Mon;
SELECT * FROM De_Thi;
SELECT * FROM Cau_Hoi;

--B Tạo bổ sung bảng Ma_Tran, CT_DT và cột Ma_MT cho De_Thi
USE QL_ThiTracNghiem;
GO

IF OBJECT_ID('Ma_Tran', 'U') IS NULL
BEGIN
    CREATE TABLE Ma_Tran (
        Ma_MT VARCHAR(20) PRIMARY KEY,
        Ma_Mon VARCHAR(20) FOREIGN KEY REFERENCES Mon(Ma_Mon),
        Ty_le_co_ban FLOAT NOT NULL,
        Ty_le_van_dung FLOAT NOT NULL,
        Ty_le_vd_nang_cao FLOAT NOT NULL
    );
END
GO

IF COL_LENGTH('De_Thi', 'Ma_MT') IS NULL
    ALTER TABLE De_Thi ADD Ma_MT VARCHAR(20) NULL;
GO

IF OBJECT_ID('CT_DT', 'U') IS NULL
BEGIN
    CREATE TABLE CT_DT (
        Ma_De VARCHAR(20) FOREIGN KEY REFERENCES De_Thi(Ma_De) ON DELETE CASCADE,
        Ma_CH VARCHAR(20) FOREIGN KEY REFERENCES Cau_Hoi(Ma_CH),
        PRIMARY KEY (Ma_De, Ma_CH)
    );
END
GO

--C Cập nhật trạng thái hiển thị đề thi và tạo dữ liệu đề mẫu (DE_TEST01 + 5 câu hỏi)
USE QL_ThiTracNghiem;
GO

-- 1. Tự lấy 1 mã môn có sẵn trong bảng Mon (nếu bảng Mon trống thì tự thêm 'CSDL')
DECLARE @MaMon VARCHAR(20);
SELECT TOP 1 @MaMon = Ma_Mon FROM Mon;

IF @MaMon IS NULL
BEGIN
    SET @MaMon = 'CSDL';
    INSERT INTO Mon (Ma_Mon, Ten_Mon) VALUES ('CSDL', N'Cơ sở dữ liệu');
END

-- 2. Mở hiển thị cho các đề cũ (nếu có)
UPDATE De_Thi SET Trang_Thai = N'Đã đăng tải' WHERE Trang_Thai IS NULL OR Trang_Thai = N'Chờ duyệt';

-- 3. Thêm đề thi mẫu DE_TEST01 (15 phút, 5 câu) nếu chưa tồn tại
IF NOT EXISTS (SELECT 1 FROM De_Thi WHERE Ma_De = 'DE_TEST01')
BEGIN
    INSERT INTO De_Thi (Ma_De, Ma_Mon, So_Cau_Hoi, Thoi_Gian, Ma_KT, Trang_Thai)
    VALUES ('DE_TEST01', @MaMon, 5, 15, 'HK1_2627', N'Đã đăng tải');
END

-- 4. Thêm 5 câu hỏi mẫu cho đề DE_TEST01 (đáp án đúng lần lượt: A, B, C, D, A)
IF NOT EXISTS (SELECT 1 FROM Cau_Hoi WHERE Ma_CH = 'CH_T01')
    INSERT INTO Cau_Hoi (Ma_CH, Ma_De, Noi_Dung, Dap_An_A, Dap_An_B, Dap_An_C, Dap_An_D, Dap_An_Dung)
    VALUES ('CH_T01', 'DE_TEST01', N'Câu 1: Ngôn ngữ nào dùng để phát triển WinForms .NET?', N'C#', N'HTML', N'CSS', N'SQL', 'A');

IF NOT EXISTS (SELECT 1 FROM Cau_Hoi WHERE Ma_CH = 'CH_T02')
    INSERT INTO Cau_Hoi (Ma_CH, Ma_De, Noi_Dung, Dap_An_A, Dap_An_B, Dap_An_C, Dap_An_D, Dap_An_Dung)
    VALUES ('CH_T02', 'DE_TEST01', N'Câu 2: Trong SQL Server, lệnh nào dùng để truy vấn dữ liệu?', N'INSERT', N'SELECT', N'DELETE', N'DROP', 'B');

IF NOT EXISTS (SELECT 1 FROM Cau_Hoi WHERE Ma_CH = 'CH_T03')
    INSERT INTO Cau_Hoi (Ma_CH, Ma_De, Noi_Dung, Dap_An_A, Dap_An_B, Dap_An_C, Dap_An_D, Dap_An_Dung)
    VALUES ('CH_T03', 'DE_TEST01', N'Câu 3: Khóa chính trong CSDL có đặc điểm gì?', N'Được trùng lặp', N'Được phép NULL', N'Duy nhất và không NULL', N'Chỉ chứa số', 'C');

IF NOT EXISTS (SELECT 1 FROM Cau_Hoi WHERE Ma_CH = 'CH_T04')
    INSERT INTO Cau_Hoi (Ma_CH, Ma_De, Noi_Dung, Dap_An_A, Dap_An_B, Dap_An_C, Dap_An_D, Dap_An_Dung)
    VALUES ('CH_T04', 'DE_TEST01', N'Câu 4: Kiến trúc 3 lớp trong dự án gồm các lớp nào?', N'MVC', N'HTML - CSS - JS', N'Client - Server', N'GUI - BUS - DAO', 'D');

IF NOT EXISTS (SELECT 1 FROM Cau_Hoi WHERE Ma_CH = 'CH_T05')
    INSERT INTO Cau_Hoi (Ma_CH, Ma_De, Noi_Dung, Dap_An_A, Dap_An_B, Dap_An_C, Dap_An_D, Dap_An_Dung)
    VALUES ('CH_T05', 'DE_TEST01', N'Câu 5: Từ khóa khai báo lớp trong C# là gì?', N'class', N'struct', N'def', N'function', 'A');
GO

--D Điều chỉnh ràng buộc Ho_Ten và thêm hồ sơ sinh viên SV01, SV02
USE QL_ThiTracNghiem;
GO

-- Mở khóa NULL cho Ho_Ten nếu bảng Sinh_Vien có cột này
IF COL_LENGTH('Sinh_Vien', 'Ho_Ten') IS NOT NULL
    ALTER TABLE Sinh_Vien ALTER COLUMN Ho_Ten NVARCHAR(100) NULL;
GO

-- Thêm SV01 nếu chưa có
IF NOT EXISTS (SELECT 1 FROM Sinh_Vien WHERE Ma_SV = 'SV01')
BEGIN
    INSERT INTO Sinh_Vien (Ma_SV, Ho_Lot, Ten, Ngay_Sinh, Gioi_Tinh, Ma_Khoa, Lop)
    VALUES ('SV01', N'Nguyễn Văn', N'An', '2005-05-15', N'Nam', 'CNTT', 'DCT1231');
END

-- Thêm SV02 nếu chưa có
IF NOT EXISTS (SELECT 1 FROM Sinh_Vien WHERE Ma_SV = 'SV02')
BEGIN
    INSERT INTO Sinh_Vien (Ma_SV, Ho_Lot, Ten, Ngay_Sinh, Gioi_Tinh, Ma_Khoa, Lop)
    VALUES ('SV02', N'Trần Thị', N'Bình', '2005-08-20', N'Nữ', 'CNTT', 'DCT1232');
END
GO

--E Tạo các bảng gốc và dữ liệu cơ bản
-- Bảng Khoa
IF OBJECT_ID('Khoa', 'U') IS NULL
    CREATE TABLE Khoa (Ma_Khoa VARCHAR(20) PRIMARY KEY, Ten_Khoa NVARCHAR(100) NOT NULL);
GO
INSERT INTO Khoa (Ma_Khoa, Ten_Khoa) SELECT 'CNTT', N'Công nghệ thông tin' WHERE NOT EXISTS (SELECT 1 FROM Khoa WHERE Ma_Khoa = 'CNTT');
INSERT INTO Khoa (Ma_Khoa, Ten_Khoa) SELECT 'KT', N'Kinh tế' WHERE NOT EXISTS (SELECT 1 FROM Khoa WHERE Ma_Khoa = 'KT');
INSERT INTO Khoa (Ma_Khoa, Ten_Khoa) SELECT 'NN', N'Ngoại ngữ' WHERE NOT EXISTS (SELECT 1 FROM Khoa WHERE Ma_Khoa = 'NN');
INSERT INTO Khoa (Ma_Khoa, Ten_Khoa) SELECT 'CB', N'Cơ bản' WHERE NOT EXISTS (SELECT 1 FROM Khoa WHERE Ma_Khoa = 'CB');
GO
-- Bảng Lop
IF OBJECT_ID('Lop', 'U') IS NULL
    CREATE TABLE Lop (Ma_Lop VARCHAR(20) PRIMARY KEY, Ten_Lop NVARCHAR(100) NOT NULL, Ma_Khoa VARCHAR(20) FOREIGN KEY REFERENCES Khoa(Ma_Khoa) ON UPDATE CASCADE);
GO
INSERT INTO Lop (Ma_Lop, Ten_Lop, Ma_Khoa) SELECT 'A1', N'Lớp A1', 'CNTT' WHERE NOT EXISTS (SELECT 1 FROM Lop WHERE Ma_Lop = 'A1');
INSERT INTO Lop (Ma_Lop, Ten_Lop, Ma_Khoa) SELECT 'DCT1231', N'Lớp DCT1231', 'CNTT' WHERE NOT EXISTS (SELECT 1 FROM Lop WHERE Ma_Lop = 'DCT1231');
INSERT INTO Lop (Ma_Lop, Ten_Lop, Ma_Khoa) SELECT 'DCT1232', N'Lớp DCT1232', 'CNTT' WHERE NOT EXISTS (SELECT 1 FROM Lop WHERE Ma_Lop = 'DCT1232');
GO
-- Bảng Mon (Hỗ trợ cột Ma_Khoa)
IF OBJECT_ID('Mon', 'U') IS NULL
BEGIN
    CREATE TABLE Mon (
        Ma_Mon VARCHAR(20) PRIMARY KEY,
        Ten_Mon NVARCHAR(100) NOT NULL,
        Tin_Chi INT DEFAULT 3,
        Ma_Khoa VARCHAR(20) NULL
    );
END
ELSE
BEGIN
    IF COL_LENGTH('Mon', 'Ma_Khoa') IS NULL ALTER TABLE Mon ADD Ma_Khoa VARCHAR(20) NULL;
END
GO
INSERT INTO Mon (Ma_Mon, Ten_Mon, Tin_Chi, Ma_Khoa) SELECT 'CSDL', N'Cơ sở dữ liệu', 3, 'CNTT' WHERE NOT EXISTS (SELECT 1 FROM Mon WHERE Ma_Mon = 'CSDL');
INSERT INTO Mon (Ma_Mon, Ten_Mon, Tin_Chi, Ma_Khoa) SELECT 'CNPM', N'Công nghệ phần mềm', 3, 'CNTT' WHERE NOT EXISTS (SELECT 1 FROM Mon WHERE Ma_Mon = 'CNPM');
INSERT INTO Mon (Ma_Mon, Ten_Mon, Tin_Chi, Ma_Khoa) SELECT 'LTTV', N'Lập trình trực quan', 3, 'CNTT' WHERE NOT EXISTS (SELECT 1 FROM Mon WHERE Ma_Mon = 'LTTV');
GO
-- Bảng Ky_Thi
IF OBJECT_ID('Ky_Thi', 'U') IS NULL
BEGIN
    CREATE TABLE Ky_Thi (
        Ma_KT VARCHAR(20) PRIMARY KEY,
        Ten_KT NVARCHAR(50) NOT NULL,
        Ngay_Thi DATE NOT NULL,
        Nam_Hoc VARCHAR(20) NOT NULL
    );
END
GO
IF NOT EXISTS (SELECT 1 FROM Ky_Thi WHERE Ma_KT = 'HK1_2526')
    INSERT INTO Ky_Thi (Ma_KT, Ten_KT, Ngay_Thi, Nam_Hoc) VALUES ('HK1_2526', N'Học kỳ 1', '2025-12-15', '2025-2026');
GO
-- Bảng Giang_Vien (Hỗ trợ Ho_lot, Ten, Ma_Mon NVARCHAR)
IF OBJECT_ID('Giang_Vien', 'U') IS NULL
BEGIN
    CREATE TABLE Giang_Vien (
        Ma_GV VARCHAR(20) PRIMARY KEY,
        Ho_lot NVARCHAR(50),
        Ten NVARCHAR(50) NOT NULL,
        Ngay_sinh DATE,
        Gioi_tinh NVARCHAR(10),
        Ma_Khoa VARCHAR(20),
        Ma_Mon NVARCHAR(255)
    );
END
GO
IF NOT EXISTS (SELECT 1 FROM Giang_Vien WHERE Ma_GV = 'GV01')
    INSERT INTO Giang_Vien (Ma_GV, Ho_lot, Ten, Ngay_sinh, Gioi_tinh, Ma_Khoa, Ma_Mon) 
    VALUES ('GV01', N'Nguyễn Văn', N'An', '1980-01-01', N'Nam', 'CNTT', N'Cơ sở dữ liệu');
GO
-- Bảng Sinh_Vien (Hỗ trợ Ho_lot, Ten, Lop)
IF OBJECT_ID('Sinh_Vien', 'U') IS NULL
BEGIN
    CREATE TABLE Sinh_Vien (
        Ma_SV VARCHAR(20) PRIMARY KEY,
        Ho_lot NVARCHAR(50),
        Ten NVARCHAR(50) NOT NULL,
        Ngay_sinh DATE,
        Gioi_tinh NVARCHAR(10),
        Ma_Khoa VARCHAR(20),
        Lop VARCHAR(20)
    );
END
GO
IF NOT EXISTS (SELECT 1 FROM Sinh_Vien WHERE Ma_SV = 'SV01')
    INSERT INTO Sinh_Vien (Ma_SV, Ho_lot, Ten, Ngay_sinh, Gioi_tinh, Ma_Khoa, Lop) 
    VALUES ('SV01', N'Trần Thị', N'Bình', '2004-05-10', N'Nữ', 'CNTT', 'DCT1231');
GO
-- Bảng De_Thi
IF OBJECT_ID('De_Thi', 'U') IS NULL
BEGIN
    CREATE TABLE De_Thi (
        Ma_De VARCHAR(20) PRIMARY KEY,
        Ten_De NVARCHAR(100),
        Ma_GV VARCHAR(20),
        Ma_Mon VARCHAR(20),
        So_Cau_Hoi INT DEFAULT 0,
        Thoi_Gian INT DEFAULT 15,
        Trang_Thai NVARCHAR(30) DEFAULT N'Chờ duyệt',
        Ma_KT VARCHAR(20)
    );
END
GO
-- Bảng TaiKhoan (Phục vụ đăng nhập)
IF OBJECT_ID('TaiKhoan', 'U') IS NULL
BEGIN
    CREATE TABLE TaiKhoan (
        TenDangNhap VARCHAR(50) PRIMARY KEY,
        MatKhau VARCHAR(50) NOT NULL,
        LoaiTaiKhoan VARCHAR(20) NOT NULL
    );
END
GO
IF NOT EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = 'admin')
    INSERT INTO TaiKhoan (TenDangNhap, MatKhau, LoaiTaiKhoan) VALUES ('admin', '12345', 'AD');
IF NOT EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = 'gv01')
    INSERT INTO TaiKhoan (TenDangNhap, MatKhau, LoaiTaiKhoan) VALUES ('gv01', '12345', 'GV');
IF NOT EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = 'sv01')
    INSERT INTO TaiKhoan (TenDangNhap, MatKhau, LoaiTaiKhoan) VALUES ('sv01', '12345', 'SV');
GO

--F TẠO CÁC BẢNG NGHIỆP VỤ (CÂU HỎI, BÀI THI)
IF OBJECT_ID('Cau_Hoi', 'U') IS NULL
BEGIN
    CREATE TABLE Cau_Hoi (
        Ma_CH VARCHAR(20) PRIMARY KEY,
        Ma_De VARCHAR(20),
        Noi_Dung NVARCHAR(MAX) NOT NULL,
        Dap_An_A NVARCHAR(500),
        Dap_An_B NVARCHAR(500),
        Dap_An_C NVARCHAR(500),
        Dap_An_D NVARCHAR(500),
        Dap_An_Dung VARCHAR(5) NOT NULL
    );
END
GO
IF OBJECT_ID('Bai_Thi', 'U') IS NULL
BEGIN
    CREATE TABLE Bai_Thi (
        Ma_BT VARCHAR(20) PRIMARY KEY,
        Ma_SV VARCHAR(20),
        Ma_De VARCHAR(20),
        Ngay_Thi DATETIME DEFAULT GETDATE(),
        Thoi_Gian_Bat_Dau DATETIME,
        Thoi_Gian_Ket_Thuc DATETIME,
        So_Cau_Dung INT DEFAULT 0,
        So_Cau_Sai INT DEFAULT 0,
        Tong_Diem FLOAT DEFAULT 0,
        Trang_Thai NVARCHAR(30) DEFAULT N'Đang làm'
    );
END
GO
IF OBJECT_ID('Chi_Tiet_Bai_Thi', 'U') IS NULL
BEGIN
    CREATE TABLE Chi_Tiet_Bai_Thi (
        Ma_BT VARCHAR(20),
        Ma_CH VARCHAR(20),
        Dap_An_Chon VARCHAR(5),
        Ket_Qua NVARCHAR(20),
        PRIMARY KEY (Ma_BT, Ma_CH)
    );
END
GO

--G Bổ sung cột cho giảng viên và sinh viên nếu chưa có
USE QL_ThiTracNghiem;
GO
-- 1. Bổ sung cột cho bảng Giang_Vien nếu chưa có
IF COL_LENGTH('Giang_Vien', 'Ho_lot') IS NULL ALTER TABLE Giang_Vien ADD Ho_lot NVARCHAR(50) NULL;
IF COL_LENGTH('Giang_Vien', 'Ten') IS NULL ALTER TABLE Giang_Vien ADD Ten NVARCHAR(50) NULL;
IF COL_LENGTH('Giang_Vien', 'Ngay_sinh') IS NULL ALTER TABLE Giang_Vien ADD Ngay_sinh DATE NULL;
IF COL_LENGTH('Giang_Vien', 'Gioi_tinh') IS NULL ALTER TABLE Giang_Vien ADD Gioi_tinh NVARCHAR(10) NULL;
GO
-- 2. Bổ sung cột cho bảng Sinh_Vien nếu chưa có
IF COL_LENGTH('Sinh_Vien', 'Ho_lot') IS NULL ALTER TABLE Sinh_Vien ADD Ho_lot NVARCHAR(50) NULL;
IF COL_LENGTH('Sinh_Vien', 'Ten') IS NULL ALTER TABLE Sinh_Vien ADD Ten NVARCHAR(50) NULL;
IF COL_LENGTH('Sinh_Vien', 'Ngay_sinh') IS NULL ALTER TABLE Sinh_Vien ADD Ngay_sinh DATE NULL;
IF COL_LENGTH('Sinh_Vien', 'Gioi_tinh') IS NULL ALTER TABLE Sinh_Vien ADD Gioi_tinh NVARCHAR(10) NULL;
GO

--H Cột mã môn và cột phụ trợ cho đề thi
USE QL_ThiTracNghiem;
GO
-- 1. Thêm cột Ma_Mon vào bảng De_Thi nếu chưa có
IF COL_LENGTH('De_Thi', 'Ma_Mon') IS NULL
    ALTER TABLE De_Thi ADD Ma_Mon VARCHAR(20) NULL;
GO
-- 2. Thêm các cột phụ trợ thường dùng cho De_Thi nếu thiếu
IF COL_LENGTH('De_Thi', 'So_Cau_Hoi') IS NULL
    ALTER TABLE De_Thi ADD So_Cau_Hoi INT DEFAULT 0;
IF COL_LENGTH('De_Thi', 'Thoi_Gian') IS NULL
    ALTER TABLE De_Thi ADD Thoi_Gian INT DEFAULT 15;
GO

--J Chỉnh họ tên cho giảng viên và sinh viên
USE QL_ThiTracNghiem;
GO
IF COL_LENGTH('Giang_Vien', 'Ho_Ten') IS NOT NULL
    ALTER TABLE Giang_Vien ALTER COLUMN Ho_Ten NVARCHAR(100) NULL;
GO
IF COL_LENGTH('Sinh_Vien', 'Ho_Ten') IS NOT NULL
    ALTER TABLE Sinh_Vien ALTER COLUMN Ho_Ten NVARCHAR(100) NULL;
GO
