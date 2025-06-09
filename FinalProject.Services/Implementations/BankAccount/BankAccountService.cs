using FinalProject.Repository.Interfaces.BankAccount;
using FinalProject.Repository.Interfaces.UserToAccount;
using FinalProject.Services.DTOs.BankAccount;
using FinalProject.Services.Interfaces.BankAccount;

namespace FinalProject.Services.Implementations.BankAccount
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IUserToAccountRepository _userToAccountRepository;
        public BankAccountService(IBankAccountRepository bankAccountRepository, IUserToAccountRepository userToAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
            _userToAccountRepository = userToAccountRepository;
        }

        public async Task<GetAllUserBankAccountsResponse> GetAllUserBankAccounts(int userId)
        {
            if (userId <= 0)
            {
                return new GetAllUserBankAccountsResponse
                {
                    Accounts = new List<BankAccountInfo>(),
                    TotalCount = 0
                };
            }

            var userToAccounts = await _userToAccountRepository.RetrieveCollectionAsync(new UserToAccountFilter { UserId = userId }).ToListAsync();
            var accountIds = userToAccounts.Select(b => b.BankAccountId).ToList();

            if (!accountIds.Any())
            {
                return new GetAllUserBankAccountsResponse
                {
                    Accounts = new List<BankAccountInfo>(),
                    TotalCount = 0
                };
            }

            var bankAccounts = new List<BankAccountInfo>();
            foreach (var accountId in accountIds)
            {
                var bankAccount = await _bankAccountRepository.RetrieveAsync(accountId);
                if (bankAccount == null)
                {
                    continue;
                }
                var bankAccountInfo = MapToBankAccountInfo(bankAccount);
                bankAccounts.Add(bankAccountInfo);
            }
            bankAccounts = bankAccounts.OrderBy(b => b.BankAccountId).ToList();

            return new GetAllUserBankAccountsResponse
            {
                Accounts = bankAccounts,
                TotalCount = bankAccounts.Count
            };
        }

        private BankAccountInfo MapToBankAccountInfo(Models.BankAccount bankAccount)
        {
            return new BankAccountInfo
            {
                BankAccountId = bankAccount.BankAccountId,
                IBAN = bankAccount.IBAN,
                Balance = bankAccount.Balance
            };
        }
    }
}
