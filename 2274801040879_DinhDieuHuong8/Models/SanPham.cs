using System.ComponentModel.DataAnnotations;

namespace _2274801040879_DinhDieuHuong8.Models
{
    public class SanPham
    {
        int tt;
        string masp;
        string tensp;
        string gia;
        string soluong;
        string daban;


        [Key]
        public int Tt { get => tt; set => tt = value; }
        [Required]
        public string Masp { get => masp; set => masp = value; }
        [Required]
        public string Tensp { get => tensp; set => tensp = value; }
        public string Gia { get => gia; set => gia = value; }
        [Required]
        public string Soluong { get => soluong; set => soluong = value; }
        public string Daban { get => daban; set => daban = value; }
    }
}
