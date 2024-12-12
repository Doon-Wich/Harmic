using Azure.Core;
using Harmic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Harmic.Models;
using Harmic.Utilities;
namespace Harmic.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly HarmicContext _context;
        Function function = new Function();
        public LoginController(HarmicContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(TbAccount account)
        {
            IActionResult response = Unauthorized();
            if (account == null)
            {
                return BadRequest();
            }

            var user = _context.TbAccounts.Where(u => u.Email == account.Email).FirstOrDefault();

            if (user != null && function.VerifyPassword(account.Password, user.Password))
            {
                Function.account = user;
                if(user.RoleId == 1)
                {
					return RedirectToAction("Index", "Home");
				}
                return Redirect("/");

            }
            Function.msg = "Tài Khoản Hoặc Mật Khẩu Không Chính Xác";
            return RedirectToAction("Index", "Login"); ;
        }
    }
}