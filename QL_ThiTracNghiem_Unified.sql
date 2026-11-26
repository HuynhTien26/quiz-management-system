USE QL_ThiTracNghiem;
GO

-- =========================================================
-- 1. CÁC BẢNG DANH MỤC GỐC (KHOA, LỚP, MÔN, KỲ THI)
-- =========================================================
IF OBJECT_ID('Khoa', 'U') IS NULL
BEGIN
    CREATE TABLE Khoa (
        Ma_Khoa  VARCHAR(20)   PRIMARY KEY,
        Ten_Khoa NVARCHAR(100) NOT NULL
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM Khoa WHERE Ma_Khoa = 'CNTT')
    INSERT INTO Khoa (Ma_Khoa, Ten_Khoa) VALUES ('CNTT', N'Công nghệ thông tin');
GO

IF OBJECT_ID('Lop', 'U') IS NULL
BEGIN
    CREATE TABLE Lop (
        Ma_Lop  VARCHAR(20)   PRIMARY KEY,
        Ten_Lop NVARCHAR(100) NOT NULL,
        Ma_Khoa VARCHAR(20)   NULL FOREIGN KEY REFERENCES Khoa(Ma_Khoa) ON UPDATE CASCADE
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM Lop WHERE Ma_Lop = 'DCT1231')
    INSERT INTO Lop (Ma_Lop, Ten_Lop, Ma_Khoa) VALUES ('DCT1231', N'Lớp DCT1231', 'CNTT');
GO

IF OBJECT_ID('Mon', 'U') IS NULL
BEGIN
    CREATE TABLE Mon (
        Ma_Mon  VARCHAR(20)   PRIMARY KEY,
        Ten_Mon NVARCHAR(100) NOT NULL,
        Tin_Chi INT           NOT NULL DEFAULT 3,
        Ma_Khoa VARCHAR(20)   NULL FOREIGN KEY REFERENCES Khoa(Ma_Khoa) ON UPDATE CASCADE
    );
END
ELSE
BEGIN
    IF COL_LENGTH('Mon', 'Tin_Chi') IS NULL ALTER TABLE Mon ADD Tin_Chi INT NOT NULL DEFAULT 3;
    IF COL_LENGTH('Mon', 'Ma_Khoa') IS NULL ALTER TABLE Mon ADD Ma_Khoa VARCHAR(20) NULL;
    ALTER TABLE Mon ALTER COLUMN Ten_Mon NVARCHAR(100) NOT NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM Mon WHERE Ma_Mon = 'CSDL')
    INSERT INTO Mon (Ma_Mon, Ten_Mon, Tin_Chi, Ma_Khoa) VALUES ('CSDL', N'Cơ sở dữ liệu', 3, 'CNTT');
IF NOT EXISTS (SELECT 1 FROM Mon WHERE Ma_Mon = 'CNPM')
    INSERT INTO Mon (Ma_Mon, Ten_Mon, Tin_Chi, Ma_Khoa) VALUES ('CNPM', N'Công nghệ phần mềm', 4, 'CNTT');
GO

IF OBJECT_ID('Ky_Thi', 'U') IS NULL
BEGIN
    CREATE TABLE Ky_Thi (
        Ma_KT    VARCHAR(20)  PRIMARY KEY,
        Ten_KT   NVARCHAR(50) NOT NULL,
        Ngay_Thi DATE         NOT NULL,
        Nam_Hoc  VARCHAR(20)  NOT NULL
    );
END
ELSE
BEGIN
    IF COL_LENGTH('Ky_Thi', 'Nam_Hoc') IS NULL ALTER TABLE Ky_Thi ADD Nam_Hoc VARCHAR(20) NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM Ky_Thi WHERE Ma_KT = 'HK1_2526')
    INSERT INTO Ky_Thi (Ma_KT, Ten_KT, Ngay_Thi, Nam_Hoc) VALUES ('HK1_2526', N'Học kỳ 1', '2025-12-15', '2025-2026');
IF NOT EXISTS (SELECT 1 FROM Ky_Thi WHERE Ma_KT = 'HK2_2526')
    INSERT INTO Ky_Thi (Ma_KT, Ten_KT, Ngay_Thi, Nam_Hoc) VALUES ('HK2_2526', N'Học kỳ 2', '2026-05-15', '2025-2026');
