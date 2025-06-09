using System.Data.SqlTypes;

namespace FinalProject.Repository.Interfaces.Payment
{
    public class PaymentFilter
    {
        public SqlInt32? UserId { get; set; }
    }
}
