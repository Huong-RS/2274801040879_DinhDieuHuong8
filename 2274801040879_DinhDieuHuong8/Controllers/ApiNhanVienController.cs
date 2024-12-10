using _2274801040879_DinhDieuHuong8.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace _2274801040879_DinhDieuHuong8.Controllers
{
    [Route("api/NhanVien")]
    [ApiController]
    public class ApiNhanVienController : ControllerBase
    {
        private readonly NhanVienActions _nvActions;

        public ApiNhanVienController()
        {
            _nvActions = new NhanVienActions(); // Khởi tạo NhanVienActions
        }

        // GET: api/<StudentController>
        [HttpGet]
        public string Get()
        {
            var dsNhanVien = _nvActions.GetAll(); // Lấy tất cả sinh viên
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson = JsonSerializer.Serialize<IList<NhanVien>>(dsNhanVien, opt);
            return strJson;
        }

        [HttpGet("sort")]
        public string GetSorted(string sortBy, string order = "asc")
        {
            var dsNhanVien = _nvActions.GetAll(); 
            if (!string.IsNullOrEmpty(sortBy))
            {
                // Lấy kiểu dữ liệu của thuộc tính được chỉ định để sắp xếp
                var propertyType = typeof(NhanVien).GetProperty(sortBy).PropertyType;
                // Kiểm tra xem thuộc tính có phải là chuỗi không
                var isString = propertyType == typeof(string);
                // Kiểm tra xem thứ tự sắp xếp có phải là giảm dần không
                var isDesc = order.ToLower() == "des";
                dsNhanVien = isDesc
                    ? isString
                        // Nếu giảm dần và thuộc tính là chuỗi, sắp xếp giảm dần theo chuỗi
                        ? dsNhanVien.OrderByDescending(nv => nv.GetType().GetProperty(sortBy).GetValue(nv).ToString()).ToList()
                        // Nếu giảm dần và thuộc tính không phải là chuỗi, sắp xếp giảm dần theo số
                        : dsNhanVien.OrderByDescending(nv => Convert.ToDouble(nv.GetType().GetProperty(sortBy).GetValue(nv))).ToList()
                    : isString
                        // Nếu tăng dần và thuộc tính là chuỗi, sắp xếp tăng dần theo chuỗi
                        ? dsNhanVien.OrderBy(nv => nv.GetType().GetProperty(sortBy).GetValue(nv).ToString()).ToList()
                        // Nếu tăng dần và thuộc tính không phải là chuỗi, sắp xếp tăng dần theo số
                        : dsNhanVien.OrderBy(nv => Convert.ToDouble(nv.GetType().GetProperty(sortBy).GetValue(nv))).ToList();
            }
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson = JsonSerializer.Serialize<IList<NhanVien>>(dsNhanVien, opt);
            return strJson;
        }


        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var nv = _nvActions.GetByID(id); // Lấy tất cả sinh viên
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson = JsonSerializer.Serialize<NhanVien>(nv, opt);
            return strJson;
        }

        // POST api/<StudentController>
        //[HttpPost]
        //public IActionResult Post([FromBody] NhanVien nv)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _nvActions.Add(nv);
        //    return CreatedAtAction(nameof(Get), new { id = nv.Tt }, nv);
        //}


        // POST (CÁCH 2)
        [HttpPost]
        public string Post([FromBody] NhanVien nv)
        {
            if (!ModelState.IsValid)
            {
                return JsonSerializer.Serialize(new { error = "Invalid model state" });
            }

            _nvActions.Add(nv);
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            return JsonSerializer.Serialize(nv, opt);
        }

        // PUT api/<StudentController>/5
        //[HttpPut("{id}")]
        //public IActionResult Put(int id, [FromBody] NhanVien nv)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var existing_nv = _nvActions.GetByID(id);
        //    if (existing_nv == null)
        //    {
        //        return NotFound(new { error = "NhanVien not found" });
        //    }

        //    // Update the existing NhanVien with new values
        //    existing_nv.Tt = nv.Tt;
        //    existing_nv.Hodem = nv.Hodem;
        //    existing_nv.Ten = nv.Ten;
        //    existing_nv.Cccd = nv.Cccd;
        //    existing_nv.Nickname = nv.Nickname;
        //    existing_nv.Email = nv.Email;
        //    existing_nv.Dienthoai = nv.Dienthoai;
        //    existing_nv.Diem_tichluy = nv.Diem_tichluy;
        //    existing_nv.Diem_renluyen = nv.Diem_renluyen;
        //    // Add other properties as needed

        //    _nvActions.Update(existing_nv);

        //    return NoContent(); // 204 No Content
        //}


        // PUT (Cách 2)

        [HttpPut("{id}")]
        public string Put(int id, [FromBody] NhanVien nv)
        {
            if (!ModelState.IsValid)
            {
                return JsonSerializer.Serialize(new { error = "Invalid model state" });
            }

            var existing_nv = _nvActions.GetByID(id);
            if (existing_nv == null)
            {
                return JsonSerializer.Serialize(new { error = "NhanVien not found" });
            }

            // Update the existing NhanVien with new values
            existing_nv.Tt = nv.Tt;
            existing_nv.Hodem = nv.Hodem;
            existing_nv.Ten = nv.Ten;
            existing_nv.Nickname = nv.Nickname;
            existing_nv.Cccd = nv.Cccd;
            existing_nv.Email = nv.Email;
            existing_nv.Dienthoai = nv.Dienthoai;
            existing_nv.ChucVu = nv.ChucVu;
            // Add other properties as needed

            _nvActions.Update(existing_nv);

            return JsonSerializer.Serialize(new { message = "Update successful" });
        }


        // DELETE api/<StudentController>/5
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var existing_nv = _nvActions.GetByID(id);
        //    if (existing_nv == null)
        //    {
        //        return NotFound(new { error = "NhanVien not found" });
        //    }

        //    _nvActions.DeleteByID(id);

        //    return NoContent(); // 204 No Content
        //}

        // DELETE (Cách 2)
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            var existing_nv = _nvActions.GetByID(id);
            if (existing_nv == null)
            {
                return JsonSerializer.Serialize(new { error = "NhanVien not found" });
            }

            _nvActions.DeleteByID(id);

            return JsonSerializer.Serialize(new { message = "Delete successful" });
        }

        [HttpDelete]
        public string DeleteAll()
        {
            var allNhanViens = _nvActions.GetAll();
            if (allNhanViens == null || !allNhanViens.Any())
            {
                return JsonSerializer.Serialize(new { message = "No NhanVien records to delete" });
            }

            foreach (var nv in allNhanViens)
            {
                _nvActions.DeleteAll();
            }

            return JsonSerializer.Serialize(new { message = "All NhanVien records deleted successfully" });
        }
    }
}