IF NOT EXISTS (SELECT 1 FROM Ky_Thi WHERE Ma_KT = 'HK1_2627')
    INSERT INTO Ky_Thi (Ma_KT, Ten_KT, Ngay_Thi, Nam_Hoc) VALUES ('HK1_2627', N'Học kỳ 1', '2026-12-15', '2026-2027');
GO

-- =========================================================
-- 2. NGƯỜI DÙNG, GIẢNG VIÊN, SINH VIÊN
-- =========================================================
IF OBJECT_ID('TaiKhoan', 'U') IS NULL
BEGIN
    CREATE TABLE TaiKhoan (
        TenDangNhap  VARCHAR(50) PRIMARY KEY,
        MatKhau      VARCHAR(50) NOT NULL,
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

IF OBJECT_ID('Giang_Vien', 'U') IS NULL
BEGIN
    CREATE TABLE Giang_Vien (
        Ma_GV     VARCHAR(20)   PRIMARY KEY,
        Ho_lot    NVARCHAR(50)  NULL,
        Ten       NVARCHAR(50)  NOT NULL,
        Ngay_sinh DATE          NULL,
        Gioi_tinh NVARCHAR(10)  NULL,
        Ma_Khoa   VARCHAR(20)   NULL,
        Ma_Mon    NVARCHAR(255) NULL
    );
END
ELSE
BEGIN
    IF COL_LENGTH('Giang_Vien', 'Ho_lot') IS NULL ALTER TABLE Giang_Vien ADD Ho_lot NVARCHAR(50) NULL;
    IF COL_LENGTH('Giang_Vien', 'Ten') IS NULL ALTER TABLE Giang_Vien ADD Ten NVARCHAR(50) NULL;
    IF COL_LENGTH('Giang_Vien', 'Ngay_sinh') IS NULL ALTER TABLE Giang_Vien ADD Ngay_sinh DATE NULL;
    IF COL_LENGTH('Giang_Vien', 'Gioi_tinh') IS NULL ALTER TABLE Giang_Vien ADD Gioi_tinh NVARCHAR(10) NULL;
    IF COL_LENGTH('Giang_Vien', 'Ho_Ten') IS NOT NULL ALTER TABLE Giang_Vien ALTER COLUMN Ho_Ten NVARCHAR(100) NULL;
    ALTER TABLE Giang_Vien ALTER COLUMN Ma_Mon NVARCHAR(255) NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM Giang_Vien WHERE Ma_GV = 'GV01')
    INSERT INTO Giang_Vien (Ma_GV, Ho_lot, Ten, Ngay_sinh, Gioi_tinh, Ma_Khoa, Ma_Mon)
    VALUES ('GV01', N'Nguyễn Văn', N'An', '1980-01-01', N'Nam', 'CNTT', 'CSDL');
GO

IF OBJECT_ID('Sinh_Vien', 'U') IS NULL
BEGIN
    CREATE TABLE Sinh_Vien (
        Ma_SV     VARCHAR(20)  PRIMARY KEY,
        Ho_lot    NVARCHAR(50) NULL,
        Ten       NVARCHAR(50) NOT NULL,
        Ngay_sinh DATE         NULL,
        Gioi_tinh NVARCHAR(10) NULL,
        Ma_Khoa   VARCHAR(20)  NULL,
        Lop       VARCHAR(20)  NULL
    );
END
ELSE
BEGIN
    IF COL_LENGTH('Sinh_Vien', 'Ho_lot') IS NULL ALTER TABLE Sinh_Vien ADD Ho_lot NVARCHAR(50) NULL;
    IF COL_LENGTH('Sinh_Vien', 'Ten') IS NULL ALTER TABLE Sinh_Vien ADD Ten NVARCHAR(50) NULL;
    IF COL_LENGTH('Sinh_Vien', 'Ngay_sinh') IS NULL ALTER TABLE Sinh_Vien ADD Ngay_sinh DATE NULL;
    IF COL_LENGTH('Sinh_Vien', 'Gioi_tinh') IS NULL ALTER TABLE Sinh_Vien ADD Gioi_tinh NVARCHAR(10) NULL;
    IF COL_LENGTH('Sinh_Vien', 'Ho_Ten') IS NOT NULL ALTER TABLE Sinh_Vien ALTER COLUMN Ho_Ten NVARCHAR(100) NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM Sinh_Vien WHERE Ma_SV = 'SV01')
    INSERT INTO Sinh_Vien (Ma_SV, Ho_lot, Ten, Ngay_sinh, Gioi_tinh, Ma_Khoa, Lop)
    VALUES ('SV01', N'Trần Thị', N'Bình', '2004-05-10', N'Nữ', 'CNTT', 'DCT1231');
