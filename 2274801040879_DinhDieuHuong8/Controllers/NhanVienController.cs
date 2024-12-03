using _2274801040879_DinhDieuHuong8.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _2274801040879_DinhDieuHuong8.Controllers
{
    [Route("api/NhanVien1")]
    [ApiController]
    public class NhanvienController : ControllerBase
    {
        private readonly NhanVienActions _nvActions;
        public NhanvienController()
        {
            _nvActions = new NhanVienActions(); // Khởi tạo SinhVienActions
        }

        // GET: api/<NhanVienController>
        [HttpGet]
        public string Get()
        {
            var dsNhanVien = _nvActions.GetAll(); // Lấy tất cả Nhan viên
                                                  //  return new string[] { "value1", "value2" };
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson = JsonSerializer.Serialize<IList<NhanVien>>(dsNhanVien, opt);
            return strJson;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            var nv = _nvActions.GetByID(id); // Lấy tất cả Nhân viên
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson = JsonSerializer.Serialize<NhanVien>(nv, opt);
            return strJson;
        }

      
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
            existing_nv.Cccd = nv.Cccd;
            existing_nv.Nickname = nv.Nickname;
            existing_nv.Email = nv.Email;
            existing_nv.Dienthoai = nv.Dienthoai;
            existing_nv.ChucVu = nv.ChucVu;
            // Add other properties as needed

            _nvActions.Update(existing_nv);

            return JsonSerializer.Serialize(new { message = "Update successful" });
        }

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


        // DELETE api/<StudentController>/5
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var existing_sv = _svActions.GetByID(id);
        //    if (existing_sv == null)
        //    {
        //        return NotFound(new { error = "SinhVien not found" });
        //    }

        //    _svActions.DeleteByID(id);

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