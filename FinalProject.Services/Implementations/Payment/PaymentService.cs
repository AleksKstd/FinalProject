using FinalProject.Models;
using FinalProject.Repository.Helpers;
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
            _bankAccountRepository = bankAccountRepository;
        }
        public async Task<CreatePaymentResponse> CreatePayment(CreatePaymentRequest request)
        {
            if (request.UserId <= 0 || request.BankAccountId <= 0 ||
                request.RecieverIBAN.Length != 22 ||
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

        public async Task<GetAllUserPaymentsResponse> GetAllUserPayments(int userId)
        {
            if(userId<=0)
            {
                return new GetAllUserPaymentsResponse
                {
                    Payments = new List<PaymentInfo>()
                };
            }

            var payments = await _paymentRepository.RetrieveCollectionAsync(new PaymentFilter { UserId = userId }).ToListAsync();
            var paymentInfos = payments.Select(payment => MapToPaymentInfo(payment)).ToList();

            return new GetAllUserPaymentsResponse
            {
                Payments = paymentInfos
            };
        }

        public async Task<UpdatePaymentResponse> UpdatePaymentStatus(UpdatePaymentRequest request)
        {
            if (request.PaymentId <= 0 || string.IsNullOrEmpty(request.Status))
            {
                return new UpdatePaymentResponse
                {
                    Success = false,
                    ErrorMessage = "Невалидни данни за oдобрение."
                };
            }

            var payment = await _paymentRepository.RetrieveAsync(request.PaymentId);
            if (payment == null)
            {
                return new UpdatePaymentResponse
                {
                    Success = false,
                    ErrorMessage = "Плащането не е намерено."
                };
            }
            if (payment.Status != "ИЗЧАКВА")
            {
                return new UpdatePaymentResponse
                {
                    Success = false,
                    ErrorMessage = "Плащането вече е обработено."
                };
            }


            var account = await _bankAccountRepository.RetrieveAsync(payment.BankAccountId);
            if (request.Status == "ОДОБРЕНО" && payment.Credit > account.Balance)
            {
                return new UpdatePaymentResponse
                {
                    Success = false,
                    ErrorMessage = "Недостатъчна наличност по сметка за одобрение на плащането."
                };
            }
            if(account.BankAccountId == payment.BankAccountId)
            {
                return new UpdatePaymentResponse
                {
                    Success = false,
                    ErrorMessage = "Не може да се одобри плащане към същата сметка."
                };
            }

            var isUpdated = await _paymentRepository.UpdateAsync(request.PaymentId, new PaymentUpdate { Status = request.Status });
            if (!isUpdated)
            {
                return new UpdatePaymentResponse
                {
                    Success = false,
                    ErrorMessage = "Грешка при обновяване на плащането."
                };
            }
            if (request.Status == "ОДОБРЕНО")
            {
                if (account == null)
                {
                    return new UpdatePaymentResponse
                    {
                        Success = false,
                        ErrorMessage = "Сметката не е намерена."
                    };
                }
                account.Balance -= payment.Credit;
                var isBalanceUpdated = await _bankAccountRepository.UpdateAsync(payment.BankAccountId, new BankAccountUpdate { Balance = account.Balance });
                if (!isBalanceUpdated)
                {
                    return new UpdatePaymentResponse
                    {
                        Success = false,
                        ErrorMessage = "Грешка при обновяване на баланса на сметката."
                    };
                }

                var bankAccounts = await _bankAccountRepository.RetrieveCollectionAsync(new BankAccountFilter()).ToListAsync();

                foreach (var bAccount in bankAccounts)
                {
                    if (bAccount.IBAN == payment.RecieverIBAN)
                    {
                        bAccount.Balance += payment.Credit;
                        var isReceiverBalanceUpdated = await _bankAccountRepository.UpdateAsync(bAccount.BankAccountId, new BankAccountUpdate { Balance = bAccount.Balance });
                    }
                }
                return new UpdatePaymentResponse
                {
                    Success = true,
                    ErrorMessage = "Плащането е одобрено успешно."
                };
            }
            return new UpdatePaymentResponse
            {
                Success = true,
                ErrorMessage = "Плащането е отказано успешно."
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
