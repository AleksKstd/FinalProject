using FinalProject.Services.DTOs.Payment;
using FinalProject.Services.Interfaces.Payment;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace FinalProject.Web.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        public async Task<IActionResult> Index()
        {
            
            if (!HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public async Task<IActionResult> CreatePayment(CreatePaymentViewModel model)
        {
            if (!HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            Regex ibanRegex = new Regex(@"^[A-Z]{2}\d{2}[A-Z0-9]{22}$");

            if (string.IsNullOrWhiteSpace(model.RecieverIBAN) || !ibanRegex.IsMatch(model.RecieverIBAN))
            {
                TempData["ErrorMessage"] = "Невалиден IBAN на получател";
                return RedirectToAction("CreatePayment");
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

            return View();
        }
    }
}
