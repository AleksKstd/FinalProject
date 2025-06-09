
public class HomeViewModel
{
    public List<BankAccountViewModel> BankAccounts { get; set; }
}
public class BankAccountViewModel
{
    public int BankAccountId { get; set; }
    public string IBAN { get; set; }
    public decimal Balance { get; set; }
}

