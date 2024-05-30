using BackEndAPI.Controllers.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BackEndAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhoaController : ControllerBase
    {
        public static List<Khoa> khoas  = new List<Khoa>();
        private readonly IConfiguration _configuration;

        public KhoaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult GetAll()
        {

            string query = "select * from KHOA";
            DataTable table = new DataTable();
            String sqlDataSource = _configuration.GetConnectionString("quanlySV");
            SqlDataReader myreader;
            using (SqlConnection myconn = new SqlConnection(sqlDataSource))
            {
                myconn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myconn))
                {
                    myreader = myCommand.ExecuteReader();
                    table.Load(myreader);
                    myreader.Close();
                    myconn.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpGet("{id}")]
        public JsonResult GetById(Khoa kh) {
            try
            {
                string query = @"insert into KHOA values ('" + kh.MaKhoa + "',N'" + kh.NameKhoa + "'" + ",'" + kh.NamThanhLap + "')";
                DataTable table = new DataTable();
                String sqlDataSource = _configuration.GetConnectionString("quanlySV");
                SqlDataReader myreader;
                using (SqlConnection myconn = new SqlConnection(sqlDataSource))
                {
                    myconn.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myconn))
                    {
                        myreader = myCommand.ExecuteReader();
                        table.Load(myreader);
                        myreader.Close();
                        myconn.Close();
                    }
                }
                return new JsonResult("Thêm mới thành công");
            }
            catch { 
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Create(KhoaN khN) {
            var khoa = new Khoa
            {
                MaKhoa = Guid.NewGuid(),
                NameKhoa = khN.NameKhoa,
                NamThanhLap = khN.NamThanhLap
            };
            khoas.Add(khoa);
            return Ok(new
            {
                Success = "Thành công",
                data = khoa
            });
        }

        [HttpPut("{id}")]
        public IActionResult Edit(string id, Khoa khEdit)
        {
            try
            {
                var khoa = khoas.SingleOrDefault(khN => khN.MaKhoa == Guid.Parse(id));
                if (khoa == null)
                {
                    return NotFound();
                }
                if (id != khoa.MaKhoa.ToString())
                { 
                    return BadRequest();
                }

                khoa.NameKhoa = khEdit.NameKhoa;
                khoa.NamThanhLap = khEdit.NamThanhLap;

                return Ok(new {
                    Success = "Cập nhật thành công",
                    data = khoa
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id) {
            try
            {
                var khoa = khoas.SingleOrDefault(kh => kh.MaKhoa == Guid.Parse(id));
                if (khoa == null)
                {
                    return NotFound();
                }
                khoas.Remove(khoa);
                return Ok(new { Success = "Xóa thành công" });
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
