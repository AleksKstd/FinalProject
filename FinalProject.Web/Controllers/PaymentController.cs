using FinalProject.Services.DTOs.Payment;
using FinalProject.Services.Interfaces.BankAccount;
using FinalProject.Services.Interfaces.Payment;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace FinalProject.Web.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IBankAccountService _bankAccountService;

        public PaymentController(IPaymentService paymentService, IBankAccountService bankAccountService)
        {
            _paymentService = paymentService;
            _bankAccountService = bankAccountService;
        }
        public async Task<IActionResult> Index()
        {
            
            if (!HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CreatePayment()
        {
            if (!HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var accounts = await _bankAccountService.GetAllUserBankAccounts(HttpContext.Session.GetInt32("UserId").Value);
            var viewAccounts = accounts.Accounts.Select(a => new BankAccountViewModel
            {
                BankAccountId = a.BankAccountId,
                IBAN = a.IBAN,
                Balance = a.Balance
            }).ToList();
            ViewBag.BankAccounts = viewAccounts;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePayment(CreatePaymentViewModel model)
        {
            if (!HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            Regex ibanRegex = new Regex(@"^[A-Z]{2}\d{2}[A-Z0-9]{18}$");

            if (string.IsNullOrWhiteSpace(model.RecieverIBAN) || !ibanRegex.IsMatch(model.RecieverIBAN))
            {
                TempData["ErrorMessage"] = "Невалиден IBAN на получател";
                var accounts = await _bankAccountService.GetAllUserBankAccounts(HttpContext.Session.GetInt32("UserId").Value);
                var viewAccounts = accounts.Accounts.Select(a => new BankAccountViewModel
                {
                    BankAccountId = a.BankAccountId,
                    IBAN = a.IBAN,
                    Balance = a.Balance
                }).ToList();
                ViewBag.BankAccounts = viewAccounts;
                return View(model);
            }

            var accountsList = await _bankAccountService.GetAllUserBankAccounts(HttpContext.Session.GetInt32("UserId").Value);
            var accountsViewList = accountsList.Accounts.Select(a => new BankAccountViewModel
            {
                BankAccountId = a.BankAccountId,
                IBAN = a.IBAN,
                Balance = a.Balance
            }).ToList();
            ViewBag.BankAccounts = accountsViewList;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var request = new CreatePaymentRequest
            {
                UserId = HttpContext.Session.GetInt32("UserId").Value,
                BankAccountId = model.BankAccountId,
                RecieverIBAN = model.RecieverIBAN,
                Credit = model.Credit,
                Purpose = model.Purpose
            };

            var response = await _paymentService.CreatePayment(request);

            if (response.Success)
            {
                TempData["SuccessMessage"] = response.ErrorMessage;
            }
            else
            {
                TempData["ErrorMessage"] = response.ErrorMessage;
            }

            return View();
        }
    }
}
