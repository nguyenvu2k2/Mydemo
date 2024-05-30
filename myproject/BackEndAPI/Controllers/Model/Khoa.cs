namespace BackEndAPI.Controllers.Model
{
    public class KhoaN
    {
        public string NameKhoa { get; set; }
        public int NamThanhLap { get; set; }

    }

    public class Khoa: KhoaN
    {
        public Guid MaKhoa { get; set; }
    }
}
