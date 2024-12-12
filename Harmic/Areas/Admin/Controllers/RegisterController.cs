using Harmic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Harmic.Models;
using Harmic.Utilities;
namespace Harmic.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RegisterController : Controller
    {
        private readonly HarmicContext _context;
        Function function = new Function();
        public RegisterController(HarmicContext context)
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
            if (account == null)
            {
                return BadRequest();
            }
            var acc = _context.TbAccounts.Where(m => m.Email == account.Email).FirstOrDefault();
            if (acc != null)
            {
                Function.msg = "Duplicate Email!";
                return RedirectToAction("Index", "Register");
            }

            account.Password = function.HashPassword(account.Password);
            _context.TbAccounts.Add(account);
            _context.SaveChanges();
            return RedirectToAction("Index", "Login");
        }
    }
}