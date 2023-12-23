global
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using P08_Authorization.Areas.Identity.Data;
using P08_Authorization.Data;
using P08_Authorization.Service;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AuthorizationContextConnection") ?? throw new InvalidOperationException("Connection string 'AuthorizationContextConnection' not found.");

builder.Services.AddDbContext<AuthorizationContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IRoleDtoService, RoleDtoService>();

builder.Services.AddDefaultIdentity<MyUser>(options =>
{

    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 3;
// ��ͧ����駹��֧���ѹ�� 
}).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AuthorizationContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
// ��ͧ���ء����
app.UseAuthentication();
// ��ͧ���ء����

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
// ��ͧ���ء����
app.MapRazorPages();
// ��ͧ���ء����

app.Run();
