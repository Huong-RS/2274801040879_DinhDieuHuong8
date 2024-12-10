using Microsoft.Extensions.Hosting;
using System.ComponentModel;
using System.Data;
using System.IO;
using OfficeOpenXml;
using _2274801040879_DinhDieuHuong8.Models;


namespace _2274801040879_DinhDieuHuong8.Models
{
    public class SanPhamActions
    {
        // Khai báo đường dẫn file excel
        private string filePath = @"D:\ASP.NET\2274801040879_DinhDieuHuong8vv\2274801040879_DinhDieuHuong8\Data\ds_sanpham.xlsx";
        private FileInfo GetFileExcel()
        {
            return new FileInfo(filePath);
        }

        public List<SanPham> GetAll()
        {
            var ds_sp = new List<SanPham>();

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
                    SanPham sp = new SanPham();
                    sp.Tt = Int32.Parse(worksheet.Cells[row, 1].Text);
                    sp.Masp = worksheet.Cells[row, 2].Text;
                    sp.Tensp = worksheet.Cells[row, 3].Text;
                    sp.Gia = worksheet.Cells[row, 4].Text;
                    sp.Soluong = worksheet.Cells[row, 5].Text;
                    sp.Daban = worksheet.Cells[row, 6].Text;
                   

                    ds_sp.Add(sp);
                }
            }

            return ds_sp;
        }

        // Lấy thông chi tiết của một nhân viên (GetByID)
        public SanPham GetByID(int id)
        {
            var file_excel = GetFileExcel();
            SanPham sp = null;

            using (var package = new ExcelPackage(file_excel))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    if (Int32.Parse(worksheet.Cells[row, 1].Text) == id) // Kiểm tra ID
                    {
                        sp = new SanPham
                        {
                            Tt = id,
                            Masp = worksheet.Cells[row, 2].Text,
                            Tensp = worksheet.Cells[row, 3].Text,
                            Gia = worksheet.Cells[row, 4].Text,
                            Soluong = worksheet.Cells[row, 5].Text,
                            Daban = worksheet.Cells[row, 6].Text,
                            
                        };
                        break; // Dừng vòng lặp khi tìm thấy nhân viên
                    }
                }
            }

            return sp; // Trả về nhan viên hoặc null nếu không tìm thấy
        }

        // Thêm (Add)
        public void Add(SanPham sp)
        {
            var file_excel = GetFileExcel(); // Sử dụng phương thức để lấy FileInfo

            using (var package = new ExcelPackage(file_excel))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                // Thêm sinh viên mới vào hàng tiếp theo
                worksheet.Cells[rowCount + 1, 1].Value = sp.Tt;
                worksheet.Cells[rowCount + 1, 2].Value = sp.Masp;
                worksheet.Cells[rowCount + 1, 3].Value = sp.Tensp;
                worksheet.Cells[rowCount + 1, 4].Value = sp.Gia;
                worksheet.Cells[rowCount + 1, 5].Value = sp.Soluong;
                worksheet.Cells[rowCount + 1, 6].Value = sp.Daban;
              

                package.Save(); // Lưu thay đổi vào tệp
            }
        }

        // Cập nhật (UpdateByID)
        public void Update(SanPham sp)
        {
            var file_excel = GetFileExcel(); // Sử dụng phương thức để lấy FileInfo

            using (var package = new ExcelPackage(file_excel))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    if (Int32.Parse(worksheet.Cells[row, 1].Text) == sp.Tt) // Kiểm tra ID
                    {
                        // Cập nhật thông tin sinh viên
                        worksheet.Cells[row, 2].Value = sp.Masp;
                        worksheet.Cells[row, 3].Value = sp.Tensp;
                        worksheet.Cells[row, 4].Value = sp.Gia;
                        worksheet.Cells[row, 5].Value = sp.Soluong;
                        worksheet.Cells[row, 6].Value = sp.Daban;
                       
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