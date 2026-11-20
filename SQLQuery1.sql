USE QL_ThiTracNghiem;
GO

-- ==========================================
-- 1. TẠO VÀ CẬP NHẬT BẢNG KHOA
-- ==========================================
IF OBJECT_ID('Khoa', 'U') IS NULL
BEGIN
    CREATE TABLE Khoa (
        Ma_Khoa VARCHAR(20) PRIMARY KEY,
        Ten_Khoa NVARCHAR(100) NOT NULL
    );
END
GO

INSERT INTO Khoa (Ma_Khoa, Ten_Khoa) 
SELECT 'CNTT', N'Công nghệ thông tin' WHERE NOT EXISTS (SELECT 1 FROM Khoa WHERE Ma_Khoa = 'CNTT');
INSERT INTO Khoa (Ma_Khoa, Ten_Khoa) 
SELECT 'KT', N'Kinh tế' WHERE NOT EXISTS (SELECT 1 FROM Khoa WHERE Ma_Khoa = 'KT');
INSERT INTO Khoa (Ma_Khoa, Ten_Khoa) 
SELECT 'NN', N'Ngoại ngữ' WHERE NOT EXISTS (SELECT 1 FROM Khoa WHERE Ma_Khoa = 'NN');
INSERT INTO Khoa (Ma_Khoa, Ten_Khoa) 
SELECT 'CB', N'Cơ bản' WHERE NOT EXISTS (SELECT 1 FROM Khoa WHERE Ma_Khoa = 'CB');
GO

-- ==========================================
-- 2. TẠO VÀ CẬP NHẬT BẢNG LỚP
-- ==========================================
IF OBJECT_ID('Lop', 'U') IS NULL
BEGIN
    CREATE TABLE Lop (
        Ma_Lop VARCHAR(20) PRIMARY KEY,
        Ten_Lop NVARCHAR(100) NOT NULL,
        Ma_Khoa VARCHAR(20) FOREIGN KEY REFERENCES Khoa(Ma_Khoa) ON UPDATE CASCADE
    );
END
GO

INSERT INTO Lop (Ma_Lop, Ten_Lop, Ma_Khoa) 
SELECT 'A1', N'Lớp A1', 'CNTT' WHERE NOT EXISTS (SELECT 1 FROM Lop WHERE Ma_Lop = 'A1');
INSERT INTO Lop (Ma_Lop, Ten_Lop, Ma_Khoa) 
SELECT 'A2', N'Lớp A2', 'CNTT' WHERE NOT EXISTS (SELECT 1 FROM Lop WHERE Ma_Lop = 'A2');
INSERT INTO Lop (Ma_Lop, Ten_Lop, Ma_Khoa) 
SELECT 'DCT1231', N'Lớp DCT1231', 'CNTT' WHERE NOT EXISTS (SELECT 1 FROM Lop WHERE Ma_Lop = 'DCT1231');
INSERT INTO Lop (Ma_Lop, Ten_Lop, Ma_Khoa) 
SELECT 'DCT1232', N'Lớp DCT1232', 'CNTT' WHERE NOT EXISTS (SELECT 1 FROM Lop WHERE Ma_Lop = 'DCT1232');
INSERT INTO Lop (Ma_Lop, Ten_Lop, Ma_Khoa) 
SELECT 'KT01', N'Lớp KT01', 'KT' WHERE NOT EXISTS (SELECT 1 FROM Lop WHERE Ma_Lop = 'KT01');
GO

-- ==========================================
-- 3. TẠO VÀ CẬP NHẬT BẢNG KỲ THI (Học kỳ 1, 2, 3 và Năm học chuẩn 20xy-20yz)
-- ==========================================
IF OBJECT_ID('Ky_Thi', 'U') IS NULL
BEGIN
    CREATE TABLE Ky_Thi (
        Ma_KT VARCHAR(20) PRIMARY KEY,
        Ten_KT NVARCHAR(50) NOT NULL, -- Học kỳ 1, Học kỳ 2, Học kỳ 3
        Ngay_Thi DATE NOT NULL,
        Nam_Hoc VARCHAR(20) NOT NULL -- Định dạng 20xy-20yz
    );
END
GO

