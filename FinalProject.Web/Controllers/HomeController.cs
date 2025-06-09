using System.Diagnostics;
using FinalProject.Services.Interfaces.BankAccount;
using FinalProject.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBankAccountService _bankAccountService;
        public HomeController(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }

        public async Task<IActionResult> Index()
        {
            if (!HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var bankAccounts = await _bankAccountService.GetAllUserBankAccounts(HttpContext.Session.GetInt32("UserId").Value);

            var usersAccounts = new List<BankAccountViewModel>();

            foreach (var bankAccount in bankAccounts.Accounts)
            {
                usersAccounts.Add(new BankAccountViewModel
                {
                    BankAccountId = bankAccount.BankAccountId,
                    IBAN = bankAccount.IBAN,
                    Balance = bankAccount.Balance
                });
            }

            var viewModel = new HomeViewModel
            {
                BankAccounts = usersAccounts
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
