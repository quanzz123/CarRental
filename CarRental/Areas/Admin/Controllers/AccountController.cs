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
        
        public async Task<IActionResult> Edit()
        {
            var ID = Function._AccountId;
            var accID = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == ID);
            if ( accID == null)
            {
                return NotFound();
            }
            return View(accID);
        }
        // Hành động để xử lý chỉnh sửa thông tin tài khoản
        [Area("Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Account userModel)
        {
            if (id != userModel.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _context.Accounts.FirstOrDefaultAsync(u => u.AccountId == id);
                    if (user == null)
                    {
                        return NotFound();
                    }

                    // Cập nhật thông tin tài khoản
                    user.Username = userModel.Username;
                    user.Email = userModel.Email;
                    // Cập nhật mật khẩu nếu cần (đảm bảo bạn mã hóa mật khẩu trước khi lưu vào DB)
                    //if (!string.IsNullOrEmpty(userModel.))
                    //{
                    //    user.PasswordHash = userModel.PasswordHash; // Bạn cần sử dụng hàm mã hóa như BCrypt hoặc một thư viện bảo mật khác
                    //}
                    user.Password = Function.MD5Password(userModel.Password);

                    _context.Update(user);
                    await _context.SaveChangesAsync();

                    // Sau khi lưu thành công, chuyển hướng về trang chỉnh sửa
                    return RedirectToAction(nameof(Edit));
                    //return RedirectToAction("Index", "Home");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userModel.AccountId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
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
