using QL_Thi_Trac_Nghiem.BUS;
using QL_Thi_Trac_Nghiem.Web.Components;
using QL_Thi_Trac_Nghiem.Web.Services;

// Khởi tạo đối tượng cấu hình ứng dụng web Blazor
var builder = WebApplication.CreateBuilder(args);

// Đăng ký dịch vụ Razor Components và chế độ tương tác phía máy chủ (Interactive Server)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Đăng ký các dịch vụ nghiệp vụ và quản lý phiên đăng nhập theo từng kết nối (Scoped)
builder.Services.AddScoped<MonBUS>();
builder.Services.AddScoped<PhienDangNhapService>();

// Xây dựng ứng dụng từ các cấu hình đã khai báo
var app = builder.Build();

// Cấu hình xử lý lỗi và bảo mật khi chạy ở môi trường thực tế (không phải Development)
if (!app.Environment.IsDevelopment())
{
    // Chuyển hướng về trang /Error khi phát sinh ngoại lệ hệ thống
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // Kích hoạt HSTS (mặc định 30 ngày) để yêu cầu trình duyệt truy cập qua HTTPS
    app.UseHsts();
}

// Chuyển hướng về trang /not-found khi truy cập đường dẫn không tồn tại (lỗi 404)
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

// Tự động chuyển hướng các yêu cầu HTTP sang HTTPS
app.UseHttpsRedirection();

// Kích hoạt cơ chế bảo vệ chống giả mạo yêu cầu (Anti-forgery)
app.UseAntiforgery();

// Ánh xạ các tệp tài nguyên tĩnh (CSS, JS, hình ảnh trong wwwroot)
app.MapStaticAssets();

// Ánh xạ component gốc (App.razor) và bật chế độ Interactive Server
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Khởi chạy ứng dụng web
app.Run();