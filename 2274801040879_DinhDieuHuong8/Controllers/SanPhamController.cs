using _2274801040879_DinhDieuHuong8.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _2274801040879_DinhDieuHuong8.Controllers
{
    [Route("api/SanPham1")]
    [ApiController]
    public class NhaspienController : ControllerBase
    {
        private readonly SanPhamActions _spActions;
        public NhaspienController()
        {
            _spActions = new SanPhamActions(); // Khởi tạo SinhVienActions
        }

        // GET: api/<SanPhamController>
        [HttpGet]
        public string Get()
        {
            var dsSanPham = _spActions.GetAll(); // Lấy tất cả Nhan viên
                                                  //  return new string[] { "value1", "value2" };
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson = JsonSerializer.Serialize<IList<SanPham>>(dsSanPham, opt);
            return strJson;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            var sp = _spActions.GetByID(id); // Lấy tất cả Nhân viên
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson = JsonSerializer.Serialize<SanPham>(sp, opt);
            return strJson;
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