IF COL_LENGTH('Ky_Thi', 'Nam_Hoc') IS NULL
BEGIN
    ALTER TABLE Ky_Thi ADD Nam_Hoc VARCHAR(20) NULL;
END
GO

-- Chèn dữ liệu mẫu Kỳ Thi chuẩn các năm
IF NOT EXISTS (SELECT 1 FROM Ky_Thi WHERE Ma_KT = 'HK1_2425')
    INSERT INTO Ky_Thi (Ma_KT, Ten_KT, Ngay_Thi, Nam_Hoc) VALUES ('HK1_2425', N'Học kỳ 1', '2024-12-15', '2024-2025');
IF NOT EXISTS (SELECT 1 FROM Ky_Thi WHERE Ma_KT = 'HK2_2425')
    INSERT INTO Ky_Thi (Ma_KT, Ten_KT, Ngay_Thi, Nam_Hoc) VALUES ('HK2_2425', N'Học kỳ 2', '2025-05-15', '2024-2025');
IF NOT EXISTS (SELECT 1 FROM Ky_Thi WHERE Ma_KT = 'HK3_2425')
    INSERT INTO Ky_Thi (Ma_KT, Ten_KT, Ngay_Thi, Nam_Hoc) VALUES ('HK3_2425', N'Học kỳ 3', '2025-07-20', '2024-2025');

IF NOT EXISTS (SELECT 1 FROM Ky_Thi WHERE Ma_KT = 'HK1_2526')
    INSERT INTO Ky_Thi (Ma_KT, Ten_KT, Ngay_Thi, Nam_Hoc) VALUES ('HK1_2526', N'Học kỳ 1', '2025-12-15', '2025-2026');
IF NOT EXISTS (SELECT 1 FROM Ky_Thi WHERE Ma_KT = 'HK2_2526')
    INSERT INTO Ky_Thi (Ma_KT, Ten_KT, Ngay_Thi, Nam_Hoc) VALUES ('HK2_2526', N'Học kỳ 2', '2026-05-15', '2025-2026');
IF NOT EXISTS (SELECT 1 FROM Ky_Thi WHERE Ma_KT = 'HK3_2526')
    INSERT INTO Ky_Thi (Ma_KT, Ten_KT, Ngay_Thi, Nam_Hoc) VALUES ('HK3_2526', N'Học kỳ 3', '2026-07-20', '2025-2026');

IF NOT EXISTS (SELECT 1 FROM Ky_Thi WHERE Ma_KT = 'HK1_2627')
    INSERT INTO Ky_Thi (Ma_KT, Ten_KT, Ngay_Thi, Nam_Hoc) VALUES ('HK1_2627', N'Học kỳ 1', '2026-12-15', '2026-2027');
IF NOT EXISTS (SELECT 1 FROM Ky_Thi WHERE Ma_KT = 'HK2_2627')
    INSERT INTO Ky_Thi (Ma_KT, Ten_KT, Ngay_Thi, Nam_Hoc) VALUES ('HK2_2627', N'Học kỳ 2', '2027-05-15', '2026-2027');
IF NOT EXISTS (SELECT 1 FROM Ky_Thi WHERE Ma_KT = 'HK3_2627')
    INSERT INTO Ky_Thi (Ma_KT, Ten_KT, Ngay_Thi, Nam_Hoc) VALUES ('HK3_2627', N'Học kỳ 3', '2027-07-20', '2026-2027');
GO

-- ==========================================
-- 4. LIÊN KẾT KHÓA NGOẠI CHO BẢNG ĐỀ THI
-- ==========================================
IF COL_LENGTH('De_Thi', 'Ma_KT') IS NULL
BEGIN
    ALTER TABLE De_Thi ADD Ma_KT VARCHAR(20) NULL FOREIGN KEY REFERENCES Ky_Thi(Ma_KT) ON UPDATE CASCADE;
END
GO

UPDATE De_Thi SET Ma_KT = 'HK1_2526' WHERE Ma_KT IS NULL;
GO

-- ==========================================
-- 5. LÀM SẠCH VÀ CHUẨN HÓA DỮ LIỆU CŨ (GIẢNG VIÊN, SINH VIÊN, MÔN HỌC)
-- ==========================================
-- Đảm bảo cột lưu tên/mã môn ở bảng Giang_Vien hỗ trợ tiếng Việt NVARCHAR
ALTER TABLE Giang_Vien ALTER COLUMN Ma_Mon NVARCHAR(255);
GO

