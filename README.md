## 1. Code SQL bổ sung
* **File SQLQuery2.sql**: Cung cấp các code SQL đã được sử dụng để có thể chạy được dữ liệu và kiểm thử đề thi thử

## 2. Các code ngoài của thành viên khác được lấy
* Code Vy: `ExamConfigBUS.cs` và `ExamConfigDAO.cs`
* Code Thảo: `ExcelCauHoiCodec.cs`, `CauHoiBUS.cs` trong BUS. `CauHoiDAO.cs` trong DAO và `KetQuaImportDTO.cs`, `CauHoiDTO.cs` trong DTO

## 3. Các code lấy về có sự chỉnh sửa để chạy được trên máy tv4
Trong: 
* `BaiThiBUS` thêm code từ đoạn 165 trở xuống
* `BaiThiDAO` thêm code mới từ đoạn 503
* `ExamConfigBUS.cs` sửa code từ

```text
public DataTable LayDanhSachMon()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT Ma_Mon, Ten_mon FROM Mon");
        }
```
Thành 
```text
public DataTable LayDanhSachMon()
        {
            return _dao.GetAllMon();
        }
```
* `ExamConfigDAO.cs` sửa toàn bộ code
