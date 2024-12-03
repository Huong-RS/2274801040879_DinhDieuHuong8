using Microsoft.Extensions.Hosting;
using System.ComponentModel;
using System.Data;
using System.IO;
using OfficeOpenXml;
using _2274801040879_DinhDieuHuong8.Models;


namespace _2274801040879_DinhDieuHuong8.Models
{
    public class NhanVienActions
    {
        // Khai báo đường dẫn file excel
        private string filePath = @"D:\ASP.NET\2274801040879_DinhDieuHuong8\2274801040879_DinhDieuHuong8\Data\ds_nhanvien.xlsx";
        private FileInfo GetFileExcel()
        {
            return new FileInfo(filePath);
        }

        // Lấy tất cả các sinh viên (GetAll)
        public List<NhanVien> GetAll()
        {
            var ds_nv = new List<NhanVien>();

            // Thiết lập LicenseContext
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            var file_excel = GetFileExcel();

            using (var package = new ExcelPackage(file_excel))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Lấy về sheet đầu tiên
                var rowCount = worksheet.Dimension.Rows; // Đếm số dòng trong excel (có dữ liệu
                var colCount = worksheet.Dimension.Columns;

                for (int row = 2; row <= rowCount; row++) // Mỗi một dong trong danh là một nhân viên
                {
                    // Ex Row Data: 0 |   1   |    2       | 3  | ....
                    // Ex Row Data: 1 | 11234 | Nguyễn Văn | An | ....
                    NhanVien nv = new NhanVien();
                    nv.Tt = Int32.Parse(worksheet.Cells[row, 1].Text);
                    nv.Cccd = worksheet.Cells[row, 2].Text;
                    nv.Hodem = worksheet.Cells[row, 3].Text;
                    nv.Ten = worksheet.Cells[row, 4].Text;
                    nv.Nickname = worksheet.Cells[row, 5].Text;
                    nv.Email = worksheet.Cells[row, 6].Text;
                    nv.Dienthoai = worksheet.Cells[row, 7].Text;
                    nv.ChucVu = worksheet.Cells[row, 8].Text;

                    ds_nv.Add(nv);
                }
            }

            return ds_nv;
        }

        // Lấy thông chi tiết của một nhân viên (GetByID)
        public NhanVien GetByID(int id)
        {
            var file_excel = GetFileExcel();
            NhanVien nv = null;

            using (var package = new ExcelPackage(file_excel))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    if (Int32.Parse(worksheet.Cells[row, 1].Text) == id) // Kiểm tra ID
                    {
                        nv = new NhanVien
                        {
                            Tt = id,
                            Cccd = worksheet.Cells[row, 2].Text,
                            Hodem = worksheet.Cells[row, 3].Text,
                            Ten = worksheet.Cells[row, 4].Text,
                            Nickname = worksheet.Cells[row, 5].Text,
                            Email = worksheet.Cells[row, 6].Text,
                            Dienthoai = worksheet.Cells[row, 7].Text,
                            ChucVu = worksheet.Cells[row, 8].Text,
                        };
                        break; // Dừng vòng lặp khi tìm thấy nhân viên
                    }
                }
            }

            return nv; // Trả về nhan viên hoặc null nếu không tìm thấy
        }

        // Thêm (Add)
        public void Add(NhanVien nv)
        {
            var file_excel = GetFileExcel(); // Sử dụng phương thức để lấy FileInfo

            using (var package = new ExcelPackage(file_excel))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                // Thêm sinh viên mới vào hàng tiếp theo
                worksheet.Cells[rowCount + 1, 1].Value = nv.Tt;
                worksheet.Cells[rowCount + 1, 2].Value = nv.Cccd;
                worksheet.Cells[rowCount + 1, 3].Value = nv.Hodem;
                worksheet.Cells[rowCount + 1, 4].Value = nv.Ten;
                worksheet.Cells[rowCount + 1, 5].Value = nv.Nickname;
                worksheet.Cells[rowCount + 1, 6].Value = nv.Email;
                worksheet.Cells[rowCount + 1, 7].Value = nv.Dienthoai;
                worksheet.Cells[rowCount + 1, 8].Value = nv.ChucVu;

                package.Save(); // Lưu thay đổi vào tệp
            }
        }

        // Cập nhật (UpdateByID)
        public void Update(NhanVien nv)
        {
            var file_excel = GetFileExcel(); // Sử dụng phương thức để lấy FileInfo

            using (var package = new ExcelPackage(file_excel))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    if (Int32.Parse(worksheet.Cells[row, 1].Text) == nv.Tt) // Kiểm tra ID
                    {
                        // Cập nhật thông tin sinh viên
                        worksheet.Cells[row, 2].Value = nv.Cccd;
                        worksheet.Cells[row, 3].Value = nv.Hodem;
                        worksheet.Cells[row, 4].Value = nv.Ten;
                        worksheet.Cells[row, 5].Value = nv.Nickname;
                        worksheet.Cells[row, 6].Value = nv.Email;
                        worksheet.Cells[row, 7].Value = nv.Dienthoai;
                        worksheet.Cells[row, 8].Value= nv.ChucVu;

                        break; // Dừng vòng lặp khi đã cập nhật
                    }
                }

                package.Save(); // Lưu thay đổi vào tệp
            }
        }

        // Xóa tất cả (DeleteAll)
        public void DeleteAll()
        {
            var file_excel = GetFileExcel(); // Sử dụng phương thức để lấy FileInfo

            using (var package = new ExcelPackage(file_excel))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                // Xóa tất cả hàng từ hàng 2 trở đi
                for (int row = 2; row <= rowCount; row++)
                {
                    worksheet.DeleteRow(2); // Luôn xóa hàng thứ 2 cho đến khi không còn hàng
                }

                package.Save(); // Lưu thay đổi vào tệp
            }
        }

        // Xóa một Nhan viên (DeleteByID)
        public void DeleteByID(int id)
        {
            var file_excel = GetFileExcel(); // Sử dụng phương thức để lấy FileInfo

            using (var package = new ExcelPackage(file_excel))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    if (Int32.Parse(worksheet.Cells[row, 1].Text) == id) // Kiểm tra ID
                    {
                        worksheet.DeleteRow(row); // Xóa hàng sinh viên
                        break; // Dừng vòng lặp khi đã xóa
                    }
                }

                package.Save(); // Lưu thay đổi vào tệp
            }
        }

    }
}