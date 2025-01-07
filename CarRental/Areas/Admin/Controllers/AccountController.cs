using CarRental.Models;
using CarRental.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly DbRenalCarContext _context;
        public AccountController(DbRenalCarContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            // Kiểm tra trạng thái đăng nhập
            if (!Function.IsLogin())
            {
                return RedirectToAction("Index", "Login");
            }

            var ID = Function._AccountId;
            var accID = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == ID);

            if (accID == null)
            {
                return NotFound();
            }

            return View(accID);
        }

        // Hành động để xử lý chỉnh sửa thông tin tài khoản

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Edit(Account userModel)
        {

            

            if (ModelState.IsValid)
            {
                
                var id = Function._AccountId;
                var user = _context.Accounts.FirstOrDefault(u => u.AccountId == id);
                if (user == null)
                {
                    return NotFound();
                }
                user.Username = userModel.Username;
                //kiểm tra xem có tòn tại Email trong DB hay không

                if(userModel.Email != user.Email)
                {
                    var emailExists = _context.Accounts.Any(p => p.Email == userModel.Email);
                    if(emailExists)
                    {
                        Function._MessageEmail = "Email đã tồn tại";
                        return View(user);
                    } else
                    {

                        user.Email = userModel.Email;
                        
                    }
                    


                }
                user.Phone = userModel.Phone;
                Function._MessageEmail = string.Empty;
                user.Password = Function.MD5Password(userModel.Password);
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                // Sau khi lưu thành công, chuyển hướng về trang chỉnh sửa
                return RedirectToAction("Edit", "Account");
                    //return RedirectToAction("Index", "Home");
                
               
            }
            return View(userModel);
        }

        // Kiểm tra xem người dùng có tồn tại hay không
        private bool UserExists(int id)
        {
            return _context.Accounts.Any(e => e.AccountId == id);
        }
    }
}