GO

-- =========================================================
-- 3. MA TRẬN, ĐỀ THI, NGÂN HÀNG CÂU HỎI & CHI TIẾT ĐỀ THI
-- =========================================================
IF OBJECT_ID('Ma_Tran', 'U') IS NULL
BEGIN
    CREATE TABLE Ma_Tran (
        Ma_MT              VARCHAR(20) PRIMARY KEY,
        Ma_Mon             VARCHAR(20) NULL FOREIGN KEY REFERENCES Mon(Ma_Mon) ON UPDATE CASCADE,
        Ty_le_co_ban       FLOAT       NOT NULL DEFAULT 50,
        Ty_le_van_dung     FLOAT       NOT NULL DEFAULT 30,
        Ty_le_vd_nang_cao  FLOAT       NOT NULL DEFAULT 20
    );
END
GO

IF OBJECT_ID('De_Thi', 'U') IS NULL
BEGIN
    CREATE TABLE De_Thi (
        Ma_De       VARCHAR(20)  PRIMARY KEY,
        Ten_De      NVARCHAR(100) NULL,
        Ma_GV       VARCHAR(20)  NULL,
        Ma_Mon      VARCHAR(20)  NULL,
        Ma_MT       VARCHAR(20)  NULL,
        Ma_KT       VARCHAR(20)  NULL,
        So_Cau_Hoi  INT          NOT NULL DEFAULT 0,
        Thoi_Gian   INT          NOT NULL DEFAULT 15,
        Trang_Thai  NVARCHAR(30) NOT NULL DEFAULT N'Chờ duyệt'
    );
END
ELSE
BEGIN
    IF COL_LENGTH('De_Thi', 'Ten_De') IS NULL ALTER TABLE De_Thi ADD Ten_De NVARCHAR(100) NULL;
    IF COL_LENGTH('De_Thi', 'Ma_GV') IS NULL ALTER TABLE De_Thi ADD Ma_GV VARCHAR(20) NULL;
    IF COL_LENGTH('De_Thi', 'Ma_Mon') IS NULL ALTER TABLE De_Thi ADD Ma_Mon VARCHAR(20) NULL;
    IF COL_LENGTH('De_Thi', 'Ma_MT') IS NULL ALTER TABLE De_Thi ADD Ma_MT VARCHAR(20) NULL;
    IF COL_LENGTH('De_Thi', 'Ma_KT') IS NULL ALTER TABLE De_Thi ADD Ma_KT VARCHAR(20) NULL;
    IF COL_LENGTH('De_Thi', 'So_Cau_Hoi') IS NULL ALTER TABLE De_Thi ADD So_Cau_Hoi INT NOT NULL DEFAULT 0;
    IF COL_LENGTH('De_Thi', 'Thoi_Gian') IS NULL ALTER TABLE De_Thi ADD Thoi_Gian INT NOT NULL DEFAULT 15;
    IF COL_LENGTH('De_Thi', 'Trang_Thai') IS NULL ALTER TABLE De_Thi ADD Trang_Thai NVARCHAR(30) NOT NULL DEFAULT N'Chờ duyệt';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_De_Thi_Ky_Thi')
    ALTER TABLE De_Thi ADD CONSTRAINT FK_De_Thi_Ky_Thi FOREIGN KEY (Ma_KT) REFERENCES Ky_Thi(Ma_KT) ON UPDATE CASCADE;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_De_Thi_Ma_Tran')
    ALTER TABLE De_Thi ADD CONSTRAINT FK_De_Thi_Ma_Tran FOREIGN KEY (Ma_MT) REFERENCES Ma_Tran(Ma_MT);
GO

