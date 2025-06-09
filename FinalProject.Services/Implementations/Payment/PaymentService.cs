using FinalProject.Models;
using FinalProject.Repository.Interfaces.BankAccount;
using FinalProject.Repository.Interfaces.Payment;
using FinalProject.Repository.Interfaces.UserToAccount;
using FinalProject.Services.DTOs.Payment;
using FinalProject.Services.Interfaces.Payment;

namespace FinalProject.Services.Implementations.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUserToAccountRepository _userToAccountRepository;
        private readonly IBankAccountRepository _bankAccountRepository;
        public PaymentService(IPaymentRepository paymentRepository, IUserToAccountRepository userToAccountRepository, IBankAccountRepository bankAccountRepository)
        {
            _paymentRepository = paymentRepository;
            _userToAccountRepository = userToAccountRepository;
        }
        public async Task<CreatePaymentResponse> CreatePayment(CreatePaymentRequest request)
        {
            if (request.UserId <= 0 || request.BankAccountId <= 0 ||
                request.RecieverIBAN.Length < 22 || request.RecieverIBAN.Length > 22 ||
                request.Credit <= 0 ||
                request.Purpose.Length > 32 || request.Purpose.Length <= 0)
            {
                return new CreatePaymentResponse
                {
                    Success = false,
                    ErrorMessage = "Невалидни данни за плащане."
                };
            }

            var userToAccount = await _userToAccountRepository.RetrieveCollectionAsync(new UserToAccountFilter { BankAccountId = request.BankAccountId, UserId = request.UserId }).ToListAsync();
            if (userToAccount.Count == 0)
            {
                return new CreatePaymentResponse
                {
                    Success = false,
                    ErrorMessage = "Потребителят няма достъп до тази сметка."
                };
            }

            var bankAccount = await _bankAccountRepository.RetrieveAsync(request.BankAccountId);

            if (request.Credit > bankAccount.Balance)
            {
                return new CreatePaymentResponse
                {
                    Success = false,
                    ErrorMessage = "Недостатъчна наличност по сметка."
                };
            }

            Models.Payment payment = new Models.Payment
            {
                UserId = request.UserId,
                BankAccountId = request.BankAccountId,
                RecieverIBAN = request.RecieverIBAN,
                Credit = request.Credit,
                Purpose = request.Purpose,
                PaymentDate = DateTime.Now,
                Status = "ИЗЧАКВА"
            };

            int paymentId = await _paymentRepository.CreateAsync(payment);
            if (paymentId <= 0)
            {
                return new CreatePaymentResponse
                {
                    Success = false,
                    ErrorMessage = "Грешка при създаване на плащането."
                };
            }

            return new CreatePaymentResponse
            {
                Success = true,
                ErrorMessage = "Успешно създадена заявка за плащане."
            };

        }
        private PaymentInfo MapToPaymentInfo(Models.Payment payment)
        {
            return new PaymentInfo
            {
                PaymentId = payment.PaymentId,
                UserId = payment.UserId,
                BankAccountId = payment.BankAccountId,
                RecieverIBAN = payment.RecieverIBAN,
                Credit = payment.Credit,
                Purpose = payment.Purpose,
                PaymentDate = payment.PaymentDate,
                Status = payment.Status
            };
        }
    }
}
