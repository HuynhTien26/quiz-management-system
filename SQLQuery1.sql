USE QL_ThiTracNghiem;
GO

IF OBJECT_ID('Gian_Vien', 'U') IS NOT NULL AND OBJECT_ID('Giang_Vien', 'U') IS NULL
BEGIN
    EXEC sp_rename 'Gian_Vien', 'Giang_Vien';
END
GO

IF OBJECT_ID('Giang_Vien', 'U') IS NULL
BEGIN
    CREATE TABLE Giang_Vien (
        Ma_GV VARCHAR(20) PRIMARY KEY,
        Ho_lot NVARCHAR(50),
        Ten NVARCHAR(30),
        Ngay_sinh DATE,
        Gioi_tinh NVARCHAR(10),
        Ma_Khoa NVARCHAR(50),
        Ma_Mon NVARCHAR(100)
    );

   END
GO

IF OBJECT_ID('Sinh_Vien', 'U') IS NULL
BEGIN
    CREATE TABLE Sinh_Vien (
        Ma_SV VARCHAR(20) PRIMARY KEY,
        Ho_lot NVARCHAR(50),
        Ten NVARCHAR(30),
        Ngay_sinh DATE,
        Gioi_tinh NVARCHAR(10),
        Ma_Khoa NVARCHAR(50),
        Lop NVARCHAR(30)
    );

    END
GO

IF NOT EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = 'admin')
BEGIN
    INSERT INTO TaiKhoan (TenDangNhap, MatKhau, HoTen, LoaiTaiKhoan)
    VALUES ('admin', '123456', N'Quản trị viên', 'AD');
END
GO

INSERT INTO TaiKhoan (TenDangNhap, MatKhau, HoTen, LoaiTaiKhoan)
SELECT Ma_GV, '123456', Ho_lot + N' ' + Ten, 'GV'
FROM Giang_Vien
WHERE Ma_GV NOT IN (SELECT TenDangNhap FROM TaiKhoan);
GO

INSERT INTO TaiKhoan (TenDangNhap, MatKhau, HoTen, LoaiTaiKhoan)
SELECT Ma_SV, '123456', Ho_lot + N' ' + Ten, 'TS'
FROM Sinh_Vien
WHERE Ma_SV NOT IN (SELECT TenDangNhap FROM TaiKhoan);
GO

SELECT * FROM Giang_Vien;
SELECT * FROM Sinh_Vien;
SELECT * FROM TaiKhoan;

USE QL_ThiTracNghiem;
GO

-- 1. Xóa tài khoản thisinh1 khỏi bảng TaiKhoan
DELETE FROM TaiKhoan WHERE TenDangNhap = 'thisinh1';

-- (Tùy chọn) Nếu bạn muốn xóa luôn tài khoản nháp giaovien1 lúc đầu thì bỏ dấu -- ở dòng dưới:
-- DELETE FROM TaiKhoan WHERE TenDangNhap = 'giaovien1';

-- 2. Chuyển toàn bộ LoaiTaiKhoan đang là 'TS' sang 'SV' cho đồng bộ
UPDATE TaiKhoan 
SET LoaiTaiKhoan = 'SV' 
WHERE LoaiTaiKhoan = 'TS';
GO

-- 3. Cập nhật lại Trigger (nếu có) để sinh viên mới luôn mang mã quyền 'SV'
CREATE OR ALTER TRIGGER trg_TuDongTaoTK_SinhVien
ON Sinh_Vien
AFTER INSERT
AS
BEGIN
    INSERT INTO TaiKhoan (TenDangNhap, MatKhau, HoTen, LoaiTaiKhoan)
    SELECT i.Ma_SV, '123456', i.Ho_lot + N' ' + i.Ten, 'SV'
    FROM inserted i
    WHERE i.Ma_SV NOT IN (SELECT TenDangNhap FROM TaiKhoan);
END;
GO

-- 4. Kiểm tra lại bảng TaiKhoan sau khi cập nhật
SELECT * FROM TaiKhoan;