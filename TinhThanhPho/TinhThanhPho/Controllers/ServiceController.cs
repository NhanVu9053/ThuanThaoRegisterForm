
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

using TinhThanhPho.Models;

namespace TinhThanhPho.Controllers
{
    public class ServiceController : Controller
    {

        public MyDbContext context;
        public ServiceController(MyDbContext _context)
        {
            context = _context;
        }
        public IActionResult GetAllProvinces()
        {

            using (var db = new MyDbContext())
            {
                var data = context.provinces.ToList();
                return Ok(data);
            }
        }
        public IActionResult GetAllDistrictByProvinceId(int? id)
        {
            using (var connection = new SqlConnection("Server=HOANGLINH;Database=ThuanThaoDrugstore;Trusted_Connection=True;"))
            {
                string query = "select * from district where district._province_id= @Id";
                var districts = connection.Query<District>(query, new { Id = id });

                return Ok(districts);
            }
        }
        public IActionResult GetAllWardByDistrictId(int? id)
        {
            using (var connection = new SqlConnection("Server=HOANGLINH;Database=ThuanThaoDrugstore;Trusted_Connection=True;"))
            {
                string query = "select * from ward where ward._district_id = @Id";
                var districts = connection.Query<District>(query, new { Id = id });

                return Ok(districts);
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User taikhoan)
        {
            taikhoan.Diachi += ", " + context.wards.Find(int.Parse(taikhoan.PhuongXa))._name;
            taikhoan.Diachi += ", " + context.districts.Find(int.Parse(taikhoan.QuanHuyen))._name;
            taikhoan.Diachi += ", " + context.provinces.Find(int.Parse(taikhoan.TinhTp))._name;
            context.users.Add(taikhoan);
            context.SaveChanges();
            return RedirectToAction("ShowItems", "Service");
        }
        public IActionResult ShowItems()
        {
            return View();
        }

    }
}