IF OBJECT_ID('Mon', 'U') IS NOT NULL
BEGIN
    ALTER TABLE Mon ALTER COLUMN Ten_Mon NVARCHAR(100);
END
GO

-- Cập nhật tên môn học chuẩn cho giảng viên
UPDATE Giang_Vien SET Ma_Mon = N'Cơ sở dữ liệu' WHERE Ma_GV = 'GV01';
UPDATE Giang_Vien SET Ma_Mon = N'Công nghệ phần mềm' WHERE Ma_GV = 'GV02';
UPDATE Giang_Vien SET Ma_Mon = N'Lập trình trực quan' WHERE Ma_GV = 'GV03';
GO

-- Làm sạch dữ liệu khoa và lớp cho sinh viên/giảng viên cũ
UPDATE Giang_Vien SET Ma_Khoa = 'KT' WHERE Ma_Khoa LIKE 'Kinh%';
UPDATE Giang_Vien SET Ma_Khoa = 'CNTT' WHERE Ma_Khoa LIKE 'C%ng ngh%' OR Ma_Khoa = 'CNTT';
UPDATE Giang_Vien SET Ma_Khoa = 'CB' WHERE Ma_Khoa LIKE 'C%B%n%';

UPDATE Sinh_Vien SET Ma_Khoa = 'CNTT' WHERE Ma_Khoa LIKE 'C%ng ngh%' OR Ma_Khoa = 'CNTT';
UPDATE Sinh_Vien SET Lop = 'DCT1231' WHERE Ma_SV = 'SV01';
UPDATE Sinh_Vien SET Lop = 'DCT1232' WHERE Ma_SV = 'SV02';
GO

USE QL_ThiTracNghiem;
GO

-- Thêm cột Trang_Thai vào bảng De_Thi (Mặc định khi GV mới tạo là N'Chờ duyệt')
IF COL_LENGTH('De_Thi', 'Trang_Thai') IS NULL
BEGIN
    ALTER TABLE De_Thi ADD Trang_Thai NVARCHAR(30) DEFAULT N'Chờ duyệt';
END
GO

UPDATE De_Thi SET Trang_Thai = N'Đã đăng tải' WHERE Trang_Thai IS NULL;
GO

-- ==========================================
-- 6. TẠO BẢNG CÂU HỎI (NGÂN HÀNG CÂU HỎI)
-- ==========================================
-- Kiểm tra kiểu khóa ngoại trước khi chạy để tránh lỗi 1778:
--   EXEC sp_help 'Mon';
--   EXEC sp_help 'Giang_Vien';
IF OBJECT_ID('CAU_HOI', 'U') IS NULL
BEGIN
    CREATE TABLE CAU_HOI (
        Ma_CH        VARCHAR(20)    PRIMARY KEY,
        Ma_Mon       VARCHAR(20)    NOT NULL FOREIGN KEY REFERENCES Mon(Ma_Mon) ON UPDATE CASCADE,
        Ma_GV        VARCHAR(20)    NULL FOREIGN KEY REFERENCES Giang_Vien(Ma_GV) ON UPDATE CASCADE,
        Noi_Dung     NVARCHAR(1000) NOT NULL,
        Dap_An_A     NVARCHAR(500)  NOT NULL,
        Dap_An_B     NVARCHAR(500)  NOT NULL,
        Dap_An_C     NVARCHAR(500)  NOT NULL,
        Dap_An_D     NVARCHAR(500)  NOT NULL,
        Dap_An_Dung  CHAR(1)        NOT NULL CHECK (Dap_An_Dung IN ('A','B','C','D')),
        Muc_Do       NVARCHAR(30)   NOT NULL CHECK (Muc_Do IN (N'Cơ bản', N'Vận dụng', N'Nâng cao'))
    );
    CREATE INDEX IX_CAU_HOI_MaMon ON CAU_HOI(Ma_Mon);
    CREATE INDEX IX_CAU_HOI_MaGV ON CAU_HOI(Ma_GV);
END
GO