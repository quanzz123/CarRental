﻿using CarRental.Models;
using CarRental.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class LoginController : Controller
    {
        private readonly DbRenalCarContext _context;
        public LoginController (DbRenalCarContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Customer c)
        {
            if (c == null)
            {

                return NotFound();
            }

            //mã hoá mật khẩu trc khi kiểm tra
            string p = Function.MD5Password(c.Password);
            //kiểm tra tài khoản có tồn tại trong cớ sở dữ liêu
            var check = _context.Customers.Where(m => (m.Email == c.Email) && (m.Password == p)).FirstOrDefault();
            if (check == null)
            {
                //hiên thị thông báo
                Function._Message = "Sai thông tin đăng nhập";
                return RedirectToAction("Index", "Login");
            }

            //đăng nhập thành công
            Function._Message  = string.Empty;
            Function._AccountId = check.CustomerId;
            Function._UserName = string.IsNullOrEmpty(check.Name) ? string.Empty : check.Name;
            Function._Email = string.IsNullOrEmpty(check.Email) ? string.Empty : check.Email;
            return RedirectToAction("Index", "Home");
        }
    }
}
