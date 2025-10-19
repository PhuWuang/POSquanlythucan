POSquanlythucan — POS Fast-Food (C# WinForms + SQL Server)
📝 Mô tảPhần mềm quản lý bán đồ ăn nhanh (POS) viết bằng C# WinForms, lưu trữ dữ liệu trên Microsoft SQL Server.Mục tiêu của dự án là cung cấp một bộ khởi tạo (starter project) dễ sử dụng để các thành viên trong nhóm có thể clone về là chạy ngay, phục vụ cho mục đích học phần và làm việc nhóm hiệu quả.
🚀 Tính năng chính🔑 Đăng nhập: Phân quyền cơ bản (Quản lý, Nhân viên).🍔 Quản lý sản phẩm: Thêm, sửa, xoá món ăn, đồ uống.🧾 Bán hàng & Đặt món: Giao diện POS trực quan để tạo đơn hàng.📊 Quản lý hóa đơn & Thống kê: Xem lại lịch sử bán hàng.
📦 Cài đặt (Installation)Yêu cầu phần mềmWindows 10/11.Visual Studio 2022 (Đã cài workload .NET desktop development).SQL Server 2019 (hoặc bản Express/Developer mới hơn).SQL Server Management Studio (SSMS).Git for Windows.Các bước cài đặt
Bước 1: Clone mã nguồnMở Git Bash hoặc Terminal và chạy lệnh:Bashgit clone https://github.com/PhuWuang/POSquanlythucan.git
cd POSquanlythucan
Bước 2: Tạo cơ sở dữ liệuMở SSMS (SQL Server Management Studio) và kết nối (Connect) vào SQL Server của bạn.Nhấn chuột phải vào thư mục Databases → New Database...Đặt tên cho database, ví dụ: PosFastFoods.Tìm đến file script trong thư mục dự án: database/PosFastFoods.sql.Mở file này, sao chép (copy) toàn bộ nội dung script.Quay lại SSMS, nhấn New Query (nhớ chọn database PosFastFoods ở thanh dropdown).Dán (paste) nội dung script vào và nhấn Execute để tạo tất cả bảng và dữ liệu mẫu.
Bước 3: Cấu hình chuỗi kết nối (Connection String)Mở thư mục QLBanDoAnNhanh/ trong dự án.Mở file App.config bằng Visual Studio hoặc text editor.Tìm đến phần <connectionStrings>:XML<connectionStrings>
  <add name="PosFastFood"
       connectionString="data source=LAPTOP-43L5U4AM\SQLEXPRESS;
                         initial catalog=PosFastFoods;
                         integrated security=True;
                         MultipleActiveResultSets=True;
                         App=EntityFramework"
       providerName="System.Data.SqlClient" />
</connectionStrings>
Quan trọng: Thay thế các giá trị sau cho phù hợp với máy của bạn:data source=LAPTOP-43L5U4AM\SQLEXPRESS: Thay bằng tên SQL Server của bạn (xem trong SSMS lúc kết nối).initial catalog=PosFastFoods: Thay bằng tên database bạn đã tạo ở Bước 2 (nếu bạn đặt tên khác).Nếu dùng tài khoản SQL (SQL Authentication):Thay integrated security=True;Bằng User Id=sa;Password=<mat-khau-cua-ban>;TrustServerCertificate=True;
🏃 Sử dụng (Usage)Mở file QLBanDoAnNhanh.sln bằng Visual Studio 2022.Build dự án (Menu Build → Build Solution hoặc Ctrl+Shift+B).Chạy dự án (Nhấn nút Start màu xanh hoặc F5).Đăng nhập và sử dụng các chức năng.
📁 Cấu trúc thư mục (Rút gọn)BashPOSquanlythucan/
├── QLBanDoAnNhanh.sln       # Solution file (mở file này bằng Visual Studio)
├── QLBanDoAnNhanh/          # Project WinForms chính
│   ├── Models/              # Các lớp Entity Framework (Data Model)
│   ├── frmMain.cs
│   ├── frmLogin.cs
│   ├── frmAddItem.cs
│   ├── frmOrder.cs
│   ├── App.config         # Chứa chuỗi kết nối (ConnectionStrings)
│   └── ...
├── database/
│   └── PosFastFoods.sql     # Script SQL để tạo CSDL và dữ liệu mẫu
└── README.md
⚙️ Ghi chú & Troubleshooting (Sửa lỗi)Không kết nối được DatabaseKiểm tra dịch vụ: Đảm bảo dịch vụ SQL Server đang chạy (mở Services.msc và tìm SQL Server (MSSQLSERVER) hoặc (SQLEXPRESS)).Kiểm tra Server Name: Đảm bảo data source trong App.config chính xác 100% (copy từ SSMS).Kiểm tra tên DB: Đảm bảo initial catalog trong App.config khớp với tên DB bạn tạo ở Bước 2.Firewall: Đảm bảo Firewall (tường lửa) của Windows không chặn kết nối đến SQL Server.Lỗi Entity Framework (Model) / App không chạyNếu ứng dụng build thành công nhưng không chạy (thường do Models/ không khớp với CSDL), hãy thử tái tạo lại model:Trong Solution Explorer (Visual Studio), xoá các file trong thư mục Models/.Chuột phải vào thư mục Models → Add → New Item...Chọn Data → ADO.NET Entity Data Model → Đặt tên (ví dụ: PosModel) → Add.Chọn Code First from database → Next.Chọn New Connection... → Điền thông tin Server name và chọn Database (PosFastFoods) của bạn.OK → Next → Tick vào Tables (mở rộng dbo/ và bỏ tick sysdiagrams nếu có).Finish.Build lại dự án (Ctrl+Shift+B) và chạy (F5).Build thất bại (Build fail)Đảm bảo bạn đã cài đặt workload .NET desktop development trong Visual Studio Installer.Thử Clean Solution và Rebuild Solution.
