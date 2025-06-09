using System.Data.SqlTypes;

namespace FinalProject.Repository.Interfaces.BankAccount
{
    public class BankAccountFilter
    {
        public SqlInt32? BankAccountId { get; set; }
        public SqlString? IBAN { get; set; }
    }
}
