using System.Data.SqlTypes;

namespace FinalProject.Repository.Interfaces.BankAccount
{
    public class BankAccountUpdate
    {
        public SqlDecimal? Balance { get; set; }
    }
}
