using _2274801040879_DinhDieuHuong8.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace _2274801040879_DinhDieuHuong8.Controllers
{
    [Route("api/SanPham")]
    [ApiController]
    public class ApiSanPhamController : ControllerBase
    {
        private readonly SanPhamActions _spActions;

        public ApiSanPhamController()
        {
            _spActions = new SanPhamActions(); // Khởi tạo SanPhamActions
        }

        // GET: api/<StudentController>
        [HttpGet]
        public string Get()
        {
            var dsSanPham = _spActions.GetAll(); 
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson = JsonSerializer.Serialize<IList<SanPham>>(dsSanPham, opt);
            return strJson;
        }


        [HttpGet("sort")]
        public string GetSorted(string sortBy, string order = "asc")
        {
            var dsSanPham = _spActions.GetAll();
            if (!string.IsNullOrEmpty(sortBy))
            {
                // Lấy kiểu dữ liệu của thuộc tính được chỉ định để sắp xếp
                var propertyType = typeof(SanPham).GetProperty(sortBy).PropertyType;
                // Kiểm tra xem thuộc tính có phải là chuỗi không
                var isString = propertyType == typeof(string);
                // Kiểm tra xem thứ tự sắp xếp có phải là giảm dần không
                var isDesc = order.ToLower() == "des";
                dsSanPham = isDesc
                    ? isString
                        // Nếu giảm dần và thuộc tính là chuỗi, sắp xếp giảm dần theo chuỗi
                        ? dsSanPham.OrderByDescending(sp => sp.GetType().GetProperty(sortBy).GetValue(sp).ToString()).ToList()
                        // Nếu giảm dần và thuộc tính không phải là chuỗi, sắp xếp giảm dần theo số
                        : dsSanPham.OrderByDescending(sp => Convert.ToDouble(sp.GetType().GetProperty(sortBy).GetValue(sp))).ToList()
                    : isString
                        // Nếu tăng dần và thuộc tính là chuỗi, sắp xếp tăng dần theo chuỗi
                        ? dsSanPham.OrderBy(sp => sp.GetType().GetProperty(sortBy).GetValue(sp).ToString()).ToList()
                        // Nếu tăng dần và thuộc tính không phải là chuỗi, sắp xếp tăng dần theo số
                        : dsSanPham.OrderBy(sp => Convert.ToDouble(sp.GetType().GetProperty(sortBy).GetValue(sp))).ToList();
            }
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson = JsonSerializer.Serialize<IList<SanPham>>(dsSanPham, opt);
            return strJson;
        }



        [HttpGet("{id}")]
        public string Get(int id)
        {
            var sp = _spActions.GetByID(id); // Lấy tất cả sinh viên
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson = JsonSerializer.Serialize<SanPham>(sp, opt);
            return strJson;
        }

        
        [HttpPost]
        public string Post([FromBody] SanPham sp)
        {
            if (!ModelState.IsValid)
            {
                return JsonSerializer.Serialize(new { error = "Ispalid model state" });
            }

            _spActions.Add(sp);
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            return JsonSerializer.Serialize(sp, opt);
        }


        [HttpPut("{id}")]
        public string Put(int id, [FromBody] SanPham sp)
        {
            if (!ModelState.IsValid)
            {
                return JsonSerializer.Serialize(new { error = "Ispalid model state" });
            }

            var existing_sp = _spActions.GetByID(id);
            if (existing_sp == null)
            {
                return JsonSerializer.Serialize(new { error = "SanPham not found" });
            }

            // Update the existing SanPham with new values
            existing_sp.Tt = sp.Tt;
            existing_sp.Masp = sp.Masp;
            existing_sp.Tensp = sp.Tensp;
            existing_sp.Gia = sp.Gia;
            existing_sp.Soluong = sp.Soluong;
            existing_sp.Daban = sp.Daban;
          
            // Add other properties as needed

            _spActions.Update(existing_sp);

            return JsonSerializer.Serialize(new { message = "Update successful" });
        }


        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            var existing_sp = _spActions.GetByID(id);
            if (existing_sp == null)
            {
                return JsonSerializer.Serialize(new { error = "SanPham not found" });
            }

            _spActions.DeleteByID(id);

            return JsonSerializer.Serialize(new { message = "Delete successful" });
        }

        [HttpDelete]
        public string DeleteAll()
        {
            var allSanPhams = _spActions.GetAll();
            if (allSanPhams == null || !allSanPhams.Any())
            {
                return JsonSerializer.Serialize(new { message = "No SanPham records to delete" });
            }

            foreach (var sp in allSanPhams)
            {
                _spActions.DeleteAll();
            }

            return JsonSerializer.Serialize(new { message = "All SanPham records deleted successfully" });
        }
    }
}

