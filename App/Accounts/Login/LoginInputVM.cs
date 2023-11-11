using System.ComponentModel.DataAnnotations;

namespace Web.Accounts.Login
{
    /// <summary>
    /// Model for Logging View.
    /// </summary>
    public class LoginInputVM
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
