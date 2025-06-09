using System.ComponentModel.DataAnnotations;

public class LoginViewModel
{
    [Required(ErrorMessage = "Потребителското име е задължително.")]
    public string Username { get; set; }
    [Required(ErrorMessage = "Паролата е задължителна.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public string ReturnUrl { get; set; }
}