IF OBJECT_ID('CAU_HOI', 'U') IS NULL
BEGIN
    CREATE TABLE CAU_HOI (
        Ma_CH       VARCHAR(20)    PRIMARY KEY,
        Ma_Mon      VARCHAR(20)    NOT NULL FOREIGN KEY REFERENCES Mon(Ma_Mon) ON UPDATE CASCADE,
        Ma_GV       VARCHAR(20)    NULL FOREIGN KEY REFERENCES Giang_Vien(Ma_GV) ON UPDATE CASCADE,
        Noi_Dung    NVARCHAR(1000) NOT NULL,
        Dap_An_A    NVARCHAR(500)  NOT NULL,
        Dap_An_B    NVARCHAR(500)  NOT NULL,
        Dap_An_C    NVARCHAR(500)  NOT NULL,
        Dap_An_D    NVARCHAR(500)  NOT NULL,
        Dap_An_Dung CHAR(1)        NOT NULL CHECK (Dap_An_Dung IN ('A','B','C','D')),
        Muc_Do      NVARCHAR(30)   NOT NULL CHECK (Muc_Do IN (N'Cơ bản', N'Vận dụng', N'Nâng cao'))
    );
END
GO

IF OBJECT_ID('CT_DT', 'U') IS NULL
BEGIN
    CREATE TABLE CT_DT (
        Ma_De VARCHAR(20) NOT NULL FOREIGN KEY REFERENCES De_Thi(Ma_De) ON DELETE CASCADE,
        Ma_CH VARCHAR(20) NOT NULL FOREIGN KEY REFERENCES CAU_HOI(Ma_CH),
        PRIMARY KEY (Ma_De, Ma_CH)
    );
END
GO

-- =========================================================
-- 4. BÀI THI & CHI TIẾT BÀI THI
-- =========================================================
IF OBJECT_ID('Bai_Thi', 'U') IS NULL
BEGIN
    CREATE TABLE Bai_Thi (
        Ma_BT              VARCHAR(20)  PRIMARY KEY,
        Ma_SV              VARCHAR(20)  NULL FOREIGN KEY REFERENCES Sinh_Vien(Ma_SV),
        Ma_De              VARCHAR(20)  NULL FOREIGN KEY REFERENCES De_Thi(Ma_De),
        Ngay_Thi           DATETIME     NOT NULL DEFAULT GETDATE(),
        Thoi_Gian_Bat_Dau  DATETIME     NULL,
        Thoi_Gian_Ket_Thuc DATETIME     NULL,
        So_Cau_Dung        INT          NOT NULL DEFAULT 0,
        So_Cau_Sai         INT          NOT NULL DEFAULT 0,
        Tong_Diem          FLOAT        NOT NULL DEFAULT 0,
        Trang_Thai         NVARCHAR(30) NOT NULL DEFAULT N'Đang làm'
    );
END
ELSE
BEGIN
    IF COL_LENGTH('Bai_Thi', 'Ngay_Thi') IS NULL ALTER TABLE Bai_Thi ADD Ngay_Thi DATETIME NOT NULL DEFAULT GETDATE();
    IF COL_LENGTH('Bai_Thi', 'Thoi_Gian_Bat_Dau') IS NULL ALTER TABLE Bai_Thi ADD Thoi_Gian_Bat_Dau DATETIME NULL;
    IF COL_LENGTH('Bai_Thi', 'Thoi_Gian_Ket_Thuc') IS NULL ALTER TABLE Bai_Thi ADD Thoi_Gian_Ket_Thuc DATETIME NULL;
    IF COL_LENGTH('Bai_Thi', 'So_Cau_Dung') IS NULL ALTER TABLE Bai_Thi ADD So_Cau_Dung INT NOT NULL DEFAULT 0;
    IF COL_LENGTH('Bai_Thi', 'So_Cau_Sai') IS NULL ALTER TABLE Bai_Thi ADD So_Cau_Sai INT NOT NULL DEFAULT 0;
    IF COL_LENGTH('Bai_Thi', 'Tong_Diem') IS NULL ALTER TABLE Bai_Thi ADD Tong_Diem FLOAT NOT NULL DEFAULT 0;
    IF COL_LENGTH('Bai_Thi', 'Trang_Thai') IS NULL ALTER TABLE Bai_Thi ADD Trang_Thai NVARCHAR(30) NOT NULL DEFAULT N'Đang làm';
