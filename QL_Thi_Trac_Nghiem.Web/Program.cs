using QL_Thi_Trac_Nghiem.BUS;
using QL_Thi_Trac_Nghiem.Web.Components;
using QL_Thi_Trac_Nghiem.DAO;
using QL_Thi_Trac_Nghiem.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<MonBUS>();
builder.Services.AddScoped<PhienDangNhapService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

DataProvider.ConnectionString = @"Data Source=localhost;Initial Catalog=QL_ThiTracNghiem;Integrated Security=True;Encrypt=False";

app.Run();
