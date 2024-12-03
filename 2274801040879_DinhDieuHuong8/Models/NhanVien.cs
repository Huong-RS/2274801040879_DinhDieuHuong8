using System.ComponentModel.DataAnnotations;

namespace _2274801040879_DinhDieuHuong8.Models
{
    public class NhanVien
    {
        // tt	cccd	hodem	ten	nickname	email	dienthoai	chucvu
        int tt;
        string cccd;
        string hodem;
        string ten;
        string nickname;
        string email;
        string dienthoai;
        string chucvu;
       

        [Key]
        public int Tt { get => tt; set => tt = value; }
        [Required]
        public string Cccd { get => cccd; set => cccd = value; }
        [Required]
        public string Hodem { get => hodem; set => hodem = value; }
        [Required]
        public string Ten { get => ten; set => ten = value; }
        public string Nickname { get => nickname; set => nickname = value; }
        public string Email { get => email; set => email = value; }
        public string Dienthoai { get => dienthoai; set => dienthoai = value; }
        public string ChucVu { get => chucvu; set => chucvu = value; }
    }
}
