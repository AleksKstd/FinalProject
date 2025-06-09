using System.Data.SqlTypes;

namespace FinalProject.Repository.Interfaces.UserToAccount
{
    public class UserToAccountFilter
    {
        public SqlInt32? UserId { get; set; }
    }
}
