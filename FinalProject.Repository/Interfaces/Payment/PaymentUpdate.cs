using System.Data.SqlTypes;

namespace FinalProject.Repository.Interfaces.Payment
{
    public class PaymentUpdate
    {
        public SqlString? Status { get; set; }
    }
}