END
GO

IF OBJECT_ID('Chi_Tiet_Bai_Thi', 'U') IS NULL
BEGIN
    CREATE TABLE Chi_Tiet_Bai_Thi (
        Ma_BT        VARCHAR(20)  NOT NULL FOREIGN KEY REFERENCES Bai_Thi(Ma_BT) ON DELETE CASCADE,
        Ma_CH        VARCHAR(20)  NOT NULL FOREIGN KEY REFERENCES CAU_HOI(Ma_CH),
        Ma_De        VARCHAR(20)  NULL,
        Dap_An_Chon  VARCHAR(5)   NULL,
        Ket_Qua      NVARCHAR(20) NULL,
        PRIMARY KEY (Ma_BT, Ma_CH)
    );
END
ELSE
BEGIN
    IF COL_LENGTH('Chi_Tiet_Bai_Thi', 'Ma_De') IS NULL ALTER TABLE Chi_Tiet_Bai_Thi ADD Ma_De VARCHAR(20) NULL;
END
GO

-- =========================================================
-- 5. DỮ LIỆU MẪU TỐI THIỂU ĐỂ CHẠY THỬ ĐỀ THI
-- =========================================================
IF NOT EXISTS (SELECT 1 FROM De_Thi WHERE Ma_De = 'DE_TEST01')
BEGIN
    INSERT INTO De_Thi (Ma_De, Ten_De, Ma_GV, Ma_Mon, So_Cau_Hoi, Thoi_Gian, Ma_KT, Trang_Thai)
    VALUES ('DE_TEST01', N'Đề kiểm tra mẫu CSDL', 'GV01', 'CSDL', 3, 15, 'HK1_2627', N'Đã đăng tải');
END
GO

IF NOT EXISTS (SELECT 1 FROM CAU_HOI WHERE Ma_CH = 'CH_T01')
    INSERT INTO CAU_HOI (Ma_CH, Ma_Mon, Ma_GV, Noi_Dung, Dap_An_A, Dap_An_B, Dap_An_C, Dap_An_D, Dap_An_Dung, Muc_Do)
    VALUES ('CH_T01', 'CSDL', 'GV01', N'Trong SQL Server, lệnh nào dùng để truy vấn dữ liệu?', N'INSERT', N'SELECT', N'DELETE', N'DROP', 'B', N'Cơ bản');

IF NOT EXISTS (SELECT 1 FROM CAU_HOI WHERE Ma_CH = 'CH_T02')
    INSERT INTO CAU_HOI (Ma_CH, Ma_Mon, Ma_GV, Noi_Dung, Dap_An_A, Dap_An_B, Dap_An_C, Dap_An_D, Dap_An_Dung, Muc_Do)
    VALUES ('CH_T02', 'CSDL', 'GV01', N'Khóa chính trong CSDL có đặc điểm gì?', N'Được trùng lặp', N'Được phép NULL', N'Duy nhất và không NULL', N'Chỉ chứa số', 'C', N'Cơ bản');

IF NOT EXISTS (SELECT 1 FROM CAU_HOI WHERE Ma_CH = 'CH_T03')
    INSERT INTO CAU_HOI (Ma_CH, Ma_Mon, Ma_GV, Noi_Dung, Dap_An_A, Dap_An_B, Dap_An_C, Dap_An_D, Dap_An_Dung, Muc_Do)
    VALUES ('CH_T03', 'CSDL', 'GV01', N'Ràng buộc khóa ngoại dùng để làm gì?', N'Tăng tốc độ index', N'Đảm bảo toàn vẹn tham chiếu', N'Mã hóa dữ liệu', N'Tự động sao lưu', 'B', N'Vận dụng');
GO

INSERT INTO CT_DT (Ma_De, Ma_CH)
SELECT 'DE_TEST01', ch.Ma_CH
FROM CAU_HOI ch
WHERE ch.Ma_CH IN ('CH_T01', 'CH_T02', 'CH_T03')
  AND NOT EXISTS (SELECT 1 FROM CT_DT ct WHERE ct.Ma_De = 'DE_TEST01' AND ct.Ma_CH = ch.Ma_CH);
GO